using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using DBModels;
using ERPService;

namespace Repositories
{
    public class ExchangeRepository
    {
        public Operation<List<ExchangeByDate>> GetExchangesFromBankByDate(DateTime Date_)
        {
            Operation<List<ExchangeByDate>> operation = new Operation<List<ExchangeByDate>>();
            try
            {
                List<ExchangeByDate> list = new List<ExchangeByDate>();

                string format = "dd.MM.yyyy";
                string date_str = Date_.Date.ToString(format);

                XDocument xDoc = XDocument.Load(string.Format("http://cbar.az/currencies/{0}.xml", date_str));

                IOperation<List<ExchangeMaster>> op_Exchanges = GetAllExchanges();
                if (!op_Exchanges.Successful) return null;
                IEnumerable<XElement> elements = xDoc.Descendants("Valute");

                foreach (ExchangeMaster exchangeMaster in op_Exchanges.Value)
                {
                    var xelement = (elements.Where(x => x.Attribute("Code").Value.ToString() == exchangeMaster.Name_)).FirstOrDefault();
                    if (xelement == null) continue;
                    ExchangeByDate exchangeByDate = new ExchangeByDate();
                    exchangeByDate.Date_ = Date_.Date;
                    exchangeByDate.ExchangeId = exchangeMaster.Id;
                    exchangeByDate.ExchangeRate = Convert.ToDecimal(xelement.Element("Value").Value.ToString());
                    list.Add(exchangeByDate);
                }
                operation.Value = list;
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<ExchangeMaster>> GetAllExchanges()
        {
            Operation<List<ExchangeMaster>> r = new Operation<List<ExchangeMaster>>();
            try
            {
                r.Value = DataIO.GetAllOff<ExchangeMaster>();
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