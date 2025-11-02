namespace JX.Database.Model
{
    public class SkillModel
    {
        public virtual ulong Id { get; set; }
        public virtual uint Character_Id { get; set; }

        public virtual ushort Skill_Id { get; set; }
        public virtual byte Skill_Level { get; set; }
        public virtual uint Skill_Exp { get; set; }
    }
}
