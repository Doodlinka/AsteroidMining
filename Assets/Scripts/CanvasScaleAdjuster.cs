using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class CanvasScaleAdjuster : MonoBehaviour
{
    private PixelPerfectCamera _camera;
    private Canvas _canvas;
 
    void Start()
    {
        _camera = Camera.main.GetComponent<PixelPerfectCamera>();
        _canvas = GetComponent<Canvas>();
        AdjustScalingFactor();
    }
 
    void LateUpdate()
    {
        AdjustScalingFactor();
    }
 
    void AdjustScalingFactor()
    {
        _canvas.scaleFactor = _camera.pixelRatio;
    }
 
}