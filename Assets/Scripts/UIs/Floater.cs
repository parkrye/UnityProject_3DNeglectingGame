using Cinemachine;
using System.Linq;

public class Floater : UI
{
    private CinemachineBrain _mainCamera;

    public override void Init()
    {
        _mainCamera = FindObjectsOfType<CinemachineBrain>().FirstOrDefault();
    }

    private void LateUpdate()
    {
        if (_mainCamera == null)
            return;

        transform.LookAt(_mainCamera.transform);
    }
}
