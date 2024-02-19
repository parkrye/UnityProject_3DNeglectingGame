public class ShopDialog : Dialog
{
    public override void Init()
    {
        base.Init();

        var products = G.Data.Product;

        var productSlotTemplate = GetTemplate("ProductTemplate");
        var content = GetContent("ProductScroll");

        foreach (var (id, product) in products.Products)
        {
            var thisId = id;
            var instant = Instantiate(productSlotTemplate, content);
            instant.GetText("Name").text = product.Name;
            instant.GetText("Description").text = product.Descripntion;
            instant.GetText("Cost").text = $"Gold {product.Cost}";
            instant.GetButton("Button").onClick.AddListener(() => OnClickLevelUpButton(thisId));
        }

        productSlotTemplate.gameObject.SetActive(false);
    }

    public override void OpenDialog()
    {
        base.OpenDialog();
    }

    public override void CloseDialog()
    {
        base.CloseDialog();
    }

    private void OnClickLevelUpButton(int id)
    {
        var product = G.Data.Product.GetProduct(id);
        if (product == null)
            return;

        switch (product.ProductType)
        {
            case ProductType.Currency:
                if (G.Data.User.TryUseCurrency(product.CostType, product.Cost) == true)
                {
                    G.Data.User.AddCurrency((CurrencyType)product.TargetId, product.Count);
                }
                break;
            case ProductType.Skill:
                if (G.Data.User.TryUseCurrency(product.CostType, product.Cost) == true)
                {
                    G.Data.Skill.GetSkillData(product.TargetId).Level++;
                }
                break;
            case ProductType.Item:
                if (G.Data.User.TryUseCurrency(product.CostType, product.Cost) == true)
                {
                    G.Data.Item.GetItemData(product.TargetId).Level++;
                }
                break;
        }
    }
}
