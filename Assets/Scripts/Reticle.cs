using System;
using TMPro;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [NonSerialized]
    public RaycastHit? raycastHit;
    public TextMeshProUGUI reticleInfoText;

    // void FixedUpdate()
    // {
    //     var ray = Camera.main.ViewportPointToRay(Vector3.one / 2);
    //     // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    //     if (Physics.Raycast(ray, out var hit))
    //     {
    //         raycastHit = hit;
    //         GetComponent<TextMeshProUGUI>().text = ">> ( × ) <<";
    //         GetComponent<TextMeshProUGUI>().color = Color.green;
    //         // reticleInfoText.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         raycastHit = null;
    //         GetComponent<TextMeshProUGUI>().text = "(    )";
    //         GetComponent<TextMeshProUGUI>().color = Color.white;
    //         // reticleInfoText.gameObject.SetActive(false);
    //     }
    // }
}
