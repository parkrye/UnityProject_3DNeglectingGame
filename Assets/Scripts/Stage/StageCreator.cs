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

        var id = G.V.ActorId + 1;
        while (id > 0)
        {
            var enemy = Resource.Load<EnemyActor>($"Actors/Enemy{id - G.V.ActorId}");
            if (enemy == null)
            {
                id = -1;
                break;
            }
            else
            {
                var enemyData = G.Data.Enemy.GetEnemyData(id);
                stage.AddEnemyActor(id, enemy);
                id++;
            }
        }

        stage.EndSetting();
    }

    private void SettingUI()
    {
        var mainUI = FindFirstObjectByType<MainView>();
        var views = FindObjectsByType<View>(FindObjectsSortMode.None);
        foreach (var view in views) 
        {
            view.Init();
            if (view != mainUI)
                view.gameObject.SetActive(false);
            else
                G.UI.OpenView(mainUI);
        }

        var dialogs = FindObjectsByType<Dialog>(FindObjectsSortMode.None);
        foreach (var dialog in dialogs) 
        {
            dialog.Init();
            dialog.gameObject.SetActive(false);
        }
    }
}
