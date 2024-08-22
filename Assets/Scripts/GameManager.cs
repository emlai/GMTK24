using System;
using FlatKit;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public UniversalRendererData rendererData;
    FlatKitFog fog;
    Gradient originalDistanceGradient;

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

    public void UpdateFogColor(float energy)
    {
        var color = Color.white * energy;
        color.a = 1;
        fog.settings.distanceGradient.colorKeys = new[] { new GradientColorKey(color, 1) };
        rendererData.SetDirty(); // force update after updating renderer feature settings. not sure if there's a better way to do this.
    }
}
