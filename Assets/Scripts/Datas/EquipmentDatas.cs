using System.Collections.Generic;

public class EquipmentDatas
{

    private Dictionary<int, EquipmentData> _equipmentData = new Dictionary<int, EquipmentData>();
    public Dictionary<int, EquipmentData> EquipmentData { get { return _equipmentData; } set { _equipmentData = value; } }

    public void AddEquipmentData(EquipmentData equipmentData)
    {
        _equipmentData[equipmentData.Id] = equipmentData;
    }

    public EquipmentData GetEquipmentData(int id)
    {
        if (_equipmentData.ContainsKey(id) == false)
            return null;

        return _equipmentData[id].Clone();
    }
}
