using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderValue : MonoBehaviour
{
    public Slider[] sliders;
    public float transitionDuration = 1.5f;
    public int sliderIndex;

    public float targetValue;
    private float currentValue;
    private float transitionStartTime;
    private bool isTransitioning;

    private void Start()
    {
        currentValue = sliders[sliderIndex].value;
        targetValue = currentValue;
        isTransitioning = false;
    }

    private void Update()
    {
        if (isTransitioning)
        {
            float timeSinceStart = Time.time - transitionStartTime;
            float t = Mathf.Clamp01(timeSinceStart / transitionDuration);

            float smoothedValue = Mathf.Lerp(currentValue, targetValue, t);
            sliders[sliderIndex].value = smoothedValue;

            if (t >= 1.0f)
            {
                isTransitioning = false;
                currentValue = targetValue;
            }
        }
    }

    public void SetSliderValue(float value)
    {
        targetValue = value;
        transitionStartTime = Time.time;
        isTransitioning = true;
    }
}