using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private float crosshairRatio = 20;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width/crosshairRatio, Screen.width/crosshairRatio);
    }

}
