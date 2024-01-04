using UnityEngine;

public class StageCreator : MonoBehaviour
{
    private void Start()
    {
        CreateStage();
    }

    public void CreateStage()
    {
        var stage = Resource.Instantiate<Stage>("Stage", Vector3.zero, Quaternion.identity, null);

        Destroy(gameObject);
    }
}
