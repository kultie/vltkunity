using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
	public class AccountMap : ClassMap<AccountModel>
	{
		public AccountMap()
		{
			Id(x => x.Id).Column("id");

            Map(x => x.iClientID).Column("iClientID");
            Map(x => x.Type).Column("isAdmin");

            Map(x => x.Account).Column("username");
			Map(x => x.Password).Column("password");
            Map(x => x.PasswordSecond).Column("passwordSecond");

            Map(x => x.FullName).Column("fullname");
            Map(x => x.Gender).Column("gender");
            Map(x => x.Birthday).Column("birthday");

            Map(x => x.Email).Column("email");
            Map(x => x.Phone).Column("phone");
            Map(x => x.Address).Column("address");
            Map(x => x.IP).Column("ip");
            Map(x => x.Country).Column("country");

            Map(x => x.IsBan).Column("isban");
            Map(x => x.IsLock).Column("isblock");
            Map(x => x.Lock_Date).Column("block_date");

            Map(x => x.Platform).Column("platform");
            Map(x => x.System).Column("system");
            Map(x => x.Model).Column("model");
            Map(x => x.Uuid).Column("uuid");

            Map(x => x.Created_at).Column("dateCreate");
            Map(x => x.LogIn_at).Column("dateLogin");
            Map(x => x.LogOut_at).Column("dateLogout");

			Table("accounts");
		}
	}
}
