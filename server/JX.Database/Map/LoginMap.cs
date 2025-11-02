using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class LoginMap : ClassMap<LoginModel>
    {
        public LoginMap()
        {
            Id(x => x.CId).Column("cid");

            Map(x => x.FirstLogin).Column("firstlogin");

            Map(x => x.LastLogin).Column("lastlogin");

            Map(x => x.LoginDays).Column("logindays");

            Table("fw_logins");
        }
    }
}
