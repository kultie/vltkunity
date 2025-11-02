using JX.Database;
using Photon.JXGameServer.Common;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Utils;
using System.Linq;

namespace Photon.JXGameServer.Items
{
    public class ItemCommonAttr
    {
        public byte nItemGenre;             // 道具种类 (武器? 药品? 矿石?)
        public ushort nDetailType;            // 在该种类中的具体类别
        public byte nParticularType;        // 详细类别
        public int nObjIdx;                // 地图上摆放时对应的物件数据编号
        public bool bStack;                    // 是否可叠放
        public byte nWidth;                 // 道具栏中所占宽度
        public byte nHeight;                // 道具栏中所占高度
        public int nPrice;                 // 商店购买价格
        public byte nLevel;                 // 等级
        public byte nSeries;                // 五行属性
        public string szItemName;            // 名称
        public string szWonName;         // 所有者名称
        public int nColor;                 // 颜色
        public int nLianjieFlg;            // 链接状态
        public int nCanUse;                // 是否可以使用
        public byte nSellModel;             // 摆摊交易的模式
        public string ItmeInfo;
        public string szScript;
        public int nSet;                    // 套装
        public int nSetId;                  // 当前 穿戴套装的Id
        public int nSetNum;                 // 套装数量
        public int nBigSet;                 // 套装扩展
        public int nGoldId;                 // 黄金Id
        public int nIsPlatina;              // 是否白金装
        public byte nStackNum;               //叠放数量
        public byte nEnChance;               //强化
        public byte nPoint;                  //紫装用
        public KTime LimitTime;               //限时
        public int nIsSell;                 //是否可以买卖
        public int nPriceXu;                //金币
        public int nJiFen;                  //积分
        public int nIsTrade;                //是否可以交易
        public int nIsDrop;                 //是否可以丢弃
        public int nRongNum;                 //可溶属性数量
        public int nWengangPin;              //可溶纹钢品质
        public int nBinfujiazhi;             //兵富甲值
        public int nRongpiont;               //熔炼用
        public int nIsBang;                  //是否绑定
        public int nIsKuaiJie;               //是否快捷栏
        public int nSkillType;               //技能类型
        public int nISMagic;                 //是否带属性
        public int nMagicID;                 //纹钢的技能ID
        public int nIsUse;                   //是否捡到立即使用
        public int nIsWhere;                 //是第几行
        public int nChiBangRes;              //翅膀的外观序号
        public int nParticularTypea;         //类型2
        public int nUseMap;
        public int nSixSkill;
        public int nTenSkill;
        public int nRes;                     //改变的装备的外观
        public int nUseKind;				  //使用的类型
        public string ItemDataId; // used for sync with DB
        public int SetIdx = -1;
    }

    public struct KTime
    {
        public byte bYear;
        public byte bMonth;
        public byte bDay;
        public byte bHour;
        public byte bMin;
    };

    public class ItemObj
    {
        public ItemCommonAttr m_CommonAttrib;
        public KMagicAttrib[] m_aryBaseAttrib = new KMagicAttrib[7];
        public KMagicAttrib[] m_aryRequireAttrib = new KMagicAttrib[6];
        public KMagicAttrib[] m_aryMagicAttribute = new KMagicAttrib[6];
        public KMagicAttrib[] m_ronMagicAttribute = new KMagicAttrib[6];
        public KMagicAttrib[] m_yinMagicAttribute = new KMagicAttrib[2];
        public KMagicAttrib[] m_TempPlatinaAttrib = new KMagicAttrib[6];
        public KMagicAttrib[] m_TempMagicAttribute = new KMagicAttrib[6];
        public KMagicAttrib[] m_TempRMagicAttribute = new KMagicAttrib[6];

        public ItemGeneratorParam m_GeneratorParam = new ItemGeneratorParam();
        public int m_nCurrentDurc = 0;
        public ItemObj() { }
        internal void InitMemb()
        {
            Utils.InitArr(m_aryBaseAttrib);
            Utils.InitArr(m_aryRequireAttrib);
            Utils.InitArr(m_aryMagicAttribute);
            Utils.InitArr(m_ronMagicAttribute);
            Utils.InitArr(m_yinMagicAttribute);
            Utils.InitArr(m_TempPlatinaAttrib);
            Utils.InitArr(m_TempMagicAttribute);
            Utils.InitArr(m_TempRMagicAttribute);
        }
        public ItemObj(BasicPropEquiment data)
        {
            SetCommonAttrib(data);
            InitMemb();
        }
        protected void SetCommonAttrib(BasicPropEquiment data)
        {
            m_CommonAttrib = new ItemCommonAttr()
            {
                nItemGenre = data.ItemGenre,
                nDetailType = data.DetailType,
                nParticularType = data.ParticularType,
                nParticularTypea = data.ParticularTypea,
                nObjIdx = data.ObjIdx,
                bStack = false,
                nWidth = data.Width,
                nHeight = data.Height,
                nPrice = data.Price,
                nPriceXu = data.PriceXu,
                nLevel = data.Level,
                nSeries = data.Series,
                nUseMap = 0,
                nUseKind = 0,
                nRes = 0,
                nSet = 0,
                nSetId = 0,
                nSetNum = 0,
                nBigSet = 0,
                nSixSkill = 0,
                nTenSkill = 0,
                nGoldId = 0,
                nIsPlatina = 0,
                nStackNum = 1,
                nEnChance = 0,
                nPoint = 0,
                nIsWhere = 0,
                nRongNum = data.RongNum,
                nWengangPin = data.WengangPin,
                nBinfujiazhi = data.Binfujiazhi,
                nChiBangRes = 0,
                nRongpiont = 0,
                nIsBang = 0,
                nIsKuaiJie = 0,
                nISMagic = 0,
                nSkillType = 0,
                nMagicID = 0,
                nIsUse = 0,
                nLianjieFlg = 0,
                nCanUse = 1, // �Ƿ����ʹ��
                nSellModel = 0,
                szItemName = data.Name,
                szScript = null,
                szWonName = "",
                LimitTime = new KTime()
                {
                    bYear = 0,
                    bMonth = 0,
                    bDay = 0,
                    bHour = 0,
                    bMin = 0,
                },
                ItmeInfo = data.Intro
            };

        }
        protected void SetCommonAttrib(BasicPropEquimentGold data)
        {
            m_CommonAttrib = new ItemCommonAttr()
            {
                nItemGenre = data.ItemGenre,
                nDetailType = data.DetailType,
                nParticularType = data.ParticularType,
                nParticularTypea = 0,
                nObjIdx = data.ObjIdx,
                bStack = false,
                nWidth = data.Width,
                nHeight = data.Height,
                nPrice = data.Price,
                nPriceXu = 0,
                nLevel = data.Level,
                nSeries = data.Series,
                nUseMap = 0,
                nUseKind = 0,
                nRes = 0,
                nSet = 0,
                nSetId = 0,
                nSetNum = 0,
                nBigSet = 0,
                nSixSkill = 0,
                nTenSkill = 0,
                nGoldId = 0,
                nIsPlatina = 0,
                nStackNum = 1,
                nEnChance = 0,
                nPoint = 0,
                nIsWhere = 0,
                nRongNum = data.RongNum,
                nWengangPin = data.WengangPin,
                nBinfujiazhi = data.Binfujiazhi,
                nChiBangRes = 0,
                nRongpiont = 0,
                nIsBang = 0,
                nIsKuaiJie = 0,
                nISMagic = 0,
                nSkillType = 0,
                nMagicID = 0,
                nIsUse = 0,
                nLianjieFlg = 0,
                nCanUse = 1, // �Ƿ����ʹ��
                nSellModel = 0,
                szItemName = data.Name,
                szScript = null,
                szWonName = "",
                LimitTime = new KTime()
                {
                    bYear = 0,
                    bMonth = 0,
                    bDay = 0,
                    bHour = 0,
                    bMin = 0,
                },
                ItmeInfo = data.Intro
            };

        }
        public ItemObj(BasicPropMaterial data) { }
        public ItemObj(BasicPropMine data) { }
        public ItemObj(BasicPropFusion data) { }
        public ItemObj(BasicPropQuest data) { }
        public ItemObj(BasicPropTownPortal data) { }
        public ItemObj(BasicPropMedicine sData)
        {
            ItemCommonAttr pCA = m_CommonAttrib;
            pCA.nItemGenre = sData.ItemGenre;
            pCA.nDetailType = sData.DetailType;
            pCA.nParticularType = sData.ParticularType;
            pCA.nObjIdx = sData.ObjIdx;
            //pCA.bStack = sData.m_Stack;
            pCA.nWidth = sData.Width;
            pCA.nHeight = sData.Height;
            pCA.nPrice = sData.Price;
            //pCA.nPriceXu = sData.PriceXu;
            pCA.nLevel = sData.Level;
            pCA.nSeries = sData.Series;
            pCA.nUseMap = sData.UseMap;
            pCA.nUseKind = 0;
            pCA.nRes = 0;
            pCA.nSixSkill = 0;
            pCA.nTenSkill = 0;
            pCA.nSet = 0;
            pCA.nSetId = 0;
            pCA.nSetNum = 0;
            pCA.nBigSet = 0;
            pCA.nGoldId = 0;
            pCA.nIsPlatina = 0;
            pCA.nStackNum = 1;
            pCA.nEnChance = 0;
            pCA.nPoint = 0;
            pCA.nIsWhere = 0;

            pCA.nRongNum = 0;
            pCA.nWengangPin = 0;
            pCA.nBinfujiazhi = 0;
            pCA.nChiBangRes = 0;
            pCA.nRongpiont = 0;
            pCA.nIsBang = 0;
            pCA.nIsKuaiJie = 1;
            pCA.nISMagic = 0;
            pCA.nSkillType = 0;
            pCA.nMagicID = 0;
            pCA.nIsUse = 0;
            pCA.nLianjieFlg = 0;
            pCA.nSellModel = 0;
            pCA.szItemName = sData.Name;
            pCA.szScript = null;
            pCA.szWonName = "";
            pCA.LimitTime.bYear = 0;
            pCA.LimitTime.bMonth = 0;
            pCA.LimitTime.bDay = 0;
            pCA.LimitTime.bHour = 0;
            pCA.LimitTime.bMin = 0;
            pCA.ItmeInfo = sData.Intro;
            Utils.InitArr(m_aryBaseAttrib);
            KMagicAttrib[] pBA = m_aryBaseAttrib;
            pBA[0].nAttribType = (short)sData.Attrib0;
            pBA[0].nValue[0] = (short)sData.AttribValue0;
            pBA[0].nValue[1] = (short)sData.ValueTime0;

            pBA[1].nAttribType = (short)sData.Attrib1;
            pBA[1].nValue[0] = (short)sData.AttribValue1;
            pBA[1].nValue[1] = (short)sData.ValueTime1;

            pBA[2].nAttribType = (short)sData.Attrib2;
            pBA[2].nValue[0] = (short)sData.AttribValue2;
            pBA[2].nValue[1] = (short)sData.ValueTime2;

            pBA[3].nAttribType = (short)sData.Attrib3;
            pBA[3].nValue[0] = (short)sData.AttribValue3;
            pBA[3].nValue[1] = (short)sData.ValueTime3;

            pBA[4].nAttribType = (short)sData.Attrib4;
            pBA[4].nValue[0] = (short)sData.AttribValue4;
            pBA[4].nValue[1] = (short)sData.ValueTime4;

            pBA[5].nAttribType = (short)sData.Attrib5;
            pBA[5].nValue[0] = (short)sData.AttribValue5;
            pBA[5].nValue[1] = (short)sData.ValueTime5;
            InitMemb();
        }
        public ItemObj(BasicPropEquipmentPlatina data) { }
        public ItemObj(BasicPropEquimentGold data) { }

        public void SetBasicProp(BasicPropMedicine sData)
        {
            ItemCommonAttr pCA = m_CommonAttrib;
            pCA.nItemGenre = sData.ItemGenre;
            pCA.nDetailType = sData.DetailType;
            pCA.nParticularType = sData.ParticularType;
            pCA.nObjIdx = sData.ObjIdx;
            //pCA.bStack = sData.m_Stack;
            pCA.nWidth = sData.Width;
            pCA.nHeight = sData.Height;
            pCA.nPrice = sData.Price;
            //pCA.nPriceXu = sData.PriceXu;
            pCA.nLevel = sData.Level;
            pCA.nSeries = sData.Series;
            pCA.nUseMap = sData.UseMap;
            pCA.nUseKind = 0;
            pCA.nRes = 0;
            pCA.nSixSkill = 0;
            pCA.nTenSkill = 0;
            pCA.nSet = 0;
            pCA.nSetId = 0;
            pCA.nSetNum = 0;
            pCA.nBigSet = 0;
            pCA.nGoldId = 0;
            pCA.nIsPlatina = 0;
            pCA.nStackNum = 1;
            pCA.nEnChance = 0;
            pCA.nPoint = 0;
            pCA.nIsWhere = 0;

            pCA.nRongNum = 0;
            pCA.nWengangPin = 0;
            pCA.nBinfujiazhi = 0;
            pCA.nChiBangRes = 0;
            pCA.nRongpiont = 0;
            pCA.nIsBang = 0;
            pCA.nIsKuaiJie = 1;
            pCA.nISMagic = 0;
            pCA.nSkillType = 0;
            pCA.nMagicID = 0;
            pCA.nIsUse = 0;
            pCA.nLianjieFlg = 0;
            pCA.nSellModel = 0;
            pCA.szItemName = sData.Name;
            pCA.szScript = null;
            pCA.szWonName = "";
            pCA.LimitTime.bYear = 0;
            pCA.LimitTime.bMonth = 0;
            pCA.LimitTime.bDay = 0;
            pCA.LimitTime.bHour = 0;
            pCA.LimitTime.bMin = 0;
            pCA.ItmeInfo = sData.Intro;
            Utils.InitArr(m_aryBaseAttrib);
            KMagicAttrib[] pBA = m_aryBaseAttrib;
            pBA[0].nAttribType = (short)sData.Attrib0;
            pBA[0].nValue[0] = (short)sData.AttribValue0;
            pBA[0].nValue[1] = (short)sData.ValueTime0;

            pBA[1].nAttribType = (short)sData.Attrib1;
            pBA[1].nValue[0] = (short)sData.AttribValue1;
            pBA[1].nValue[1] = (short)sData.ValueTime1;

            pBA[2].nAttribType = (short)sData.Attrib2;
            pBA[2].nValue[0] = (short)sData.AttribValue2;
            pBA[2].nValue[1] = (short)sData.ValueTime2;

            pBA[3].nAttribType = (short)sData.Attrib3;
            pBA[3].nValue[0] = (short)sData.AttribValue3;
            pBA[3].nValue[1] = (short)sData.ValueTime3;

            pBA[4].nAttribType = (short)sData.Attrib4;
            pBA[4].nValue[0] = (short)sData.AttribValue4;
            pBA[4].nValue[1] = (short)sData.ValueTime4;

            pBA[5].nAttribType = (short)sData.Attrib5;
            pBA[5].nValue[0] = (short)sData.AttribValue5;
            pBA[5].nValue[1] = (short)sData.ValueTime5;
            InitMemb();
        }
        public void SetBasicProp(BasicPropMine sData)
        {
            m_CommonAttrib = new ItemCommonAttr()
            {
                nItemGenre = sData.ItemGenre,
                nDetailType = sData.DetailType,
                nParticularType = sData.ParticularType,
                nObjIdx = sData.ObjIdx,
                bStack = true,
                nWidth = sData.Width,
                nHeight = sData.Height,
                nPrice = sData.Price,
                nPriceXu = sData.PriceXu,
                nLevel = sData.Level,
                nUseMap = 0,
                nUseKind = 0,
                nRes = 0,
                nSet = 0,
                nSetId = 0,
                nSetNum = 0,
                nSixSkill = 0,
                nTenSkill = 0,
                nBigSet = sData.Delet,
                nGoldId = 0,
                nIsPlatina = 0,
                nStackNum = 1,
                nEnChance = 0,
                nPoint = 0,
                nIsWhere = 0,
                nRongNum = 0,
                nWengangPin = 0,
                nBinfujiazhi = 0,
                nChiBangRes = 0,
                nRongpiont = 0,
                nIsBang = 0,
                nIsKuaiJie = sData.IsKuaiJie,
                nISMagic = 0,
                nMagicID = sData.Magic0,
                nIsUse = sData.IsUse,
                nLianjieFlg = 0,
                nSellModel = 0,
                szItemName = sData.Name,
                szScript = sData.Script,
                szWonName = "",
                LimitTime = new KTime()
                {
                    bYear = 0,
                    bMonth = 0,
                    bDay = 0,
                    bHour = 0,
                    bMin = 0
                },

            };
            InitMemb();
        }
        public void SetAttrib_CBR(BasicPropEquiment basicProp)
        {
            SetCommonAttrib(basicProp);
            InitMemb();
            SetAttrib_Base(basicProp.m_aryPropBasic);
            SetAttrib_Req(basicProp.m_aryPropReq);
        }
        internal void SetAttrib_CBR(BasicPropEquimentGold basicProp)
        {
            SetCommonAttrib(basicProp);
            InitMemb();
        }
        public void SetAttrib_Base(EquipmentCoreParamBasic[] pBasic)
        {
            for (int i = 0; i < m_aryBaseAttrib.Length; i++)
            {
                KMagicAttrib pDst = m_aryBaseAttrib[i];
                EquipmentCoreParamBasic pSrc = pBasic[i];
                pDst.nAttribType = pSrc.Type;
                pDst.nValue[0] = (short)KRandom.GetRandomNumber(pSrc.Range.Min, pSrc.Range.Max);
                pDst.nValue[1] = 0;
                pDst.nValue[2] = 0;
                if (pDst.nAttribType == (short)MagicAttributeType.magic_durability_v)
                {
                    SetDurability(pDst.nValue[0]);
                }
            }
            if (m_nCurrentDurc == 0)
            {
                m_nCurrentDurc = -1;
            }
        }
        public void SetAttrib_Req(EquipmentCoreParamRequirment[] pReq, int inDel = 0)
        {
            for (int i = 0; i < m_aryRequireAttrib.Length; ++i)
            {
                KMagicAttrib pDst = m_aryRequireAttrib[i];
                if (inDel > 0 && pReq[i].Type == 39)
                {
                    pDst.nAttribType = 0;
                    pDst.nValue[0] = 0;
                    pDst.nValue[1] = 0; // RESERVED
                    pDst.nValue[2] = 0; // RESERVED
                }
                else
                {
                    pDst.nAttribType = pReq[i].Type;
                    pDst.nValue[0] = pReq[i].Para;
                    pDst.nValue[1] = 0; // RESERVED
                    pDst.nValue[2] = 0; // RESERVED
                }
            }
        }
        public void SetSeries(byte nSeriesReq)
        {
            m_CommonAttrib.nSeries = nSeriesReq;
        }
        public void SetAttrib_MA(KMagicAttrib[] pMA)
        {
            for (int i = 0; i < m_aryMagicAttribute.Length; ++i)
            {
                m_aryMagicAttribute[i] = pMA[i];
                m_TempMagicAttribute[i] = pMA[i];
                if (m_aryMagicAttribute[i].nAttribType == (short)MagicAttributeType.magic_indestructible_b)
                {
                    SetDurability(-1);
                }
            }
        }
        public void SetAttrib_RON(KMagicAttrib[] pMA)
        {
            for (int i = 0; i < m_ronMagicAttribute.Length; ++i)
            {

                m_ronMagicAttribute[i] = pMA[i]; // �Ǽ��ܱ��  ��ֵ�����ܱ������
                m_TempRMagicAttribute[i] = pMA[i];
                // ����ĥ��
                if (m_ronMagicAttribute[i].nAttribType == (short)MagicAttributeType.magic_indestructible_b)
                {
                    SetDurability(-1);
                }
            }
        }
        public void SetRPoint(byte nRPoint)
        {
            m_CommonAttrib.nPoint = nRPoint;
        }
        public void SetIsWhere(int nIsWhere)
        {
            m_CommonAttrib.nIsWhere = nIsWhere;
        }

        public void SetDurability(int val)
        {
            m_nCurrentDurc = val;
        }
        public void EnChance(int nEnChance /*= 1*/)
        {
            /* int nCanEn = 0, i;

             if (nEnChance < 31)
             {
                 // int nOldEnChance = m_CommonAttrib.nEnChance;  //ԭ���ļӳ�

                 for (i = 0; i < 6; ++i)
                 {
                     nCanEn = CheckEnChance("ForBitEn", m_aryMagicAttribute[i].nAttributeType);

                     if (m_aryMagicAttribute[i].nAttributeType && nCanEn == 1)
                     {
                         if (m_aryMagicAttribute[i].nValue[0] >= (JIACHENG_VAL << 1))
                         {
                             int nTempVal = m_TempMagicAttribute[i].nValue[0]; // ԭʼֵ

                             for (int j = 0; j < nEnChance; ++j) // nEnChance+1
                             {
                                 nTempVal += nTempVal >> JIACHENG_VAL; //*j
                                                                       // if (nTempVal%JIACHENG_VAL>=2)
                                 if (nTempVal - (nTempVal >> JIACHENG_VAL << JIACHENG_VAL) > 2)
                                     nTempVal += 1;
                             }

                             m_aryMagicAttribute[i].nValue[0] = nTempVal;
                         }
                     }
                 }

                 m_CommonAttrib.nEnChance = nEnChance;
             }
             else
             {
                 m_CommonAttrib.nEnChance = 30;
             }*/
        }
        public void SetPoint(byte nPoint)
        {
            m_CommonAttrib.nPoint = nPoint;
        }
        public bool CheckEnChance(string nKey, int nAttributeType)
        {
            /*int nRow = g_ForbitMap.GetHeight() + 1, nReg = 0;

            for (int i = 2; i < nRow; ++i)
            {
                int nMaps = 0;

                g_ForbitMap.GetInteger(i, nKey, 0, &nMaps);

                if (nAttributeType == nMaps)
                {
                    nReg = 1;
                    break;
                }
            }
            return nReg;*/
            return true;
        }
        private ItemData _ItemData;
        public int SetIdx
        {
            get
            {
                return m_CommonAttrib.SetIdx;
            }
        }

        public ItemData ItemData
        {
            get
            {
                if (_ItemData != null) return _ItemData;
                ItemData item = new ItemData();
                SaveTo(item);
                _ItemData = item;
                //if (_ItemData.Id == "")
                //{
                //    _ItemData.Id = m_CommonAttrib.ItemDataId = ItemModule.Me.GenUniqueId;
                //}
                return item;
            }
        }
        public void SaveTo(ItemData itemData)
        {
            string ItemDataId = (m_CommonAttrib == null || m_CommonAttrib.ItemDataId == null) ? "" : m_CommonAttrib.ItemDataId;
            //itemData.Id = ItemDataId;
            //if (itemData.Id == "")
            //{
            //    itemData.Id = m_CommonAttrib.ItemDataId = ItemModule.Me.GenUniqueId;
            //}
            itemData.Equipclasscode = m_CommonAttrib.nItemGenre;
            itemData.Detailtype = m_CommonAttrib.nDetailType;
            itemData.Particulartype = m_CommonAttrib.nParticularType;
            itemData.Level = m_CommonAttrib.nLevel;
            itemData.Series = m_CommonAttrib.nSeries;

            itemData.Param1 = m_GeneratorParam.GeneratorLevel[0];
            itemData.Param2 = m_GeneratorParam.GeneratorLevel[1];
            itemData.Param3 = m_GeneratorParam.GeneratorLevel[2];
            itemData.Param4 = m_GeneratorParam.GeneratorLevel[3];
            itemData.Param5 = m_GeneratorParam.GeneratorLevel[4];
            itemData.Param6 = m_GeneratorParam.GeneratorLevel[5];

            itemData.Paramr1 = m_GeneratorParam.RGeneratorLevel[0];
            itemData.Paramr2 = m_GeneratorParam.RGeneratorLevel[1];
            itemData.Paramr3 = m_GeneratorParam.RGeneratorLevel[2];
            itemData.Paramr4 = m_GeneratorParam.RGeneratorLevel[3];
            itemData.Paramr5 = m_GeneratorParam.RGeneratorLevel[4];
            itemData.Paramr6 = m_GeneratorParam.RGeneratorLevel[5];

            itemData.Paramj1 = m_GeneratorParam.JGeneratorLevel[0];
            itemData.Paramj2 = m_GeneratorParam.JGeneratorLevel[1];
            itemData.Paramj3 = m_GeneratorParam.JGeneratorLevel[2];
            itemData.Paramj4 = m_GeneratorParam.JGeneratorLevel[3];
            itemData.Paramj5 = m_GeneratorParam.JGeneratorLevel[4];
            itemData.Paramj6 = m_GeneratorParam.JGeneratorLevel[5];
/*
            itemData.Luck = m_GeneratorParam.Luck;
            itemData.Durability = m_nCurrentDurc;
            itemData.Stack = m_CommonAttrib.nStackNum;
            itemData.Identify = 0;
            itemData.Goldid = m_CommonAttrib.nGoldId;
            itemData.Isplatina = m_CommonAttrib.nIsPlatina;
            itemData.RongNum = m_CommonAttrib.nRongNum;
            itemData.WengangPin = m_CommonAttrib.nWengangPin;
            itemData.Binfujiazhi = m_CommonAttrib.nBinfujiazhi;
            //pItemData->iRongpiont = Item[nItemIndex].IsRong();
            itemData.Enchance = m_CommonAttrib.nEnChance;
            itemData.Point = m_CommonAttrib.nPoint;
            itemData.Iswhere = m_CommonAttrib.nIsWhere;
            itemData.Year = m_CommonAttrib.LimitTime.bYear;
            itemData.Month = m_CommonAttrib.LimitTime.bMonth;
            itemData.Day = m_CommonAttrib.LimitTime.bDay;
            itemData.Hour = m_CommonAttrib.LimitTime.bHour;
            itemData.Min = m_CommonAttrib.LimitTime.bMin;
            itemData.IsBang = m_CommonAttrib.nIsBang;
            itemData.IsKuaiJie = m_CommonAttrib.nIsKuaiJie;
            itemData.IsMagic = m_CommonAttrib.nISMagic;
            itemData.SkillType = m_CommonAttrib.nSkillType;
            
            // Magics
            itemData.Magics.Clear();
            foreach (var item in m_aryMagicAttribute)
            {
                itemData.Magics.Add(new MagicAttribute()
                {
                    AttributeType = item.AttributeType,
                    Value0 = item.Value0,
                    Value1 = item.Value1,
                    Value2 = item.Value2,
                    AttributeIndex = item.AttributeIndex,
                });
            }
*/
        }

        public void LoadFrom(ItemData itemData)
        {
            if (m_CommonAttrib == null) m_CommonAttrib = new ItemCommonAttr();
            InitMemb();
            _ItemData = itemData;
            //物件基本信息**************************************************************
            //m_CommonAttrib.ItemDataId = itemData.Id;
            m_CommonAttrib.nItemGenre = itemData.Equipclasscode;
            m_CommonAttrib.nDetailType = itemData.Detailtype;
            m_CommonAttrib.nParticularType = itemData.Particulartype;
            m_CommonAttrib.nLevel = itemData.Level;
            m_CommonAttrib.nSeries = itemData.Series;
            m_CommonAttrib.nStackNum = itemData.Stack;
            m_CommonAttrib.nEnChance = itemData.Enchance;  //强化点数
            m_CommonAttrib.nPoint = itemData.Point;
//            m_CommonAttrib.nIsWhere = itemData.Iswhere;    //是辨识的了
            m_GeneratorParam.Luck = itemData.Lucky;

            //m_CommonAttrib.nRongpiont =itemData.iRongpiont;  //熔炼
            int nItemX = itemData.X;
            int nItemY = itemData.Y;
            int nLocal = itemData.Local;     //位置
            m_GeneratorParam.GeneratorLevel[0] = itemData.Param1;
            m_GeneratorParam.GeneratorLevel[1] = itemData.Param2;
            m_GeneratorParam.GeneratorLevel[2] = itemData.Param3;
            m_GeneratorParam.GeneratorLevel[3] = itemData.Param4;
            m_GeneratorParam.GeneratorLevel[4] = itemData.Param5;
            m_GeneratorParam.GeneratorLevel[5] = itemData.Param6;

            m_GeneratorParam.RGeneratorLevel[0] = itemData.Paramr1;
            m_GeneratorParam.RGeneratorLevel[1] = itemData.Paramr2;
            m_GeneratorParam.RGeneratorLevel[2] = itemData.Paramr3;
            m_GeneratorParam.RGeneratorLevel[3] = itemData.Paramr4;
            m_GeneratorParam.RGeneratorLevel[4] = itemData.Paramr5;
            m_GeneratorParam.RGeneratorLevel[5] = itemData.Paramr6;

            m_GeneratorParam.JGeneratorLevel[0] = itemData.Paramj1;
            m_GeneratorParam.JGeneratorLevel[1] = itemData.Paramj2;
            m_GeneratorParam.JGeneratorLevel[2] = itemData.Paramj3;
            m_GeneratorParam.JGeneratorLevel[3] = itemData.Paramj4;
            m_GeneratorParam.JGeneratorLevel[4] = itemData.Paramj5;
            m_GeneratorParam.JGeneratorLevel[5] = itemData.Paramj6;

            //m_GeneratorParam.Version = itemData.Equipversion;
            //m_GeneratorParam.RandomSeed = itemData.Randseed;    //随机种子
/*
            m_CommonAttrib.nGoldId = itemData.Goldid;      //黄金ID
            m_CommonAttrib.nIsPlatina = itemData.Isplatina;    //是否白金

            m_CommonAttrib.nRongNum = itemData.RongNum;     //可溶属性数量
            m_CommonAttrib.nWengangPin = itemData.WengangPin;  //纹纲的品质
            m_CommonAttrib.nBinfujiazhi = itemData.Binfujiazhi;
            m_CommonAttrib.nIsBang = itemData.IsBang;      //是否绑定
            m_CommonAttrib.nIsKuaiJie = itemData.IsKuaiJie;   //是否快捷栏
            m_CommonAttrib.nISMagic = itemData.IsMagic;     //是否魔法属性
            m_CommonAttrib.nSkillType = itemData.SkillType;
            m_CommonAttrib.nUseMap = itemData.Paramb1;       //专用地图
            m_CommonAttrib.nRes = itemData.Paramb2;      //改变的装备外观
            m_CommonAttrib.nChiBangRes = itemData.Paramb3;       //翅膀
            if (itemData.Paramb4 < 0)
                itemData.Paramb4 = 0;
            if (itemData.Paramb4 > 1)
                itemData.Paramb4 = 1;
            m_CommonAttrib.nUseKind = itemData.Paramb4;      //使用类型
            //m_CommonAttrib.szWonName = itemData.iWonName
*/
            m_aryMagicAttribute = itemData.Magics.ToArray();
        }

        internal void SetBasicProp(BasicPropTownPortal sData)
        {
            m_CommonAttrib = new ItemCommonAttr();
            m_CommonAttrib.nItemGenre = sData.ItemGenre;
            m_CommonAttrib.nDetailType = 0;
            m_CommonAttrib.nParticularType = 0;
            m_CommonAttrib.nObjIdx = sData.ObjIdx;
            m_CommonAttrib.bStack = false;
            m_CommonAttrib.nWidth = sData.Width;
            m_CommonAttrib.nHeight = sData.Height;
            m_CommonAttrib.nPrice = sData.Price;
            m_CommonAttrib.nUseMap = 0;
            m_CommonAttrib.nUseKind = 0;
            m_CommonAttrib.nRes = 0;
            m_CommonAttrib.nPriceXu = 0;
            m_CommonAttrib.nLevel = 1;
            m_CommonAttrib.nSeries = 0;
            m_CommonAttrib.nSet = 0;
            m_CommonAttrib.nSetId = 0;
            m_CommonAttrib.nSetNum = 0;
            m_CommonAttrib.nBigSet = 0;
            m_CommonAttrib.nGoldId = 0;
            m_CommonAttrib.nIsPlatina = 0;
            m_CommonAttrib.nStackNum = 1;
            m_CommonAttrib.nEnChance = 0;
            m_CommonAttrib.nPoint = 0;
            m_CommonAttrib.nIsWhere = 0;
            m_CommonAttrib.nRongNum = 0;
            m_CommonAttrib.nWengangPin = 0;
            m_CommonAttrib.nBinfujiazhi = 0;
            m_CommonAttrib.nChiBangRes = 0;
            m_CommonAttrib.nRongpiont = 0;
            m_CommonAttrib.nIsBang = 0;
            m_CommonAttrib.nIsKuaiJie = 1;
            m_CommonAttrib.nISMagic = 0;
            m_CommonAttrib.nSkillType = 0;
            m_CommonAttrib.nMagicID = 0;
            m_CommonAttrib.nIsUse = 0;
            m_CommonAttrib.nLianjieFlg = 0;
            m_CommonAttrib.nSellModel = 0;
            m_CommonAttrib.szItemName = sData.Name;
            m_CommonAttrib.szScript = null;
            m_CommonAttrib.szWonName = "";
            m_CommonAttrib.LimitTime.bYear = 0;
            m_CommonAttrib.LimitTime.bMonth = 0;
            m_CommonAttrib.LimitTime.bDay = 0;
            m_CommonAttrib.LimitTime.bHour = 0;
            m_CommonAttrib.LimitTime.bMin = 0;
            m_CommonAttrib.ItmeInfo = sData.Intro;
            InitMemb();
        }

        internal void SetPerXu(int nFsxu)
        {
            m_CommonAttrib.nPriceXu = nFsxu;
        }

        internal void SetPerPrice(int nPrice)
        {
            m_CommonAttrib.nPriceXu = nPrice;
        }

        internal void SetBang(int nIsBang)
        {
            m_CommonAttrib.nIsBang = nIsBang;
        }

        internal void SetStackNum(byte nStackNum)
        {
            m_CommonAttrib.nStackNum = nStackNum;
        }

        internal void SetLevel(byte nLevel)
        {
            m_CommonAttrib.nLevel = nLevel;
        }
        public int GetGenre() => m_CommonAttrib.nItemGenre;
        public int GetDetailType() => m_CommonAttrib.nDetailType;
        public int GetParticular() => m_CommonAttrib.nParticularType;
        public bool GetIsBang() => m_CommonAttrib.nIsBang != 0;
        public KTime GetTime() => m_CommonAttrib.LimitTime;
        public int GetSeries() => m_CommonAttrib.nSeries;
        public int GetLevel() => m_CommonAttrib.nLevel;
        public bool CanStack() => m_CommonAttrib.bStack;

        internal void SetGoldId(int nIndex)
        {
            m_CommonAttrib.nGoldId = nIndex;    
        }
        public int GetGoldId() => m_CommonAttrib.nGoldId;

        public byte ColorName()
        {
/*
            if (m_CommonAttrib.nGoldId > 0)
            { 
                if (m_aryMagicAttribute.FirstOrDefault(o => o.nAttribType > 0) == null)
                    return 0;
                else
                    return 3;
            }

            if ((0 < m_CommonAttrib.nPoint) && (m_CommonAttrib.nPoint <= 6))
                return 4;

            if (m_aryMagicAttribute.FirstOrDefault(o => o.nAttribType > 0) != null)
                return 1;

            if ((ItemGenre)m_CommonAttrib.nItemGenre == ItemGenre.item_medicine)
                return 8;

            if ((ItemGenre)m_CommonAttrib.nItemGenre == ItemGenre.item_task)
                return 8;

            if ((ItemGenre)m_CommonAttrib.nItemGenre == ItemGenre.item_mine)
                return 8;

            if ((ItemGenre)m_CommonAttrib.nItemGenre == ItemGenre.item_fusion)
                return 3;
*/
            return 0;
        }
    }
}
