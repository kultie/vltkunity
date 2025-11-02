using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum ItemPart : byte
    {
        itempart_head = 0,  // 头
        itempart_body,      // 身体
        itempart_belt,      // 腰带
        itempart_weapon,    // 武器
        itempart_foot,      //靴子
        itempart_cuff,      //手镯或护腕
        itempart_amulet,    //项链
        itempart_ring1,     //戒子
        itempart_ring2,     //戒子
        itempart_pendant,   //香囊或玉佩
        itempart_horse,     //马
        itempart_mask,      //面具
        itempart_pifeng,
        itempart_yinjian,
        itempart_shiping,
        itempart_num,
    };
    public enum ItemPosition : byte
    {
        pos_hand = 1,       // ÊÖÉÏ
        pos_equip,          // ×°±¸×ÅµÄ
        pos_equiproom,      // µÀ¾ßÀ¸  °ü¸¤
        pos_repositoryroom, // ÖüÎïÏä
        pos_exbox1room,     // mo rong ruong 1  À©Õ¹Ïä
        pos_exbox2room,     // mo rong ruong 2
        pos_exbox3room,     // mo rong ruong 3
        pos_equiproomex,    // Í¬°é°ü¸¤
        pos_traderoom,      // ½»Ò×À¸
        pos_trade1,         // ½»Ò×¹ý³ÌÖÐ¶Ô·½µÄ½»Ò×À¸
        pos_immediacy,      // ¿ì½ÝÀ¸ÎïÆ·
        pos_give,           // ¸øÓè½çÃæ
        pos_dazao,          // ´òÔì¿ò½çÃæ
        pos_cailiao,        // ²ÄÁÏÀ¸
        pos_xiangqi,        // ÏóÆåÀ¸
    };

    public enum ItemGenre : byte
    {
        item_equip = 0,         // 装备
        item_medicine,          // 药品
        item_mine,              // 矿石
        item_materials,         // 药材
        item_task,              // 任务
        item_townportal,        // 传送门
        item_fusion,            // 纹纲
        item_number,            // 类型数目
        item_equip_gold,
        item_equip_platinum,
    };

    public enum EquipDetailType : byte
    {
        equip_meleeweapon = 0,
        equip_rangeweapon,
        equip_armor,
        equip_ring,
        equip_amulet,
        equip_boots,
        equip_belt,
        equip_helm,
        equip_cuff,
        equip_pendant,
        equip_horse,
        equip_mask, // mat na
        equip_pifeng,
        equip_yinjian,
        equip_shiping,
        equip_detailnum,
    };

    public enum MEDICINEDETAILTYPE : byte
    {
        medicine_blood = 0,   //吃药的类型  血
        medicine_mana,        //内
        medicine_both,        //血和内
        medicine_stamina,     //体力
        medicine_antipoison,  //
        medicine_allboth = 8,
        medicine_detailnum,
    };
}
