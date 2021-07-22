using DreamTeam.Models;
using DreamTeam.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamTeam.ViewModels
{
    public class Invoices_Index_Object
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public int AddressId { get; set; }
    }
}