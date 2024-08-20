using System.Collections;
using DG.Tweening;
using FlatKit;
using TMPro;
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
    [Range(0, 1)] public float initialEnergy = 0.5f;
    float energy; // range: 0-1
    public float energyDepleteSpeed = 0.1f;
    public PauseMenu pauseMenu;
    [Range(0, 1)]
    public float energyPerLightball = 0.25f;
    float boostTime = -999;

    void Start()
    {
        energy = initialEnergy;
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
            if (energy <= 0)
            {
                pauseMenu.Die();
                yield break;
            }
            energy -= 1f / 30f * speedMult;
            if (energy < 0)
            {
                energy = 0;
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
        if (z >= 0 && !player.movementFrozen) // Don't auto-move ship forward when player is manually moving backwards, or when player movement is frozen.
        {
            // Boost
            var timeSinceBoost = Time.time - boostTime;
            var boostDuration = 1f;
            var boostMult = timeSinceBoost < boostDuration ? (boostDuration - timeSinceBoost) * 4f : z > 0 ? 1 : 0;
            var diff = transform.forward * forwardMoveSpeed * boostMult * Time.fixedDeltaTime;
            transform.position += diff;
        }
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
        var maxProgress = 30;
        var progressValue = (int)(energy * maxProgress);
        Debug.Assert(progressValue >= 0 && progressValue <= maxProgress);
        var hyphens = new string('-', Mathf.Min(maxProgress, progressValue));
        var spaces = new string(' ', Mathf.Max(0, maxProgress - progressValue));
        progressbar.text = $"LIGHTNESS [{hyphens}{spaces}]";
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
        energy += energyPerLightball;
        if (energy > 1)
        {
            energy = 1;
            pauseMenu.Win();
        }
        UpdateProgressbar();
        UpdateFogColor();
        boostTime = Time.time;
    }

    void UpdateFogColor()
    {
        var color = Color.white * energy;
        color.a = 1;
        fogSettings.distanceGradient.colorKeys = new[] { new GradientColorKey(color, 1) };
        rendererData.SetDirty(); // force update after updating renderer feature settings. not sure if there's a better way to do this.
    }
}
