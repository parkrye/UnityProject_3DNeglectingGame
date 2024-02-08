using System.Collections.Generic;

public class ItemDatas
{

    private Dictionary<int, ItemData> _items = new Dictionary<int, ItemData>();
    public Dictionary<int, ItemData> Items { get { return _items; } set { _items = value; } }

    public void AddEquipmentData(ItemData itemData)
    {
        _items[itemData.Id] = itemData;
    }

    public ItemData GetItemData(int id)
    {
        if (_items.ContainsKey(id) == false)
            return null;

        return _items[id].Clone();
    }
}
