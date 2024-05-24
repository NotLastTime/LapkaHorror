using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [SerializeField] Light Sun;
    [SerializeField] Light Moon;
    [SerializeField] Material DaySky;
    [SerializeField] Material NightSky;
    [SerializeField] ParticleSystem Stars;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public float _dayDuration = 30f; // Продолжительность суток
    
    [Range(0, 1)]
    private float _timeOfDay;
    private float _sunIntensity;
    private float _moonIntensity;

    private void Start()
    {
        _sunIntensity = Sun.intensity;
        _moonIntensity = Moon.intensity;
    }

    private void Update()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;
        if( _timeOfDay >= 1 ) _timeOfDay -= 1;

        Sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f, 180, 0);
        Sun.intensity = _sunIntensity * SunCurve.Evaluate(_timeOfDay);
        
        Moon.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f + 180, 180, 0);
        Moon.intensity = _moonIntensity * MoonCurve.Evaluate(_timeOfDay);

        // Меняем дневное небо на ночное
        RenderSettings.skybox.Lerp(NightSky, DaySky, SkyboxCurve.Evaluate(_timeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(_timeOfDay) >0.1 ? Sun : Moon;
        DynamicGI.UpdateEnvironment();
        
        // Отключаем звезды днем
        var _mainModule = Stars.main;
        _mainModule.startColor = new Color(1,1,1, 1 - SkyboxCurve.Evaluate(_timeOfDay));
    }
}
