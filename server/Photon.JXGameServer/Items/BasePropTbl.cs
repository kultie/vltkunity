using ExitGames.Logging;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Photon.JXGameServer.Items
{
    public class BasePropConst
    {
        public static readonly int MATF_CBDR = 15;      // 物品类型 +1 容错 type(现在的值为 equip_detailnum)  装备属性可出现部位数量限制
        public static readonly int MATF_PREFIXPOSFIX = 2; // 前缀后缀
        public static readonly int MATF_SERIES = 5;     // 五行
        public static readonly int MATF_LEVEL = 10;
    }
   
    public struct RowIndexRecord
    {
        public int ItemGenre;
        public int Level;
        public int DetailType;
        public int ParticularType;
        public int RowIndex;
    }
    public class BasicPropertyTable
    {
        protected int SizeOfEntry { get; set; }
        protected string TabFile { get; set; }
        protected int NumOfEntries { get; set; }
        protected JX_Table tb;
        protected Type PropType;

        protected Dictionary<string,int> RowIndexRecord = new Dictionary<string, int>();
        public bool Init(string root, string TabFile)
        {
            string fullPath = Path.Combine(root, ItemFilePath.TABFILE_PATH + "\\" + TabFile);
            tb = new JX_Table(fullPath);
            NumOfEntries = tb.RowCount - 1;
            SizeOfEntry = tb.HeaderCount;
            return true;
        }
        public int FindItemRecord(int ItemGenre, int DetailType, int ParticularType, int Level) { 
            return FindItemRecord(string.Format("{0},{1},{2},{3}", ItemGenre, DetailType, ParticularType, Level)); 
        }
        public int FindItemRecord(string cacheKey)
        {
            return RowIndexRecord[cacheKey];
        }
        public void LoadRecord(Type type, object obj, int row, int intDefault = 0)
        {
            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if (i <= tb.HeaderCount)
                {
                    Type fieldType = fields[i].FieldType;
                    if (fieldType == typeof(byte)||fieldType == typeof(short)||fieldType == typeof(ushort)||fieldType == typeof(int)||fieldType == typeof(uint))
                    {
                        var v = tb.GetInt(i, row, intDefault);

                        if (fieldType == typeof(byte))
                            fields[i].SetValue(obj, (byte)v);
                        else
                        if (fieldType == typeof(short))
                            fields[i].SetValue(obj, (short)v);
                        else
                        if (fieldType == typeof(ushort))
                            fields[i].SetValue(obj, (ushort)v);
                        else
                            fields[i].SetValue(obj, v);
                    }
                    else
                    if (fieldType == typeof(string))
                    {
                        fields[i].SetValue(obj, tb.GetString(i, row));
                    }
                    else
                    if (fieldType == typeof(bool))
                    {
                        int val = tb.GetInt(i, row, 0);
                        fields[i].SetValue(obj, Convert.ToBoolean(val));
                    }
                }
            }
        }
    }

    public class BasicPropMine
    {
        public string Name;
        public byte ItemGenre;               // 道具种类
        public ushort DetailType;              // 具体类别
        public byte ParticularType;          // 详细类别
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 道具栏中所占宽度
        public byte Height;                  // 道具栏中所占高度
        public string Intro;     // 说明文字
        public byte Series;                  // 五行属性
        public int Price;                   // 价格
        public byte Level;                   // 等级
        public bool Stack;                  // 是否可叠放
        public string Script;    // 执行脚本
        public int Magic0;
        public int Magic1;
        public int Magic2;
        public int Magic3;
        public int Magic4;
        public int Delet;
        public int PriceXu;                 // 金币
        public int IsKuaiJie;
        public int IsMagic;
        public int IsUse;
    }

    public class KAttrib
    {
        int Attrib;
        int Value;
        int Time;
    }

    public class BasicPropMedicine
    {
        public string Name;      // 名称
        public byte ItemGenre;               // 道具种类
        public ushort DetailType;              // 具体类别
        public byte ParticularType;          // 详细类别
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 道具栏中所占宽度
        public byte Height;                  // 道具栏中所占高度
        public string Intro;     // 说明文字
        public byte Series;                  // 五行属性
        public int Price;                   // 价格
        public byte Level;              // 等级
        public bool Stack;             // 是否可叠放
        public int IsKuaiJie;
        public int Attrib0;
        public int AttribValue0;
        public int ValueTime0;
        public int Attrib1;
        public int AttribValue1;
        public int ValueTime1;
        public int Attrib2;
        public int AttribValue2;
        public int ValueTime2;
        public int Attrib3;
        public int AttribValue3;
        public int ValueTime3;
        public int Attrib4;
        public int AttribValue4;
        public int ValueTime4;
        public int Attrib5;
        public int AttribValue5;
        public int ValueTime5;
        public int UseMap;
    }

    public class BasicPropMaterial
    {
        string Name;      // 名称
        byte ItemGenre;               // 道具种类
        ushort DetailType;              // 具体类别
        byte ParticularType;          // 详细类别
        string ImageName; // 界面中的动画文件名
        int ObjIdx;                  // 对应物件索引
        byte Width;                   // 道具栏中所占宽度
        byte Height;                  // 道具栏中所占高度
        string Intro;     // 说明文字
        byte Series;                  // 五行属性
        int Price;                   // 价格
        int PriceXu;
        byte Level;        // 等级
        bool bStack;       // 是否可叠放
        int Attrib1_Type; // 属性1类型
        int Attrib1_Para; // 属性1参数
        int Attrib2_Type; // 属性2类型
        int Attrib2_Para; // 属性2参数
        int Attrib3_Type; // 属性2类型
        int Attrib3_Para; // 属性2参数
        int IsBang;
        int IsKuaiJie;
        int IsMagic;
    }

    public class MinMaxPair
    {
        public int Min;
        public int Max;
    }

    public class EquipmentCoreParamBasic
    {
        public short Type;          // 属性类型
        public MinMaxPair Range; // 取值范围
    }

    public class EquipmentCoreParamRequirment
    {
        public short Type; // 属性类型
        public short Para; // 数值
    }

    public class MagicAtrribCoreParam
    {
        int PropKind;           // 修改的属性类型（对同一个数值加百分比和加点数被认为是两个属性）
        MinMaxPair[] Range = new MinMaxPair[3]; // 修改属性所需的几个参数
    }

    public class MagicAttributeTabFile
    {
        public string Name;
        public int Pos;                 // 前缀还是后缀
        public int Class;               // 五行要求
        public int Level;               // 等级要求
        public int PropKind;
        public int MagicAttriRangeMin0;
        public int MagicAttriRangeMax0;
        public int MagicAttriRangeMin1;
        public int MagicAttriRangeMax1;
        public int MagicAttriRangeMin2;
        public int MagicAttriRangeMax2;
        public string Intro; // 说明文字
        public int DropRate0;
        public int DropRate1;
        public int DropRate2;
        public int DropRate3;
        public int DropRate4;
        public int DropRate5;
        public int DropRate6;
        public int DropRate7;
        public int DropRate8;
        public int DropRate9;
        public int DropRate10;
        public int DropRate11;
        public int DropRate12;
        public int DropRate13;
        public int DropRate14;

        public bool m_nUseFlag;
    }

    public class MagicGoldAttributeTabFile
    {
        public string name;
        public int prefix;
        public int series;
        public int faction;
        public int type;
        public int value1Min;
        public int value1Max;
        public int value2Min;
        public int value2Max;
        public int value3Min;
        public int value3Max;
        public string intr;
    }

    public class BasicPropEquiment
    {
        public string Name; // 名称
        public byte ItemGenre;          // 道具种类 (武器? 药品? 矿石?)
        public ushort DetailType;         // 具体类别
        public byte ParticularType;     // 详细类别     
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 道具栏中所占宽度
        public byte Height;                  // 道具栏中所占高度
        public string Intro;     // 说明文字
        public byte Series;                  // 五行属性
        public int Price;                   // 价格
        public byte Level;                  // 等级
        public bool Stack;                 // 是否可叠放
        public int PropBasicType0;
        public int PropBasicRangeMin0;
        public int PropBasicRangeMax0;
        public int PropBasicType1;
        public int PropBasicRangeMin1;
        public int PropBasicRangeMax1;
        public int PropBasicType2;
        public int PropBasicRangeMin2;
        public int PropBasicRangeMax2;
        public int PropBasicType3;
        public int PropBasicRangeMin3;
        public int PropBasicRangeMax3;
        public int PropBasicType4;
        public int PropBasicRangeMin4;
        public int PropBasicRangeMax4;
        public int PropBasicType5;
        public int PropBasicRangeMin5;
        public int PropBasicRangeMax5;
        public int PropBasicType6;
        public int PropBasicRangeMin6;
        public int PropBasicRangeMax6;
        public int PropReqType0;
        public int PropReqPara0;
        public int PropReqType1;
        public int PropReqPara1;
        public int PropReqType2;
        public int PropReqPara2;
        public int PropReqType3;
        public int PropReqPara3;
        public int PropReqType4;
        public int PropReqPara4;
        public int PropReqType5;
        public int PropReqPara5;
        public int YingNuma;    // 隐藏属性1
        public int YingNumb;    // 隐藏属性2
        public int RongNum;     // 可溶属性数量
        public int WengangPin;  // 可溶纹钢品质
        public int Binfujiazhi; // 兵富甲值
        public int ChiBangRes;
        public int ParticularTypea;

        public int PriceXu;

        public EquipmentCoreParamBasic[] m_aryPropBasic
        {
            get
            {
                EquipmentCoreParamBasic[] m_aryPropBasic = new EquipmentCoreParamBasic[7];
                for (int i = 0; i < m_aryPropBasic.Length; i++)
                {
                    if (m_aryPropBasic[i] == null)
                    {
                        m_aryPropBasic[i] = new EquipmentCoreParamBasic();
                     }
                    m_aryPropBasic[i].Type = (short)GetType().GetField("PropBasicType" + i).GetValue(this);
                    m_aryPropBasic[i].Range = new MinMaxPair()
                    {
                        Min = (int)GetType().GetField(string.Format("PropBasicRangeMin{0}", i)).GetValue(this),
                        Max = (int)GetType().GetField(string.Format("PropBasicRangeMax{0}", i)).GetValue(this),
                    };
                }
                return m_aryPropBasic;
            }
        }

        public EquipmentCoreParamRequirment[] m_aryPropReq
        {
            get
            {
                EquipmentCoreParamRequirment[] m_aryPropReq = new EquipmentCoreParamRequirment[6];

                for (int i = 0; i < m_aryPropReq.Length; i++)
                {
                    if (m_aryPropReq[i] == null)
                    {
                        m_aryPropReq[i] = new EquipmentCoreParamRequirment();
                    }
                    m_aryPropReq[i].Type = (short)GetType().GetField("PropReqType" + i).GetValue(this);
                    m_aryPropReq[i].Para = (short)GetType().GetField("PropReqPara" + i).GetValue(this);
                }
                return m_aryPropReq;
            }
        }
    }

    // 以下结构用于描述唯一装备的初始属性. 相关数据由配置文件(tab file)提供 ----蓝装
    public class BasicPropEquimentUnique
    {

        string Name;      // 名称
        byte ItemGenre;               // 道具种类 (武器? 药品? 矿石?)
        ushort DetailType;              // 具体类别
        byte ParticularType;          // 详细类别
        string szImageName; // 界面中的动画文件名
        int ObjIdx;                  // 对应物件索引
        string Intro;     // 说明文字
        byte Series;                  // 五行属性
        int Price;                   // 价格
        int PriceXu;
        byte Level;               // 等级
        int Rarity;              // 稀有程度
        EquipmentCoreParamRequirment[] m_aryPropReq = new EquipmentCoreParamRequirment[6];  // 需求属性
        MagicAtrribCoreParam[] m_aryMagicAttributes = new MagicAtrribCoreParam[6]; // 魔法属性
        int m_IsBang;
        int m_IsKuaiJie;
        int m_IsMagic;
    }

    // 以下结构用于描述黄金装备的初始属性. 相关数据由配置文件(tab file)提供 =====白金
    public class BasicPropEquipmentPlatina
    {
        public string Name; // 名称
        public byte ItemGenre;          // 道具种类 (武器? 药品? 矿石?)
        public ushort DetailType;         // 具体类别
        public byte ParticularType;     // 详细类别               
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 物品栏宽度
        public byte Height;                  // 物品栏高度
        public string Intro;     // 说明文字
        public byte Series;                  // 五行属性
        public int Price;                   // 价格
        public byte Level;                  // 等级
        public bool Stack;                  // 是否可叠放
        public int PropBasicType0;
        public int PropBasicRangeMin0;
        public int PropBasicRangeMax0;
        public int PropBasicType1;
        public int PropBasicRangeMin1;
        public int PropBasicRangeMax1;
        public int PropBasicType2;
        public int PropBasicRangeMin2;
        public int PropBasicRangeMax2;
        public int PropBasicType3;
        public int PropBasicRangeMin3;
        public int PropBasicRangeMax3;
        public int PropBasicType4;
        public int PropBasicRangeMin4;
        public int PropBasicRangeMax4;
        public int PropBasicType5;
        public int PropBasicRangeMin5;
        public int PropBasicRangeMax5;
        public int PropBasicType6;
        public int PropBasicRangeMin6;
        public int PropBasicRangeMax6;

        public int PropReqType0;
        public int PropReqPara0;
        public int PropReqType1;
        public int PropReqPara1;
        public int PropReqType2;
        public int PropReqPara2;
        public int PropReqType3;
        public int PropReqPara3;
        public int PropReqType4;
        public int PropReqPara4;
        public int PropReqType5;
        public int PropReqPara5;

        public int MagicAttributes0;
        public int TempPlatinaAttrib0;
        public int MagicAttributes1;
        public int TempPlatinaAttrib1;
        public int MagicAttributes2;
        public int TempPlatinaAttrib2;
        public int MagicAttributes3;
        public int TempPlatinaAttrib3;
        public int MagicAttributes4;
        public int TempPlatinaAttrib4;
        public int MagicAttributes5;
        public int TempPlatinaAttrib5;
        public int Set;                 // 所在套装
        public int SetNum;              // 套装数量
        public int SixSkill;
        public int TenSkill;
        public int UpSet;               // 扩展套装
        public int SetId;               // 所属序号
        public int YinMagicAttributes0;
        public int YinMagicAttributes1;
        public int RongNum;             // 可溶属性数量
        public int WengangPin;          // 可溶纹钢品质
        public int Binfujiazhi;         // 兵富甲值
        public int ChiBangRes; // 翅膀的外观类型
    }

    // 以下结构用于描述黄金装备的初始属性. 相关数据由配置文件(tab file)提供 =====黄装
    // flying 根据策划需求修改自KBASICPROP_EQUIPMENT_UNIQUE类型
    public class BasicPropEquimentGold
    {
        public string Name;      // 名称
        public byte ItemGenre;               // 道具种类 (武器? 药品? 矿石?)
        public ushort DetailType;              // 具体类别
        public byte ParticularType;          // 详细类别                               
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 物品栏宽度
        public byte Height;                  // 物品栏高度
        public string Intro;     // 说明文字
        public byte Series;                  // 五行属性
        public int Price;                   // 价格
        public byte Level;
        public bool Stack;                  // 是否可叠放
        public int PropBasicType0;
        public int PropBasicRangeMin0;
        public int PropBasicRangeMax0;
        public int PropBasicType1;
        public int PropBasicRangeMin1;
        public int PropBasicRangeMax1;
        public int PropBasicType2;
        public int PropBasicRangeMin2;
        public int PropBasicRangeMax2;
        public int PropBasicType3;
        public int PropBasicRangeMin3;
        public int PropBasicRangeMax3;
        public int PropBasicType4;
        public int PropBasicRangeMin4;
        public int PropBasicRangeMax4;
        public int PropBasicType5;
        public int PropBasicRangeMin5;
        public int PropBasicRangeMax5;
        public int PropBasicType6;
        public int PropBasicRangeMin6;
        public int PropBasicRangeMax6;

        public int PropReqType0;
        public int PropReqPara0;
        public int PropReqType1;
        public int PropReqPara1;
        public int PropReqType2;
        public int PropReqPara2;
        public int PropReqType3;
        public int PropReqPara3;
        public int PropReqType4;
        public int PropReqPara4;
        public int PropReqType5;
        public int PropReqPara5;

        public int MagicAttributes0;
        public int MagicAttributes1;
        public int MagicAttributes2;
        public int MagicAttributes3;
        public int MagicAttributes4;
        public int MagicAttributes5;

        public int Id;
        public int Set;
        public int SetNum;
        public int UpSet;
        public int YinMagicAttributes0;
        public int YinMagicAttributes1;
        public int RongNum;
        public int WengangPin;
        public int Binfujiazhi;
        public int ChiBangRes;
    }

    public class BasicPropQuest
    {
        public string Name;      // 名称
        public int ItemGenre;               // 道具种类
        public int DetailType;              // 具体类别
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public int Width;                   // 道具栏中所占宽度
        public int Height;                  // 道具栏中所占高度
        public string Script;    // 执行脚本
        public string Intro;     // 说明文字
        public int Price;
        public int PriceXu;
        public int Delet;
        public int IsSell;
        public int IsTrade;
        public int IsDrop; // 材料？
        public int IsKuaiJie;
        public int SkillType; // 纹钢的属性魔法ID
        public int Series;   // 五行属性
        public int ISMagic;
        public int Level;
        public int Stack;  // 是否叠放
        public int MagicID; // 技能ID
        public int IsUse;
    }

    public class BasicPropFusion
    {
        public string Name;      // 名称
        public int ItemGenre;               // 道具种类
        public int DetailType;              // 具体类别
        public int ParticularType;          // 详细类别
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public int Width;                   // 道具栏中所占宽度
        public int Height;                  // 道具栏中所占高度
        public string Intro;     // 说明文字
        public int Series;   // 五行属性
        public int Price;
        public int Level;
        public int Stack;  // 是否叠放
        public int InPin;     // 品质
        public int MagIndex;  // 魔法索引
        public int Magic0;
        public int Magic1;
        public int Magic2;
        public int Magic3;
        public int Magic4;
        public int Magic5;
        public int IsBang;
        public int PriceXu;
    }

    public class BasicPropTownPortal
    {
        public string Name;      // 名称
        public byte ItemGenre;               // 道具种类
        public string ImageName; // 界面中的动画文件名
        public int ObjIdx;                  // 对应物件索引
        public byte Width;                   // 道具栏中所占宽度
        public byte Height;                  // 道具栏中所占高度
        public int Price;                   // 价格
        public string Intro; // 说明文字
    }

    public class KBPT_Equipment : BasicPropertyTable
    {
        public BasicPropEquiment[] props;
        public KBPT_Equipment(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropEquiment[NumOfEntries];

            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropEquiment prop = new BasicPropEquiment();
                LoadRecord(typeof(BasicPropEquiment), prop, row);
                props[i] = prop;
                RowIndexRecord[string.Format(string.Format("{0},{1},{2},{3}", prop.ItemGenre, prop.DetailType, prop.ParticularType, prop.Level))] = i;
            }
            log.Info("KBPT_Equipment: Load Finished " + props.Length);
        }
    }

    public class KBPT_Equipment_Gold : BasicPropertyTable
    {
        public BasicPropEquimentGold[] props;
        public KBPT_Equipment_Gold(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropEquimentGold[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropEquimentGold prop = new BasicPropEquimentGold();
                LoadRecord(typeof(BasicPropEquimentGold), prop, row);
                props[i] = prop;
                //Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Equipment_Gold: Load Finished " + props.Length);
        }
    }

    public class KBPT_Medicine : BasicPropertyTable
    {
        public BasicPropMedicine[] props;
        public KBPT_Medicine(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropMedicine[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropMedicine prop = new BasicPropMedicine();
                LoadRecord(typeof(BasicPropMedicine), prop, row);
                props[i] = prop;
                //Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Medicine: Load Finished " + props.Length);
        }
    }

    public class KBPT_Quest : BasicPropertyTable
    {
        public BasicPropQuest[] props;
        public KBPT_Quest(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropQuest[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropQuest prop = new BasicPropQuest();
                LoadRecord(typeof(BasicPropQuest), prop, row);
                props[i] = prop;
               // Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Quest: Load Finished " + props.Length);
        }
    }

    public class KBPT_TownPortal : BasicPropertyTable
    {
        public BasicPropTownPortal[] props;
        public KBPT_TownPortal(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropTownPortal[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropTownPortal prop = new BasicPropTownPortal();
                LoadRecord(typeof(BasicPropTownPortal), prop, row);
                props[i] = prop;
               // Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_TownPortal: Load Finished " + props.Length);
        }
    }

    public class KBPT_Mine : BasicPropertyTable
    {
        public BasicPropMine[] props;
        public KBPT_Mine(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropMine[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropMine prop = new BasicPropMine();
                LoadRecord(typeof(BasicPropMine), prop, row);
                props[i] = prop;
               // Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Mine: Load Finished " + props.Length);
        }
    }

    public class KBPT_MagicAttribute_TF : BasicPropertyTable
    {
        public MagicAttributeTabFile[] props;
        public KBPT_MagicAttribute_TF(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new MagicAttributeTabFile[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                MagicAttributeTabFile prop = new MagicAttributeTabFile();
                prop.m_nUseFlag = false;
                LoadRecord(typeof(MagicAttributeTabFile), prop, row, -1);
                props[i] = prop;
                //Debug.WriteLine(prop.Name);

            }
            log.Info("KBPT_MagicAttribute_TF: Load Finished " + props.Length);
        }
    }

    public class KBPT_MagicGoldAttribute_TF : BasicPropertyTable
    {
        public MagicGoldAttributeTabFile[] props;
        public KBPT_MagicGoldAttribute_TF(ILogger log, string root, string TabFile)
        {
            Init(root, TabFile);
            props = new MagicGoldAttributeTabFile[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                MagicGoldAttributeTabFile prop = new MagicGoldAttributeTabFile();
                LoadRecord(typeof(MagicGoldAttributeTabFile), prop, row, 0);
                props[i] = prop;
            }
            log.Info("KBPT_MagicGoldAttribute_TF: Load Finished " + props.Length);
        }
    }

    public class KBPT_Fusion : BasicPropertyTable
    {
        public BasicPropFusion[] props;
        public KBPT_Fusion(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropFusion[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropFusion prop = new BasicPropFusion();
                LoadRecord(typeof(BasicPropFusion), prop, row);
                props[i] = prop;
                //Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Fusion: Load Finished " + props.Length);
        }
    }

    public class KBPT_Equipment_Platina : BasicPropertyTable
    {
        public BasicPropEquipmentPlatina[] props;
        public KBPT_Equipment_Platina(ILogger log,string root,string TabFile)
        {
            Init(root, TabFile);
            props = new BasicPropEquipmentPlatina[NumOfEntries];
            for (int i = 0; i < props.Length; i++)
            {
                int row = i + 1;
                BasicPropEquipmentPlatina prop = new BasicPropEquipmentPlatina();
                LoadRecord(typeof(BasicPropEquipmentPlatina), prop, row);
                props[i] = prop;
               // Debug.WriteLine(prop.Name);
            }
            log.Info("KBPT_Equipment_Platina: Load Finished " + props.Length);
        }
    }

    public class KBPT_ClassMAIT
    {
        public int[] m_pnTable;
        public int m_nSize;
        public int m_nNumOfValidData;

        public int GetCount()
        {
            return m_nNumOfValidData;
        }

        public int Get(int i)
        {
            return m_pnTable[i];
        }
        internal bool Insert(int index)
        {
            if (m_pnTable == null)
            {
                m_pnTable = new int[4];
                m_nNumOfValidData = 0;
                m_nSize = 4;
            }
            if (m_nNumOfValidData >= m_nSize)
            {
                int[] pnaryTemp = new int[m_nSize + 8]; // Original: 8
                Array.Copy(m_pnTable, pnaryTemp, m_nNumOfValidData);
                m_nSize += 8; // Original: 8
                m_pnTable = pnaryTemp;
            }
            m_pnTable[m_nNumOfValidData++] = index;
            return true;
        }
    }
}