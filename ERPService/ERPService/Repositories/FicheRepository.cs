using BusinessModels;
using Dapper;
using Dapper.Contrib.Extensions;
using DBModels;
using ERPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Repositories
{
    public class FicheRepository
    {
        public IDbConnection connection { get { return DataIO.connection; } }
        public Operation<Fiche> PostFiche(Fiche fiche)
        {

            if (fiche == null)
            {
                return new Operation<Fiche>() { Fail = "Fiche is null !" };
            }
            if (fiche.FicheMaster == null)
            {
                return new Operation<Fiche>() { Fail = "Fiche Master is null !" };
            }
            if (fiche.FicheLines == null)
            {
                return new Operation<Fiche>() { Fail = "Fiche Lines are null !" };
            }
            if (fiche.FicheLines.Count == 0)
            {
                return new Operation<Fiche>() { Fail = "Fiche has no lines !" };
            }

            Operation<Fiche> op_fiche = new Operation<Fiche>();

            if (fiche.FicheMaster.Id == 0)
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    // string _ficheno = connection.ExecuteScalar<string>("SP_GetNewDocumentNumber",new { DocumentTypeNumber = 1 },transaction);
                    string _ficheno = connection.ExecuteScalar<string>("EXECUTE SP_GetNewDocumentNumber " + fiche.FicheMaster.DocTypeId.ToString(), null, transaction);
                    fiche.FicheMaster.Ficheno = _ficheno;
                    connection.Insert(fiche.FicheMaster, transaction);
                    int LineNumber = 0;
                    foreach (FicheLine line in fiche.FicheLines)
                    {
                        LineNumber++;
                        line.FicheId = fiche.FicheMaster.Id;
                        line.LineNumber = LineNumber;
                        if (line.Note == null) line.Note = "";
                    }
                    connection.Insert(fiche.FicheLines, transaction);
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
                    Fiche oldFiche = GetFicheById(fiche.FicheMaster.Id).Value;

                    connection.Open();
                    transaction = connection.BeginTransaction();
                    connection.Update(fiche.FicheMaster, transaction);

                    CollectionChangesComparer<FicheLine> ccf = new CollectionChangesComparer<FicheLine>();
                    ccf.KeyFieldName = "Id";
                    ccf.SetInitialList(oldFiche.FicheLines);
                    ccf.SetFinalList(fiche.FicheLines);

                    var deletes = ccf.GetDeletes();
                    connection.Delete(deletes, transaction);
                    var inserts = ccf.GetInserts();
                    inserts.ForEach(x => x.FicheId = fiche.FicheMaster.Id);
                    connection.Insert(inserts, transaction);
                    var updates = ccf.GetUpdates();
                    connection.Update(updates, transaction);
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

        public Operation<Fiche> GetFicheById(int Id)
        {
            Operation<Fiche> op_fiche = new Operation<Fiche>();
            try
            {
                Fiche fiche = new Fiche();
                FicheMaster ficheMaster = new FicheMaster();
                List<FicheLine> ficheLines = new List<FicheLine>();
                ficheMaster = connection.Get<FicheMaster>(Id);
                ficheLines = connection.Query<FicheLine>("SELECT * FROM FicheLine WHERE FicheId = "+Id.ToString()).ToList();
                fiche.FicheMaster = ficheMaster;
                fiche.FicheLines = ficheLines;
                op_fiche.Value = fiche;
                op_fiche.Successful = true;
            }
            catch (Exception ex)
            {
                op_fiche.Fail = ex.Message;
            }
            return op_fiche;
        }

        public Operation<FicheView> GetFicheViewById(int Id)
        {
            Operation<FicheView> op_fiche = new Operation<FicheView>();
            try
            {
                FicheView fiche = new FicheView();
                FicheMasterView ficheMaster = new FicheMasterView();
                List<FicheLineView> ficheLines = new List<FicheLineView>();
                ficheMaster = connection.Get<FicheMasterView>(Id);
                ficheLines = connection.Query<FicheLineView>("SELECT * FROM FicheLineView WHERE FicheId = " + Id.ToString()).ToList();
                fiche.FicheMaster = ficheMaster;
                fiche.FicheLines = ficheLines;
                op_fiche.Value = fiche;
                op_fiche.Successful = true;
            }
            catch (Exception ex)
            {
                op_fiche.Fail = ex.Message;
            }
            return op_fiche;
        }

        public Operation<Fiche> ChangeFicheStatus(int Id, byte StatusId)
        {
            Operation<Fiche> op_fiche = new Operation<Fiche>();
            try
            {
                Fiche fiche = GetFicheById(Id).Value;
                fiche.FicheMaster.Status_ = StatusId;
                connection.Update(fiche.FicheMaster);
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