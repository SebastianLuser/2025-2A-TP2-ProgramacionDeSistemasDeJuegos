using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasScalerController : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;

    private void Reset()
        => canvasScaler = GetComponent<CanvasScaler>();

    private void Awake()
        => canvasScaler = GetComponent<CanvasScaler>();

    private void Update()
    {
        bool isHorizontalScreen = Screen.width >= Screen.height;
        canvasScaler.matchWidthOrHeight = isHorizontalScreen ? 1 : 0;
    }
}