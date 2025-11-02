
using System;

namespace game.resource.settings.npcres
{
    public class Damage
    {
        private const int MAX_HIT_PERCENT = 95;
        private const int MIN_HIT_PERCENT = 5;
        private const int MAX_RESIST = 95;

        public enum DAMAGE_TYPE
        {
            damage_physics = 0,
            damage_fire,
            damage_cold,
            damage_light,
            damage_poison,
            damage_magic,
            damage_num,
        }

        private readonly npcres.Controller npcController;
        private readonly npcres.Datafield data;

        public Damage(npcres.Datafield data, Controller npcController)
        {
            this.data = data;
            this.npcController = npcController;
        }

        private bool IsPlayer()
        {
            return true;
        }

        private bool CheckHitTarget(int nAR, int nDf, int nIngore/* = 0*/)
        {
            int nDefense = nDf * (100 - nIngore) / 100;
            int nPercent = 0;
            //Question nAr+ nDefense  == 0 ,error!so.....,must modify
            if ((nAR + nDefense) == 0)
                nPercent = 50;
            else
                nPercent = nAR * 100 / (nAR + nDefense);

            if (nPercent > MAX_HIT_PERCENT)
                nPercent = MAX_HIT_PERCENT;

            if (nPercent < MIN_HIT_PERCENT)
                nPercent = MIN_HIT_PERCENT;

            bool bRet = settings.skill.Static.g_RandPercent(nPercent);
            return bRet;
        }

        public void CalcDamage(settings.npcres.Controller nAttacker, int nMin, int nMax, DAMAGE_TYPE nType, bool bIsMelee, bool bDoHurt = true, bool bReturn = false, bool bDeaDly = false)
        {
            if (this.data.m_Doing == Datafield.NPCCMD.do_death)
                return;

            if (nMin + nMax <= 0)
                return;

            if (bDeaDly)
            {
                this.data.m_CurrentLife -= this.data.m_CurrentLife / 10;
                return;
            }

            int nRes = 0;
            int nDamageRange = nMax - nMin;
            int nDamage = 0;
            if (nDamageRange < 0)
            {
                nDamage = nMax + settings.skill.Static.g_Random(-nDamageRange);
            }
            else
                nDamage = nMin + settings.skill.Static.g_Random(nDamageRange);

            if (nDamage < 0)
                return;

            switch (nType)
            {
                case DAMAGE_TYPE.damage_physics:
                    nRes = this.data.m_CurrentPhysicsResist;
                    if (nRes > this.data.m_CurrentPhysicsResistMax)
                    {
                        nRes = this.data.m_CurrentPhysicsResistMax;
                    }
                    if (nRes > MAX_RESIST)
                    {
                        nRes = MAX_RESIST;
                    }

                    this.data.m_PhysicsArmor.nValue[0] -= nDamage;
                    if (this.data.m_PhysicsArmor.nValue[0] < 0)
                    {
                        nDamage = -this.data.m_PhysicsArmor.nValue[0];
                        this.data.m_PhysicsArmor.nValue[0] = 0;
                        this.data.m_PhysicsArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    if (bIsMelee)
                    {
                        nMax = this.data.m_CurrentMeleeDmgRetPercent;
                    }
                    else
                    {
                        nMax = this.data.m_CurrentRangeDmgRetPercent;
                    }
                    break;
                case DAMAGE_TYPE.damage_cold:
                    nRes = this.data.m_CurrentColdResist;
                    if (nRes > this.data.m_CurrentColdResistMax)
                    {
                        nRes = this.data.m_CurrentColdResistMax;
                    }
                    if (nRes > MAX_RESIST)
                    {
                        nRes = MAX_RESIST;
                    }

                    this.data.m_ColdArmor.nValue[0] -= nDamage;

                    if (this.data.m_ColdArmor.nValue[0] < 0)
                    {
                        nDamage = -this.data.m_ColdArmor.nValue[0];
                        this.data.m_ColdArmor.nValue[0] = 0;
                        this.data.m_ColdArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    nMax = this.data.m_CurrentRangeDmgRetPercent;
                    break;
                case DAMAGE_TYPE.damage_fire:
                    nRes = this.data.m_CurrentFireResist;
                    if (nRes > this.data.m_CurrentFireResistMax)
                    {
                        nRes = this.data.m_CurrentFireResistMax;
                    }
                    if (nRes > MAX_RESIST)
                    {
                        nRes = MAX_RESIST;
                    }

                    this.data.m_FireArmor.nValue[0] -= nDamage;

                    if (this.data.m_FireArmor.nValue[0] < 0)
                    {
                        nDamage = -this.data.m_FireArmor.nValue[0];
                        this.data.m_FireArmor.nValue[0] = 0;
                        this.data.m_FireArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    nMax = this.data.m_CurrentRangeDmgRetPercent;
                    break;
                case DAMAGE_TYPE.damage_light:
                    nRes = this.data.m_CurrentLightResist;
                    if (nRes > this.data.m_CurrentLightResistMax)
                    {
                        nRes = this.data.m_CurrentLightResistMax;
                    }
                    if (nRes > MAX_RESIST)
                    {
                        nRes = MAX_RESIST;
                    }

                    this.data.m_LightArmor.nValue[0] -= nDamage;
                    if (this.data.m_LightArmor.nValue[0] < 0)
                    {
                        nDamage = -this.data.m_LightArmor.nValue[0];
                        this.data.m_LightArmor.nValue[0] = 0;
                        this.data.m_LightArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    nMax = this.data.m_CurrentRangeDmgRetPercent;
                    break;
                case DAMAGE_TYPE.damage_poison:
                    nRes = this.data.m_CurrentPoisonResist;
                    if (nRes > this.data.m_CurrentPoisonResistMax)
                    {
                        nRes = this.data.m_CurrentPoisonResistMax;
                    }
                    if (nRes > MAX_RESIST)
                    {
                        nRes = MAX_RESIST;
                    }

                    this.data.m_PoisonArmor.nValue[0] -= nDamage;
                    if (this.data.m_PoisonArmor.nValue[0] < 0)
                    {
                        nDamage = -this.data.m_PoisonArmor.nValue[0];
                        this.data.m_PoisonArmor.nValue[0] = 0;
                        this.data.m_PoisonArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    nMax = this.data.m_CurrentRangeDmgRetPercent;
                    this.data.m_nLastPoisonDamageIdx = nAttacker.map.npcIndex;
                    break;
                case DAMAGE_TYPE.damage_magic:
                    nRes = 0;
                    break;
                default:
                    nRes = 0;
                    break;
            }
            nDamage -= this.data.m_CurrentDamageReduce;

            if (nDamage < 0)
            {
                nDamage = 0;
            }

            if (nDamage == 0)
                return;



            if (nDamage < 0)
            {
                nDamage = 0;
                return;
            }
            nDamage = nDamage * (100 - nRes) / 100;

            if (this.data.m_ManaShield.nValue[0] != 0)
            {
                int nManaDamage = nDamage * this.data.m_ManaShield.nValue[0] / 100;
                this.data.m_CurrentMana -= nManaDamage;
                if (this.data.m_CurrentMana < 0)
                {
                    nDamage -= this.data.m_CurrentMana;
                    this.data.m_CurrentMana = 0;
                }
                else
                {
                    nDamage -= nManaDamage;
                }
            }
            if (nAttacker != null && bReturn == false)
            {
                if (bIsMelee)
                {
                    nMin = this.data.m_CurrentMeleeDmgRet;
                    nMin += nDamage * nMax / 100;
                    nAttacker.CalcDamage(this.npcController, nMin, nMin, npcres.Damage.DAMAGE_TYPE.damage_magic, false, false, true);
                }
                else
                {
                    nMin = this.data.m_CurrentRangeDmgRet;
                    nMin += nDamage * nMax / 100;
                    nAttacker.CalcDamage(this.npcController, nMin, nMin, npcres.Damage.DAMAGE_TYPE.damage_magic, false, false, true);
                }
            }

            if (this.data.m_Kind == Datafield.NPCKIND.kind_player && nAttacker.data.m_Kind == Datafield.NPCKIND.kind_player)
                nDamage = nDamage * /*NpcSet.m_nPKDamageRate*/ 20 / 100;
            this.data.m_nLastDamageIdx = nAttacker.map.npcIndex;
            //if (this.data.m_Kind != Data.NPCKIND.kind_player && nAttacker.data.m_Kind == Data.NPCKIND.kind_player && nAttacker != null)
            //    m_cDeathCalcExp.AddDamage(nAttacker.data.m_nPlayerIdx, (this.data.m_CurrentLife - nDamage > 0 ? nDamage : this.data.m_CurrentLife));
            this.data.m_CurrentLife -= nDamage;


            nAttacker.data.m_CurrentLife += (nDamage + this.data.m_CurrentLife) * nAttacker.data.m_CurrentLifeStolen / 100;
            if (nAttacker.data.m_CurrentLife > nAttacker.data.m_CurrentLifeMax)
                nAttacker.data.m_CurrentLife = nAttacker.data.m_CurrentLifeMax;
            nAttacker.data.m_CurrentMana += (nDamage + this.data.m_CurrentLife) * nAttacker.data.m_CurrentManaStolen / 100;
            if (nAttacker.data.m_CurrentMana > nAttacker.data.m_CurrentManaMax)
                nAttacker.data.m_CurrentMana = nAttacker.data.m_CurrentManaMax;
            nAttacker.data.m_CurrentStamina += (nDamage + this.data.m_CurrentLife) * nAttacker.data.m_CurrentStaminaStolen / 100;
            if (nAttacker.data.m_CurrentStamina > nAttacker.data.m_CurrentStaminaMax)
                nAttacker.data.m_CurrentStamina = nAttacker.data.m_CurrentStaminaMax;

            if (nDamage > 0)
            {
                this.npcController.AddStateReceivedAppendDamage(nDamage);

                this.data.m_CurrentMana += this.data.m_CurrentDamage2Mana * nDamage / 100;
                if (this.data.m_CurrentMana > this.data.m_CurrentManaMax)
                {
                    this.data.m_CurrentMana = this.data.m_CurrentManaMax;
                }
                //if (bDoHurt)
                //    DoHurt();
                this.data.m_Hide.nTime = 0;
            }
            if (this.data.m_CurrentLife < 0)
            {
                //int nMode = DeathCalcPKValue(nAttacker);
                //DoDeath(nMode);

                //if (this.data.m_Kind == Data.NPCKIND.kind_player)
                //    Player[m_nPlayerIdx].m_cPK.CloseAll();
            }
        }

        public bool ReceiveDamage(
            settings.npcres.Controller nLauncher,
            bool bIsMelee,
            settings.skill.SkillSettingData.KMagicAttrib[] pData,
            bool bUseAR, bool bDoHurt, int nEnChance)
        {
            if (this.data.m_Doing == Datafield.NPCCMD.do_death || this.data.m_Doing == Datafield.NPCCMD.do_revive)
                return false;

            if (nLauncher == null)
                return false;

            if (pData == null || pData.Length <= 0)
                return false;

            settings.skill.SkillSettingData.KMagicAttrib pTemp = null;
            int pTempIndex = 0;

            pTemp = pData[pTempIndex = 0];
            pTemp = pData[pTempIndex += 13];
            int Serises = 0;
            if (nLauncher.data.m_Series == 0)
            {
                if (this.data.m_Series == 1)
                    Serises = pTemp.nValue[0];
                else if (this.data.m_Series == 3)
                    Serises = -pTemp.nValue[0];
                else
                    Serises = 0;
            }
            else if (nLauncher.data.m_Series == 1)
            {
                if (this.data.m_Series == 4)
                    Serises = pTemp.nValue[0];
                else if (this.data.m_Series == 0)
                    Serises = -pTemp.nValue[0];
                else
                    Serises = 0;
            }
            else if (nLauncher.data.m_Series == 2)
            {
                if (this.data.m_Series == 3)
                    Serises = pTemp.nValue[0];
                else if (this.data.m_Series == 4)
                    Serises = -pTemp.nValue[0];
                else
                    Serises = 0;
            }
            else if (nLauncher.data.m_Series == 3)
            {
                if (this.data.m_Series == 0)
                    Serises = pTemp.nValue[0];
                else if (this.data.m_Series == 2)
                    Serises = -pTemp.nValue[0];
                else
                    Serises = 0;
            }
            else if (nLauncher.data.m_Series == 4)
            {
                if (this.data.m_Series == 2)
                    Serises = pTemp.nValue[0];
                else if (this.data.m_Series == 1)
                    Serises = -pTemp.nValue[0];
                else
                    Serises = 0;
            }
            pTemp = pData[pTempIndex -= 13];
            int nAr = pTemp.nValue[0];
            pTemp = pData[pTempIndex++];
            int nIgnoreAr = pTemp.nValue[0];
            pTemp = pData[pTempIndex++];

            if (bUseAR)
            {
                if (!CheckHitTarget(nAr, this.data.m_CurrentDefend, nIgnoreAr))
                {
                    return false;
                }
            }
            /*
                if (m_Doing != do_death)
                    DoHurt(m_HurtFrame);//Question ?*/
            int nLife = this.data.m_CurrentLife;
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_physics, bIsMelee, bDoHurt);
            pTemp = pData[pTempIndex++];
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_cold, bIsMelee, bDoHurt);
            if (this.data.m_FreezeState.nTime <= 0 && pTemp.nValue[0] > 0)
            {
                if (pTemp.nValue[1] < 1 && pTemp.nValue[0] > 0)
                {
                    pTemp.nValue[1] = 60;
                }
                if (this.data.m_CurrentFreezeTimeReducePercent > 216)
                    this.data.m_FreezeState.nTime = pTemp.nValue[1] * (100 - 90) / 100;
                else
                    this.data.m_FreezeState.nTime = 25 * (240 - this.data.m_CurrentFreezeTimeReducePercent) / 100;
            }
            pTemp = pData[pTempIndex++];
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_fire, bIsMelee, bDoHurt);
            pTemp = pData[pTempIndex++];
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_light, bIsMelee, bDoHurt);
            pTemp = pData[pTempIndex++];
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_poison, bIsMelee, bDoHurt);
            if (this.data.m_PoisonState.nTime <= 0 && pTemp.nValue[0] > 0)
            {
                this.data.m_PoisonState.nTime = pTemp.nValue[1];
                if (this.data.m_PoisonState.nValue[0] > 0)
                {
                    if (settings.skill.Static.GetRandomNumber(0, 100) < 1)
                        this.data.m_PoisonState.nValue[0] += pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                    else if (IsPlayer())
                        this.data.m_PoisonState.nValue[0] = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                    else if (this.data.m_PoisonState.nValue[0] < pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100)
                        this.data.m_PoisonState.nTime = 0;
                }
                else
                    this.data.m_PoisonState.nValue[0] = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                this.data.m_PoisonState.nValue[1] = pTemp.nValue[2];

                if (this.data.m_CurrentPoisonTimeReducePercent > 135)
                    this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (100 - 90) / 100;
                else
                    this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (150 - this.data.m_CurrentPoisonTimeReducePercent) / 150;
            }
            else if (pTemp.nValue[0] > 0 && this.data.m_PoisonState.nTime > 0)
            {
                int d1, d2, t1, t2, c1, c2;
                d1 = this.data.m_PoisonState.nValue[0];
                d2 = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                t1 = this.data.m_PoisonState.nTime;
                t2 = pTemp.nValue[1];
                c1 = this.data.m_PoisonState.nValue[1];
                c2 = pTemp.nValue[2];
                if (c1 > 0 && c2 > 0 && d1 > 0 && d2 > 0)
                {
                    this.data.m_PoisonState.nValue[0] = ((c1 + c2) * d1 / c1 + (c1 + c2) * d2 / c2) / 2;
                    this.data.m_PoisonState.nTime = (t1 * d1 * c2 + t2 * d2 * c1) / (d1 * c2 + d2 * c1);
                    this.data.m_PoisonState.nValue[1] = (c1 + c2) / 2;
                }

                if (this.data.m_CurrentPoisonTimeReducePercent > 135)
                    this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (100 - 90) / 100;
                else
                    this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (150 - this.data.m_CurrentPoisonTimeReducePercent) / 150;
            }
            pTemp = pData[pTempIndex++];
            CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_magic, bIsMelee, bDoHurt);
            pTemp = pData[pTempIndex++];
            if (settings.skill.Static.GetRandomNumber(0, 100) < pTemp.nValue[0])
            {
                if (pTemp.nValue[1] <= 0)
                    pTemp.nValue[1] = 20;

                if (this.data.m_CurrentStunTimeReducePercent > 180)
                    this.data.m_StunState.nTime = pTemp.nValue[1] * (100 - 90) / 100;
                else
                    this.data.m_StunState.nTime = pTemp.nValue[1] * (200 - this.data.m_CurrentStunTimeReducePercent) / 200;
            }
            pTemp = pData[pTempIndex++];
            if (settings.skill.Static.GetRandomNumber(0, 100) < pTemp.nValue[0])
            {
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_magic, bIsMelee, bDoHurt/*,TRUE*/);
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_poison, bIsMelee, bDoHurt);
                if (this.data.m_PoisonState.nTime <= 0 && pTemp.nValue[0] > 0)
                {
                    this.data.m_PoisonState.nTime = pTemp.nValue[1];
                    if (this.data.m_PoisonState.nValue[0] > 0)
                    {
                        if (settings.skill.Static.GetRandomNumber(0, 100) < 1)
                            this.data.m_PoisonState.nValue[0] += pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                        else if (IsPlayer())
                            this.data.m_PoisonState.nValue[0] = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                        else if (this.data.m_PoisonState.nValue[0] < pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100)
                            this.data.m_PoisonState.nTime = 0;
                    }
                    else
                        this.data.m_PoisonState.nValue[0] = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;

                    this.data.m_PoisonState.nValue[1] = pTemp.nValue[2];

                    if (this.data.m_CurrentPoisonTimeReducePercent > 135)
                        this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (100 - 90) / 100;
                    else
                        this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (150 - this.data.m_CurrentPoisonTimeReducePercent) / 150;
                }
                else if (pTemp.nValue[0] > 0 && this.data.m_PoisonState.nTime > 0)
                {
                    int d1, d2, t1, t2, c1, c2;
                    d1 = this.data.m_PoisonState.nValue[0];
                    d2 = pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100;
                    t1 = this.data.m_PoisonState.nTime;
                    t2 = pTemp.nValue[1];
                    c1 = this.data.m_PoisonState.nValue[1];
                    c2 = pTemp.nValue[2];
                    if (c1 > 0 && c2 > 0 && d1 > 0 && d2 > 0)
                    {
                        this.data.m_PoisonState.nValue[0] = ((c1 + c2) * d1 / c1 + (c1 + c2) * d2 / c2) / 2;
                        this.data.m_PoisonState.nTime = (t1 * d1 * c2 + t2 * d2 * c1) / (d1 * c2 + d2 * c1);
                        this.data.m_PoisonState.nValue[1] = (c1 + c2) / 2;
                    }

                    if (this.data.m_CurrentPoisonTimeReducePercent > 135)
                        this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (100 - 90) / 100;
                    else
                        this.data.m_PoisonState.nTime = this.data.m_PoisonState.nTime * (150 - this.data.m_CurrentPoisonTimeReducePercent) / 150;
                }
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_light, bIsMelee, bDoHurt);
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_fire, bIsMelee, bDoHurt);
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_cold, bIsMelee, bDoHurt);
                pTemp = pData[pTempIndex--];
                CalcDamage(nLauncher, pTemp.nValue[0] + pTemp.nValue[0] * (nEnChance + Serises) / 100, pTemp.nValue[2] + pTemp.nValue[2] * (nEnChance + Serises) / 100, npcres.Damage.DAMAGE_TYPE.damage_physics, bIsMelee, bDoHurt);
                pTemp = pData[pTempIndex += 6];

            }
            pTemp = pData[pTempIndex++];
            if (settings.skill.Static.GetRandomNumber(0, 100) < pTemp.nValue[0])
            {
                CalcDamage(nLauncher, pTemp.nValue[0], pTemp.nValue[0], npcres.Damage.DAMAGE_TYPE.damage_magic, bIsMelee, bDoHurt, false, true);
            }
            pTemp = pData[pTempIndex++];
            if (pTemp.nValue[0] > 0 && nLife != 0)
            {
                nLauncher.data.m_CurrentLife += nLife * pTemp.nValue[0] / 100;
                if (nLauncher.data.m_CurrentLife > nLauncher.data.m_CurrentLifeMax)
                    nLauncher.data.m_CurrentLife = nLauncher.data.m_CurrentLifeMax;
            }
            pTemp = pData[pTempIndex++];
            if (pTemp.nValue[0] > 0 && nLife != 0)
            {
                nLauncher.data.m_CurrentMana += nLife * pTemp.nValue[0] / 100;
                if (nLauncher.data.m_CurrentMana > nLauncher.data.m_CurrentManaMax)
                    nLauncher.data.m_CurrentMana = nLauncher.data.m_CurrentManaMax;
            }
            pTemp = pData[pTempIndex++];
            pTemp = pData[pTempIndex++];

            if (pTemp.nValue[0] != 0 && pTemp.nValue[1] != 0 && settings.skill.Static.GetRandomNumber(1, 100) < pTemp.nValue[2])
            {
                if (this.data.m_LoseMana.nTime <= 0 && pTemp.nValue[0] == 355)
                {
                    //KSkill* pSkill = (KSkill*)g_SkillManager.GetSkill(pTemp.nValue[0], pTemp.nValue[1]);
                    //if (nLauncher != m_Index && nLauncher.data.m_CurrentCamp != m_CurrentCamp)
                    //{
                    //    pSkill->Cast(nLauncher, -1, m_Index);
                    //}
                }

            }

            //m_nPeopleIdx = nLauncher;

            //if (IsPlayer() && (this.data.m_CurrentLife - nLife < 0))
            //{
            //    if (m_nPlayerIdx > 0)
            //    {
            //        Player[m_nPlayerIdx].m_ItemList.Abrade(enumAbradeDefend);
            //    }
            //}

            return true;
        }
    }
}
