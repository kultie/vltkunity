using System;
namespace JX.Database.Model
{
    public class PaymentModel
    {
        public virtual uint CId { get; set; }
        public virtual uint Money { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual ulong Total { get; set; }
    }
}
