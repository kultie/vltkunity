
using System.Collections.Generic;

namespace game.resource.settings.item
{
    public class Getter : item.Setters
    {
        private static UnityEngine.Sprite itemThumbnailUnidentifiedSprite;
        private static UnityEngine.Sprite itemFramedTypeWhiteSprite;
        private static UnityEngine.Sprite itemFramedTypeBlueSprite;
        private static UnityEngine.Sprite itemFramedTypeGreenSprite;

        public static string GetRichText(string origin)
        {
            string result = origin.Replace("<enter>", "\n");
            result = result.Replace("<color>", "</color>");

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////

        public class GridablePos
        {
            public float cB;
            public float cE;
            public float rB;
            public float rE;

            public void Set(float cB, float cE, float rB, float rE)
            {
                this.cB = cB;
                this.cE = cE;
                this.rB = rB;
                this.rE = rE;
            }
        }

        public Getter.GridablePos GetThumbnailGridable()
        {
            Getter.GridablePos result = new GridablePos();
            result.Set(1, 38, 1, 18);

            int itemStoredSize = 0;

            if (this.equipmentBase != null)
            {
                itemStoredSize = System.Math.Max(this.equipmentBase.height, this.equipmentBase.width);
            }
            else if (this.magicScriptBase != null)
            {
                itemStoredSize = System.Math.Max(this.magicScriptBase.height, this.magicScriptBase.width);
            }

            if (itemStoredSize == 1)
            {
                result.Set(12, 27, 6, 13);
            }
            else if (itemStoredSize == 2)
            {
                result.Set(9, 30, 3, 16);
            }

            return result;
        }

        public UnityEngine.Sprite GetThumbnailSprite()
        {
            if (Item.itemThumbnailUnidentifiedSprite == null)
            {
                Item.itemThumbnailUnidentifiedSprite = Game.Resource("\\user.interface\\panel.equipment\\item.tab\\item.unidentified.png").Get<UnityEngine.Sprite>();
            }

            UnityEngine.Sprite sprite;

            if (this.equipmentBase != null)
            {
                if ((sprite = Game.Resource(this.equipmentBase.imagePath).Get<UnityEngine.Sprite>(0)) == null)
                {
                    sprite = Item.itemThumbnailUnidentifiedSprite;
                }
            }
            else if (this.magicScriptBase != null)
            {
                if ((sprite = Game.Resource(this.magicScriptBase.image).Get<UnityEngine.Sprite>(0)) == null)
                {
                    sprite = Item.itemThumbnailUnidentifiedSprite;
                }
            }
            else
            {
                sprite = Item.itemThumbnailUnidentifiedSprite;
            }

            return sprite;
        }

        public Getter.GridablePos GetTypeGridable()
        {
            Getter.GridablePos result = new GridablePos();
            result.Set(1, 38, 1, 18);
            return result;
        }

        public UnityEngine.Sprite GetTypeSprite()
        {
            UnityEngine.Sprite result = null;

            if (this.IsEquipment() == false)
            {
                if (this.magicScriptBase != null)
                {
                    if (this.magicScriptBase.script.EndsWith(".lua") == false)
                    {
                        if (Item.itemFramedTypeWhiteSprite == null)
                        {
                            Item.itemFramedTypeWhiteSprite = Game.Resource("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.white.png").Get<UnityEngine.Sprite>();
                        }

                        result = Item.itemFramedTypeWhiteSprite;
                    }
                    else
                    {
                        if (Item.itemFramedTypeGreenSprite == null)
                        {
                            Item.itemFramedTypeGreenSprite = Game.Resource("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.green.png").Get<UnityEngine.Sprite>();
                        }

                        result = Item.itemFramedTypeGreenSprite;
                    }
                }

                return result;
            }

            if (this.HaveMagicAttrib() == false)
            {
                if (Item.itemFramedTypeWhiteSprite == null)
                {
                    Item.itemFramedTypeWhiteSprite = Game.Resource("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.white.png").Get<UnityEngine.Sprite>();
                }

                result = Item.itemFramedTypeWhiteSprite;
            }
            else
            {
                if (Item.itemFramedTypeBlueSprite == null)
                {
                    Item.itemFramedTypeBlueSprite = Game.Resource("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.blue.png").Get<UnityEngine.Sprite>();
                }

                result = Item.itemFramedTypeBlueSprite;
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////

        public settings.item.EquipmentBase GetEquipmentBase()
        {
            return this.equipmentBase;
        }

        public settings.item.MagicScriptBase GetMagicScriptBase()
        {
            return this.magicScriptBase;
        }

        public List<settings.skill.SkillSettingData.KMagicAttrib> GetBasicAttribs()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.GetBasicAttrib();
            }

            return null;
        }

        public List<settings.skill.SkillSettingData.KMagicAttrib> GetRequiredAttribs()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.GetRequiredAttrib();
            }

            return null;
        }

        public bool HaveMagicAttrib()
        {
            if(this.magicAttrib != null
                && this.magicAttrib.Count > 0)
            {
                return true;
            }

            return false;
        }

        public List<settings.skill.SkillSettingData.KMagicAttrib> GetMagicAttribs()
        {
            return this.magicAttrib;
        }

        public int GetGenre()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.genre;
            }
            else if (this.magicScriptBase != null)
            {
                return this.magicScriptBase.genre;
            }

            return -1;
        }

        public int GetDetail()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.detail;
            }
            else if (this.magicScriptBase != null)
            {
                return this.magicScriptBase.detail;
            }

            return -1;
        }

        public int GetParticular()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.particular;
            }
            else if (this.magicScriptBase != null)
            {
                return this.magicScriptBase.particular;
            }

            return -1;
        }

        public int GetLevel()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.level;
            }

            return this.level;
        }

        public int GetSeries()
        {
            return this.series;
        }

        public string GetGDPLS()
        {
            return string.Empty + this.GetGenre() + ", " + this.GetDetail() + ", " + this.GetParticular() + ", " + this.GetLevel() + ", " + this.GetSeries();
        }

        public string GetName()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.name;
            }

            if (this.magicScriptBase != null)
            {
                return this.magicScriptBase.name;
            }

            return null;
        }

        public int GetPrice()
        {
            if (this.equipmentBase != null)
            {
                return this.equipmentBase.price;
            }

            if (this.magicScriptBase != null)
            {
                return this.magicScriptBase.price;
            }

            return 0;
        }

        public string GetBasicAttribDesc()
        {
            if (this.basicAttribDesc != null)
            {
                return this.basicAttribDesc;
            }

            this.basicAttribDesc = string.Empty;
            List<settings.skill.SkillSettingData.KMagicAttrib> basicList = this.GetBasicAttribs();

            if (basicList == null)
            {
                return this.basicAttribDesc;
            }

            foreach (skill.SkillSettingData.KMagicAttrib magicEntry in basicList)
            {
                if (magicEntry.nAttribType <= 0)
                {
                    continue;
                }

                if (magicEntry.nValue[0] <= 0
                    && magicEntry.nValue[1] <= 0)
                {
                    continue;
                }

                if (this.basicAttribDesc.Length > 0)
                {
                    this.basicAttribDesc += "\n";
                }

                this.basicAttribDesc += settings.MagicDesc.Get(magicEntry);
            }

            return this.basicAttribDesc;
        }

        public List<string> GetMagicAttribDesc()
        {
            if (this.magicAttribDesc != null)
            {
                return this.magicAttribDesc;
            }

            this.magicAttribDesc = new List<string>();

            if(this.magicAttrib == null)
            {
                return this.magicAttribDesc;
            }

            foreach (skill.SkillSettingData.KMagicAttrib magicEntry in this.magicAttrib)
            {
                this.magicAttribDesc.Add(settings.MagicDesc.Get(magicEntry));
            }

            return this.magicAttribDesc;
        }

        public bool IsEquipment()
        {
            if (this.equipmentBase == null)
            {
                return false;
            }

            return (int)item.Defination.Genre.item_equip == this.equipmentBase.genre;
        }

        public uint GetDatabaseId()
        {
            return this.databaseId;
        }

        public string GetIntro()
        {
            if (this.equipmentBase != null)
            {
                return Getter.GetRichText(this.equipmentBase.intro);
            }

            if (this.magicScriptBase != null)
            {
                return Getter.GetRichText(this.magicScriptBase.intro);
            }

            return null;
        }

        public settings.item.Defination.Type GetItemType()
        {
            return this.type;
        }

        public Dictionary<int, string> GetSetItemList()
        {
            if (this.type != Defination.Type.goldEquip)
            {
                return null;
            }

            if (this.setItemList != null)
            {
                return this.setItemList;
            }

            return this.setItemList = item.Getters.GetGoldItemSet(((item.GoldEquipBase)this.equipmentBase).idSet);
        }

        public int GetStackCurrently() => this.stack;
        public int GetStackMaximun()
        {
            if(this.magicScriptBase == null)
            {
                return 0;
            }

            return this.magicScriptBase.stackValue;
        }

        public long GetTimeUse() => this.timeUse;

        public void SetTimeUse(long timeUse) => this.timeUse = timeUse;

        public string GetTimeExpire()
        {
            return System.DateTimeOffset.FromUnixTimeSeconds(this.timeUse).ToString("HH:mm dd-MM-yyyy");
        }
    }
}
