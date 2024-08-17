using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Portals : MonoBehaviour
{
    public Camera cameraB;
    public Material cameraMatB;

    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, GraphicsFormat.B10G11R11_UFloatPack32, GraphicsFormat.None);
        cameraMatB.mainTexture = cameraB.targetTexture;
    }
}
