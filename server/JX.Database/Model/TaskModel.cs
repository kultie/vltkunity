namespace JX.Database.Model
{
    public class TaskModel
    {
        public virtual ulong Id { get; set; }
        public virtual uint Character_Id { get; set; }

        public virtual int Task_Id { get; set; }
        public virtual int Task_Value { get; set; }
    }
}
