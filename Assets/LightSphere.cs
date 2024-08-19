using System;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    GameObject player;
    public float speed;
    float startTime;
    bool eaten;
    [NonSerialized] public bool moveTowardsPlayer;
    public GameObject particles;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ship");
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (moveTowardsPlayer && !eaten)
        {
            var timeSinceStart = Time.time - startTime;
            transform.position += (player.transform.position - transform.position).normalized * timeSinceStart * speed * Time.fixedDeltaTime;
            var distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            transform.localScale = Vector3.one * Mathf.Clamp(distanceFromPlayer * 0.3f, 0.2f, 1f);
            if (distanceFromPlayer < 0.5f)
            {
                GetEaten();
            }
        }
    }

    void GetEaten()
    {
        eaten = true;
        // GetComponent<MeshRenderer>().enabled = false; // hide sphere
        // GetComponent<AudioSource>().Play();
        player.GetComponent<Ship>().GainEnergy();
        // yield return new WaitForSeconds(1);
        Instantiate(particles, player.transform.position, Quaternion.identity, player.transform);
        Destroy(gameObject);
    }
}
