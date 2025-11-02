
namespace game.resource.settings.item
{
    public class GoldMagicBase
    {
        public int type;
        public int value1Min;
        public int value1Max;
        public int value2Min;
        public int value2Max;
        public int value3Min;
        public int value3Max;

        ///////////////////////////////////////////////////////////////////////////
        
        public void Load(resource.Table table, int rowIndex)
        {
            this.type = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.type, rowIndex);
            this.value1Min = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value1Min, rowIndex);
            this.value1Max = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value1Max, rowIndex);
            this.value2Min = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value2Min, rowIndex);
            this.value2Max = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value2Max, rowIndex);
            this.value3Min = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value3Min, rowIndex);
            this.value3Max = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.MagicAttribGoldEquip.value3Max, rowIndex);
        }
    }
}
