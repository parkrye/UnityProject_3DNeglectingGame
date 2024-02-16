using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine.Events;

public class LevelUpDialog : Dialog
{
    private Dictionary<string, Template> _templates = new Dictionary<string, Template>();
    private UnityAction<CurrencyData> _updateCurrencyAction;

    public override void Init()
    {
        base.Init();

        var userData = G.Data.User;

        var levelSlotTemplate = GetTemplate("LevelFrameTemplate");
        _templates.Add("LV", levelSlotTemplate);
        levelSlotTemplate.GetButton("LevelUpButton").onClick.AddListener(() => OnClickLevelUpButton("LV"));

        var statSlotTemplate = GetTemplate("StatFrameTemplate");
        var content = GetContent("StatScroll");

        var instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "HP";
        _templates.Add("HP", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("HP"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Move Speed";
        _templates.Add("MS", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("MS"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Damage";
        _templates.Add("AD", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("AD"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Speed";
        _templates.Add("AS", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("AS"));

        statSlotTemplate.gameObject.SetActive(false);

        _updateCurrencyAction = UpdateCurrency;
    }

    public override void OpenDialog()
    {
        base.OpenDialog();
        UpdateCost();
        G.Data.User.CurrencyUpdate.AddListener(_updateCurrencyAction);
    }

    public override void CloseDialog()
    {
        base.CloseDialog();
        G.Data.User.CurrencyUpdate.RemoveListener(_updateCurrencyAction);
    }

    private void UpdateCost(string target = "")
    {
        var actorData = G.Data.User.ActorData;
        switch (target)
        {
            case "LV":
                _templates[target].GetText("LevelText").text = $"Lv {actorData.Level}";
                _templates[target].GetImage("LevelBar").fillAmount = actorData.GetLevelUpCost(Status.Level);
                break;
            case "HP":
                _templates[target].GetText("StatDescription").text = $"{actorData.Hp}";
                _templates[target].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.Hp)}";
                break;
            case "MS":
                _templates[target].GetText("StatDescription").text = $"{actorData.MoveSpeed}";
                _templates[target].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.MoveSpeed)}";
                break;
            case "AD":
                _templates[target].GetText("StatDescription").text = $"{actorData.AttackDamage}";
                _templates[target].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.AttackDamage)}";
                break;
            case "AS":
                _templates[target].GetText("StatDescription").text = $"{actorData.AttackSpeed}";
                _templates[target].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.AttackSpeed)}";
                break;
            default:
                _templates["LV"].GetImage("LevelBar").fillAmount = actorData.GetLevelUpCost(Status.Level);
                _templates["HP"].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.Hp)}";
                _templates["MS"].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.MoveSpeed)}";
                _templates["AD"].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.AttackDamage)}";
                _templates["AS"].GetText("StatUpCost").text = $"Diamond {(int)actorData.GetLevelUpCost(Status.AttackSpeed)}";
                break;
        }
    }

    private void OnClickLevelUpButton(string target)
    {
        var actorData = G.Data.User.ActorData;

        switch (target)
        {
            default:
                break;
            case "LV":
                if (actorData.TryLevelUp(Status.Level) == true)
                {
                    UpdateCost(target);
                }
                break;
            case "HP":
                if (actorData.TryLevelUp(Status.Hp) == true)
                {
                    UpdateCost(target);
                }
                break;
            case "MS":
                if (actorData.TryLevelUp(Status.MoveSpeed) == true)
                {
                    UpdateCost(target);
                }
                break;
            case "AD":
                if (actorData.TryLevelUp(Status.AttackDamage) == true)
                {
                    UpdateCost(target);
                }
                break;
            case "AS":
                if (actorData.TryLevelUp(Status.AttackSpeed) == true)
                {
                    UpdateCost(target);
                }
                break;
        }
    }

    private void UpdateCurrency(CurrencyData currencyData)
    {
        switch(currencyData.Id)
        {
            case (int)CurrencyType.Gold:
                break;
            case (int)CurrencyType.Diamond:
                break;
            case (int)CurrencyType.Ruby:
                break;
            case (int)CurrencyType.Exp:
                UpdateCost("LV");
                break;
        }
    }
}
