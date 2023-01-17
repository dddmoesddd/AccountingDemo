using MassTransit.Testing;
using System.Collections.Generic;

namespace AccountingVoucher.Application.Features.Queries
{
    public class VoucherVM
    {
        public long VoucherNumber { get; set; }
        public bool IsBalance { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedData { get; set; }
        public string LastModifiedBy { get; set; }
        public decimal WholePrice { get; set; }
        public List<VoucherItemsVM> VoucherItems { get; set; }
    }
}
