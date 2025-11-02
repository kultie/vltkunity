using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class PaymentMap : ClassMap<PaymentModel>
    {
        public PaymentMap()
        {
            Id(x => x.CId).Column("cid");

            Map(x => x.Money).Column("money");

            Map(x => x.Updated).Column("updated");

            Map(x => x.Total).Column("total");

            Table("fw_payments");
        }
    }
}
