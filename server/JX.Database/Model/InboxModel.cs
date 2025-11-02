using System;
namespace JX.Database.Model
{
    public class InboxModel
    {
        public virtual ulong Id { get; set; }

        public virtual uint CId { get; set; }
        public virtual uint SId { get; set; }

        public virtual string Title { get; set; }
        public virtual string Content { get; set; }

        public virtual string Items { get; set; }

        public virtual DateTime Add_Date { get; set; }
    }
}
