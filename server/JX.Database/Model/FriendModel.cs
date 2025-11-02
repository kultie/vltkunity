using System;
namespace JX.Database.Model
{
    public class FriendModel
    {
        public virtual ulong Id { get; set; }
        public virtual uint Character_Id { get; set; }
        public virtual uint Friend_Id { get; set; }
    }
}
