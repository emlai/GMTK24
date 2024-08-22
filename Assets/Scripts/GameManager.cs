using System;
using FlatKit;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UniversalRendererData rendererData;
    FlatKitFog fog;
    Gradient originalDistanceGradient;
    public Image fadeToColorOverlay;
    public Ship player;
    public float deathFadeSpeed;
    public AnimationCurve deathFadeCurve;

    void Start()
    {
        var found = rendererData.TryGetRendererFeature(out fog);
        Debug.Assert(found, "Failed to find FlatKitFog renderer feature");
        originalDistanceGradient = fog.settings.distanceGradient;
    }

    void OnDestroy()
    {
        fog.settings.distanceGradient = originalDistanceGradient; // Restore same value value as we have on disk to avoid changing.
    }

    void Update()
    {
        if (!player.dead)
        {
            if (player.energy <= 0)
            {
                var color = fadeToColorOverlay.color;
                var duration = 1 / deathFadeSpeed;
                color.a = deathFadeCurve.Evaluate(Mathf.Clamp(Time.time - player.energyDepletedTime, 0, duration) / duration);
                fadeToColorOverlay.color = color;
                if (color.a >= 1)
                {
                    player.pauseMenu.Die();
                    player.dead = true;
                }
            }
            else
            {
                fadeToColorOverlay.color = Color.clear;
            }
        }
    }

    public void UpdateFogColor(float energy)
    {
        var color = Color.white * energy;
        color.a = 1;
        fog.settings.distanceGradient.colorKeys = new[] { new GradientColorKey(color, 1) };
        rendererData.SetDirty(); // force update after updating renderer feature settings. not sure if there's a better way to do this.
    }
}
