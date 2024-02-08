using System.Collections.Generic;

public class InventoryDialog : Dialog
{
    private int _selectIndex = 0;
    private Dictionary<int, Template> _inventorySlots = new Dictionary<int, Template>();

    public override void Init()
    {
        base.Init();

        var equipmentDatas = G.Data.Item;
        var itemTemplate = GetTemplate("ItemTemplate");
        var content = GetContent("InventoryScroll");
        foreach (var equipmentId in equipmentDatas.Items.Keys)
        {
            var instant = Instantiate(itemTemplate, content);
            var equipment = equipmentDatas.GetItemData(equipmentId);
            instant.GetText("ItemName").text = equipment.Name;
            instant.GetText("ItemLevel").text = equipment.Level.ToString();
            instant.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(equipment.Id));
            _inventorySlots.Add(equipment.Id, instant);
        }
        itemTemplate.gameObject.SetActive(false);

        var equipButton = GetButton("EquipButton");
        equipButton.onClick.AddListener(OnItemEquipClick);
        var enhanceButton = GetButton("EnhanceButton");
        enhanceButton.onClick.AddListener(OnItemEnhanceClick);
    }

    public override void OpenDialog()
    {
        base.OpenDialog();

        UpdateEquipItem();
        UpdateSelectItem();
        UpdateInventory();
    }

    public override void CloseDialog()
    {
        base.CloseDialog();
    }

    private void UpdateEquipItem()
    {
        var playerData = G.Data.User.PlayerData;

        var weapon = playerData.Weapon;
        var weaponTemplate = GetTemplate("Weapon");
        if (weapon.IsCorrect())
        {
            weaponTemplate.GetText("ItemName").text = weapon.Name;
            weaponTemplate.GetText("ItemLevel").text = weapon.Level.ToString();
            weaponTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(weapon.Id));
        }
        else
        {
            weaponTemplate.gameObject.SetActive(false);
        }
        var armor = playerData.Armor;
        var armorTemplate = GetTemplate("Armor");
        if (armor.IsCorrect())
        {
            armorTemplate.GetText("ItemName").text = armor.Name;
            armorTemplate.GetText("ItemLevel").text = armor.Level.ToString();
            weaponTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(armor.Id));
        }
        else
        {
            armorTemplate.gameObject.SetActive(false);
        }
        var accessory = playerData.Armor;
        var accessoryTemplate = GetTemplate("Accessory");
        if (accessory.IsCorrect())
        {
            accessoryTemplate.GetText("ItemName").text = accessory.Name;
            accessoryTemplate.GetText("ItemLevel").text = accessory.Level.ToString();
            weaponTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(accessory.Id));
        }
        else
        {
            accessoryTemplate.gameObject.SetActive(false);
        }
    }

    private void UpdateSelectItem()
    {
        var selected = GetTemplate("Select");
        var firstItem = G.Data.Item.GetItemData(_selectIndex);
        if (firstItem == null)
            return;

        selected.GetText("ItemName").text = firstItem.Name;
        selected.GetText("ItemLevel").text = firstItem.Level.ToString();
        selected.GetText("ItemType").text = ((ItemType)firstItem.Type).ToString();
        selected.GetText("ItemValue").text = firstItem.Value.ToString();
    }

    private void UpdateInventory(int id = -1)
    {
        if(id < 0)
        {
            foreach(var slotId in _inventorySlots.Keys)
            {
                if (_inventorySlots.TryGetValue(slotId, out var slot) == false)
                    continue;
                var equipment = G.Data.Item.GetItemData(slotId);
                if (equipment == null)
                    continue;

                slot.GetText("ItemName").text = equipment.Name;
                slot.GetText("ItemLevel").text = equipment.Level.ToString();
            }
        }
        else
        {
            if (_inventorySlots.TryGetValue(id, out var slot) == false)
                return;
            var equipment = G.Data.Item.GetItemData(id);
            if (equipment == null) 
                return;

            slot.GetText("ItemName").text = equipment.Name;
            slot.GetText("ItemLevel").text = equipment.Level.ToString();
        }
    }

    private void OnItemSlotClick(int id)
    {
        if (_selectIndex == id)
            return;

        _selectIndex = id;

        UpdateSelectItem();
    }

    private void OnItemEquipClick()
    {
        var equipment = G.Data.Item.GetItemData(_selectIndex);
        if (equipment == null)
            return;
        if (equipment.Level == 0)
            return;

        switch ((ItemType)equipment.Type)
        {
            case ItemType.Weapon:
                G.Data.User.PlayerData.Weapon = equipment;
                break;
            case ItemType.Armor:
                G.Data.User.PlayerData.Armor = equipment;
                break;
            case ItemType.Accessory:
                G.Data.User.PlayerData.Accessory = equipment;
                break;
        }
    }

    private void OnItemEnhanceClick()
    {
        var equipment = G.Data.Item.GetItemData(_selectIndex);
        if (equipment == null)
            return;
        if (equipment.Level == 0)
            return;

        if (G.Data.User.TryUseCurrency(CurrencyType.Ruby, (equipment.Level + equipment.Value) * 5))
        {
            equipment.Level++;
        }
    }
}
