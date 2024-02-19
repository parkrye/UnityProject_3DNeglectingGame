using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Progress;

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
        (_selectIndex, _) = G.Data.Item.Items.First();
    }

    private void UpdateEquipItem()
    {
        var playerData = G.Data.User.PlayerData;

        var weapon = playerData.Weapon;
        var weaponTemplate = GetTemplate("Weapon");
        var isCorrect = weapon.IsCorrect();
        weaponTemplate.GetImage("ItemIcon").enabled = isCorrect;
        weaponTemplate.GetText("ItemName").text = weapon.Name;
        weaponTemplate.GetText("ItemLevel").text = weapon.Level.ToString();
        weaponTemplate.GetButton("ItemButton").onClick.RemoveAllListeners();
        weaponTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(weapon.Id));

        var armor = playerData.Armor;
        var armorTemplate = GetTemplate("Armor");
        isCorrect = weapon.IsCorrect();
        armorTemplate.GetImage("ItemIcon").enabled = isCorrect;
        armorTemplate.GetText("ItemName").text = weapon.Name;
        armorTemplate.GetText("ItemLevel").text = weapon.Level.ToString();
        armorTemplate.GetButton("ItemButton").onClick.RemoveAllListeners();
        armorTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(weapon.Id));

        var accessory = playerData.Armor;
        var accessoryTemplate = GetTemplate("Accessory");
        isCorrect = weapon.IsCorrect();
        accessoryTemplate.GetImage("ItemIcon").enabled = isCorrect;
        accessoryTemplate.GetText("ItemName").text = weapon.Name;
        accessoryTemplate.GetText("ItemLevel").text = weapon.Level.ToString();
        accessoryTemplate.GetButton("ItemButton").onClick.RemoveAllListeners();
        accessoryTemplate.GetButton("ItemButton").onClick.AddListener(() => OnItemSlotClick(weapon.Id));
    }

    private void UpdateSelectItem()
    {
        var selected = GetTemplate("Select");
        var item = G.Data.Item.GetItemData(_selectIndex);
        if (item == null)
            (_selectIndex, item) = G.Data.Item.Items.First();

        selected.GetText("ItemName").text = item.Name;
        selected.GetText("ItemLevel").text = item.Level.ToString();
        selected.GetText("ItemType").text = ((ItemType)item.Type).ToString();
        selected.GetText("ItemValue").text = item.Value.ToString();
        selected.GetButton("EquipButton").gameObject.SetActive(item.IsCorrect());
        selected.GetButton("EnhanceButton").gameObject.SetActive(item.IsCorrect());
        selected.GetText("EnhanceText").text = $"Enhance\nRuby {item.Level * item.Value}";
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
        if (equipment.HasItem() == false)
            return;

        if (G.Data.User.TryUseCurrency(CurrencyType.Emerald, (equipment.Level + equipment.Value) * 5))
        {
            equipment.Level++;
        }
    }
}
