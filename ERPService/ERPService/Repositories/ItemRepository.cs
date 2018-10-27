using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;
using DBModels;
using ERPService;
using BusinessModels;

namespace Repositories
{
    public class ItemRepository
    {
        public IDbConnection connection { get {return DataIO.connection; } }
        public Operation<Item> GetItem(int Id)
        {
            Operation<Item> op_item = new Operation<Item>();
            Item item = null;
            try
            {
                item = new Item();
                item = connection.Get<Item>(Id);
                op_item.Value = item;
                op_item.Successful = (item != null);
            }
            catch (Exception ex)
            {
                op_item.Fail = ex.Message;
            }

            return op_item;
        }
        public Operation<Item> PostItem(Item item)
        {
            if (item == null)
            {
                return new Operation<Item>() { Fail = "item is null !" };
            }
            
            Operation<Item> op_item = new Operation<Item>();

            if (item.Id == 0)
            {
                op_item = InsertItem(item);
            }
            else
            {
                op_item = UpdateItem(item);
            }

            return op_item;
        }
        public Operation<List<ItemView>> GetAllItems()
        {
            Operation<List<ItemView>> r = new Operation<List<ItemView>>();
            try
            {
                r.Value = DataIO.GetAllOff<ItemView>();
                r.Successful = true;

            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }
        public Operation<bool> PostItemPrices(List<ItemPriceOperation> ItemPrices)
        {

            Operation<bool> op_ = new Operation<bool>();

            IDbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                ExchangeMaster exch =DataIO.GetAllOff<ExchangeMaster>().Where(x => x.IsMainExchange).First();

                foreach (ItemPriceOperation itemPriceOp in ItemPrices)
                {
                    ItemPrice itemPrice = (itemPriceOp as ItemPrice);
                    itemPrice.ExchangeId = exch.Id;
                    ItemPrice existingRow = connection.Query<ItemPrice>("SELECT * FROM ItemPrice WHERE PriceTypeId = @PriceTypeId AND ItemId = @ItemId AND CardId = @CardId", new { PriceTypeId = itemPrice.PriceTypeId, ItemId = itemPrice.ItemId, CardId = itemPrice.CardId }, transaction).FirstOrDefault();
                    if (itemPriceOp.OperationType == 1)
                    {
                        //var existingRow = GetAllOff<ItemPrice>(" PriceTypeId = " +
                        //    itemPrice.PriceTypeId.ToString() + " AND ItemId = " +
                        //    itemPrice.ItemId.ToString() + " AND CardId = " + itemPrice.CardId.ToString(),transaction).FirstOrDefault();
                        if (existingRow == null)
                        {
                            connection.Insert(itemPrice, transaction);
                        }
                        else
                        {
                            itemPrice.Id = existingRow.Id;
                            connection.Update(itemPrice, transaction);
                        }
                    }
                    if (itemPriceOp.OperationType == 0)
                    {
                        if (existingRow != null) connection.Delete(existingRow, transaction);
                    }
                }
                transaction.Commit();
                connection.Close();
                op_.Value = true;
                op_.Successful = true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                { }
                op_.Fail = ex.Message;
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
            return op_;
        }
        Operation<Item> InsertItem(Item item)
        {
            Operation<Item> op_item = new Operation<Item>();
            if (item.ItemTypeId == 0)
            {
                op_item.Fail = " Məhsulun Növü Daxil Edilməyib ";
                return op_item;
            }
            if (item.ItemCode.Length == 0)
            {
                op_item.Fail = " Məhsulun Kodu Daxil Edilməyib ";
                return op_item;
            }

            if (item.ItemName.Length == 0)
            {
                op_item.Fail = " Məhsulun Adı Daxil Edilməyib ";
                return op_item;
            }

            try
            {
                connection.Insert(item);
                op_item.Value = item;
                op_item.Successful = true;
            }
            catch (Exception ex)
            {
                op_item.Fail = ex.Message;
            }
            finally
            {

               
            }
            return op_item;
        }
        Operation<Item> UpdateItem(Item item)
        {
            Operation<Item> op_item = new Operation<Item>();
            if (item.ItemTypeId == 0)
            {
                op_item.Fail = " Məhsulun Növü Daxil Edilməyib ";
                return op_item;
            }
            if (item.ItemCode.Length == 0)
            {
                op_item.Fail = " Məhsulun Kodu Daxil Edilməyib ";
                return op_item;
            }

            if (item.ItemName.Length == 0)
            {
                op_item.Fail = " Məhsulun Adı Daxil Edilməyib ";
                return op_item;
            }

            try
            {
                connection.Update(item);
                op_item.Value = item;
                op_item.Successful = true;
            }
            catch (Exception ex)
            {
                op_item.Fail = ex.Message;
            }
            finally
            {


            }
            return op_item;
        }
        public Operation<List<PriceCalcType>> GetPriceCalcTypes()
        {
            Operation<List<PriceCalcType>> operation = new Operation<List<PriceCalcType>>();
            
            try
            {
                operation.Value = DataIO.GetAllOff<PriceCalcType>();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }

            return operation;
        }

        public Operation<List<ItemPriceForCard>> GetItemPriceForCards(string ItemId)
        {
            string sqlQuery = " SELECT * FROM [FN_ItemPriceForCards] (" + ItemId + ") ";
            return DataIO.Query<ItemPriceForCard>(sqlQuery);
        }

        public Operation<List<ItemPrice>> GetItemPricesList()
        {
            Operation<List<ItemPrice>> r = new Operation<List<ItemPrice>>();
            try
            {
                r.Value = DataIO.GetAllOff<ItemPrice>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }

        public Operation<List<VW_ItemPricesDefault>> GetItemDefaultPricesView()
        {
            Operation<List<VW_ItemPricesDefault>> r = new Operation<List<VW_ItemPricesDefault>>();
            try
            {
                r.Value = DataIO.GetAllOff<VW_ItemPricesDefault>();
                r.Successful = true;
            }
            catch (Exception ex)
            {
                r.Fail = ex.Message;
            }
            return r;
        }
    }
}