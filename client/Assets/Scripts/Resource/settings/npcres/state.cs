
namespace game.resource.settings.npcres
{
    public class State
    {
        public const int orderBack = -2;
        public const int orderFront = 20;
        public const int orderSpecialSpr = 21;

        ////////////////////////////////////////////////////////////////////////////////

        private readonly npcres.Controller npcController;
        private readonly UnityEngine.GameObject parent;
        private readonly npcres.state.KStateSpr[] m_cStateSpr;
        private readonly npcres.state.SpecialSpr m_cSpecialSpr;
        private readonly npcres.state.Text[] text;

        private int currentTime;
        private int totalDamageValue;
        private int totalDamageNextTime;

        ////////////////////////////////////////////////////////////////////////////////

        public State(npcres.Controller controller, UnityEngine.GameObject parent)
        {
            this.npcController = controller;
            this.parent = parent;
            this.m_cStateSpr = new npcres.state.KStateSpr[18];
            this.m_cSpecialSpr = new npcres.state.SpecialSpr();
            this.text = new npcres.state.Text[20];

            this.currentTime = 0;
            this.totalDamageValue = 0;
            this.totalDamageNextTime = 0;

            for (int index = 0; index < m_cStateSpr.Length; index++)
            {
                this.m_cStateSpr[index] = new npcres.state.KStateSpr();
            }

            for(int index = 0; index < this.text.Length; index++)
            {
                this.text[index] = new state.Text(index);
            }
        }

        public void Add(
            skill.StateSetting.Data stateData, 
            settings.skill.SkillSettingData.KMagicAttrib[] pData, 
            int nDataNum, 
            int nTime)
        {
            if (stateData.m_szName == string.Empty || stateData.m_szName == null)
            {
                return;
            }

            for (int i = 0; i < 18; i++)
            {
                if (m_cStateSpr[i].m_nID == stateData.m_nID)
                {
                    this.npcController.ModifyAttrib(m_cStateSpr[i].m_State);
                    this.npcController.ModifyAttrib(pData, nDataNum);

                    m_cStateSpr[i].m_LeftTime = nTime;
                    m_cStateSpr[i].m_State.Clear();

                    for(int index = 0; index < nDataNum; index ++)
                    {
                        skill.SkillSettingData.KMagicAttrib newMA = new skill.SkillSettingData.KMagicAttrib();
                        newMA.nAttribType = pData[index].nAttribType;
                        newMA.nValue[0] = -pData[index].nValue[0];
                        newMA.nValue[1] = -pData[index].nValue[1];
                        newMA.nValue[2] = -pData[index].nValue[2];

                        m_cStateSpr[i].m_State.Add(newMA);
                    }

                    return;
                }
            }

            for(int i = 0; i < 18; i ++)
            {
                if(m_cStateSpr[i].m_nID != 0)
                {
                    continue;
                }

                m_cStateSpr[i].m_nID = stateData.m_nID;
                m_cStateSpr[i].m_nType = stateData.m_nType;
                m_cStateSpr[i].m_nPlayType = stateData.m_nPlayType;
                m_cStateSpr[i].m_nBackStart = stateData.m_nBackStart;
                m_cStateSpr[i].m_nBackEnd = stateData.m_nBackEnd;
                m_cStateSpr[i].m_nTotalFrame = stateData.m_nTotalFrame;
                m_cStateSpr[i].m_nTotalDir = stateData.m_nTotalDir;
                m_cStateSpr[i].m_nInterVal = stateData.m_nInterVal;
                m_cStateSpr[i].m_szName = stateData.m_szName;
                m_cStateSpr[i].m_LeftTime = nTime;

                this.npcController.ModifyAttrib(m_cStateSpr[i].m_State);
                this.npcController.ModifyAttrib(pData, nDataNum);

                m_cStateSpr[i].m_State.Clear();

                for (int index = 0; index < nDataNum; index++)
                {
                    skill.SkillSettingData.KMagicAttrib newMA = new skill.SkillSettingData.KMagicAttrib();
                    newMA.nAttribType = pData[index].nAttribType;
                    newMA.nValue[0] = -pData[index].nValue[0];
                    newMA.nValue[1] = -pData[index].nValue[1];
                    newMA.nValue[2] = -pData[index].nValue[2];

                    m_cStateSpr[i].m_State.Add(newMA);
                }

                if (stateData.m_nType == skill.Defination.StateMagicType.STATE_MAGIC_FOOT)
                {
                    m_cStateSpr[i].SetOrderLayer(State.orderBack);
                }
                else if (stateData.m_nType == skill.Defination.StateMagicType.STATE_MAGIC_HEAD)
                {
                    m_cStateSpr[i].SetOrderLayer(State.orderFront);
                }

                m_cStateSpr[i].SetActive(true);

                return;
            }
        }


        public void Add(
            int stateId,
            settings.skill.SkillSettingData.KMagicAttrib[] pData,
            int nDataNum,
            int nTime)
        {
            skill.StateSetting.Data stateData = skill.StateSetting.Get(stateId);

            if (stateData.m_nID == 0)
            {
                return;
            }

            this.Add(stateData, pData, nDataNum, nTime);
        }

        public void SetSpecialSpr(string sprPath)
        {
            this.m_cSpecialSpr.SetSpecialSpr(sprPath);
        }

        public void AddTextState(
            state.Text.FlyForm flyForm,
            string text, UnityEngine.Color color,
            float fontMinSize, float fontMaxSize,
            int startTime, int growUpToTime, int endTime,
            TMPro.FontStyles fontStyles)
        {
            for(int index = 0; index < this.text.Length; index++)
            {
                if (this.text[index].IsActive() == true)
                {
                    continue;
                }

                this.text[index].SetInfo(flyForm, text, color, fontMinSize, fontMaxSize, startTime, growUpToTime, endTime, fontStyles);
                this.text[index].SetActive(true);

                break;
            }
        }
        
        public void AddTextStateCriticalDamage(int damage)
        {
            this.AddTextState(
                state.Text.FlyForm.criticalDamage,
                string.Empty + damage, UnityEngine.Color.red, 
                1.8f, 5f, 
                0, 3, 14,
                TMPro.FontStyles.Bold
            );
        }

        public void AddTextStateNormalDamage(int damage)
        {
            state.Text.FlyForm flyForm;

            if(settings.skill.Static.g_Random(2) == 0)
            {
                flyForm = state.Text.FlyForm.normalDamageLeft;
            }
            else
            {
                flyForm = state.Text.FlyForm.normalDamageRight;
            }

            this.AddTextState(
                flyForm,
                string.Empty + damage, UnityEngine.Color.red,
                1.8f, 1.8f,
                0, 0, 14,
                TMPro.FontStyles.Normal
            );
        }

        public void AppendTotalDamage(int damage)
        {
            this.totalDamageValue += damage;
        }

        public void AddTextStateEXP(int exp)
        {
            this.AddTextState(
                npcres.state.Text.FlyForm.criticalDamage,
                "EXP +" + exp, new UnityEngine.Color(0.51f, 0.95f, 0.89f),
                1.8f, 2.2f,
                0, 2, 14,
                TMPro.FontStyles.Normal
            );
        }

        public void AddTextStateHealth(int value)
        {
            this.AddTextState(
                npcres.state.Text.FlyForm.criticalDamage,
                "+" + value, new UnityEngine.Color(0.05f, 0.89f, 0.15f),
                1.8f, 2.2f,
                0, 2, 14,
                TMPro.FontStyles.Bold
            );
        }

        public void SetNpcPate(int value)
        {
            for(int i = 0; i < 18; i++)
            {
                m_cStateSpr[i].SetNpcPate(value);
            }

            m_cSpecialSpr.SetNpcPate(value);
        }

        public void SetNpcPateType2(int value)
        {
            for (int i = 0; i < 20; i++)
            {
                this.text[i].SetNpcPate(value);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public void Activate()
        {
            for (int i = 0; i < 18; i++)
            {
                if (m_cStateSpr[i].IsActive() == false)
                {
                    continue;
                }

                m_cStateSpr[i].Activate();
            }

            if(m_cSpecialSpr.IsActive() == true)
            {
                if (m_cSpecialSpr.CalcNextFrame(false))
                {
                    if (m_cSpecialSpr.CheckEnd())
                        m_cSpecialSpr.Release();
                }
            }

            if(this.totalDamageValue != 0
                && this.currentTime >= this.totalDamageNextTime)
            {
                this.AddTextStateCriticalDamage(this.totalDamageValue);
                this.totalDamageValue = 0;
                this.totalDamageNextTime = this.currentTime + 18;
            }

            for(int i = 0; i < 20; i++)
            {
                if (this.text[i].IsActive() == false)
                {
                    continue;
                }

                this.text[i].Activate();
            }

            this.currentTime++;
        }

        public void Update()
        {
            for(int i = 0; i < 18;i++)
            {
                if (m_cStateSpr[i].IsActive() == false)
                {
                    m_cStateSpr[i].Destroy(this.npcController);
                    continue;
                }

                m_cStateSpr[i].Initialize(this.parent);
                m_cStateSpr[i].Update();
            }

            if (m_cSpecialSpr.IsActive() == true)
            {
                m_cSpecialSpr.Initialize(this.parent);
                m_cSpecialSpr.Update();
            }
            else
            {
                m_cSpecialSpr.Destroy();
            }

            for (int i = 0; i < 20; i++)
            {
                if (this.text[i].IsActive() == false)
                {
                    this.text[i].Destroy();
                    continue;
                }

                this.text[i].Initialize(this.parent);
                this.text[i].Update();
            }
        }
    }
}
