
namespace game.resource.settings.item
{
    public class Database
    {
        // id của vật phẩm trong database
        public uint databaseId;

        // loại vật phẩm
        // trang bị thường, trang bị hoàng kim
        public item.Defination.Type type = Defination.Type.unidentified;

        // dùng cho trang bị hoàng kim
        // id của trang bị trong file goldequip.txt
        public int rowIndex;

        // thông số cơ bản xác định vật phẩm
        public int genre;
        public int detail;
        public int particular;
        public int level;
        public int series;

        // tổng số lượng vật phẩm xếp chồng hiện có
        // số lượng cùng loại xếp chống hiển thị ở góc dưới bên phải của ô vật phẩm trong rương hành trang
        // hỗ trợ cho các vật phẩm trong magicscript.txt
        public int stack = 0;

        // thời hạn sử dụng đến thời điểm chỉ định, tính bằng unix time seconds 
        // System.DateTimeOffset.UtcNow.ToUnixTimeSeconds
        // hoặc số lần sử dụng còn lại
        // tùy thuộc vào cấu hình của vật phẩm
        public long timeUse = 0;

        // [BEGIN] thuộc tính ma pháp của vật phẩm, nếu có
        // phân tích dữ liệu từ settings.skill.SkillSettingData.KMagicAttrib

        public int magic0Type;
        public int magic0Value0;
        public int magic0Value1;
        public int magic0Value2;

        public int magic1Type;
        public int magic1Value0;
        public int magic1Value1;
        public int magic1Value2;

        public int magic2Type;
        public int magic2Value0;
        public int magic2Value1;
        public int magic2Value2;

        public int magic3Type;
        public int magic3Value0;
        public int magic3Value1;
        public int magic3Value2;

        public int magic4Type;
        public int magic4Value0;
        public int magic4Value1;
        public int magic4Value2;

        public int magic5Type;
        public int magic5Value0;
        public int magic5Value1;
        public int magic5Value2;

        // [END] thuộc tính ma pháp của vật phẩm
    }
}
