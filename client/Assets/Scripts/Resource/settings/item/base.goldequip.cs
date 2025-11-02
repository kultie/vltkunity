
using System.Collections.Generic;

namespace game.resource.settings.item
{
    public class GoldEquipBase : item.EquipmentBase
    {
        public int magic0;
        public int magic1;
        public int magic2;
        public int magic3;
        public int magic4;
        public int magic5;
        public int idSet;
        public int set;
        public int setNum;
        public int upSet;
        public int setId;
        public int yinMagicAttribs0;
        public int yinMagicAttribs1;
        public int rongNum;
        public int wengangPin;
        public int binfujiazhi;
        public int chiBangRes;

        ///////////////////////////////////////////////////////////////////////////

        private List<int> magicIndexList;

        ///////////////////////////////////////////////////////////////////////////

        public new void Load(resource.Table table, int rowIndex)
        {
            base.Load(table, rowIndex);

            this.magic0 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic0, rowIndex);
            this.magic1 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic1, rowIndex);
            this.magic2 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic2, rowIndex);
            this.magic3 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic3, rowIndex);
            this.magic4 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic4, rowIndex);
            this.magic5 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.magic5, rowIndex);
            this.idSet = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.idSet, rowIndex);
            this.set = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.set, rowIndex);
            this.setNum = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.setNum, rowIndex);
            this.upSet = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.upSet, rowIndex);
            this.setId = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.setId, rowIndex);
            this.yinMagicAttribs0 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.yinMagicAttribs0, rowIndex);
            this.yinMagicAttribs1 = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.yinMagicAttribs1, rowIndex);
            this.rongNum = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.rongNum, rowIndex);
            this.wengangPin = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.wengangPin, rowIndex);
            this.binfujiazhi = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.binfujiazhi, rowIndex);
            this.chiBangRes = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquip.chiBangRes, rowIndex);

            this.magicIndexList = new List<int>()
            {
                this.magic0,
                this.magic1,
                this.magic2,
                this.magic3,
                this.magic4,
                this.magic5,
            };
        }

        public List<int> GetMagicIndexList()
        {
            return this.magicIndexList;
        }
    }
}
