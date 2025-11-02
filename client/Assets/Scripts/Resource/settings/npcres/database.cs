
using System.Collections.Generic;

namespace game.resource.settings.npcres
{
    public class Database
    {
        // tên nhân vật
        // định danh nhân vật, là duy nhất, không trùng lặp
        // độ dài cho phép từ 3 --> 30 ký tự
        // cho phép khoảng trống, [a-zA-Z0-9], cho phép dùng tiếng việt
        // chú ý: không cho phép tạo khoảng trống bằng các ký tự rỗng
        public string name;

        // tên tài khoản
        // dùng để đăng nhập trò chơi
        public string account;

        // giới tính
        // 0: nữ, 1: nam
        public int sex;

        // ngũ hành nhân vật
        // 0: kim, 1: mộc, 2: thủy, 3: hỏa, 4: thổ
        public int fiveprop;

        // môn phái
        // 1: thiên vương, 2: thiếu lâm, 3: đường môn, 4: ngũ độc, 5: nga mi
        // 6: thúy yên, 7: cái bang, 8: thiên nhẫn, 9: võ đang, 10: côn lôn
        public int sect;

        // chế độ chiến đấu
        // 0: luyện công, 1: chiến đấu, 2: đồ sát
        public int fightMode;

        // tổng số người đã đồ sát
        // thể hiện mức án phạt khi bị người chơi khác đồ sát lại
        public int currentPk;

        // vị trí bản đồ trong trò chơi
        public int mapId;
        public int mapX;
        public int mapY;

        // tiền vạn trong hành trang
        public ulong bagMoney;

        // tiền vạn lưu trong rương chứa đồ
        public ulong saveMoney;

        // cấp nhân vật
        public int level;

        // điểm kinh nghiệm
        public ulong exp;

        // sức mạnh
        public int power;

        // thân pháp
        public int agility;

        // sinh khí
        public int outer;

        // nội công
        public int inside;

        // may mắn
        public int luck;

        // máu tối đa
        // máu hiện tại
        public int maxLife;
        public int curLife;

        // thể lực tối đa
        // thể lực hiện tại
        public int maxStamina;
        public int curStamina;

        // nội lực tối đa
        // nội lực hiện tại
        public int maxMana;
        public int curMana;

        // hình ảnh phần đầu
        public int headRes;

        // hình ảnh phần thân
        public int bodyRes;

        // hình ảnh vũ khí
        public int weaponRes;

        // hình ảnh ngựa
        public int horseRes;

        // xếp hạng thế giới theo đẳng cấp
        public int worldLevelSort;

        // băng đảng
        // 0: sơ nhập giang hồ, 1: chính phái, 2: tà phái, 3: trung lập, 4: xuất sư, tự do
        public int camp;

        // kháng hỏa
        public int fireResist;

        // kháng băng
        public int coldResist;

        // kháng độc
        public int poisResist;

        // kháng lôi
        public int lighResist;

        // phòng thủ vật lý
        public int physResist;

        // danh sách skill
        // mapping[skill.id] => <skill.level>
        public Dictionary<int, int> skillList;
    }
}
