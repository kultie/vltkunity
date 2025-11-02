using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum NPCKIND : byte
    {
        kind_normal = 0,
        kind_player,
        kind_partner,
        kind_dialoger,
        kind_bird,
        kind_mouse,
        kind_num
    };
    public enum NPCSERIES : byte
    {
        series_metal = 0,
        series_wood,
        series_water,
        series_fire,
        series_earth,
        series_num,
    };
    public enum NPCCAMP : byte
    {
        camp_begin = 0,
        camp_justice,
        camp_evil,
        camp_balance,
        camp_free,
        camp_animal,
        camp_event,
        camp_blue,
        camp_green,
        camp_num,
    };
    public enum NPCRELATION : byte
    {
        relation_empty = 0,
        relation_none = 1,
        relation_self = 2,
        relation_ally = 4,
        relation_enemy = 8,
        relation_dialog = 16,
        relation_all = relation_none | relation_ally | relation_enemy | relation_self | relation_dialog,
        relation_num,
    };
}
