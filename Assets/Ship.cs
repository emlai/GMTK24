using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float forwardMoveSpeed;
    Player player;
    bool growing;
    public float growthFactor = 1;
    public TextMeshProUGUI progressbar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateProgressbar();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Vector3.zero;
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
        var newScale = transform.localScale + Vector3.one * growthFactor;
        foreach (var shrimp in GameObject.FindGameObjectsWithTag("Shrimp"))
        {
            shrimp.transform.localScale = newScale;
        }

        transform.DOScale(newScale, 0.2f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                growing = false;
                UpdateProgressbar();
            }
        );
    }

    void UpdateProgressbar()
    {
        var weight = Mathf.Pow(2, transform.localScale.x) * 0.0005;
        var progressValue = (int)transform.localScale.x - 1;
        var maxProgress = 30;
        var hyphens = new string('-', Mathf.Min(maxProgress, progressValue));
        var spaces = new string(' ', Mathf.Max(0, maxProgress - progressValue));
        progressbar.text = $"Progress: [{hyphens}{spaces}]\nYour weight: {weight} KG";
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
}
