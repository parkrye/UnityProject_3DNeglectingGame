using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SceneManager.LoadScene("MainScene");
    }
}
