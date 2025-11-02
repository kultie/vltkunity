using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class TaskMap : ClassMap<TaskModel>
    {
        public TaskMap()
        {
            Id(x => x.Id).Column("id");

            Map(x => x.Character_Id).Column("character_id");

            Map(x => x.Task_Id).Column("task_Id");

            Map(x => x.Task_Value).Column("task_Value");

            Table("tasks");
        }
    }
}
