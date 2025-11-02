using ExitGames.Logging;
using Photon.JXGameServer.Entitys;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;

namespace Photon.JXGameServer.Skills
{
    public class KNpcAttribModify
    {
        public static KNpcAttribModify Me;

        System.Action<CharacterObj, KMagicAttrib>[] ProcessFunc;

        ILogger log;
        public KNpcAttribModify(ILogger log)
        {
            Me = this;
            this.log = log;
        }
        public void LoadConfig(string root)
        {
            ProcessFunc = new System.Action<CharacterObj, KMagicAttrib>[((int)MAGIC_ATTRIB.magic_normal_end)];

            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsresmax_p] = PhysicsResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_coldresmax_p] = ColdResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fireresmax_p] = FireResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingresmax_p] = LightingResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisonresmax_p] = PoisonResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_allresmax_p] = AllResMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifepotion_v] = LifePotionV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manapotion_v] = ManaPotionV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_meleedamagereturn_v] = MeleeDamageReturnV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_meleedamagereturn_p] = MeleeDamageReturnP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_rangedamagereturn_v] = RangeDamageReturnV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_rangedamagereturn_p] = RangeDamageReturnP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_damagetomana_p] = Damage2ManaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_adddefense_v] = ArmorDefenseV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisonenhance_p] = PoisonEnhanceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingenhance_p] = LightingEnhanceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fireenhance_p] = FireEnhanceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_coldenhance_p] = ColdEnhanceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_armordefense_v] = ArmorDefenseV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifemax_v] = LifeMaxV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifemax_p] = LifeMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_life_v] = LifeV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifereplenish_v] = LifeReplenishV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manamax_v] = ManaMaxV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manamax_p] = ManaMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_mana_v] = ManaV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manareplenish_v] = ManaReplenishV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_staminamax_v] = StaminaMaxV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_staminamax_p] = StaminaMaxP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stamina_v] = StaminaV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_staminareplenish_v] = StaminaReplenishV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_strength_v] = StrengthV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_dexterity_v] = DexterityV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_vitality_v] = VitalityV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_energy_v] = EnergyV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisonres_p] = PoisonresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fireres_p] = FireresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingres_p] = LightingresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsres_p] = PhysicsresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_coldres_p] = ColdresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_freezetimereduce_p] = FreezeTimeReduceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_burntimereduce_p] = BurnTimeReduceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisontimereduce_p] = PoisonTimeReduceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisondamagereduce_v] = PoisonDamageReduceV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stuntimereduce_p] = StunTimeReduceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fastwalkrun_p] = FastWalkRunP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_visionradius_p] = VisionRadiusP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fasthitrecover_v] = FastHitRecoverV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_allres_p] = AllresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackratingenhance_v] = AddAttackRatingV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackratingenhance_p] = AddAttackRatingP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackspeed_v] = AttackSpeedV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_castspeed_v] = CastSpeedV;

            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackrating_v] = AttackRatingV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackrating_p] = AttackRatingP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_ignoredefense_p] = Ignoredefense_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsdamage_v] = AddPhysicsMagic;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_colddamage_v] = AddColdMagic;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_firedamage_v] = AddFireMagic;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingdamage_v] = AddLightingMagic;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisondamage_v] = AddPoisonMagic;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsenhance_p] = Add_neiphysicsenhance_p;

            ProcessFunc[(int)MAGIC_ATTRIB.magic_addphysicsdamage_p] = AddPhysicsDamagePP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addphysicsdamage_v] = Addphysicsdamagevp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addfiredamage_v] = Aaddfiredamagevp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addcolddamage_v] = Addcolddamagevp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addpoisondamage_v] = Addpoisondamagevp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addlightingdamage_v] = Addlightingdamagevp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addphysicsmagic_v] = Addphysicsmagicvb;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addcoldmagic_v] = Addcoldmagicvp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addfiremagic_v] = Addfiremagicv;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addlightingmagic_v] = Addlightingmagicv;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addpoisonmagic_v] = Addpoisonmagicv;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addphysicsmagic_p] = Addphysicsmagicp;

            ProcessFunc[(int)MAGIC_ATTRIB.magic_slowmissle_b] = SlowMissleB;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_changecamp_b] = ChangeCampV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsarmor_v] = PhysicsArmorV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_coldarmor_v] = ColdArmorV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_firearmor_v] = FireArmorV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisonarmor_v] = PoisonArmorV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingarmor_v] = LightingArmorV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lucky_v] = LuckyV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_steallife_p] = StealLifeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_steallifeenhance_p] = StealLifeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stealstamina_p] = StealStaminaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stealstaminaenhance_p] = StealStaminaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stealmana_p] = StealManaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stealmanaenhance_p] = StealManaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_allskill_v] = AllSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_metalskill_v] = MetalSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_woodskill_v] = WoodSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_waterskill_v] = WaterSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fireskill_v] = FireSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_earthskill_v] = EarthSkillV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_knockback_p] = KnockBackP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_knockbackenhance_p] = KnockBackP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fatallystrike_p] = DeadlyStrikeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fatallystrikeenhance_p] = DeadlyStrikeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_deadlystrike_p] = DeadlyStrikeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_deadlystrikeenhance_p] = DeadlyStrikeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_badstatustimereduce_v] = BadStatusTimeReduceV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manashield_p] = ManaShieldP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fatallystrikeres_p] = fatallystrikeresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage1] = addskilldamage1;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage2] = addskilldamage2;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_expenhance_p] = expenhanceP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage3] = addskilldamage3;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage4] = addskilldamage4;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage5] = addskilldamage5;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage6] = addskilldamage6;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_dynamicmagicshield_v] = dynamicmagicshieldV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addstealfeatureskill] = addstealfeatureskill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifereplenish_p] = lifereplenishP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_ignoreskill_p] = ignoreskillP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisondamagereturn_v] = poisondamagereturnV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisondamagereturn_p] = poisondamagereturnP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_returnskill_p] = returnskillP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_autoreplyskill] = autoreplyskill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_skill_mintimepercast_v] = skill_mintimepercastV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_mintimepercastonhorse_v] = mintimepercastonhorseV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_poison2decmana_p] = poison2decmanaP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_skill_appendskill] = skill_appendskil;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_hide] = hide;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_clearnegativestate] = clearnegativestate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_returnres_p] = returnresP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_dec_percasttimehorse] = decPercasttimehorse;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_dec_percasttime] = decPercasttime;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_autoSkill] = enhance_autoSkill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_life_p] = enhance_lifeP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_life_v] = enhance_lifeV;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_711_auto] = enhance_711_auto;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_714_auto] = enhance_714_auto;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_717_auto] = enhance_717_auto;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhance_723_miss_p] = enhance_723_missP;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_seriesdamage_p] = SerisesDamage;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_autoattackskill] = autoskill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_block_rate] = block_rate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_enhancehit_rate] = enhancehit_rate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_block_rate] = anti_block_rate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_enhancehit_rate] = anti_enhancehit_rate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_sorbdamage_p] = sorbdamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_poisonres_p] = anti_poisonres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_fireres_p] = anti_fireres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_lightingres_p] = anti_lightingres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_physicsres_p] = anti_physicsres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_coldres_p] = anti_coldres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_not_add_pkvalue_p] = not_add_pkvalue_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_add_boss_damage] = add_boss_damage;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_five_elements_enhance_v] = five_elements_enhance_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_five_elements_resist_v] = five_elements_resist_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_skill_enhance] = skill_enhance;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_allres_p] = anti_allres_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_add_alldamage_p] = add_alldamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_auto_Revive_rate] = auto_Revive_rate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addcreatnpc_v] = addcreatnpc_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addzhuabu_v] = addzhuabu_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_reduceskillcd1] = reduceskillcd1;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_reduceskillcd2] = reduceskillcd2;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_reduceskillcd3] = reduceskillcd3;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_clearallcd] = clearallcd;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addblockrate] = addblockrate;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_walkrunshadow] = walkrunshadow;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_returnskill2enemy] = returnskill2enemy;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manatoskill_enhance] = manatoskill_enhance;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_add_alldamage_v] = add_alldamage_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage7] = addskilldamage7;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_ignoreattacrating_v] = ignoreattacrating_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_alljihuo_v] = alljihuo_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addexp_v] = addexp_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_doscript_v] = doscript_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_me2metaldamage_p] = me2metaldamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_metal2medamage_p] = metal2medamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_me2wooddamage_p] = me2wooddamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_wood2medamage_p] = wood2medamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_me2waterdamage_p] = me2waterdamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_water2medamage_p] = water2medamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_me2firedamage_p] = me2firedamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fire2medamage_p] = fire2medamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_me2earthdamage_p] = me2earthdamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_earth2medamage_p] = earth2medamage_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_manareplenish_p] = ManaReplenishp;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fasthitrecover_p] = fasthitrecover_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_stuntrank_p] = stuntrank_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_sorbdamage_v] = sorbdamage_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_creatstatus_v] = creatstatus_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_randmove] = randmove;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addbaopoisondmax_p] = addbaopoisondmax_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_dupotion_v] = dupotion_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_npcallattackSpeed_v] = npcallattackSpeed_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_eqaddskill_v] = eqaddskill_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_autodeathskill] = autodeathskill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_autorescueskill] = autorescueskill;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_staticmagicshield_p] = staticmagicshield_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_ignorenegativestate_p] = ignorenegativestate_p;

            ProcessFunc[(int)MAGIC_ATTRIB.magic_poisonres_yan_p] = poisonres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fireres_yan_p] = fireres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lightingres_yan_p] = lightingres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_physicsres_yan_p] = physicsres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_coldres_yan_p] = coldres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifemax_yan_v] = lifemax_yan_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_lifemax_yan_p] = lifemax_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_sorbdamage_yan_p] = sorbdamage_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_attackspeed_yan_v] = attackspeed_yan_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_castspeed_yan_v] = castspeed_yan_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_allres_yan_p] = allres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_fasthitrecover_yan_v] = fasthitrecover_yan_v;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_physicsres_yan_p] = anti_physicsres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_poisonres_yan_p] = anti_poisonres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_coldres_yan_p] = anti_coldres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_fireres_yan_p] = anti_fireres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_lightingres_yan_p] = anti_lightingres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_allres_yan_p] = anti_allres_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_sorbdamage_yan_p] = anti_sorbdamage_yan_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_hitrecover] = anti_hitrecover;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_do_hurt_p] = do_hurt_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskillexp1] = addskillexp1;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_poisontimereduce_p] = anti_poisontimereduce_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_anti_stuntimereduce_p] = anti_stuntimereduce_p;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage8] = addskilldamage8;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage9] = addskilldamage9;
            ProcessFunc[(int)MAGIC_ATTRIB.magic_addskilldamage10] = addskilldamage10;
        }
        void PhysicsResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ColdResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        } 
        void FireResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LightingResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AllResMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LifePotionV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaPotionV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void MeleeDamageReturnV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void MeleeDamageReturnP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void RangeDamageReturnV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void RangeDamageReturnP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Damage2ManaP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ArmorDefenseV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonEnhanceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LightingEnhanceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FireEnhanceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ColdEnhanceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LifeMaxV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LifeMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LifeV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LifeReplenishV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaMaxV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaReplenishV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StaminaMaxV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StaminaMaxP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StaminaV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StaminaReplenishV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StrengthV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void DexterityV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void VitalityV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void EnergyV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FireresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LightingresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PhysicsresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ColdresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FreezeTimeReduceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void BurnTimeReduceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonTimeReduceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonDamageReduceV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StunTimeReduceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FastWalkRunP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void VisionRadiusP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FastHitRecoverV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AllresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddAttackRatingV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddAttackRatingP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AttackSpeedV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void CastSpeedV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AttackRatingV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AttackRatingP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Ignoredefense_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddPhysicsMagic(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddColdMagic(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddFireMagic(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddLightingMagic(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddPoisonMagic(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Add_neiphysicsenhance_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AddPhysicsDamagePP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addphysicsdamagevp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Aaddfiredamagevp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addcolddamagevp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addpoisondamagevp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addlightingdamagevp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addphysicsmagicvb(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addcoldmagicvp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addfiremagicv(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addlightingmagicv(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addpoisonmagicv(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void Addphysicsmagicp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void SlowMissleB(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ChangeCampV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PhysicsArmorV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ColdArmorV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FireArmorV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void PoisonArmorV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LightingArmorV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void LuckyV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StealLifeP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StealStaminaP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void StealManaP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void AllSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void MetalSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void WoodSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void WaterSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void FireSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void EarthSkillV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void KnockBackP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void DeadlyStrikeP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void BadStatusTimeReduceV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaShieldP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void fatallystrikeresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage1(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage2(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage3(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage4(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage5(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage6(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void expenhanceP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void dynamicmagicshieldV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addstealfeatureskill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void lifereplenishP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ignoreskillP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void poisondamagereturnV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void poisondamagereturnP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void returnskillP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void autoreplyskill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void skill_mintimepercastV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void mintimepercastonhorseV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void poison2decmanaP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void skill_appendskil(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void hide(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void clearnegativestate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void returnresP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void decPercasttimehorse(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void decPercasttime(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_autoSkill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_lifeP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_lifeV(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_711_auto(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_714_auto(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_717_auto(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhance_723_missP(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void SerisesDamage(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void autoskill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void block_rate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void enhancehit_rate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_block_rate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_enhancehit_rate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void sorbdamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_poisonres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_fireres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_lightingres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_physicsres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_coldres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void not_add_pkvalue_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void add_boss_damage(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void five_elements_enhance_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void five_elements_resist_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void skill_enhance(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_allres_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void add_alldamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void auto_Revive_rate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addcreatnpc_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addzhuabu_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void reduceskillcd1(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void reduceskillcd2(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void reduceskillcd3(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void clearallcd(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addblockrate(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void walkrunshadow(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void returnskill2enemy(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void manatoskill_enhance(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void add_alldamage_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage7(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ignoreattacrating_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void alljihuo_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addexp_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void doscript_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void me2metaldamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void metal2medamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void me2wooddamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void wood2medamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void me2waterdamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void water2medamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void me2firedamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void fire2medamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void me2earthdamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void earth2medamage_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ManaReplenishp(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void fasthitrecover_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void stuntrank_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void sorbdamage_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void creatstatus_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void randmove(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addbaopoisondmax_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void dupotion_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void npcallattackSpeed_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void eqaddskill_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void autodeathskill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void autorescueskill(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void staticmagicshield_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void ignorenegativestate_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void poisonres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void fireres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void lightingres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void physicsres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void coldres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void lifemax_yan_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void lifemax_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void sorbdamage_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void attackspeed_yan_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void castspeed_yan_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void allres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void fasthitrecover_yan_v(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_physicsres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_poisonres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_coldres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_fireres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_lightingres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_allres_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_sorbdamage_yan_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_hitrecover(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void do_hurt_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskillexp1(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_poisontimereduce_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void anti_stuntimereduce_p(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage8(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage9(CharacterObj obj, KMagicAttrib attrib)
        {
        }
        void addskilldamage10(CharacterObj obj, KMagicAttrib attrib)
        {
        }

        public void ModifyAttrib(CharacterObj obj, KMagicAttrib pMagic)
        {
            if (pMagic.nAttribType < 0 || pMagic.nAttribType >= (short)MAGIC_ATTRIB.magic_normal_end || null == ProcessFunc[pMagic.nAttribType])
                return;

            ProcessFunc[pMagic.nAttribType](obj, pMagic);
        }
    }
}
