using System.Collections;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    GameObject player;
    public float speed;
    float startTime;
    bool eaten;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (!eaten)
        {
            var timeSinceStart = Time.time - startTime;
            transform.position += (player.transform.position - transform.position).normalized * timeSinceStart * speed * Time.fixedDeltaTime;
            var distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            transform.localScale = Vector3.one * Mathf.Clamp(distanceFromPlayer * 0.3f, 0.2f, 1f);
            if (distanceFromPlayer < 0.5f)
            {
                StartCoroutine(GetEaten());
            }
        }
    }

    IEnumerator GetEaten()
    {
        eaten = true;
        GetComponent<MeshRenderer>().enabled = false; // hide sphere
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
