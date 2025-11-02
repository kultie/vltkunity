
namespace game.resource.settings
{
    public class Item : item.Generator
    {
        public static void Initialize()
        {
            new settings.item.Initialize();
        }

        ///////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// tạo vật phẩm mới - trang bị
        /// tạo vật phẩm mới - mask.txt
        /// tạo vật phẩm mới - magicscript.txt
        /// </summary>
        /// <param name="genre">item.Defination.Genre: trang bị, thuốc, vật phẩm nhiệm vụ, ...</param>
        /// <param name="detail">vũ khí, áo, ngựa, dây chuyền, ...</param>
        /// <param name="paticular">đơn đao, song đao, chùy, thương, kiếm, ...</param>
        /// <param name="level">cấp độ</param>
        /// <param name="series">ngũ hành</param>
        /// <param name="luckyPercent">may mắn: từ 0 -> 100%</param>
        public Item(int genre, int detail, int paticular, int level = 1, int series = 0, int luckyPercent = 5)
        {
            this.Generate(genre, detail, paticular, level, series, luckyPercent);
        }

        /// <summary>
        /// tạo vật phẩm mới - goldequip.txt
        /// </summary>
        /// <param name="id">vật phẩm chỉ định</param>
        /// <param name="type">loại vật phẩm</param>
        public Item(int id, item.Defination.Type type = item.Defination.Type.goldEquip)
        {
            this.Generate(id, type);
        }

        /// <summary>
        /// khôi phục item từ dữ liệu database
        /// </summary>
        public Item(item.Database database)
        {
            this.SetData(database);
        }
    }
}
