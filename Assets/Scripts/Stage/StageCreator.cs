using UnityEngine;

public class StageCreator : MonoBehaviour
{
    private void Start()
    {
        CreateStage();
        SettingUI();
        Destroy(gameObject);
    }

    private void CreateStage()
    {
        var stage = Resource.Instantiate<Stage>("Stage", Vector3.zero, Quaternion.identity, null);
        stage.Initialize();

        var player = Resource.Load<PlayerActor>("Actors/Player");
        if (player == null)
            Debug.LogError("Player Actor is Null!");
        stage.SpawnPlayer(player);

        var enemyCount = 1;
        while (enemyCount > 0)
        {
            var enemy = Resource.Load<EnemyActor>($"Actors/Enemy{enemyCount}");
            if (enemy == null)
            {
                enemyCount = -1;
                break;
            }
            else
            {
                stage.AddEnemyActor(enemy);
                enemyCount++;
            }
        }

        stage.EndSetting();
    }

    private void SettingUI()
    {
        var mainUI = GameObject.Find("MainView");
        if (mainUI.TryGetComponent<MainView>(out var mainView) == true)
        {
            Global.UI.OpenView(mainView);
        }
    }
}
