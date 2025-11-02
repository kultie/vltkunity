using System;
namespace JX.Database.Model
{
	public class AccountModel
	{
		public virtual uint Id { get; set; }

        public virtual bool iClientID { get; set; }
        public virtual byte Type { get; set; }// Web, Root, Admin, User

        public virtual string Account { get; set; }
		public virtual string Password { get; set; }
		public virtual string PasswordSecond { get; set; }

        public virtual string FullName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Birthday { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual string IP { get; set; }
        public virtual string Country { get; set; }

        public virtual bool IsBan { get; set; }
        public virtual bool IsLock { get; set; }
        public virtual DateTime Lock_Date { get; set; }

        public virtual string Platform { get; set; }
        public virtual string System { get; set; }
        public virtual string Model { get; set; }
        public virtual string Uuid { get; set; }
        public virtual DateTime Created_at { get; set; }
        public virtual DateTime LogIn_at { get; set; }
        public virtual DateTime LogOut_at { get; set; }
	}
}
