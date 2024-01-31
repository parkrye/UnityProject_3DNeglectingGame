public class Datas
{
    private UserDatas _userData = new UserDatas();
    public UserDatas User { get { return _userData; } }
    private EnemyDatas _enemyDatas = new EnemyDatas();
    public EnemyDatas Enemy { get {  return _enemyDatas; } }
    private RewardDatas _rewardDatas = new RewardDatas();
    public RewardDatas Reward { get { return _rewardDatas; } }
}
