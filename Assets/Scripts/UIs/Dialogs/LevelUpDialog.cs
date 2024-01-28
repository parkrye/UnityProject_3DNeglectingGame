public class LevelUpDialog : Dialog
{
    protected override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        var statSlotTemplate = GetTemplate("StatFrameTemplate");
        var content = GetContent("StatScroll");
        var playerActor = Global.Datas.UserData.ActorData;

        var instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "HP";
        instant.GetText("StatDescription").text = $"{playerActor.Hp}";
        instant.GetText("StatUpCost").text = "10";

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Move Speed";
        instant.GetText("StatDescription").text = $"{playerActor.MoveSpeed}";
        instant.GetText("StatUpCost").text = "10";

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Damage";
        instant.GetText("StatDescription").text = $"{playerActor.AttackDamage}";
        instant.GetText("StatUpCost").text = "10";

        instant = Instantiate(statSlotTemplate, content);
        instant.GetText("StatName").text = "Attack Speed";
        instant.GetText("StatDescription").text = $"{playerActor.AttackSpeed}";
        instant.GetText("StatUpCost").text = "10";

        statSlotTemplate.gameObject.SetActive(false);
    }
}
