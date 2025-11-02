
namespace game.resource.settings.npcres.state
{
    public class Text
    {
        public enum FlyForm
        {
            criticalDamage,
            normalDamageLeft,
            normalDamageRight,
        }

        private class Info
        {
            public state.Text.FlyForm flyForm;
            public string text;
            public UnityEngine.Color color;
            public float fontMinSize;
            public float fontMaxSize;
            public int startTime;
            public int endTime;
            public float growUpToTime = 0;
            public TMPro.FontStyles fontStyles;
        }

        private UnityEngine.GameObject gameObject;
        private UnityEngine.MeshRenderer meshRendererComponent;
        private TMPro.TextMeshPro textMeshProComponent;
        private UnityEngine.RectTransform rectTransformComponent;

        private bool isActive;
        private readonly int stateIndex;
        private int updateRemaning;
        private int destroyRemaning;
        private int currentTime;

        private float npcPateOffsetY;
        private float currentFontSize;
        private UnityEngine.Vector2 currentAnchored;

        private state.Text.Info info;

        public Text(int stateIndex)
        {
            this.isActive = false;
            this.stateIndex = stateIndex;
            this.npcPateOffsetY = 0;
            this.currentAnchored = UnityEngine.Vector2.zero;
        }

        public void SetActive(bool active) => this.isActive = active;
        public bool IsActive() => this.isActive;

        public void SetInfo(
            state.Text.FlyForm flyForm,
            string text, UnityEngine.Color color, 
            float fontMinSize, float fontMaxSize,
            int startTime, int growUpToTime, int endTime,
            TMPro.FontStyles fontStyles)
        {
            state.Text.Info newInfo = new Info();

            newInfo.flyForm = flyForm;
            newInfo.text = text;
            newInfo.color = color;
            newInfo.fontMinSize = fontMinSize;
            newInfo.fontMaxSize = fontMaxSize;
            newInfo.startTime = startTime;
            newInfo.endTime = endTime;
            newInfo.growUpToTime = growUpToTime;
            newInfo.fontStyles = fontStyles;

            this.updateRemaning = 0;
            this.destroyRemaning = 0;
            this.currentTime = 0;
            this.currentFontSize = newInfo.fontMinSize;
            this.currentAnchored.y = this.npcPateOffsetY;

            switch(flyForm)
            {
                case FlyForm.criticalDamage:
                    this.currentAnchored.x = 0;
                    break;
                case FlyForm.normalDamageLeft:
                    this.currentAnchored.x = -0.25f;
                    break;
                case FlyForm.normalDamageRight:
                    this.currentAnchored.x = 0.25f;
                    break;
            }

            this.info = newInfo;
        }

        public void SetNpcPate(int value)
        {
            this.npcPateOffsetY = value / 100f;
            this.npcPateOffsetY += 0.28f;
            this.currentAnchored.y = this.npcPateOffsetY;
        }

        public void Initialize(UnityEngine.GameObject parent)
        {
            if(this.gameObject != null)
            {
                return;
            }

            this.gameObject = new UnityEngine.GameObject("state.text." + this.stateIndex);
            this.meshRendererComponent = this.gameObject.AddComponent<UnityEngine.MeshRenderer>();
            this.textMeshProComponent = this.gameObject.AddComponent<TMPro.TextMeshPro>();
            this.rectTransformComponent = this.gameObject.GetComponent<UnityEngine.RectTransform>();

            this.textMeshProComponent.fontStyle = this.info.fontStyles;
            this.textMeshProComponent.color = this.info.color;
            this.textMeshProComponent.alignment = TMPro.TextAlignmentOptions.Center;
            this.textMeshProComponent.horizontalAlignment = TMPro.HorizontalAlignmentOptions.Center;
            this.textMeshProComponent.fontSize = this.info.fontMinSize;
            this.textMeshProComponent.text = this.info.text;

            this.textMeshProComponent.fontMaterial.EnableKeyword(TMPro.ShaderUtilities.Keyword_Underlay);
            this.textMeshProComponent.fontMaterial.SetColor(TMPro.ShaderUtilities.ID_UnderlayColor, UnityEngine.Color.black);
            this.textMeshProComponent.fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_UnderlayOffsetX, 0);
            this.textMeshProComponent.fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_UnderlayOffsetY, 0);
            this.textMeshProComponent.fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_UnderlayDilate, 0.5f);
            this.textMeshProComponent.fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_UnderlaySoftness, 0);
            this.textMeshProComponent.UpdateMeshPadding();

            this.rectTransformComponent.anchoredPosition = this.currentAnchored;

            this.gameObject.transform.SetParent(parent.transform, false);
        }

        public void Activate()
        {
            if (this.currentTime < this.info.startTime)
            {
                this.currentTime++;
                return;
            }

            if(this.currentTime > this.info.endTime)
            {
                this.SetActive(false);
                this.destroyRemaning++;
                return;
            }

            int runTime = this.currentTime - this.info.startTime;

            if (runTime <= this.info.growUpToTime)
            {
                float currentPercentMax = ((runTime * 100) / this.info.growUpToTime);
                float fontRange = this.info.fontMaxSize - this.info.fontMinSize;
                float fontSize = (fontRange / 100) * currentPercentMax;

                this.currentFontSize = this.info.fontMinSize + fontSize;
                this.currentAnchored.y += 0.12f;
            }
            else
            {
                this.currentFontSize = this.info.fontMinSize;
                this.currentAnchored.y += 0.02f;
            }

            switch (this.info.flyForm)
            {
                case FlyForm.normalDamageLeft:
                case FlyForm.normalDamageRight:
                    int totalTime = this.info.endTime - this.info.startTime;
                    float runPercent = (runTime * 100f) / totalTime;
                    float speedPercent = ((runPercent * 100) / 80);
                    const float diffPerFrame = 0.05f;

                    if (runPercent < 80)
                    {
                        float nowSpeed = (diffPerFrame / 100) * (100 - speedPercent);
                        this.currentAnchored.y += nowSpeed;
                    }
                    else
                    {
                        float nowSpeed = (diffPerFrame / 100) * speedPercent;
                        this.currentAnchored.y -= nowSpeed;
                    }
                    break;
            }

            switch (this.info.flyForm)
            {
                case FlyForm.normalDamageLeft:
                    switch (skill.Static.g_Random(4))
                    {
                        case 0:
                            this.currentAnchored.x -= 0.01f;
                            break;
                        case 1:
                            this.currentAnchored.x -= 0.02f;
                            break;
                        case 2:
                            this.currentAnchored.x -= 0.04f;
                            break;
                        case 3:
                            this.currentAnchored.x -= 0.06f;
                            break;
                    }
                    break;

                case FlyForm.normalDamageRight:
                    switch (skill.Static.g_Random(4))
                    {
                        case 0:
                            this.currentAnchored.x += 0.01f;
                            break;
                        case 1:
                            this.currentAnchored.x += 0.02f;
                            break;
                        case 2:
                            this.currentAnchored.x += 0.04f;
                            break;
                        case 3:
                            this.currentAnchored.x += 0.06f;
                            break;
                    }
                    break;
            }

            //UnityEngine.Debug.Log("this.npcPateOffsetY: " + this.npcPateOffsetY);
            //UnityEngine.Debug.Log("achorX: " + this.currentAnchored.x + ", form: " + this.info.flyForm);

            this.currentTime++;
            this.updateRemaning++;
        }

        public void Update()
        {
            if(this.updateRemaning <= 0)
            {
                return;
            }

            this.textMeshProComponent.fontSize = this.currentFontSize;
            this.rectTransformComponent.anchoredPosition = this.currentAnchored;

            this.updateRemaning--;
        }

        public void Destroy()
        {
            if(this.destroyRemaning <= 0)
            {
                return;
            }

            UnityEngine.GameObject.Destroy(this.gameObject);

            this.destroyRemaning--;
        }
    }
}
