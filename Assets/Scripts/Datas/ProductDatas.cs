using System.Collections.Generic;

public class ProductDatas
{
    private Dictionary<int, ProductData> _products = new Dictionary<int, ProductData>();
    public Dictionary<int, ProductData> Products { get { return _products; } }

    public void AddProductTable(ProductData rewardData)
    {
        _products[rewardData.Id] = rewardData;
    }

    public ProductData GetProduct(int id)
    {
        if (_products.ContainsKey(id))
            return _products[id];

        return null;
    }
}