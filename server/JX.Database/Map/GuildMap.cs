using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class GuildMap : ClassMap<GuildModel>
    {
        public GuildMap()
        {
            Id(x => x.Id).Column("id");

            Map(x => x.Name).Column("name");

            Map(x => x.MasterId).Column("masterid");

            Map(x => x.ViceId).Column("viceid");

            Map(x => x.Member).Column("member");

            Map(x => x.Gold).Column("gold");

            Map(x => x.Jelly).Column("jelly");

            Id(x => x.BuildBy).Column("build_by");

            Map(x => x.BuildAt).Column("build_at");

            Table("fw_guilds");
        }
    }
}
