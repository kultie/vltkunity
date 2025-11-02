
namespace game.resource.settings.item
{
    public class GoldResBase
    {
        public int equipRowIndex;
        public int resId;

        ///////////////////////////////////////////////////////////////////////////

        public void Load(resource.Table table, int rowIndex)
        {
            this.equipRowIndex = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquipRes.equipRowIndex, rowIndex);
            this.resId = table.Get<int>((int)resource.mapping.settings.Item.HeaderIndexer.GoldEquipRes.resId, rowIndex);
        }
    }
}
