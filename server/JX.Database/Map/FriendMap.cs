using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    class FriendMap : ClassMap<FriendModel>
    {
        public FriendMap()
        {
            Id(x => x.Id).Column("id");

            Map(x => x.Character_Id).Column("cid");

            Map(x => x.Friend_Id).Column("fid");

            Table("friends");
        }
    }
}

