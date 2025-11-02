
namespace game.resource.settings.item
{
    public class Setters : item.Datafield
    {
        private void SetDataEquipment(item.Database database)
        {
            this.magicAttrib = new System.Collections.Generic.List<skill.SkillSettingData.KMagicAttrib>();
            this.databaseId = database.databaseId;
            this.type = database.type;
            this.stack = database.stack;
            this.timeUse = database.timeUse;

            switch (this.type)
            {
                case Defination.Type.normalEquip:
                    this.equipmentBase = item.Getters.GetEquipmentBase(database.genre, database.detail, database.particular, database.level);
                    this.series = database.series;
                    break;

                case Defination.Type.goldEquip:
                    this.equipmentBase = item.Getters.GetGoldEquipBase(database.rowIndex);
                    this.series = this.equipmentBase.series;
                    break;
            }

            System.Collections.Generic.List<skill.SkillSettingData.KMagicAttrib> magicList = new System.Collections.Generic.List<skill.SkillSettingData.KMagicAttrib>()
            {
                new skill.SkillSettingData.KMagicAttrib(database.magic0Type, database.magic0Value0, database.magic0Value1, database.magic0Value2),
                new skill.SkillSettingData.KMagicAttrib(database.magic1Type, database.magic1Value0, database.magic1Value1, database.magic1Value2),
                new skill.SkillSettingData.KMagicAttrib(database.magic2Type, database.magic2Value0, database.magic2Value1, database.magic2Value2),
                new skill.SkillSettingData.KMagicAttrib(database.magic3Type, database.magic3Value0, database.magic3Value1, database.magic3Value2),
                new skill.SkillSettingData.KMagicAttrib(database.magic4Type, database.magic4Value0, database.magic4Value1, database.magic4Value2),
                new skill.SkillSettingData.KMagicAttrib(database.magic5Type, database.magic5Value0, database.magic5Value1, database.magic5Value2),
            };

            foreach (skill.SkillSettingData.KMagicAttrib magicEntry in magicList)
            {
                if (magicEntry.nAttribType == 0)
                {
                    break;
                }

                this.magicAttrib.Add(magicEntry);
            }
        }

        private void SetDataMaskEquip(item.Database database)
        {
            this.type = Defination.Type.normalEquip;
            this.equipmentBase = item.Getters.GetMaskBase(database.genre, database.detail, database.particular);
            this.stack = database.stack;
            this.timeUse = database.timeUse;
        }

        private void SetDataMagicScript(item.Database database)
        {
            this.databaseId = database.databaseId;
            this.magicScriptBase = item.Getters.GetMagicScriptBase(database.genre, database.detail, database.particular);
            this.level = database.level;
            this.series = database.series;
            this.stack = database.stack;
            this.timeUse = database.timeUse;
        }

        protected void SetData(item.Database database)
        {
            switch (database.genre)
            {
                case 0:
                    switch(database.detail)
                    {
                        case 11:
                            this.SetDataMaskEquip(database);
                            break;

                        default:
                            this.SetDataEquipment(database);
                            break;
                    }
                    break;

                case 6:
                    this.SetDataMagicScript(database);
                    break;
            }
        }
    }
}
