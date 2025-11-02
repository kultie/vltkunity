using System;
namespace JX.Database.Model
{
    public class GuildModel
    {
        public virtual uint Id { get; set; }
        public virtual string Name { get; set; }

        public virtual uint MasterId { get; set; }
        public virtual uint ViceId { get; set; }

        public virtual ushort Member { get; set; }

        public virtual uint Gold { get; set; }
        public virtual uint Jelly { get; set; }

        public virtual string BuildBy { get; set; }

        public virtual DateTime BuildAt { get; set; }
    }
}
