using UnityEngine;

public static class Helpers
{
    public static float ExponentialInterpolate(float a, float b, float t)
    {
        return a * Mathf.Exp(t * Mathf.Log(b / a));
    }
}
