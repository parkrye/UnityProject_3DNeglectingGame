using System.Collections.Generic;
using System.Linq;

public class SkillDialog : Dialog
{
    private int _selectSlotIndex = 0;
    private int _selectIndex = 0;
    private Dictionary<int, Template> _skillSlots = new Dictionary<int, Template>();

    public override void Init()
    {
        base.Init();

        var skillDatas = G.Data.Skill;
        var skillTemplate = GetTemplate("SkillTemplate");
        var content = GetContent("SkillScroll");
        foreach (var skillId in skillDatas.Skills.Keys)
        {
            var instant = Instantiate(skillTemplate, content);
            var skill = skillDatas.GetSkillData(skillId);
            instant.GetText("SkillName").text = skill.Name;
            instant.GetText("SkillLevel").text = skill.Level.ToString();
            instant.GetButton("SkillButton").onClick.AddListener(() => OnSkillListClick(skill.Id));
            _skillSlots.Add(skill.Id, instant);
        }
        skillTemplate.gameObject.SetActive(false);

        var skill1Template = GetTemplate("Slot1");
        skill1Template.GetButton("SkillButton").onClick.AddListener(() => OnSkillSlotClick(0));
        var skill2Template = GetTemplate("Slot2");
        skill2Template.GetButton("SkillButton").onClick.AddListener(() => OnSkillSlotClick(1));
        var skill3Template = GetTemplate("Slot3");
        skill3Template.GetButton("SkillButton").onClick.AddListener(() => OnSkillSlotClick(2));

        var equipButton = GetButton("EquipButton");
        equipButton.onClick.AddListener(OnSkillEquipClick);
        var enhanceButton = GetButton("EnhanceButton");
        enhanceButton.onClick.AddListener(OnSkillEnhanceClick);
    }

    public override void OpenDialog()
    {
        base.OpenDialog();

        UpdateSkillSlots();
        UpdateSelectSkill();
        UpdateSkillist();
    }

    public override void CloseDialog()
    {
        base.CloseDialog();
        (_selectIndex, _) = G.Data.Skill.Skills.First();
    }

    private void UpdateSkillSlots()
    {
        var playerData = G.Data.User.PlayerData;

        var skill1 = playerData.Skill1;
        var skill1Template = GetTemplate("Slot1");
        var isCorrect = skill1 == null ? false : skill1.IsCorrect();
        skill1Template.GetImage("SkillIcon").enabled = isCorrect;
        skill1Template.GetText("SkillName").text = isCorrect ? skill1.Name : string.Empty;
        skill1Template.GetText("SkillLevel").text = isCorrect ? $"Lv.{skill1.Level}" : string.Empty;

        var skill2 = playerData.Skill2;
        var skill2Template = GetTemplate("Slot2");
        isCorrect = skill2 == null ? false : skill2.IsCorrect();
        skill2Template.GetImage("SkillIcon").enabled = isCorrect;
        skill2Template.GetText("SkillName").text = isCorrect ? skill2.Name : string.Empty;
        skill2Template.GetText("SkillLevel").text = isCorrect ? $"Lv.{skill2.Level}" : string.Empty;

        var skill3 = playerData.Skill3;
        var skill3Template = GetTemplate("Slot3");
        isCorrect = skill3 == null ? false : skill3.IsCorrect();
        skill3Template.GetImage("SkillIcon").enabled = isCorrect;
        skill3Template.GetText("SkillName").text = isCorrect ? skill3.Name : string.Empty;
        skill3Template.GetText("SkillLevel").text = isCorrect ? $"Lv.{skill3.Level}" : string.Empty;
    }

    private void UpdateSelectSkill()
    {
        var selected = GetTemplate("Select");
        var skill = G.Data.Skill.GetSkillData(_selectIndex);
        if (skill == null)
            (_selectIndex, skill) = G.Data.Skill.Skills.First();

        selected.GetText("SkillName").text = skill.Name;
        selected.GetText("SkillLevel").text = $"Lv.{skill.Level}";
        selected.GetText("Description").text = skill.Description;
        selected.GetButton("EquipButton").gameObject.SetActive(skill.IsCorrect());
        selected.GetButton("EnhanceButton").gameObject.SetActive(skill.IsCorrect());
        selected.GetText("EnhanceText").text = $"Enhance\nRuby {skill.Level * 5}";
    }

    private void UpdateSkillist(int id = -1)
    {
        if (id < 0)
        {
            foreach (var slotId in _skillSlots.Keys)
            {
                if (_skillSlots.TryGetValue(slotId, out var slot) == false)
                    continue;
                var skill = G.Data.Skill.GetSkillData(slotId);
                if (skill == null)
                    continue;

                slot.GetText("SkillName").text = skill.Name;
                slot.GetText("SkillLevel").text = $"Lv.{skill.Level}";
            }
        }
        else
        {
            if (_skillSlots.TryGetValue(id, out var slot) == false)
                return;
            var skill = G.Data.Skill.GetSkillData(id);
            if (skill == null)
                return;

            slot.GetText("SkillName").text = skill.Name;
            slot.GetText("SkillLevel").text = $"Lv.{skill.Level}";
        }
    }

    private void OnSkillSlotClick(int slot)
    {
        if (_selectSlotIndex == slot)
            return;

        _selectSlotIndex = slot;

        UpdateSelectSkill();
    }

    private void OnSkillListClick(int id)
    {
        if (_selectIndex == id)
            return;

        _selectIndex = id;

        UpdateSelectSkill();
    }

    private void OnSkillEquipClick()
    {
        var skill = G.Data.Skill.GetSkillData(_selectIndex);
        if (skill == null)
            return;
        if (skill.HasSkill() == false)
            return;

        switch (_selectSlotIndex)
        {
            case 0:
                G.Data.User.PlayerData.SetSkill(0, G.Data.User.PlayerData.Skill1, skill);
                break;
            case 1:
                G.Data.User.PlayerData.SetSkill(1, G.Data.User.PlayerData.Skill2, skill);
                break;
            case 2:
                G.Data.User.PlayerData.SetSkill(2, G.Data.User.PlayerData.Skill3, skill);
                break;
        }

        UpdateSkillSlots();
    }

    private void OnSkillEnhanceClick()
    {
        var skill = G.Data.Skill.GetSkillData(_selectIndex);
        if (skill == null)
            return;
        if (skill.HasSkill() == false)
            return;

        if (G.Data.User.TryUseCurrency(CurrencyType.Ruby, skill.Level * 5))
        {
            skill.Level++;
            UpdateSelectSkill();
        }
    }
}
