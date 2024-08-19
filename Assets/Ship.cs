using System.Collections;
using DG.Tweening;
using FlatKit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ship : MonoBehaviour
{
    public float forwardMoveSpeed;
    Player player;
    bool growing;
    public float growthFactor = 1;
    public TextMeshProUGUI progressbar;
    public FogSettings fogSettings;
    public UniversalRendererData rendererData;
    float energy = 0.333f; // range: 0-1
    public float energyDepleteSpeed = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateProgressbar();
        UpdateFogColor();
        StartCoroutine(EnergyDepleteLoop());
    }

    IEnumerator EnergyDepleteLoop()
    {
        while (true)
        {
            var speedMult = 0.1f;
            yield return new WaitForSeconds(1f / energyDepleteSpeed * speedMult);
            energy -= 1f / 30f * speedMult;
            if (energy < 0)
            {
                energy = 0;
                // TODO: fade to black, game over screen
            }

            UpdateProgressbar();
            UpdateFogColor();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Grow();
        }

        var z = Input.GetAxis("Vertical");
        if (z >= 0) // Don't auto-move ship forward when player is manually moving backwards.
        {
            // Auto-move forward
            var diff = transform.forward * forwardMoveSpeed * Time.fixedDeltaTime;
            transform.position += diff;
        }
        // player.transform.position += diff;
    }

    public void Grow()
    {
        if (growing) return;
        growing = true;
        var newScale = transform.localScale * growthFactor;
        // foreach (var shrimp in GameObject.FindGameObjectsWithTag("Shrimp"))
        // {
        //     shrimp.transform.localScale = newScale;
        // }

        transform.DOScaleX(newScale.x, 0.2f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                growing = false;
            }
        );
    }

    void UpdateProgressbar()
    {
        var weight = Mathf.Pow(2, transform.localScale.x) * 0.0005;
        var maxProgress = 30;
        var progressValue = (int)(energy * maxProgress);
        Debug.Assert(progressValue >= 0 && progressValue <= maxProgress);
        var hyphens = new string('-', Mathf.Min(maxProgress, progressValue));
        var spaces = new string(' ', Mathf.Max(0, maxProgress - progressValue));
        progressbar.text = $"Energy: [{hyphens}{spaces}]\nYour weight: {weight} KG";
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shrimp"))
        {
            StartCoroutine(Eat(other.gameObject));
        }
    }

    IEnumerator Eat(GameObject other)
    {
        var mouth = transform.Find("MouthPosition");

        while (Vector3.Distance(other.gameObject.transform.position, mouth.position) > 0.1f)
        {
            var dir = (mouth.position - other.gameObject.transform.position).normalized;
            other.gameObject.transform.position += dir * 30 * Time.deltaTime;
            yield return null;
        }

        mouth.GetComponent<AudioSource>().Play();
        Grow();
        Destroy(other);
    }

    public void GainEnergy()
    {
        energy += 0.1f;
        if (energy > 1) energy = 1;
        UpdateProgressbar();
        UpdateFogColor();
    }

    void UpdateFogColor()
    {
        var color = (Color.white * energy).WithAlpha(1);
        fogSettings.distanceGradient.colorKeys = new[] { new GradientColorKey(color, 1) };
        rendererData.SetDirty(); // force update after updating renderer feature settings. not sure if there's a better way to do this.
    }
}
