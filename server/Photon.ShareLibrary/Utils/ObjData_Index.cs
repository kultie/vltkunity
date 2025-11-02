using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public enum ObjData_Index : byte
    {
        ObjDataField_Name = 0,
        ObjDataField_DataID,
        ObjDataField_Kind,
        ObjDataField_ScriptName,
        ObjDataField_ImageName,
        ObjDataField_SoundName,
        ObjDataField_LifeTime,
        ObjDataField_Layer,
        ObjDataField_Height,

        ObjDataFiled_SkillKind,
        ObjDataFiled_SkillCamp,
        ObjDataFiled_SkillRange,
        ObjDataField_SkillCastTime,
        ObjDataField_SkillID,
        ObjDataField_SkillLevel,

        ObjDataField_LightRadius,
        ObjDataField_LightRed,
        ObjDataField_LightGreen,
        ObjDataField_LightBlue,
        ObjDataField_LightAlpha,
        ObjDataField_LightReflectType,

        ObjDataField_ImageTotalFrame,
        ObjDataField_ImageCurFrame,
        ObjDataField_ImageTotalDir,
        ObjDataField_ImageCurDir,
        ObjDataField_ImageInterval,
        ObjDataField_ImageCgXpos,
        ObjDataField_ImageCgYpos,

        ObjDataField_Bar0,
        ObjDataField_Bar1,
        ObjDataField_Bar2,
        ObjDataField_Bar3,
        ObjDataField_Bar4,
        ObjDataField_Bar5,
        ObjDataField_Bar6,
        ObjDataField_Bar7,
        ObjDataField_Bar8,
        ObjDataField_Bar9,
        ObjDataField_Bar10,
        ObjDataField_Bar11,
        ObjDataField_Bar12,
        ObjDataField_Bar13,
        ObjDataField_Bar14,

        ObjDataField_ImageDropName,
        ObjDataField_ImageDropTotalFrame,
        ObjDataField_ImageDropCurFrame,
        ObjDataField_ImageDropTotalDir,
        ObjDataField_ImageDropCurDir,
        ObjDataField_ImageDropInterval,
        ObjDataField_ImageDropCgXpos,
        ObjDataField_ImageDropCgYpos,

        ObjDataField_DrawFlag,

        ObjDataField_Num,
    }
}
