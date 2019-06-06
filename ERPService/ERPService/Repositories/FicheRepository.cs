using Dapper;
using Dapper.Contrib.Extensions;
using ERPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ERPService
{
    public class FicheRepository
    {
        public IDbConnection connection { get { return DataIO.connection; } }
        public Operation<FicheMasterView> PostFiche(FicheMasterView fiche)
        {

            if (fiche == null)
            {
                return new Operation<FicheMasterView>() { Fail = "Fiche is null !" };
            }
            if (fiche.FicheLines == null)
            {
                return new Operation<FicheMasterView>() { Fail = "Fiche Lines are null !" };
            }
            if (fiche.FicheLines.Count == 0)
            {
                return new Operation<FicheMasterView>() { Fail = "Fiche has no lines !" };
            }

            Operation<FicheMasterView> op_fiche = new Operation<FicheMasterView>();

            if (fiche.Id == 0)
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    // string _ficheno = connection.ExecuteScalar<string>("SP_GetNewDocumentNumber",new { DocumentTypeNumber = 1 },transaction);
                    string _ficheno = connection.ExecuteScalar<string>("EXECUTE SP_GetNewDocumentNumber " + fiche.DocTypeId.ToString(), null, transaction);
                    fiche.Ficheno = _ficheno;
                    FicheMaster dbFicheModel = fiche;
                    connection.Insert(dbFicheModel, transaction);
                    int LineNumber = 0;
                    foreach (FicheLine line in fiche.FicheLines)
                    {
                        LineNumber++;
                        line.FicheId = fiche.Id;
                        line.LineNumber = LineNumber;
                        if (line.Note == null) line.Note = "";
                    }
                    //List<FicheLine> dbLinesModel = fiche.FicheLines.Cast<FicheLine>().ToList();
                    //connection.Insert(dbLinesModel, transaction);

                    foreach (var line in fiche.FicheLines)
                    {
                        FicheLine flmodel = line;
                        connection.Insert(flmodel, transaction);
                        foreach (var serviceLine in line.Services)
                        {
                            serviceLine.FicheLineId = line.Id;
                        }
                        line.Services.ForEach(x=>x.Note  = x.Note ?? string.Empty);
                        connection.Insert(line.Services.Cast<FicheLineService>(), transaction);
                    }
                    
                    transaction.Commit();
                    connection.Close();
                    op_fiche.Value = fiche;
                    op_fiche.Successful = true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { }
                    op_fiche.Fail = ex.Message;
                }
                finally
                {

                    try
                    {
                        connection.Close();
                    }
                    catch
                    { }
                }
            }
            else
            {

                IDbTransaction transaction = null;
                try
                {
                    FicheMasterView oldFiche = GetFicheById(fiche.Id).Value;
                   
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    connection.Update((fiche as FicheMaster), transaction);

                    CollectionChangesComparer<FicheLineView> ccf = new CollectionChangesComparer<FicheLineView>();
                    ccf.KeyFieldName = "Id";
                    ccf.SetInitialList(oldFiche.FicheLines);
                    ccf.SetFinalList(fiche.FicheLines);

                    var deletes = ccf.GetDeletes();
                    deletes.ForEach(x=> connection.Delete(x.Services.Cast<FicheLineService>(), transaction));
                    connection.Delete(deletes.Cast<FicheLine>(), transaction);
                    var inserts = ccf.GetInserts();
                    inserts.ForEach(x => x.FicheId = fiche.Id);
                    foreach (var line in inserts)
                    {
                        FicheLine flmodel = line;
                        connection.Insert(flmodel, transaction);
                        inserts.ForEach(x => x.Services = x.Services ?? new List<FicheLineServiceView>());
                        foreach (var serviceLine in line.Services)
                        {
                            serviceLine.FicheLineId = line.Id;
                        }
                        line.Services.ForEach(x => x.Note = x.Note ?? string.Empty);

                        connection.Insert(line.Services.Cast<FicheLineService>(), transaction);
                    }
                    // connection.Insert(inserts.Cast<FicheLine>(), transaction);
                   
                    //inserts.ForEach(x=>x.Services.ForEach(t=>t.FicheLineId= x.Id));
                    var updates = ccf.GetUpdates();
                    connection.Update(updates.Cast<FicheLine>(), transaction);
                    foreach (var upItem in updates)
                    {

                        //var servicesOfLine = connection.Query<FicheLineService>();
                    }
                    transaction.Commit();
                    connection.Close();
                    op_fiche.Value = fiche;
                    op_fiche.Successful = true;
                }
                catch (Exception ex)
                {
                    op_fiche.Fail = ex.Message;
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { }
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch
                    { }
                }


            }

            return op_fiche;
        }

        public Operation<List<FicheMasterView>> GetFiches(byte DocType, DateTime dateBegin, DateTime dateEnd)
        {
            Operation<List<FicheMasterView>> op_fichemaster = new Operation<List<FicheMasterView>>();
            try
            {
                op_fichemaster.Value = connection.Query<FicheMasterView>("SELECT * FROM FicheMasterView WHERE DocTypeId = @DocTypeId and CreatedDate between @dateBegin and @dateEnd ", new { DocTypeId = DocType, dateBegin = dateBegin, dateEnd = dateEnd }).ToList();
            }
            catch (Exception ex)
            {
                op_fichemaster.Fail = ex.Message;
            }
            return op_fichemaster;
        }

        public Operation<FicheMasterView> GetFicheById(int Id)
        {
            Operation<FicheMasterView> op_fiche = new Operation<FicheMasterView>();
            try
            {
                FicheMasterView fiche = connection.Get<FicheMasterView>(Id);
                fiche.FicheLines = connection.Query<FicheLineView>("SELECT * FROM FicheLineView WHERE FicheId = " + Id.ToString()).ToList();
                foreach (var line in fiche.FicheLines)
                {
                    line.Services = connection.Query<FicheLineServiceView>("SELECT * FROM FicheLineServiceView WHERE FicheLineId = " + line.Id.ToString()).ToList();
                }
                op_fiche.Value = fiche;
                op_fiche.Successful = true;
            }
            catch (Exception ex)
            {
                op_fiche.Fail = ex.Message;
            }
            return op_fiche;
        }

        public Operation<FicheMasterView> ChangeFicheStatus(int Id, byte StatusId)
        {
            Operation<FicheMasterView> op_fiche = new Operation<FicheMasterView>();
            try
            {
                FicheMasterView fiche = GetFicheById(Id).Value;
                fiche.Status_ = StatusId;
                connection.Update(fiche as FicheMaster);
                op_fiche.Value = fiche;
                op_fiche.Successful = true;
            }
            catch (Exception ex)
            {
                op_fiche.Fail = ex.Message;
            }
            return op_fiche;
        }

    }
}