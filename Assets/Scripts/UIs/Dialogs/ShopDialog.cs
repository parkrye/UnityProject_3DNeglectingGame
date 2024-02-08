using System.Collections.Generic;
using UnityEngine.Events;

public class ShopDialog : Dialog
{
    private Dictionary<string, Template> _templates = new Dictionary<string, Template>();
    private UnityAction<CurrencyData> _updateCurrencyAction;

    public override void Init()
    {
        base.Init();

        var userData = G.Data.User;

        var levelSlotTemplate = GetTemplate("LevelFrameTemplate");
        levelSlotTemplate.GetText("LevelText").text = $"Lv {userData.ActorData.Level}";
        levelSlotTemplate.GetImage("LevelBar").fillAmount = (float)userData.GetCurrency(CurrencyType.Exp) / (userData.ActorData.Level * 10);
        _templates.Add("LV", levelSlotTemplate);
        levelSlotTemplate.GetButton("LevelUpButton").onClick.AddListener(() => OnClickLevelUpButton("LV"));

        var statSlotTemplate = GetTemplate("StatFrameTemplate");
        var content = GetContent("StatScroll");

        var instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "HP";
        instant.GetText("StatDescription").text = $"{userData.ActorData.Hp}";
        instant.GetText("StatUpCost").text = $"Diamond {userData.ActorData.Hp * 5}";
        _templates.Add("HP", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("HP"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Move Speed";
        instant.GetText("StatDescription").text = $"{userData.ActorData.MoveSpeed}";
        instant.GetText("StatUpCost").text = $"Diamond {userData.ActorData.MoveSpeed * 5}";
        _templates.Add("MS", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("MS"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Damage";
        instant.GetText("StatDescription").text = $"{userData.ActorData.AttackDamage}";
        instant.GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackDamage * 5}";
        _templates.Add("AD", instant);
        instant.GetButton("StatUpButton").onClick.AddListener(() => OnClickLevelUpButton("AD"));

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Speed";
        instant.GetText("StatDescription").text = $"{userData.ActorData.AttackSpeed}";
        instant.GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackSpeed * 5}";
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
        var userData = G.Data.User;
        switch (target)
        {
            case "LV":
                _templates[target].GetText("LevelText").text = $"Lv {userData.ActorData.Level}";
                _templates[target].GetImage("LevelBar").fillAmount = (float)userData.GetCurrency(CurrencyType.Exp) / (userData.ActorData.Level * 10);
                break;
            case "HP":
                _templates[target].GetText("StatUpCost").text = $"Diamond {userData.ActorData.Hp * 5}";
                break;
            case "MS":
                _templates[target].GetText("StatUpCost").text = $"Diamond {userData.ActorData.MoveSpeed * 5}";
                break;
            case "AD":
                _templates[target].GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackDamage * 5}";
                break;
            case "AS":
                _templates[target].GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackSpeed * 5}";
                break;
            default:
                _templates["LV"].GetImage("LevelBar").fillAmount = (float)userData.GetCurrency(CurrencyType.Exp) / (userData.ActorData.Level * 10);
                _templates["HP"].GetText("StatUpCost").text = $"Diamond {userData.ActorData.Hp * 5}";
                _templates["MS"].GetText("StatUpCost").text = $"Diamond {userData.ActorData.MoveSpeed * 5}";
                _templates["AD"].GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackDamage * 5}";
                _templates["AS"].GetText("StatUpCost").text = $"Diamond {userData.ActorData.AttackSpeed * 5}";
                break;
        }
    }

    private void OnClickLevelUpButton(string target)
    {
        var userData = G.Data.User;

        switch (target)
        {
            default:
                break;
            case "LV":
                if (userData.TryUseCurrency(CurrencyType.Exp, userData.ActorData.Level * 10) == true)
                {
                    userData.ActorData.Level += 1;
                    UpdateCost(target);
                }
                break;
            case "HP":
                if (userData.TryUseCurrency(CurrencyType.Diamond, userData.ActorData.Hp * 5) == true)
                {
                    userData.ActorData.Hp += 1;
                    UpdateCost(target);
                }
                break;
            case "MS":
                if (userData.TryUseCurrency(CurrencyType.Diamond, userData.ActorData.MoveSpeed * 5) == true)
                {
                    userData.ActorData.MoveSpeed += 1;
                    UpdateCost(target);
                }
                break;
            case "AD":
                if (userData.TryUseCurrency(CurrencyType.Diamond, userData.ActorData.AttackDamage * 5) == true)
                {
                    userData.ActorData.AttackDamage += 1;
                    UpdateCost(target);
                }
                break;
            case "AS":
                if (userData.TryUseCurrency(CurrencyType.Diamond, userData.ActorData.AttackSpeed * 5) == true)
                {
                    userData.ActorData.AttackSpeed += 1;
                    UpdateCost(target);
                }
                break;
        }
    }

    private void UpdateCurrency(CurrencyData currencyData)
    {
        switch (currencyData.Id)
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
