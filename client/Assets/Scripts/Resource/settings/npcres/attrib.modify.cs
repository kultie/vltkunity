
namespace game.resource.settings.npcres
{
    public class AttribModify
    {
        private static System.Action<npcres.Controller, settings.skill.SkillSettingData.KMagicAttrib>[] ProcessFunc;

        ////////////////////////////////////////////////////////////////////////////////

        public static void Initialize()
        {
            if (AttribModify.ProcessFunc != null)
            {
                return;
            }

            //AttribModify.ProcessFunc = new System.Action<npcres.Controller, skill.SkillSettingData.KMagicAttrib>[((int)settings.skill.Defination.MAGIC_ATTRIB.magic_normal_end)];

            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsresmax_p] = AttribModify.PhysicsResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_coldresmax_p] = AttribModify.ColdResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fireresmax_p] = AttribModify.FireResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingresmax_p] = AttribModify.LightingResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisonresmax_p] = AttribModify.PoisonResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_allresmax_p] = AttribModify.AllResMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifepotion_v] = AttribModify.LifePotionV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manapotion_v] = AttribModify.ManaPotionV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_meleedamagereturn_v] = AttribModify.MeleeDamageReturnV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_meleedamagereturn_p] = AttribModify.MeleeDamageReturnP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_rangedamagereturn_v] = AttribModify.RangeDamageReturnV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_rangedamagereturn_p] = AttribModify.RangeDamageReturnP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_damagetomana_p] = AttribModify.Damage2ManaP;       
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_adddefense_v] = AttribModify.ArmorDefenseV;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisonenhance_p] = AttribModify.PoisonEnhanceP;    
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingenhance_p] = AttribModify.LightingEnhanceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fireenhance_p] = AttribModify.FireEnhanceP;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_coldenhance_p] = AttribModify.ColdEnhanceP;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_armordefense_v] = AttribModify.ArmorDefenseV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifemax_v] = AttribModify.LifeMaxV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifemax_p] = AttribModify.LifeMaxP;                
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_life_v] = AttribModify.LifeV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifereplenish_v] = AttribModify.LifeReplenishV;    
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manamax_v] = AttribModify.ManaMaxV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manamax_p] = AttribModify.ManaMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_mana_v] = AttribModify.ManaV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manareplenish_v] = AttribModify.ManaReplenishV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_staminamax_v] = AttribModify.StaminaMaxV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_staminamax_p] = AttribModify.StaminaMaxP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stamina_v] = AttribModify.StaminaV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_staminareplenish_v] = AttribModify.StaminaReplenishV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_strength_v] = AttribModify.StrengthV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_dexterity_v] = AttribModify.DexterityV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_vitality_v] = AttribModify.VitalityV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_energy_v] = AttribModify.EnergyV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisonres_p] = AttribModify.PoisonresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fireres_p] = AttribModify.FireresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingres_p] = AttribModify.LightingresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsres_p] = AttribModify.PhysicsresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_coldres_p] = AttribModify.ColdresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_freezetimereduce_p] = AttribModify.FreezeTimeReduceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_burntimereduce_p] = AttribModify.BurnTimeReduceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisontimereduce_p] = AttribModify.PoisonTimeReduceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisondamagereduce_v] = AttribModify.PoisonDamageReduceV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stuntimereduce_p] = AttribModify.StunTimeReduceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fastwalkrun_p] = AttribModify.FastWalkRunP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_visionradius_p] = AttribModify.VisionRadiusP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fasthitrecover_v] = AttribModify.FastHitRecoverV;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_allres_p] = AttribModify.AllresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackratingenhance_v] = AttribModify.AddAttackRatingV;  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackratingenhance_p] = AttribModify.AddAttackRatingP;  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackspeed_v] = AttribModify.AttackSpeedV;              
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_castspeed_v] = AttribModify.CastSpeedV;                  
                                                                                            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackrating_v] = AttribModify.AttackRatingV;            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackrating_p] = AttribModify.AttackRatingP;            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_ignoredefense_p] = AttribModify.Ignoredefense_p;         
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsdamage_v] = AttribModify.AddPhysicsMagic;         
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_colddamage_v] = AttribModify.AddColdMagic;               
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_firedamage_v] = AttribModify.AddFireMagic;               
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingdamage_v] = AttribModify.AddLightingMagic;       
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisondamage_v] = AttribModify.AddPoisonMagic;           
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsenhance_p] = AttribModify.Add_neiphysicsenhance_p;
                                                                                     
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addphysicsdamage_p] = AttribModify.AddPhysicsDamagePP;   
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addphysicsdamage_v] = AttribModify.Addphysicsdamagevp;   
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addfiredamage_v] = AttribModify.Aaddfiredamagevp;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addcolddamage_v] = AttribModify.Addcolddamagevp;         
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addpoisondamage_v] = AttribModify.Addpoisondamagevp;     
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addlightingdamage_v] = AttribModify.Addlightingdamagevp; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addphysicsmagic_v] = AttribModify.Addphysicsmagicvb;     
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addcoldmagic_v] = AttribModify.Addcoldmagicvp;           
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addfiremagic_v] = AttribModify.Addfiremagicv;            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addlightingmagic_v] = AttribModify.Addlightingmagicv;    
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addpoisonmagic_v] = AttribModify.Addpoisonmagicv;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addphysicsmagic_p] = AttribModify.Addphysicsmagicp;      
                                                                                             
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_slowmissle_b] = AttribModify.SlowMissleB;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_changecamp_b] = AttribModify.ChangeCampV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsarmor_v] = AttribModify.PhysicsArmorV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_coldarmor_v] = AttribModify.ColdArmorV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_firearmor_v] = AttribModify.FireArmorV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisonarmor_v] = AttribModify.PoisonArmorV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingarmor_v] = AttribModify.LightingArmorV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lucky_v] = AttribModify.LuckyV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_steallife_p] = AttribModify.StealLifeP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_steallifeenhance_p] = AttribModify.StealLifeP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stealstamina_p] = AttribModify.StealStaminaP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stealstaminaenhance_p] = AttribModify.StealStaminaP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stealmana_p] = AttribModify.StealManaP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stealmanaenhance_p] = AttribModify.StealManaP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_allskill_v] = AttribModify.AllSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_metalskill_v] = AttribModify.MetalSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_woodskill_v] = AttribModify.WoodSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_waterskill_v] = AttribModify.WaterSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fireskill_v] = AttribModify.FireSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_earthskill_v] = AttribModify.EarthSkillV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_knockback_p] = AttribModify.KnockBackP; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_knockbackenhance_p] = AttribModify.KnockBackP;  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fatallystrike_p] = AttribModify.DeadlyStrikeP;       
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fatallystrikeenhance_p] = AttribModify.DeadlyStrikeP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_deadlystrike_p] = AttribModify.DeadlyStrikeP;        
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_deadlystrikeenhance_p] = AttribModify.DeadlyStrikeP; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_badstatustimereduce_v] = AttribModify.BadStatusTimeReduceV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manashield_p] = AttribModify.ManaShieldP;         
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fatallystrikeres_p] = AttribModify.fatallystrikeresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage1] = AttribModify.addskilldamage1;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage2] = AttribModify.addskilldamage2;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_expenhance_p] = AttribModify.expenhanceP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage3] = AttribModify.addskilldamage3;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage4] = AttribModify.addskilldamage4;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage5] = AttribModify.addskilldamage5;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage6] = AttribModify.addskilldamage6;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_dynamicmagicshield_v] = AttribModify.dynamicmagicshieldV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addstealfeatureskill] = AttribModify.addstealfeatureskill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifereplenish_p] = AttribModify.lifereplenishP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_ignoreskill_p] = AttribModify.ignoreskillP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisondamagereturn_v] = AttribModify.poisondamagereturnV; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisondamagereturn_p] = AttribModify.poisondamagereturnP; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_returnskill_p] = AttribModify.returnskillP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_autoreplyskill] = AttribModify.autoreplyskill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_skill_mintimepercast_v] = AttribModify.skill_mintimepercastV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_mintimepercastonhorse_v] = AttribModify.mintimepercastonhorseV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poison2decmana_p] = AttribModify.poison2decmanaP;   
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_skill_appendskill] = AttribModify.skill_appendskil; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_hide] = AttribModify.hide;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_clearnegativestate] = AttribModify.clearnegativestate;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_returnres_p] = AttribModify.returnresP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_dec_percasttimehorse] = AttribModify.decPercasttimehorse;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_dec_percasttime] = AttribModify.decPercasttime;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_autoSkill] = AttribModify.enhance_autoSkill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_life_p] = AttribModify.enhance_lifeP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_life_v] = AttribModify.enhance_lifeV;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_711_auto] = AttribModify.enhance_711_auto;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_714_auto] = AttribModify.enhance_714_auto;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_717_auto] = AttribModify.enhance_717_auto;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhance_723_miss_p] = AttribModify.enhance_723_missP;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_seriesdamage_p] = AttribModify.SerisesDamage;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_autoattackskill] = AttribModify.autoskill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_block_rate] = AttribModify.block_rate;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_enhancehit_rate] = AttribModify.enhancehit_rate;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_block_rate] = AttribModify.anti_block_rate;                
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_enhancehit_rate] = AttribModify.anti_enhancehit_rate;      
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_sorbdamage_p] = AttribModify.sorbdamage_p;                      
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_poisonres_p] = AttribModify.anti_poisonres_p;              
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_fireres_p] = AttribModify.anti_fireres_p;                  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_lightingres_p] = AttribModify.anti_lightingres_p;          
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_physicsres_p] = AttribModify.anti_physicsres_p;            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_coldres_p] = AttribModify.anti_coldres_p;                  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_not_add_pkvalue_p] = AttribModify.not_add_pkvalue_p;            
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_add_boss_damage] = AttribModify.add_boss_damage;                
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_five_elements_enhance_v] = AttribModify.five_elements_enhance_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_five_elements_resist_v] = AttribModify.five_elements_resist_v;  
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_skill_enhance] = AttribModify.skill_enhance;                    
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_allres_p] = AttribModify.anti_allres_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_add_alldamage_p] = AttribModify.add_alldamage_p;                
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_auto_Revive_rate] = AttribModify.auto_Revive_rate;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addcreatnpc_v] = AttribModify.addcreatnpc_v; 
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addzhuabu_v] = AttribModify.addzhuabu_v;     
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_reduceskillcd1] = AttribModify.reduceskillcd1;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_reduceskillcd2] = AttribModify.reduceskillcd2;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_reduceskillcd3] = AttribModify.reduceskillcd3;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_clearallcd] = AttribModify.clearallcd;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addblockrate] = AttribModify.addblockrate;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_walkrunshadow] = AttribModify.walkrunshadow;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_returnskill2enemy] = AttribModify.returnskill2enemy;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manatoskill_enhance] = AttribModify.manatoskill_enhance;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_add_alldamage_v] = AttribModify.add_alldamage_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage7] = AttribModify.addskilldamage7;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_ignoreattacrating_v] = AttribModify.ignoreattacrating_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_alljihuo_v] = AttribModify.alljihuo_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addexp_v] = AttribModify.addexp_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_doscript_v] = AttribModify.doscript_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_me2metaldamage_p] = AttribModify.me2metaldamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_metal2medamage_p] = AttribModify.metal2medamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_me2wooddamage_p] = AttribModify.me2wooddamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_wood2medamage_p] = AttribModify.wood2medamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_me2waterdamage_p] = AttribModify.me2waterdamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_water2medamage_p] = AttribModify.water2medamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_me2firedamage_p] = AttribModify.me2firedamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fire2medamage_p] = AttribModify.fire2medamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_me2earthdamage_p] = AttribModify.me2earthdamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_earth2medamage_p] = AttribModify.earth2medamage_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_manareplenish_p] = AttribModify.ManaReplenishp;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fasthitrecover_p] = AttribModify.fasthitrecover_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_stuntrank_p] = AttribModify.stuntrank_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_sorbdamage_v] = AttribModify.sorbdamage_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_creatstatus_v] = AttribModify.creatstatus_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_randmove] = AttribModify.randmove;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addbaopoisondmax_p] = AttribModify.addbaopoisondmax_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_dupotion_v] = AttribModify.dupotion_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_npcallattackSpeed_v] = AttribModify.npcallattackSpeed_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_eqaddskill_v] = AttribModify.eqaddskill_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_autodeathskill] = AttribModify.autodeathskill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_autorescueskill] = AttribModify.autorescueskill;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_staticmagicshield_p] = AttribModify.staticmagicshield_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_ignorenegativestate_p] = AttribModify.ignorenegativestate_p;

            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_poisonres_yan_p] = AttribModify.poisonres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fireres_yan_p] = AttribModify.fireres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lightingres_yan_p] = AttribModify.lightingres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_physicsres_yan_p] = AttribModify.physicsres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_coldres_yan_p] = AttribModify.coldres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifemax_yan_v] = AttribModify.lifemax_yan_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_lifemax_yan_p] = AttribModify.lifemax_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_sorbdamage_yan_p] = AttribModify.sorbdamage_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_attackspeed_yan_v] = AttribModify.attackspeed_yan_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_castspeed_yan_v] = AttribModify.castspeed_yan_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_allres_yan_p] = AttribModify.allres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_fasthitrecover_yan_v] = AttribModify.fasthitrecover_yan_v;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_physicsres_yan_p] = AttribModify.anti_physicsres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_poisonres_yan_p] = AttribModify.anti_poisonres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_coldres_yan_p] = AttribModify.anti_coldres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_fireres_yan_p] = AttribModify.anti_fireres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_lightingres_yan_p] = AttribModify.anti_lightingres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_allres_yan_p] = AttribModify.anti_allres_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_sorbdamage_yan_p] = AttribModify.anti_sorbdamage_yan_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_hitrecover] = AttribModify.anti_hitrecover;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_do_hurt_p] = AttribModify.do_hurt_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskillexp1] = AttribModify.addskillexp1;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_poisontimereduce_p] = AttribModify.anti_poisontimereduce_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_anti_stuntimereduce_p] = AttribModify.anti_stuntimereduce_p;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage8] = AttribModify.addskilldamage8;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage9] = AttribModify.addskilldamage9;
            //AttribModify.ProcessFunc[(int)settings.skill.Defination.MAGIC_ATTRIB.magic_addskilldamage10] = AttribModify.addskilldamage10;
        }

        public static void ModifyAttrib(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        {
            //if (pMagic.nAttribType < 0 || pMagic.nAttribType >= (int)settings.skill.Defination.MAGIC_ATTRIB.magic_normal_end || null == ProcessFunc[pMagic.nAttribType])
            //    return;

            //AttribModify.ProcessFunc[pMagic.nAttribType](pNpc, pMagic);
        }

        //////////////////////////////////////////////////////////////////////////////////

        //public const int BASE_FANGYU_ALL_MAX = 75;
        //public const int MAX_RESIST = 150;
        //public const int MAX_NPCSKILL = 80;

        //////////////////////////////////////////////////////////////////////////////////

        //private static void AllresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;
        //    pNpc.data.m_TempFireResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempFireResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) % 100; ;

        //        if (pNpc.data.m_TempFireResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempFireResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_FireResist + nRes;
        //    else
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_FireResist - nRes;

        //    nRes = 0;
        //    nRer = 0;
        //    nIsadd = 0;
        //    pNpc.data.m_TempColdResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempColdResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempColdResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempColdResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_ColdResist + nRes;
        //    else
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_ColdResist - nRes;
        //    nRes = 0;
        //    nRer = 0;
        //    nIsadd = 0;
        //    pNpc.data.m_TempLightResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempLightResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempLightResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempLightResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_LightResist + nRes;
        //    else
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_LightResist - nRes;

        //    nRes = 0;
        //    nRer = 0;
        //    nIsadd = 0;
        //    pNpc.data.m_TempPhysicsResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempPhysicsResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempPhysicsResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempPhysicsResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_PhysicsResist + nRes;
        //    else
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_PhysicsResist - nRes;
        //    nRes = 0;
        //    nRer = 0;
        //    nIsadd = 0;
        //    pNpc.data.m_TempPoisonResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempPoisonResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempPoisonResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempPoisonResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_PoisonResist + nRes;
        //    else
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_PoisonResist - nRes;
        //    if (pNpc.data.m_CurrentFireResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentFireResistMax = pNpc.data.m_CurrentFireResist;

        //    if (pNpc.data.m_CurrentColdResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentColdResistMax = pNpc.data.m_CurrentColdResist;

        //    if (pNpc.data.m_CurrentLightResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentLightResistMax = pNpc.data.m_CurrentLightResist;

        //    if (pNpc.data.m_CurrentPoisonResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPoisonResistMax = pNpc.data.m_CurrentPoisonResist;

        //    if (pNpc.data.m_CurrentPhysicsResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPhysicsResistMax = pNpc.data.m_CurrentPhysicsResist;

        //    if (pNpc.data.m_CurrentPoisonResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentPoisonResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentLightResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentLightResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentFireResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentFireResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentColdResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentColdResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentPhysicsResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentPhysicsResistMax = MAX_RESIST;
        //    if (pNpc.data.m_CurrentPoisonResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPoisonResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentLightResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentLightResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentFireResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentFireResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentColdResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentColdResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentPhysicsResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPhysicsResistMax = BASE_FANGYU_ALL_MAX;
        //    if (pNpc.data.m_CurrentPoisonResist > pNpc.data.m_CurrentPoisonResistMax)
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_CurrentPoisonResistMax;
        //    if (pNpc.data.m_CurrentLightResist > pNpc.data.m_CurrentLightResistMax)
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_CurrentLightResistMax;
        //    if (pNpc.data.m_CurrentFireResist > pNpc.data.m_CurrentFireResistMax)
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_CurrentFireResistMax;
        //    if (pNpc.data.m_CurrentColdResist > pNpc.data.m_CurrentColdResistMax)
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_CurrentColdResistMax;
        //    if (pNpc.data.m_CurrentPhysicsResist > pNpc.data.m_CurrentPhysicsResistMax)
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_CurrentPhysicsResistMax;
        //}

        //private static void AllSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pMagic.nValue[2] != 0)
        //    {
        //        int nSkillid = 0;
        //        if (pMagic.nValue[2] > 0)

        //            nSkillid = pMagic.nValue[2];
        //        else
        //            nSkillid = -pMagic.nValue[2];
        //        {
        //            int nSkillIdx = pNpc.data.m_SkillList.FindSame(nSkillid);

        //            if (nSkillIdx != 0)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nSkillIdx) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(nSkillIdx, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[2] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void Ignoredefense_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentIgnoredefensep += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentIgnoredefensep <= 0)
        //        pNpc.data.m_CurrentIgnoredefensep = 0;

        //    if (pNpc.data.m_CurrentIgnoredefensep > 100)
        //        pNpc.data.m_CurrentIgnoredefensep = 100;
        //}

        //private static void AttackRatingP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAttackRating += pNpc.data.m_AttackRating * pMagic.nValue[0] / 100;
        //}

        //private static void AttackRatingV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAttackRating += pMagic.nValue[0];
        //}

        //private static void AddAttackRatingV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAttackRatingEnhancev += pMagic.nValue[0];
        //}

        //private static void AddAttackRatingP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAttackRatingEnhancep += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAttackRatingEnhancep < -100)
        //        pNpc.data.m_CurrentAttackRatingEnhancep = -100;
        //}

        //private static void lightingres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void fireres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void physicsres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void poisonres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void lifemax_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void lifemax_yan_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void coldres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void manamax_yan_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void manamax_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void sorbdamage_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void fastwalkrun_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void attackspeed_yan_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void castspeed_yan_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void allres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void fasthitrecover_yan_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_physicsres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_poisonres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_coldres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_fireres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_lightingres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_allres_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_sorbdamage_yan_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void anti_hitrecover(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentHitNpcRecover += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentHitNpcRecover < 0)
        //        pNpc.data.m_CurrentHitNpcRecover = 0;
        //}

        //private static void addskillexp1(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void do_hurt_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentHitRank += pMagic.nValue[0];
        //}

        //private static void ignorenegativestate_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void staticmagicshield_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Staticmagicshield_p += pMagic.nValue[0] / 100;
        //}

        //private static void autorescueskill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void autodeathskill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_Deathkill.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_Deathkill.nValue[1] = pMagic.nValue[2];
        //        pNpc.data.m_Deathkill.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_Deathkill.nValue[0] = 0;
        //        pNpc.data.m_Deathkill.nValue[1] = 0;
        //        pNpc.data.m_Deathkill.nTime = 0;
        //    }
        //}

        //private static void eqaddskill_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void npcallattackSpeed_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void AttackSpeedV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void CastSpeedV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void BadStatusTimeReduceV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_FreezeState.nTime > 0)
        //        pNpc.data.m_FreezeState.nTime += pMagic.nValue[0];
        //    if (pNpc.data.m_BurnState.nTime > 0)
        //        pNpc.data.m_BurnState.nTime += pMagic.nValue[0];
        //    if (pNpc.data.m_ConfuseState.nTime > 0)
        //        pNpc.data.m_ConfuseState.nTime += pMagic.nValue[0];
        //    if (pNpc.data.m_StunState.nTime > 0)
        //        pNpc.data.m_StunState.nTime += pMagic.nValue[0];
        //    return;
        //}

        //private static void BurnTimeReduceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentBurnTimeReducePercent += pMagic.nValue[0];
        //    return;
        //}

        //private static void ChangeCampV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_Kind != Datafield.NPCKIND.kind_player)
        //    {
        //        if (pMagic.nValue[0] > 0 && pMagic.nValue[0] < ((int)npcres.Datafield.NPCCAMP.camp_num))
        //            pNpc.data.m_CurrentCamp = pMagic.nValue[0];
        //        else
        //            pNpc.data.m_CurrentCamp = pNpc.data.m_Camp;
        //    }
        //}

        //private static void ColdArmorV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_ColdArmor.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_ColdArmor.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_ColdArmor.nValue[0] = 0;
        //        pNpc.data.m_ColdArmor.nTime = 0;
        //    }
        //}

        //private static void ColdresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;

        //    pNpc.data.m_TempColdResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempColdResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempColdResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempColdResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempColdResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_ColdResist + nRes;
        //    else
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_ColdResist - nRes;
        //    if (pNpc.data.m_CurrentColdResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentColdResistMax = pNpc.data.m_CurrentColdResist;

        //    if (pNpc.data.m_CurrentColdResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentColdResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentColdResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentColdResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentColdResist > pNpc.data.m_CurrentColdResistMax)
        //        pNpc.data.m_CurrentColdResist = pNpc.data.m_CurrentColdResistMax;
        //}

        //private static void DeadlyStrikeP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentDeadlyStrike += pMagic.nValue[0];
        //}

        //private static void DexterityV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_Kind != Datafield.NPCKIND.kind_player)
        //        return;

        //    pNpc.data.ChangeCurDexterity(pMagic.nValue[0]);
        //}

        //private static void EarthSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pNpc.data.m_Series == 4)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void EnergyV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_Kind != Datafield.NPCKIND.kind_player)
        //        return;

        //    pNpc.data.ChangeCurEngergy(pMagic.nValue[0]);
        //}

        //private static void FastHitRecoverV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentHitRecover += pMagic.nValue[0];
        //}

        //private static void stuntrank_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStunRank_p += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentStunRank_p < -100)
        //        pNpc.data.m_CurrentStunRank_p = -100;
        //}

        //private static void fasthitrecover_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] == 0)
        //        return;

        //    if (pMagic.nValue[0] > 0)
        //    {
        //        if (pNpc.data.m_HitRecover * pMagic.nValue[0] >= 50 && pNpc.data.m_HitRecover * pMagic.nValue[0] <= 100)
        //        {
        //            pNpc.data.m_CurrentHitRecover += 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentHitRecover += pNpc.data.m_HitRecover * pMagic.nValue[0] / 100;
        //            if ((pNpc.data.m_HitRecover * pMagic.nValue[0] % 100) >= 50)
        //                pNpc.data.m_CurrentHitRecover += 1;
        //        }
        //    }
        //    else
        //    {
        //        pMagic.nValue[0] = -pMagic.nValue[0];

        //        if (pNpc.data.m_HitRecover * pMagic.nValue[0] >= 50 && pNpc.data.m_HitRecover * pMagic.nValue[0] <= 100)
        //        {
        //            pNpc.data.m_CurrentHitRecover -= 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentHitRecover -= (pNpc.data.m_HitRecover * pMagic.nValue[0]) / 100;
        //            if ((pNpc.data.m_HitRecover * pMagic.nValue[0] % 100) >= 50)
        //                pNpc.data.m_CurrentHitRecover -= 1;
        //        }
        //    }
        //}

        //private static void FastWalkRunP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] == 0)
        //        return;

        //    pNpc.data.m_CurrentTempSpeed += pMagic.nValue[0];

        //    if (pMagic.nValue[0] > 0)
        //    {
        //        if (pNpc.data.m_WalkSpeed * pMagic.nValue[0] >= 50 && pNpc.data.m_WalkSpeed * pMagic.nValue[0] <= 100)
        //        {
        //            pNpc.data.m_CurrentWalkSpeed += 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentWalkSpeed += (pNpc.data.m_WalkSpeed * pMagic.nValue[0]) / 100;
        //            if ((pNpc.data.m_WalkSpeed * pMagic.nValue[0] % 100) >= 50)
        //                pNpc.data.m_CurrentWalkSpeed += 1;
        //        }

        //        if (pNpc.data.m_RunSpeed * pMagic.nValue[0] >= 50 && pNpc.data.m_RunSpeed * pMagic.nValue[0] <= 100)
        //        {
        //            pNpc.data.m_CurrentRunSpeed += 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentRunSpeed += (pNpc.data.m_RunSpeed * pMagic.nValue[0]) / 100;
        //            if ((pNpc.data.m_RunSpeed * pMagic.nValue[0] % 100) >= 50)
        //                pNpc.data.m_CurrentRunSpeed += 1;
        //        }
        //    }
        //    else
        //    {
        //        int nTempSeed = -pMagic.nValue[0];

        //        if (pNpc.data.m_WalkSpeed * nTempSeed >= 50 && pNpc.data.m_WalkSpeed * nTempSeed <= 100)
        //        {
        //            pNpc.data.m_CurrentWalkSpeed -= 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentWalkSpeed -= (pNpc.data.m_WalkSpeed * nTempSeed) / 100;
        //            if ((pNpc.data.m_WalkSpeed * nTempSeed % 100) >= 50)
        //                pNpc.data.m_CurrentWalkSpeed -= 1;
        //        }

        //        if (pNpc.data.m_RunSpeed * nTempSeed >= 50 && pNpc.data.m_RunSpeed * nTempSeed <= 100)
        //        {
        //            pNpc.data.m_CurrentRunSpeed -= 1;
        //        }
        //        else
        //        {
        //            pNpc.data.m_CurrentRunSpeed -= (pNpc.data.m_RunSpeed * nTempSeed) / 100;
        //            if ((pNpc.data.m_RunSpeed * nTempSeed % 100) >= 50)
        //                pNpc.data.m_CurrentRunSpeed -= 1;
        //        }
        //    }
        //    if (pNpc.data.m_CurrentRunSpeed < 10)
        //        pNpc.data.m_CurrentRunSpeed = 10;

        //    if (pNpc.data.m_CurrentWalkSpeed <= 5)
        //        pNpc.data.m_CurrentWalkSpeed = 5;
        //}

        //private static void FireArmorV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_FireArmor.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_FireArmor.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_FireArmor.nValue[0] = 0;
        //        pNpc.data.m_FireArmor.nTime = 0;
        //    }
        //}

        //private static void FireSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pNpc.data.m_Series == 3)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void FreezeTimeReduceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentFreezeTimeReducePercent += pMagic.nValue[0];
        //}

        //private static void KnockBackP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentKnockBack += pMagic.nValue[0];
        //}

        //private static void LifeMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLifeMax += pNpc.data.m_LifeMax * pMagic.nValue[0] / 100;

        //    if (pNpc.data.m_CurrentLifeMax <= 0)
        //        pNpc.data.m_CurrentLifeMax = 100;
        //}

        //private static void LifeMaxV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLifeMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentLifeMax <= 0)
        //        pNpc.data.m_CurrentLifeMax = 100;
        //}

        //private static void LifeReplenishV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLifeReplenish += pMagic.nValue[0];
        //}

        //private static void LifeV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLife += pMagic.nValue[0];
        //}

        //private static void LightingArmorV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_LightArmor.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_LightArmor.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_LightArmor.nValue[0] = 0;
        //        pNpc.data.m_LightArmor.nTime = 0;
        //    }
        //}

        //private static void LightingresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;

        //    pNpc.data.m_TempLightResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempLightResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempLightResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempLightResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempLightResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_LightResist + nRes;
        //    else
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_LightResist - nRes;

        //    if (pNpc.data.m_CurrentLightResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentLightResistMax = pNpc.data.m_CurrentLightResist;

        //    if (pNpc.data.m_CurrentLightResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentLightResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentLightResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentLightResistMax = BASE_FANGYU_ALL_MAX;
        //    if (pNpc.data.m_CurrentLightResist > pNpc.data.m_CurrentLightResistMax)
        //        pNpc.data.m_CurrentLightResist = pNpc.data.m_CurrentLightResistMax;
        //}

        //private static void LuckyV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.IsSpecialNpc())
        //    {
        //        pNpc.data.m_nTempLucky_p += pMagic.nValue[0];

        //        if (pNpc.data.m_nTempLucky_p < 0)
        //            pNpc.data.m_nTempLucky_p = 0;

        //        pNpc.data.m_nCurLucky = pNpc.data.m_nLucky * (pNpc.data.m_nTempLucky_p + 100) / 100;
        //    }
        //    else
        //    {
        //        pNpc.data.m_nCurNpcLucky += pMagic.nValue[0];
        //    }
        //}

        //private static void ManaMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentManaMax += pNpc.data.m_ManaMax * pMagic.nValue[0] / 100;

        //    if (pNpc.data.m_CurrentManaMax <= 0)
        //        pNpc.data.m_CurrentManaMax = 100;
        //}

        //private static void ManaMaxV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentManaMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentManaMax <= 0)
        //        pNpc.data.m_CurrentManaMax = 100;
        //}

        //private static void ManaReplenishV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentManaReplenish += pMagic.nValue[0];
        //}

        //private static void ManaReplenishp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentManaReplenish_p += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentManaReplenish_p <= -100)
        //        pNpc.data.m_CurrentManaReplenish_p = -100;
        //}

        //private static void ManaV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentMana += pMagic.nValue[0];
        //}

        //private static void ManaShieldP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_ManaShield.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_ManaShield.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_ManaShield.nValue[0] = 0;
        //        pNpc.data.m_ManaShield.nTime = 0;
        //    }
        //}

        //private static void MeleeDamageReturnP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentMeleeDmgRetPercent += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentMeleeDmgRetPercent <= 0)
        //        pNpc.data.m_CurrentMeleeDmgRetPercent = 0;
        //}

        //private static void MeleeDamageReturnV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentMeleeDmgRet += pMagic.nValue[0];
        //}

        //private static void MetalSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pNpc.data.m_Series == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void PhysicsArmorV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_PhysicsArmor.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_PhysicsArmor.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_PhysicsArmor.nValue[0] = 0;
        //        pNpc.data.m_PhysicsArmor.nTime = 0;
        //    }
        //}

        //private static void PhysicsresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;

        //    pNpc.data.m_TempPhysicsResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempPhysicsResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempPhysicsResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempPhysicsResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempPhysicsResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }

        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_PhysicsResist + nRes;
        //    else
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_PhysicsResist - nRes;

        //    if (pNpc.data.m_CurrentPhysicsResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPhysicsResistMax = pNpc.data.m_CurrentPhysicsResist;

        //    if (pNpc.data.m_CurrentPhysicsResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentPhysicsResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentPhysicsResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPhysicsResistMax = BASE_FANGYU_ALL_MAX;

        //    if (pNpc.data.m_CurrentPhysicsResist > pNpc.data.m_CurrentPhysicsResistMax)
        //        pNpc.data.m_CurrentPhysicsResist = pNpc.data.m_CurrentPhysicsResistMax;
        //}

        //private static void Damage2ManaP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentDamage2Mana += pMagic.nValue[0];
        //}

        //private static void PoisonArmorV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_PoisonArmor.nValue[0] = pMagic.nValue[0];
        //        pNpc.data.m_PoisonArmor.nTime = pMagic.nValue[1];
        //    }
        //    else
        //    {
        //        pNpc.data.m_PoisonArmor.nValue[0] = 0;
        //        pNpc.data.m_PoisonArmor.nTime = 0;
        //    }
        //}

        //private static void PoisonDamageReduceV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_PoisonState.nValue[0] = pNpc.data.m_PoisonState.nValue[0] - pMagic.nValue[0];

        //    if (pNpc.data.m_PoisonState.nValue[0] <= 0)
        //        pNpc.data.m_PoisonState.nValue[0] = 0;
        //}

        //private static void PoisonresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;

        //    pNpc.data.m_TempPoisonResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempPoisonResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempPoisonResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempPoisonResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempPoisonResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }

        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_PoisonResist + nRes;
        //    else
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_PoisonResist - nRes;
        //    if (pNpc.data.m_CurrentPoisonResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPoisonResistMax = pNpc.data.m_CurrentPoisonResist;

        //    if (pNpc.data.m_CurrentPoisonResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentPoisonResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentPoisonResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentPoisonResistMax = BASE_FANGYU_ALL_MAX;
        //    if (pNpc.data.m_CurrentPoisonResist > pNpc.data.m_CurrentPoisonResistMax)
        //        pNpc.data.m_CurrentPoisonResist = pNpc.data.m_CurrentPoisonResistMax;
        //}

        //private static void PoisonTimeReduceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPoisonTimeReducePercent += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentPoisonTimeReducePercent < -100)
        //        pNpc.data.m_CurrentPoisonTimeReducePercent = -100;
        //}

        //private static void anti_poisontimereduce_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_EnemyPoisonTimeReducePercent += pMagic.nValue[0];
        //}

        //private static void RangeDamageReturnV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentRangeDmgRet += pMagic.nValue[0];
        //}

        //private static void RangeDamageReturnP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentRangeDmgRetPercent += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentRangeDmgRetPercent <= 0)
        //        pNpc.data.m_CurrentRangeDmgRetPercent = 0;
        //}

        //private static void SlowMissleB(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //        pNpc.data.m_CurrentSlowMissle = true;
        //    else
        //        pNpc.data.m_CurrentSlowMissle = false;
        //}

        //private static void StaminaMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStaminaMax += pNpc.data.m_StaminaMax * pMagic.nValue[0] / 100;
        //}

        //private static void StaminaMaxV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStaminaMax += pMagic.nValue[0];
        //}

        //private static void StaminaReplenishV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStaminaGain += pMagic.nValue[0];
        //}

        //private static void StaminaV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStamina += pMagic.nValue[0];
        //}

        //private static void StealLifeP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLifeStolen += pMagic.nValue[0];
        //}

        //private static void StealManaP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentManaStolen += pMagic.nValue[0];
        //}

        //private static void StealStaminaP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStaminaStolen += pMagic.nValue[0];
        //}

        //private static void StrengthV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_Kind != Datafield.NPCKIND.kind_player)
        //        return;

        //    pNpc.data.ChangeCurStrength(pMagic.nValue[0]);
        //}

        //private static void StunTimeReduceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentStunTimeReducePercent += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentStunTimeReducePercent < -200)
        //        pNpc.data.m_CurrentStunTimeReducePercent = -200;
        //}

        //private static void addskilldamage10(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nListIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage9(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nListIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage8(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nListIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void anti_stuntimereduce_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_EnemyStunTimeReducePercent += pMagic.nValue[0];
        //}

        //private static void VisionRadiusP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentVisionRadius += pMagic.nValue[0];
        //}

        //private static void VitalityV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_Kind != Datafield.NPCKIND.kind_player)
        //        return;

        //    pNpc.data.ChangeCurVitality(pMagic.nValue[0]);
        //}

        //private static void WaterSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pNpc.data.m_Series == 2)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void WoodSkillV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    if (pNpc.data.m_Series == 1)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.QeuipAddPoint(i, pMagic.nValue[0]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void FireresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nRes = 0;
        //    int nRer = 0;
        //    int nIsadd = 0;

        //    pNpc.data.m_TempFireResist += pMagic.nValue[0];

        //    if (pNpc.data.m_TempFireResist >= 0)
        //    {
        //        nIsadd = 1;
        //        nRes = (pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (pNpc.data.m_TempFireResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    else
        //    {
        //        nRes = (-pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) / 100;
        //        nRer = (-pNpc.data.m_TempFireResist * BASE_FANGYU_ALL_MAX) % 100;

        //        if (-pNpc.data.m_TempFireResist == 1)
        //            nRes = 1;

        //        if (nRer >= 50)
        //            nRes += 1;
        //    }
        //    if (nIsadd == 1)
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_FireResist + nRes;
        //    else
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_FireResist - nRes;

        //    if (pNpc.data.m_CurrentFireResist > BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentFireResistMax = pNpc.data.m_CurrentFireResist;

        //    if (pNpc.data.m_CurrentFireResistMax > MAX_RESIST)
        //        pNpc.data.m_CurrentFireResistMax = MAX_RESIST;

        //    if (pNpc.data.m_CurrentFireResistMax <= BASE_FANGYU_ALL_MAX)
        //        pNpc.data.m_CurrentFireResistMax = BASE_FANGYU_ALL_MAX;
        //    if (pNpc.data.m_CurrentFireResist > pNpc.data.m_CurrentFireResistMax)
        //        pNpc.data.m_CurrentFireResist = pNpc.data.m_CurrentFireResistMax;
        //}

        //private static void ArmorDefenseV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentDefend += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentDefend < 0)
        //        pNpc.data.m_CurrentDefend = 0;
        //}

        //private static void ColdEnhanceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentColdEnhance += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentColdEnhance <= -100)
        //        pNpc.data.m_CurrentColdEnhance = -90;
        //}

        //private static void FireEnhanceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentFireEnhance += pMagic.nValue[0];
        //}

        //private static void LightingEnhanceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLightEnhance += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentLightEnhance < 0)
        //        pNpc.data.m_CurrentLightEnhance = 0;
        //}

        //private static void PoisonEnhanceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPoisonEnhance += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentPoisonEnhance < -100)
        //        pNpc.data.m_CurrentPoisonEnhance = -100;
        //}

        //private static void LifePotionV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[1] <= 0)
        //        return;

        //    int nX1, nY1, nX2, nY2;
        //    nX1 = pNpc.data.m_LifeState.nValue[0];
        //    nY1 = pNpc.data.m_LifeState.nTime;
        //    nX2 = pMagic.nValue[0];
        //    nY2 = pMagic.nValue[1];
        //    pNpc.data.m_LifeState.nTime = Math.Max(nY1, nY2);

        //    if (pNpc.data.m_LifeState.nTime != 0)
        //        pNpc.data.m_LifeState.nValue[0] = (nX1 * nY1 + nX2 * nY2) / pNpc.data.m_LifeState.nTime;
        //    else
        //        pNpc.data.m_LifeState.nValue[0] = 0;
        //}

        //private static void dupotion_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[1] <= 0)
        //        return;
        //    if (pNpc.data.m_PoisonState.nTime <= 0)
        //        return;
        //    pNpc.data.m_PoisonState.nTime = pNpc.data.m_PoisonState.nTime + pMagic.nValue[0];

        //    if (pNpc.data.m_PoisonState.nTime < 0)
        //        pNpc.data.m_PoisonState.nTime = 0;
        //}

        //private static void ManaPotionV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[1] <= 0)
        //        return;
        //    int nX1, nY1, nX2, nY2;
        //    nX1 = pNpc.data.m_ManaState.nValue[0];
        //    nY1 = pNpc.data.m_ManaState.nTime;
        //    nX2 = pMagic.nValue[0];
        //    nY2 = pMagic.nValue[1];
        //    pNpc.data.m_ManaState.nTime = Math.Max(nY1, nY2);

        //    if (pNpc.data.m_ManaState.nTime != 0)
        //        pNpc.data.m_ManaState.nValue[0] = (nX1 * nY1 + nX2 * nY2) / pNpc.data.m_ManaState.nTime;
        //    else
        //        pNpc.data.m_ManaState.nValue[0] = 0;
        //}

        //private static void PhysicsResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPhysicsResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentPhysicsResistMax > pNpc.data.m_PhysicsResistMax)
        //        pNpc.data.m_CurrentPhysicsResistMax = pNpc.data.m_PhysicsResistMax;
        //}

        //private static void ColdResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentColdResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentColdResistMax > pNpc.data.m_ColdResistMax)
        //        pNpc.data.m_CurrentColdResistMax = pNpc.data.m_ColdResistMax;
        //}

        //private static void FireResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentFireResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentFireResistMax > pNpc.data.m_FireResistMax)
        //        pNpc.data.m_CurrentFireResistMax = pNpc.data.m_FireResistMax;
        //}

        //private static void LightingResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLightResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentLightResistMax > pNpc.data.m_LightResistMax)
        //        pNpc.data.m_CurrentLightResistMax = pNpc.data.m_LightResistMax;
        //}

        //private static void PoisonResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPoisonResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentPoisonResistMax > pNpc.data.m_PoisonResistMax)
        //        pNpc.data.m_CurrentPoisonResistMax = pNpc.data.m_PoisonResistMax;
        //}

        //private static void AllResMaxP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentFireResistMax += pMagic.nValue[0];
        //    pNpc.data.m_CurrentColdResistMax += pMagic.nValue[0];
        //    pNpc.data.m_CurrentLightResistMax += pMagic.nValue[0];
        //    pNpc.data.m_CurrentPoisonResistMax += pMagic.nValue[0];
        //    pNpc.data.m_CurrentPhysicsResistMax += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentPoisonResistMax > pNpc.data.m_PoisonResistMax)
        //        pNpc.data.m_CurrentPoisonResistMax = pNpc.data.m_PoisonResistMax;
        //    if (pNpc.data.m_CurrentLightResistMax > pNpc.data.m_LightResistMax)
        //        pNpc.data.m_CurrentLightResistMax = pNpc.data.m_LightResistMax;
        //    if (pNpc.data.m_CurrentFireResistMax > pNpc.data.m_FireResistMax)
        //        pNpc.data.m_CurrentFireResistMax = pNpc.data.m_FireResistMax;
        //    if (pNpc.data.m_CurrentColdResistMax > pNpc.data.m_ColdResistMax)
        //        pNpc.data.m_CurrentColdResistMax = pNpc.data.m_ColdResistMax;
        //    if (pNpc.data.m_CurrentPhysicsResistMax > pNpc.data.m_PhysicsResistMax)
        //        pNpc.data.m_CurrentPhysicsResistMax = pNpc.data.m_PhysicsResistMax;
        //}

        //private static void FatallyStrikeP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void MixPoisonDamage(settings.skill.SkillSettingData.KMagicAttrib pDes, settings.skill.SkillSettingData.KMagicAttrib pSrc)
        //{
        //    int d1, d2, t1, t2, c1, c2;
        //    d1 = pDes.nValue[0];
        //    d2 = pSrc.nValue[0];
        //    t1 = pDes.nValue[1];
        //    t2 = pSrc.nValue[1];
        //    c1 = pDes.nValue[2];
        //    c2 = pSrc.nValue[2];
        //    if (c1 == 0 || d1 == 0)
        //    {
        //        pDes.nAttribType = pSrc.nAttribType;
        //        pDes.nValue[0] = pSrc.nValue[0];
        //        pDes.nValue[1] = pSrc.nValue[1];
        //        pDes.nValue[2] = pSrc.nValue[2];
        //        return;
        //    }
        //    if (c2 == 0 || d2 == 0)
        //    {
        //        return;
        //    }
        //    pDes.nValue[0] = ((c1 + c2) * d1 / c1 + (c1 + c2) * d2 / c2) / 2;
        //    pDes.nValue[1] = (t1 * d1 * c2 + t2 * d2 * c1) / (d1 * c2 + d2 * c1);
        //    pDes.nValue[2] += (c1 + c2) / 2;
        //}

        //private static void AddPhysicsMagic(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPhysicsMagicDamageV.nValue[0] += pMagic.nValue[0];
        //    pNpc.data.m_CurrentPhysicsMagicDamageV.nValue[2] += pMagic.nValue[0];
        //}

        //private static void Add_neiphysicsenhance_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[0] += pMagic.nValue[0];
        //    pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[2] += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[0] < 0)
        //        pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[0] = settings.skill.Static.GetRandomNumber(10, 20);

        //    if (pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[2] < 0)
        //        pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[2] = pNpc.data.m_CurrentPhysicsMagicDamageP.nValue[0] + settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void AddColdMagic(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    const int defNORMAL_COLD_TIME = 60;

        //    pNpc.data.m_CurrentMagicColdDamage.nValue[0] += pMagic.nValue[0];
        //    pNpc.data.m_CurrentMagicColdDamage.nValue[2] += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentMagicColdDamage.nValue[0] > 0 && pNpc.data.m_CurrentMagicColdDamage.nValue[2] > 0)
        //        pNpc.data.m_CurrentMagicColdDamage.nValue[1] = defNORMAL_COLD_TIME;
        //    else
        //    {
        //        pNpc.data.m_CurrentMagicColdDamage.nValue[0] = 0;
        //        pNpc.data.m_CurrentMagicColdDamage.nValue[1] = 0;
        //        pNpc.data.m_CurrentMagicColdDamage.nValue[2] = 0;
        //    }
        //}

        //private static void AddFireMagic(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentMagicFireDamage.nValue[0] += pMagic.nValue[0];
        //    pNpc.data.m_CurrentMagicFireDamage.nValue[2] += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentMagicFireDamage.nValue[0] < 0)
        //        pNpc.data.m_CurrentMagicFireDamage.nValue[0] = settings.skill.Static.GetRandomNumber(10, 20);

        //    if (pNpc.data.m_CurrentMagicFireDamage.nValue[2] < 0)
        //        pNpc.data.m_CurrentMagicFireDamage.nValue[2] = pNpc.data.m_CurrentMagicFireDamage.nValue[0] + settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void AddLightingMagic(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentMagicLightDamage.nValue[0] += pMagic.nValue[0];
        //    pNpc.data.m_CurrentMagicLightDamage.nValue[2] += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentMagicLightDamage.nValue[0] < 0)
        //        pNpc.data.m_CurrentMagicLightDamage.nValue[0] = settings.skill.Static.GetRandomNumber(10, 20);

        //    if (pNpc.data.m_CurrentMagicLightDamage.nValue[2] < 0)
        //        pNpc.data.m_CurrentMagicLightDamage.nValue[2] = pNpc.data.m_CurrentMagicLightDamage.nValue[0] + settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void AddPoisonMagic(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    const int defPOISON_DAMAGE_TIME = 60;
        //    const int defPOISON_DAMAGE_INTERVAL = 10;
        //    pNpc.data.m_CurrentMagicPoisonDamage.nValue[0] += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentMagicPoisonDamage.nValue[0] > 0)
        //    {
        //        pNpc.data.m_CurrentMagicPoisonDamage.nValue[1] = defPOISON_DAMAGE_TIME;
        //        pNpc.data.m_CurrentMagicPoisonDamage.nValue[2] = defPOISON_DAMAGE_INTERVAL;
        //    }
        //    else
        //    {
        //        pNpc.data.m_CurrentMagicPoisonDamage.nValue[0] = 0;
        //        pNpc.data.m_CurrentMagicPoisonDamage.nValue[1] = 0;
        //        pNpc.data.m_CurrentMagicPoisonDamage.nValue[2] = 0;
        //    }
        //}

        //private static void AddPhysicsDamagePP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddPhysicsDamageP += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddPhysicsDamageP < -100)
        //        pNpc.data.m_CurrentAddPhysicsDamageP = -100;
        //}

        //private static void Addphysicsdamagevp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddPhysicsDamage += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddPhysicsDamage < 0)
        //        pNpc.data.m_CurrentAddPhysicsDamage = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Aaddfiredamagevp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddFireDamagev += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddFireDamagev < 0)
        //        pNpc.data.m_CurrentAddFireDamagev = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addcolddamagevp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddColdDamagev += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddColdDamagev < 0)
        //        pNpc.data.m_CurrentAddColdDamagev = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addlightingdamagevp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddLighDamagev += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddLighDamagev < 0)
        //        pNpc.data.m_CurrentAddLighDamagev = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addpoisondamagevp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddPoisonDamagev += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddPoisonDamagev < 0)
        //        pNpc.data.m_CurrentAddPoisonDamagev = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addphysicsmagicp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicphysicsDamageP += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddmagicphysicsDamageP < -100)
        //        pNpc.data.m_CurrentAddmagicphysicsDamageP = -100;
        //}

        //private static void Addphysicsmagicvb(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicphysicsDamage += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddmagicphysicsDamage < 0)
        //        pNpc.data.m_CurrentAddmagicphysicsDamage = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addcoldmagicvp(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicColdDamagicv += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddmagicColdDamagicv < 0)
        //        pNpc.data.m_CurrentAddmagicColdDamagicv = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addfiremagicv(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicFireDamagicv += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentAddmagicFireDamagicv < 0)
        //        pNpc.data.m_CurrentAddmagicFireDamagicv = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addlightingmagicv(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicLightDamagicv += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentAddmagicLightDamagicv < 0)
        //        pNpc.data.m_CurrentAddmagicLightDamagicv = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void Addpoisonmagicv(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAddmagicPoisonDamagicv += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentAddmagicPoisonDamagicv < 0)
        //        pNpc.data.m_CurrentAddmagicPoisonDamagicv = settings.skill.Static.GetRandomNumber(10, 20);
        //}

        //private static void fatallystrikeresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void me2metaldamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Me2metaldamage_p += pMagic.nValue[0];

        //    if (pNpc.data.m_Me2metaldamage_p >= 100)
        //        pNpc.data.m_Me2metaldamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void metal2medamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Metal2medamage_p += pMagic.nValue[0];

        //    if (pNpc.data.m_Metal2medamage_p >= 100)
        //        pNpc.data.m_Metal2medamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void me2wooddamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Me2wooddamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Me2wooddamage_p >= 100)
        //        pNpc.data.m_Me2wooddamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void wood2medamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Wood2medamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Wood2medamage_p >= 100)
        //        pNpc.data.m_Wood2medamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void me2waterdamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Me2waterdamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Me2waterdamage_p >= 100)
        //        pNpc.data.m_Me2waterdamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void water2medamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Water2medamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Water2medamage_p >= 100)
        //        pNpc.data.m_Water2medamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void me2firedamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Me2firedamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Me2firedamage_p >= 100)
        //        pNpc.data.m_Me2firedamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void fire2medamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Fire2medamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Fire2medamage_p >= 100)
        //        pNpc.data.m_Fire2medamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void me2earthdamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Me2earthdamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Me2earthdamage_p >= 100)
        //        pNpc.data.m_Me2earthdamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void earth2medamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Earth2medamage_p += pMagic.nValue[0];
        //    if (pNpc.data.m_Earth2medamage_p >= 100)
        //        pNpc.data.m_Earth2medamage_p = settings.skill.Static.GetRandomNumber(80, 90);
        //}

        //private static void doscript_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_DoScriptState.nMagicAttrib = 0;
        //    pNpc.data.m_DoScriptState.nValue[0] = pMagic.nValue[0];
        //    pNpc.data.m_DoScriptState.nValue[1] = pMagic.nValue[1];
        //    pNpc.data.m_DoScriptState.nValue[2] = pMagic.nValue[2];
        //    pNpc.data.m_DoScriptState.nTime = pMagic.nValue[2];
        //}

        //private static void addexp_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_ExpState.nMagicAttrib = 0;
        //    pNpc.data.m_ExpState.nValue[0] = pMagic.nValue[0];
        //    pNpc.data.m_ExpState.nValue[1] = pMagic.nValue[1];
        //    pNpc.data.m_ExpState.nValue[2] = pMagic.nValue[2];
        //    pNpc.data.m_ExpState.nTime = pMagic.nValue[1] * 2;
        //}

        //private static void randmove(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_randmove.nMagicAttrib = 0;
        //    pNpc.data.m_randmove.nValue[0] = pMagic.nValue[0];
        //    pNpc.data.m_randmove.nValue[1] = pMagic.nValue[2];
        //    pNpc.data.m_randmove.nTime = pMagic.nValue[1];
        //}

        //private static void addbaopoisondmax_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void creatstatus_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_DoScriptState.nMagicAttrib = 0;
        //    pNpc.data.m_DoScriptState.nValue[0] = pMagic.nValue[0];
        //    pNpc.data.m_DoScriptState.nValue[1] = pMagic.nValue[1];
        //    pNpc.data.m_DoScriptState.nValue[2] = pMagic.nValue[2];
        //    pNpc.data.m_DoScriptState.nTime = pMagic.nValue[2];
        //}

        //private static void alljihuo_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentAllJiHuo += pMagic.nValue[0];
        //}

        //private static void ignoreattacrating_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentIgnorenAttacRating += pMagic.nValue[0];
        //}

        //private static void addskilldamage7(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] < 0)
        //        pMagic.nValue[0] = -pMagic.nValue[0];

        //    if (pNpc.data.nEnhanceInfo.ContainsKey(pMagic.nValue[0]))
        //    {
        //        pNpc.data.nEnhanceInfo[pMagic.nValue[0]].nSkillIdx = pMagic.nValue[0];
        //        int nTempval = pNpc.data.nEnhanceInfo[pMagic.nValue[0]].nEnhance + pMagic.nValue[2];
        //        pNpc.data.nEnhanceInfo[pMagic.nValue[0]].nEnhance = nTempval;
        //    }
        //    else
        //    {
        //        pNpc.data.nEnhanceInfo[pMagic.nValue[0]].nSkillIdx = pMagic.nValue[0];
        //        pNpc.data.nEnhanceInfo[pMagic.nValue[0]].nEnhance = pMagic.nValue[2];
        //    }

        //    if (pMagic.nValue[0] > 0 && pMagic.nValue[2] != 0)
        //    {
        //        int nActiveSkillID = pMagic.nValue[0];
        //        int nListIndex = pNpc.data.m_SkillList.FindSame(nActiveSkillID);
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage1(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];

        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null &&
        //                pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 &&
        //                pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 &&
        //                pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevel(pNpc.data.m_SkillList.GetSkillId(i)) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage2(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];

        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage3(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage4(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage5(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void addskilldamage6(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        int Same;
        //        if (pMagic.nValue[0] > 0)
        //            Same = pMagic.nValue[0];
        //        else
        //            Same = -pMagic.nValue[0];
        //        int nlistIndex = pNpc.data.m_SkillList.FindSame(Same);
        //        if (nlistIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nlistIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nlistIndex, pMagic.nValue[2]);
        //            }
        //        }
        //    }
        //    else if (pMagic.nValue[0] == 0)
        //    {
        //        for (int i = 1; i < MAX_NPCSKILL; ++i)
        //        {
        //            if (pNpc.data.m_SkillList.m_Skills[i] != null && pNpc.data.m_SkillList.m_Skills[i].SkillId != 1 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 2 && pNpc.data.m_SkillList.m_Skills[i].SkillId != 53)
        //            {
        //                if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(i) > 0)
        //                {
        //                    pNpc.data.m_SkillList.AddEnChance(i, pMagic.nValue[2]);
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void skill_enhance(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    pNpc.data.m_Currentskillenhance += pMagic.nValue[0];

        //    int nActiveSkillID = 0, nListIndex = 0;
        //    nActiveSkillID = pNpc.data.m_nLeftSkillID;
        //    nListIndex = pNpc.data.m_nLeftListidx;
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[0]);
        //            }
        //        }
        //    }
        //}

        //private static void dynamicmagicshieldV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentDamageReduce += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentDamageReduce < 0)
        //        pNpc.data.m_CurrentDamageReduce = 0;
        //}

        //private static void addstealfeatureskill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void lifereplenishP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentLifeReplenish_p += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentLifeReplenish_p < -100)
        //        pNpc.data.m_CurrentLifeReplenish_p = -100;
        //}

        //private static void ignoreskillP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentIgnoreskillp += pMagic.nValue[0];
        //}

        //private static void poisondamagereturnV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPoisondamagereturnV += pMagic.nValue[0];
        //}

        //private static void poisondamagereturnP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentPoisondamagereturnP += pMagic.nValue[0];
        //}

        //private static void returnskillP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentReturnskillp += pMagic.nValue[0];
        //}

        //private static void autoreplyskill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void skill_mintimepercastV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void mintimepercastonhorseV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void poison2decmanaP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.data.m_LoseMana.nTime <= 0)
        //    {
        //        if (pMagic.nValue[1] > 0 && pMagic.nValue[0] > 0)
        //        {
        //            pNpc.data.m_LoseMana.nValue[0] = pMagic.nValue[0];
        //            pNpc.data.m_LoseMana.nTime = pMagic.nValue[1];
        //            pNpc.data.m_LoseMana.nValue[1] = pMagic.nValue[1] * 2;
        //        }
        //    }
        //}

        //private static void skill_appendskil(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pMagic.nValue[0] > 0)
        //    {
        //        pNpc.data.m_IsMoreAura = true;
        //        for (int i = 0; i < 5; ++i)
        //        {
        //            if (pNpc.data.m_TmpAuraID[i].skillid == pMagic.nValue[0])
        //                break;
        //            if (pNpc.data.m_TmpAuraID[i].skillid == 0)
        //            {
        //                pNpc.data.m_TmpAuraID[i].skillid = pMagic.nValue[0];
        //                pNpc.data.m_TmpAuraID[i].level = pMagic.nValue[1];
        //                pNpc.data.m_TmpAuraID[i].skilllistIndex = pNpc.data.m_SkillList.FindSame(pNpc.data.m_TmpAuraID[i].skillid);
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        pNpc.data.m_IsMoreAura = false;
        //        for (int i = 0; i < 5; ++i)
        //        {
        //            if (pNpc.data.m_TmpAuraID[i].skillid == -pMagic.nValue[0])
        //            {
        //                pNpc.data.m_TmpAuraID[i].skillid = 0;
        //                pNpc.data.m_TmpAuraID[i].skilllistIndex = 0;
        //                pNpc.data.m_TmpAuraID[i].level = 0;
        //            }
        //        }
        //    }
        //}

        //private static void hide(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    int nFrame = pMagic.nValue[1];
        //    if (nFrame <= 0)
        //        nFrame = 1;

        //    pNpc.data.m_Hide.nTime = nFrame;
        //}

        //private static void clearnegativestate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void returnresP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentReturnresp += pMagic.nValue[0];
        //}

        //private static void addcreatnpc_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentCreatnpcv += pMagic.nValue[0];
        //}

        //private static void addzhuabu_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentCreatnpcv += pMagic.nValue[0];
        //}

        //private static void reduceskillcd1(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void reduceskillcd2(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void reduceskillcd3(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void clearallcd(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void addblockrate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void walkrunshadow(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void returnskill2enemy(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    {
        //        if (pMagic.nValue[0] > 0 && pMagic.nValue[1] > 0)
        //        {
        //            pNpc.data.m_Returnskill.nValue[0] = pMagic.nValue[0];

        //            pNpc.data.m_Returnskill.nValue[1] = pMagic.nValue[1];
        //        }
        //        else
        //        {
        //            pNpc.data.m_Returnskill.nValue[0] = 0;

        //            pNpc.data.m_Returnskill.nValue[1] = 0;
        //        }
        //    }
        //}

        //private static void manatoskill_enhance(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (!pNpc.IsSpecialNpc())
        //        return;
        //    pNpc.data.m_CurrentFullManaskillenhance += pMagic.nValue[0];

        //    int nActiveSkillID = 0, nListIndex = 0;
        //    nActiveSkillID = pNpc.data.GetLeftSkill();
        //    nListIndex = pNpc.data.GetLeftSkillListidx();
        //    if (pMagic.nValue[0] != 0)
        //    {
        //        if (nListIndex != 0)
        //        {
        //            if (pNpc.data.m_SkillList.GetCurrentLevelByIdx(nListIndex) > 0)
        //            {
        //                pNpc.data.m_SkillList.AddEnChance(nListIndex, pMagic.nValue[0]);
        //            }
        //        }
        //    }
        //}

        //private static void add_alldamage_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currenadddamagev += pMagic.nValue[0];
        //}

        //private static void decPercasttimehorse(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void decPercasttime(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_autoSkill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_lifeP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_lifeV(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_711_auto(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_714_auto(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_717_auto(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void enhance_723_missP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void expenhanceP(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    if (pNpc.IsSpecialNpc())
        //    {
        //        if (pMagic.nValue[0] > 5000)
        //            pMagic.nValue[0] = 100;
        //        pNpc.data.m_nUpExp += pMagic.nValue[0];

        //        if (pNpc.data.m_nUpExp < 0)
        //            pNpc.data.m_nUpExp = 0;
        //    }
        //}

        //private static void SerisesDamage(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentSerisesEnhance += pMagic.nValue[0];
        //}

        //private static void block_rate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentdanggeRate += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentdanggeRate < 0)
        //        pNpc.data.m_CurrentdanggeRate = 0;
        //}

        //private static void enhancehit_rate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentzhongjiRate += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentzhongjiRate < 0)
        //        pNpc.data.m_CurrentzhongjiRate = 0;
        //}

        //private static void anti_block_rate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentcjdanggeRate += pMagic.nValue[0];
        //    if (pNpc.data.m_CurrentcjdanggeRate < 0)
        //        pNpc.data.m_CurrentcjdanggeRate = 0;
        //}

        //private static void anti_enhancehit_rate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentcjzhongjiRate += pMagic.nValue[0];

        //    if (pNpc.data.m_CurrentcjzhongjiRate < 0)
        //        pNpc.data.m_CurrentcjzhongjiRate = 0;
        //}

        //private static void sorbdamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentsorbdamage += pMagic.nValue[0];
        //}

        //private static void sorbdamage_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentsorbdamage_v += pMagic.nValue[0];
        //}

        //private static void anti_poisonres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentpoisonres += pMagic.nValue[0];
        //}

        //private static void anti_fireres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentfireres += pMagic.nValue[0];
        //}

        //private static void anti_lightingres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentlightingres += pMagic.nValue[0];
        //}

        //private static void anti_physicsres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentphysicsres += pMagic.nValue[0];
        //}

        //private static void anti_coldres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentcoldres += pMagic.nValue[0];
        //}

        //private static void not_add_pkvalue_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentnopkvalue += pMagic.nValue[0];
        //}

        //private static void add_boss_damage(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentbossdamage += pMagic.nValue[0];
        //}

        //private static void five_elements_enhance_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentelementsenhance += pMagic.nValue[0];
        //}

        //private static void five_elements_resist_v(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentelementsresist += pMagic.nValue[0];
        //}

        //private static void add_alldamage_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

        //private static void auto_Revive_rate(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_CurrentautoReviverate += pMagic.nValue[0];
        //}

        //private static void anti_allres_p(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //    pNpc.data.m_Currentallres += pMagic.nValue[0];
        //}

        //private static void autoskill(npcres.Controller pNpc, skill.SkillSettingData.KMagicAttrib pMagic)
        //{
        //}

    }
}
