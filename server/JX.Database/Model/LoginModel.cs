using System;
namespace JX.Database.Model
{
    public class LoginModel
    {
        public virtual uint CId { get; set; }
        public virtual byte FirstLogin { get; set; }
        public virtual DateTime LastLogin { get; set; }
        public virtual byte LoginDays { get; set; }
    }
}
