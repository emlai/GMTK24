using System;
using System.Collections;
using DG.Tweening;
using FlatKit;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ship : MonoBehaviour
{
    Player player;
    bool growing;
    public float growthFactor = 1;
    public TextMeshProUGUI progressbar;
    [Range(0, 1)] public float energy;
    public float energyDepleteSpeed = 0.1f;
    public PauseMenu pauseMenu;
    [Range(0, 1)] public float energyPerLightball = 0.25f;
    internal float boostTime;
    public GameManager gameManager;
    internal bool dead;
    internal float energyDepletedTime;
    internal Transform mouth;

    void Start()
    {
        mouth = transform.Find("MouthPosition");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateProgressbar();
        gameManager.UpdateFogColor(energy);
        StartCoroutine(EnergyDepleteLoop());
    }

    IEnumerator EnergyDepleteLoop()
    {
        while (!dead)
        {
            var speedMult = 0.1f;
            yield return new WaitForSeconds(1f / energyDepleteSpeed * speedMult);
            var prevEnergy = energy;
            energy -= 1f / 30f * speedMult;
            if (energy <= 0)
            {
                energy = 0;
                if (prevEnergy > 0)
                    energyDepletedTime = Time.time;
            }

            UpdateProgressbar();
            gameManager.UpdateFogColor(energy);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grow();
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
        while (Vector3.Distance(other.transform.position, mouth.position) > 0.1f)
        {
            var dir = (mouth.position - other.transform.position).normalized;
            other.transform.position += dir * 30 * Time.deltaTime;
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
        gameManager.UpdateFogColor(energy);
        boostTime = Time.time;
    }

    public void Die()
    {
        if (GameManager.instance.immortalityMode) return;
        dead = true;
        pauseMenu.Die();
    }
}
