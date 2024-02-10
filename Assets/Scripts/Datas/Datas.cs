public class Datas
{
    readonly private UserDatas _userData = new UserDatas();
    public UserDatas User { get { return _userData; } }
    readonly private EnemyDatas _enemyDatas = new EnemyDatas();
    public EnemyDatas Enemy { get {  return _enemyDatas; } }
    readonly private RewardDatas _rewardDatas = new RewardDatas();
    public RewardDatas Reward { get { return _rewardDatas; } }

    readonly private ItemDatas _item = new ItemDatas();
    public ItemDatas Item { get {  return _item; } }

    readonly private SkillDatas _skill = new SkillDatas();
    public SkillDatas Skill { get {  return _skill; } }
}
