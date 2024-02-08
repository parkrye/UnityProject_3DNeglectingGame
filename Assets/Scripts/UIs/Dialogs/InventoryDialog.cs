using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine.Events;

public class InventoryDialog : Dialog
{
    private Dictionary<string, Template> _templates = new Dictionary<string, Template>();
    private UnityAction<CurrencyData> _updateCurrencyAction;

    private int _selectIndex = 0;
    private Dictionary<int, Template> _inventorySlots = new Dictionary<int, Template>();

    public override void Init()
    {
        base.Init();

        var equipmentDatas = G.Data.Equipment;
        var itemTemplate = GetTemplate("ItemTemplate");
        var content = GetContent("IventoryScroll");
        foreach (var equipmentId in equipmentDatas.EquipmentData.Keys)
        {
            var instant = Instantiate(itemTemplate, content);
            var equipment = equipmentDatas.GetEquipmentData(equipmentId);
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
        if (weapon != null)
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
        if (armor != null)
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
        if (accessory != null)
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
        var firstItem = G.Data.Equipment.GetEquipmentData(_selectIndex);
        if (firstItem == null)
            return;

        selected.GetText("ItemName").text = firstItem.Name;
        selected.GetText("ItemLevel").text = firstItem.Level.ToString();
        selected.GetText("ItemType").text = ((EquipmentType)firstItem.Type).ToString();
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
                var equipment = G.Data.Equipment.GetEquipmentData(slotId);
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
            var equipment = G.Data.Equipment.GetEquipmentData(id);
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
        var equipment = G.Data.Equipment.GetEquipmentData(_selectIndex);
        if (equipment == null)
            return;
        if (equipment.Level == 0)
            return;

        switch ((EquipmentType)equipment.Type)
        {
            case EquipmentType.Weapon:
                G.Data.User.PlayerData.Weapon = equipment;
                break;
            case EquipmentType.Armor:
                G.Data.User.PlayerData.Armor = equipment;
                break;
            case EquipmentType.Accessory:
                G.Data.User.PlayerData.Accessory = equipment;
                break;
        }
    }

    private void OnItemEnhanceClick()
    {
        var equipment = G.Data.Equipment.GetEquipmentData(_selectIndex);
        if (equipment == null)
            return;
        if (equipment.Level == 0)
            return;

        if (G.Data.User.TryUseCurrency(CurrencyType.Ruby, (equipment.Level) * 10))
        {
            equipment.Level++;
        }
    }
}
