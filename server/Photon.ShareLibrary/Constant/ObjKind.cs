using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum MAP_ITEMKIND_NODROP : byte
    {
        NO_DROP_ITEM = 1 << 0,
        NO_DROP_EXP = 1 << 1,
        NO_DROP_MONEY = 1 << 2,
    };
    public enum ObjKind : byte
    {
        Obj_Kind_MapObj = 0,
        Obj_Kind_Body,
        Obj_Kind_Box,
        Obj_Kind_Item,
        Obj_Kind_Money,
        Obj_Kind_LoopSound,
        Obj_Kind_RandSound,
        Obj_Kind_Light,
        Obj_Kind_Door,
        Obj_Kind_Trap,
        Obj_Kind_Prop,
        Obj_Kind_Num,
    }
}
