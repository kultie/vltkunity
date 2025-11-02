
using System.Collections.Generic;

namespace game.resource.settings.item.analysis
{
    public class Mask
    {
        public enum TimeUseType
        {
            /// <summary>
            /// không xác định
            /// </summary>
            unidentified,

            /// <summary>
            /// số lần sử dụng
            /// </summary>
            times,

            /// <summary>
            /// thời hạn sử dụng
            /// </summary>
            timeExpire,
        }

        private settings.Item item;

        public Mask(settings.Item item)
        {
            this.item = item;
        }

        public analysis.Mask.TimeUseType GetTimeUseType()
        {
            settings.item.EquipmentBase equipmentBase = this.item.GetEquipmentBase();

            if(equipmentBase == null)
            {
                return TimeUseType.unidentified;
            }

            List<skill.SkillSettingData.KMagicAttrib> basicAttrib = equipmentBase.GetBasicAttrib();

            foreach (skill.SkillSettingData.KMagicAttrib entry in basicAttrib)
            {
                if(entry.nAttribType == 44)
                {
                    return TimeUseType.timeExpire;
                }

                if(entry.nAttribType == 31)
                {
                    return TimeUseType.times;
                }
            }

            return TimeUseType.unidentified;
        }

        public long GetTimeUseLeft()
        {
            analysis.Mask.TimeUseType timeUseType = this.GetTimeUseType();

            switch (timeUseType)
            {
                case TimeUseType.unidentified:
                    return 0;

                case TimeUseType.timeExpire:
                    return (this.item.GetTimeUse() - System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                case TimeUseType.times:
                    return this.item.GetTimeUse();
            }

            return 0;
        }
    }
}
