using System.Collections;
using System.Collections.Generic;
using game.resource.settings.skill;
using Photon.ShareLibrary.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetail : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Image SkillIcon;
    [SerializeField]
    public Text nameSkill;
    [SerializeField]
    public Text levelSkill;
    [SerializeField]
    public Text skillDetail;
    [SerializeField]
    public GameObject btnCancel;
    [SerializeField]
    public GameObject panelSkill1;
    [SerializeField]
    public GameObject panelSkill2;

    private PlayerSkill skill;
    private int location;
    private bool isSkillActive2;

    private string ImageSkillPath = "SkillIcon/";

    private void Start()
    {
        btnCancel.GetComponent<Button>().onClick.AddListener(() => RemoveSkillActive());
    }

    public void SetUpSkillSetting(PlayerSkill skill, int location, bool isSkillActive2)
    {
        this.skill = skill;
        this.location = location;
        this.isSkillActive2 = isSkillActive2;
        UIChange();
        InitSkillDetail();
    }

    void RemoveSkillActive()
    {
        PopUpCanvas.instance.RemoveSkill(location);
    }

    public void UpdateUIChange(int location)
    {
        this.location = location;
        UIChange();
    }

    void UIChange()
    {
        if (location > -1)
        {
            btnCancel.SetActive(true);
            panelSkill1.SetActive(false);
            panelSkill2.SetActive(false);
        }
        else
        {
            btnCancel.SetActive(false);
            panelSkill1.SetActive(!isSkillActive2);
            panelSkill2.SetActive(isSkillActive2);
        }
    }

    void InitSkillDetail()
    {
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

        // Skill detail
        string text = skillSetting.m_property + "\n" + skillSetting.m_szSkillDesc + "\n" + skillSetting.GetDescription();
        skillDetail.text = text;
    }

    public void UseSkill(int location)
    {
        PopUpCanvas.instance.SetUPSkillLocation(skill, location);
    }
}
