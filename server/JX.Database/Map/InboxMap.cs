using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class InboxMap : ClassMap<InboxModel>
    {
        public InboxMap()
        {
            Id(x => x.Id).Column("id");

            Map(x => x.CId).Column("cid");
            Map(x => x.SId).Column("sid");

            Map(x => x.Title).Column("title");
            Map(x => x.Content).Column("content");

            Map(x => x.Items).Column("items");

            Map(x => x.Add_Date).Column("add_date");

            Table("fw_inboxs");
        }
    }
}
