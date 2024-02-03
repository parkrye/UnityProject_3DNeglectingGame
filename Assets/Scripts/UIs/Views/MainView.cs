using UnityEngine;
using UnityEngine.Events;

public class MainView : View
{
    private LevelUpDialog _levelUpDialog;
    private UnityAction<CurrencyData> _updateCurrencyAction;

    protected override void Init()
    {
        base.Init();

        var canvasList = FindObjectsOfType<Canvas>();
        foreach(var canvas in canvasList)
        {
            var levelUpDialog = canvas.transform.Find("LevelUpDialog");
            if(levelUpDialog != null)
            {
                _levelUpDialog = levelUpDialog.GetComponent<LevelUpDialog>();
            }
        }

        GetButton("LevelUpButton").onClick.AddListener(OnLevelUpButtonClick);
        GetButton("SkillButton").onClick.AddListener(OnSkillButtonClick);
        GetButton("EquipmentButton").onClick.AddListener(OnEquipmentButtonClick);
        GetButton("InventoryButton").onClick.AddListener(OnInventoryButtonClick);
        GetButton("ShopButton").onClick.AddListener(OnShopButtonClick);
        GetButton("MoreButton").onClick.AddListener(OnMoreButtonClick);

        GetImage("More").gameObject.SetActive(false);

        GetText("StageLevelText").text = $"Stage {G.CurrentStage.StageLevel}";

        _updateCurrencyAction = UpdateMoreCurreny;
        G.CurrentStage.PlayerActor.HPRatioEvent.AddListener(LifeGaugeEvent);
    }

    public override void OpenView()
    {
        base.OpenView();
        var userData = G.Data.User;
        GetText("GoldCount").text = $"{userData.GetCurrency(CurrencyType.Gold)}";
        GetText("DiamondCount").text = $"{userData.GetCurrency(CurrencyType.Diamond)}";
        GetText("RubyCount").text = $"{userData.GetCurrency(CurrencyType.Ruby)}";
        GetText("EXPCount").text = $"{userData.GetCurrency(CurrencyType.Exp)}";
        G.Data.User.CurrencyUpdate.AddListener(_updateCurrencyAction);
    }

    public override void CloseView()
    {
        base.CloseView();
        G.Data.User.CurrencyUpdate.RemoveListener(_updateCurrencyAction);
    }

    private void OnLevelUpButtonClick()
    {
        G.UI.OpenDialog(_levelUpDialog);
    }

    private void OnSkillButtonClick()
    {

    }

    private void OnEquipmentButtonClick()
    {

    }

    private void OnInventoryButtonClick()
    {

    }

    private void OnShopButtonClick()
    {

    }

    private void OnMoreButtonClick()
    {
        GetImage("More").gameObject.SetActive(GetImage("More").gameObject.activeSelf == false);
    }

    private void LifeGaugeEvent(float ratio, bool _)
    {
        GetImage("LifeGauge").fillAmount = ratio;
    }

    private void EnergyGaugeEvent(float ratio, bool _)
    {
        GetImage("EnergyGauge").fillAmount = ratio;
    }

    private void UpdateMoreCurreny(CurrencyData currencyData)
    {
        switch (currencyData.Id)
        {
            case (int)CurrencyType.Gold:
                GetText("GoldCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Diamond:
                GetText("DiamondCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Ruby:
                GetText("RubyCount").text = $"{currencyData.Count}";
                break;
            case (int)CurrencyType.Exp:
                GetText("EXPCount").text = $"{currencyData.Count}";
                break;
        }
    }
}
