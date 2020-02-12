using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPService.Models
{
    public class CardFicheLineView
    {
        public int Id { get; set; }
        public int CashTypeId { get; set; }
        public string Ficheno { get; set; }
        public int CardId { get; set; }
        public decimal Total { get; set; }
        public byte CurrencyId { get; set; }
        public int CardFicheId { get; set; }
        public decimal SignType { get; set; }
        public decimal CurrencyRate { get; set; }
        public byte StatusId { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CashTypeName { get; set; }
        public string CurrencyName { get; set; }
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public bool CardByPermission { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public decimal? DmTotal { get; set; }

    }
}