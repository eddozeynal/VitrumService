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
                /*
                IDbTransaction transaction = null;
                try
                {
                    Item old_item = GetItem(item.BaseItem_.Id).Value;

                    connection.Open();
                    transaction = connection.BeginTransaction();
                    connection.Update(item.BaseItem_, transaction);
                    foreach (ItemUnit unit in item.ItemUnits)
                    {
                        if (unit.Id == 0)
                        {
                            unit.ItemId = item.BaseItem_.Id;
                            connection.Insert(unit, transaction);
                        }
                        else
                        {
                            connection.Update(unit, transaction);
                        }
                    }

                    foreach (ItemUnit unit_old in old_item.ItemUnits)
                    {
                        if (!item.ItemUnits.Select(x => x.Id).Contains(unit_old.Id))
                        {
                            Operation<bool> op_unit_used = IsItemUnitUsed(unit_old.Id);
                            //if(!op_unit_used.Successful)
                            if (!op_unit_used.Value)
                            {
                                connection.Delete(unit_old, transaction);
                            }
                            else
                            {
                                // item unit used. cannot delete
                                try
                                {
                                    transaction.Rollback();
                                    connection.Close();
                                }
                                catch
                                { }
                                return new Operation<Item>() { Value = item, Fail = unit_old.Unit + " istifadə olunubdur. Silə bilməzsiniz!" };
                            }
                        }
                    }
                    transaction.Commit();
                    connection.Close();
                    op_fiche.Value = item;
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

                */
            }

            return op_fiche;
        }

        public Operation<List<VW_FicheMaster>> GetFiches(byte DocType, DateTime dateBegin, DateTime dateEnd)
        {
            Operation<List<VW_FicheMaster>> op_fichemaster = new Operation<List<VW_FicheMaster>>();
            try
            {
                op_fichemaster.Value = connection.Query<VW_FicheMaster>("SELECT * FROM VW_FicheMaster WHERE DocTypeId = @DocTypeId and CreatedDate between @dateBegin and @dateEnd ", new { DocTypeId = DocType, dateBegin = dateBegin, dateEnd = dateEnd }).ToList();
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
    }
}