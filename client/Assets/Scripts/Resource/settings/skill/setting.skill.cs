
namespace game.resource.settings.skill
{
    public class SkillSetting : skill.SkillSettingGetter
    {
        public static skill.SkillSetting Get(int skillId, int skillLevel)
        {
            int cacheKey = (skillId * 100) + skillLevel;

            if (Cache.Settings.Skill.skillsIdToDataMapping.ContainsKey(cacheKey) == false)
            {
                skill.SkillSetting newSkillSetting = new skill.SkillSetting();
                newSkillSetting.LoadBase(skillId);
                newSkillSetting.LoadLevel(skillId, skillLevel);

                Cache.Settings.Skill.skillsIdToDataMapping[cacheKey] = newSkillSetting;
            }

            return Cache.Settings.Skill.skillsIdToDataMapping[cacheKey];
        }
    }
}
