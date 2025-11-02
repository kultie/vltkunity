using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public static class Utils
    {
        public const string spstr = "<perfin>";
        public static T Clone<T>(this T val) where T : struct => val;

        static NPCRELATION GenOneRelation(NPCKIND Kind1, NPCKIND Kind2, NPCCAMP Camp1, NPCCAMP Camp2)
        {
            if (Kind1 == NPCKIND.kind_dialoger || Kind2 == NPCKIND.kind_dialoger)
                return NPCRELATION.relation_dialog;
            if ((Kind1 == NPCKIND.kind_partner || Kind1 == NPCKIND.kind_player) && Kind2 == NPCKIND.kind_normal && Camp1 != Camp2)
                return NPCRELATION.relation_enemy;
            if (Kind1 == NPCKIND.kind_normal && (Kind2 == NPCKIND.kind_partner || Kind2 == NPCKIND.kind_player) && Camp1 != Camp2)
                return NPCRELATION.relation_enemy;
            if ((Kind1 == NPCKIND.kind_normal && Kind2 == NPCKIND.kind_partner) || (Kind1 == NPCKIND.kind_partner && Kind2 == NPCKIND.kind_normal))
                return NPCRELATION.relation_enemy;
            if ((Kind1 == NPCKIND.kind_partner && Kind2 == NPCKIND.kind_partner) && Camp1 != Camp2)
                return NPCRELATION.relation_enemy;

            if (Camp1 == NPCCAMP.camp_event || Camp2 == NPCCAMP.camp_event)
                return NPCRELATION.relation_none;

            if ((Camp1 == NPCCAMP.camp_begin && Camp2 == NPCCAMP.camp_animal)
                || (Camp1 == NPCCAMP.camp_animal && Camp2 == NPCCAMP.camp_begin))
                return NPCRELATION.relation_enemy;

            if (Camp1 == NPCCAMP.camp_begin || Camp2 == NPCCAMP.camp_begin)
                return NPCRELATION.relation_ally;

            if (Kind1 == NPCKIND.kind_player && Kind2 == NPCKIND.kind_player)
            {
                if (Camp1 == NPCCAMP.camp_free || Camp2 == NPCCAMP.camp_free)
                {
                    return NPCRELATION.relation_enemy;
                }
            }

            if (Camp1 == Camp2)
                return NPCRELATION.relation_ally;

            return NPCRELATION.relation_enemy;
        }
        public static NPCRELATION GetRelation(ICharacterObj nId1, ICharacterObj nId2)
        {
            if (nId1 == nId2)
                return NPCRELATION.relation_self;

            if (nId1.Kind != NPCKIND.kind_player || nId2.Kind != NPCKIND.kind_player)
            {
                if (nId1.Kind == NPCKIND.kind_normal && nId2.Kind == NPCKIND.kind_normal)
                    return NPCRELATION.relation_ally;

                if (nId1.CurrentCamp == nId2.CurrentCamp && (nId1.CurrentCamp != NPCCAMP.camp_free || nId2.CurrentCamp != NPCCAMP.camp_free))
                {
                    return NPCRELATION.relation_ally;
                }

                if (nId1.Kind == NPCKIND.kind_partner || nId2.Kind == NPCKIND.kind_partner)
                {
                    var nstrNamea = nId1.MasterName;
                    var nstrNameb = nId2.MasterName;

                    if ((nId2.Kind == NPCKIND.kind_partner) && (nstrNameb == nId1.Name))
                        return NPCRELATION.relation_ally;

                    if ((nId2.Kind == NPCKIND.kind_partner) && (nstrNameb != nId1.Name))
                    {
                        var mNpcIdx = nId2.MasterObj;
                        if ((mNpcIdx != null) && (mNpcIdx.Kind == NPCKIND.kind_player) && (nId1.Kind == NPCKIND.kind_player))
                        {
                            if ((nId1.CurrentCamp == mNpcIdx.CurrentCamp) && (nId1.CurrentCamp != NPCCAMP.camp_free || mNpcIdx.CurrentCamp != NPCCAMP.camp_free))
                                return NPCRELATION.relation_ally;

                            if ((nId1.GetNormalPKState() != 0 || mNpcIdx.GetNormalPKState() != 0) && nId1.FightMode && mNpcIdx.FightMode)
                                return NPCRELATION.relation_enemy;

                            return NPCRELATION.relation_none;
                        }
                    }

                    if ((nId1.Kind == NPCKIND.kind_partner) && (nstrNamea == nId2.Name))
                        return NPCRELATION.relation_ally;

                    if ((nId1.Kind == NPCKIND.kind_partner) && (nstrNamea != nId2.Name))
                    {
                        var mNpcIdx = nId1.MasterObj;
                        if ((mNpcIdx != null) && (mNpcIdx.Kind == NPCKIND.kind_player) && (nId2.Kind == NPCKIND.kind_player))
                        {
                            if ((nId2.CurrentCamp == mNpcIdx.CurrentCamp) && (nId2.CurrentCamp != NPCCAMP.camp_free || mNpcIdx.CurrentCamp != NPCCAMP.camp_free))
                                return NPCRELATION.relation_ally;

                            if ((nId2.GetNormalPKState() != EnumPK.ENMITY_STATE_CLOSE || mNpcIdx.GetNormalPKState() != EnumPK.ENMITY_STATE_CLOSE) && nId2.FightMode && mNpcIdx.FightMode)
                                return NPCRELATION.relation_enemy;

                            return NPCRELATION.relation_none;
                        }
                    }

                    if ((nId1.Kind == NPCKIND.kind_normal && nId2.Kind == NPCKIND.kind_partner) || (nId1.Kind == NPCKIND.kind_partner && nId2.Kind == NPCKIND.kind_normal))
                        return NPCRELATION.relation_enemy;
                }

                if (nId1.Kind == NPCKIND.kind_player && !nId1.FightMode)
                    return NPCRELATION.relation_none;

                if (nId2.Kind == NPCKIND.kind_player && !nId2.FightMode)
                    return NPCRELATION.relation_none;

                return GenOneRelation(nId1.Kind, nId2.Kind, nId1.CurrentCamp, nId2.CurrentCamp);
            }
            else// P v P
            {
                if (!nId1.FightMode || !nId2.FightMode)
                    return NPCRELATION.relation_none;

                if (nId1.GetExercisePKAim() == nId2.Id)
                    return NPCRELATION.relation_enemy;

                if ((nId1.GetEnmityPKState() == EnumPK.ENMITY_STATE_PKING) && (nId1.GetEnmityPKAim() == nId2.Id))
                    return NPCRELATION.relation_enemy;

                if ((nId1.CurrentCamp == nId2.CurrentCamp) && (nId1.CurrentCamp != NPCCAMP.camp_free || nId2.CurrentCamp != NPCCAMP.camp_free))
                    return NPCRELATION.relation_ally;

                if ((nId1.GetNormalPKState() == EnumPK.ENMITY_STATE_PKING || nId2.GetNormalPKState() == EnumPK.ENMITY_STATE_PKING) && nId1.CurrentCamp != NPCCAMP.camp_begin && nId2.CurrentCamp != NPCCAMP.camp_begin)
                    return NPCRELATION.relation_enemy;

                if (nId1.GetNormalPKState() == 0 || nId2.GetNormalPKState() == 0)
                    return NPCRELATION.relation_none;

                var nRelation = GenOneRelation(nId1.Kind, nId2.Kind, nId1.CurrentCamp, nId2.CurrentCamp);

                if (nRelation == NPCRELATION.relation_enemy)
                {
                    if (nId1.GetNormalPKState() == EnumPK.ENMITY_STATE_CLOSE && nId2.GetNormalPKState() != EnumPK.ENMITY_STATE_PKING)
                        return NPCRELATION.relation_none;
                    if (nId2.GetNormalPKState() == EnumPK.ENMITY_STATE_CLOSE && nId1.GetNormalPKState() != EnumPK.ENMITY_STATE_PKING)
                        return NPCRELATION.relation_none;
                }

                return nRelation;
            }
        }

        public static void InitArr<T>(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == null)
                {
                    arr[i] = (T)Activator.CreateInstance(typeof(T));
                }
            }
        }

        public static NPCSERIES GetNPCSERIES(byte faction)
        {
            switch (faction)
            {
                case 0:
                case 1:
                    return NPCSERIES.series_metal;

                case 2:
                case 3:
                    return NPCSERIES.series_wood;

                case 4:
                case 5:
                    return NPCSERIES.series_water;

                case 6:
                case 7:
                    return NPCSERIES.series_fire;

                case 8:
                case 9:
                    return NPCSERIES.series_earth;
            }
            return NPCSERIES.series_num;
        }

        public static string SerializeBson<T>(T objectToSerialize) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BsonDataWriter writer = new BsonDataWriter(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, objectToSerialize);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static T DeserializeBson<T>(string objectToDeserialize) where T : class
        {
            byte[] data = Convert.FromBase64String(objectToDeserialize);

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BsonDataReader reader = new BsonDataReader(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(reader);
                }
            }
        }
    }
}
