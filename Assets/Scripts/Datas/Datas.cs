public class Datas
{
    readonly private UserDatas _userData = new UserDatas();
    readonly private EnemyDatas _enemyDatas = new EnemyDatas();
    readonly private ItemDatas _item = new ItemDatas();
    readonly private SkillDatas _skill = new SkillDatas();
    readonly private RewardDatas _rewardDatas = new RewardDatas();
    readonly private ProductDatas _productDatas = new ProductDatas();

    public UserDatas User { get { return _userData; } }
    public EnemyDatas Enemy { get {  return _enemyDatas; } }
    public ItemDatas Item { get {  return _item; } }
    public SkillDatas Skill { get {  return _skill; } }
    public RewardDatas Reward { get { return _rewardDatas; } }
    public ProductDatas Product { get { return _productDatas; } }
}
