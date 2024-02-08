public class Datas
{
    readonly private UserDatas _userData = new UserDatas();
    public UserDatas User { get { return _userData; } }
    readonly private EnemyDatas _enemyDatas = new EnemyDatas();
    public EnemyDatas Enemy { get {  return _enemyDatas; } }
    readonly private RewardDatas _rewardDatas = new RewardDatas();
    public RewardDatas Reward { get { return _rewardDatas; } }

    readonly private EquipmentDatas _equipmentDatas = new EquipmentDatas();
    public EquipmentDatas Equipment { get {  return _equipmentDatas; } }
}
