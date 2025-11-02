using game.resource.settings.skill;
using Photon.ShareLibrary.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    [SerializeField]
    public Image SkillIcon;
    [SerializeField]
    public Text nameSkill;
    [SerializeField]
    public Text levelSkill;
    [SerializeField]
    public Button button;
    [SerializeField]
    public Image image;
    [SerializeField]
    public Button btnUdpateSkill;

    private PlayerSkill skill;
    private string ImagePath = "WorldGameUI/Buttons/btn_fight";
    private string ImageSkillPath = "SkillIcon/";

    private void Start()
    {
        button.onClick.AddListener(() => PopUpCanvas.instance.OpenSkillDetail(skill));
        image.GetComponent<Button>().onClick.AddListener(() => PopUpCanvas.instance.OpenSkillDetail(skill));
        btnUdpateSkill.onClick.AddListener(() => Debug.Log("Update SKILL"));
    }

    public void SetUpSkillSetting(PlayerSkill skill)
    {

        this.skill = skill;
        SkillSetting skillSetting = SkillSetting.Get(skill.id, skill.level);
        nameSkill.text = skillSetting.m_szName;
        levelSkill.text = skill.level + " / " + skillSetting.m_maxLevel;
        Sprite sprite = Resources.Load<Sprite>(ImageSkillPath + skill.id);
        if (sprite == null)
        {
            sprite = Game.Resource(skillSetting.m_szSkillIcon).Get<UnityEngine.Sprite>(0);
            nameSkill.text = skillSetting.m_szName;
        }
        SkillIcon.sprite = sprite;
    }

    public void RemoveSkillData()
    {
        this.skill = null;
        nameSkill.text = "";
        levelSkill.text = "";
        Sprite loadedImage = Resources.Load<Sprite>(ImagePath);
        image.sprite = loadedImage;
    }
}
