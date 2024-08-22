using System;
using DG.Tweening;
using UnityEngine;

public class Anglerfish : MonoBehaviour
{
    bool playerInsideHitZone;
    float originalTurnSpeed;
    float originalChaseSpeed;
    Transform player;
    Chaser chaser;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        chaser = GetComponent<Chaser>();
        GetComponentInChildren<Animator>().Play("Swim");
        originalTurnSpeed = chaser.turnSpeed;
        originalChaseSpeed = chaser.chaseSpeed;
    }

    void FixedUpdate()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < 10)
        {
            chaser.turnSpeed = 5;
        }
        else if (distanceToPlayer < 20)
        {
            chaser.turnSpeed = 3;
            // chaser.chaseSpeed = 15;
        }
        else
        {
            chaser.turnSpeed = originalTurnSpeed;
            chaser.chaseSpeed = originalChaseSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideHitZone = true;
            GetComponentInChildren<Animator>().Play("Bite");
            chaser.turnSpeed = 10;
            // chaser.chaseSpeed = 15;
            // GetComponentInChildren<Animator>().speed = 2;
            // other.gameObject.GetComponent<Player>().FreezeMovement();
            // Camera.main.GetComponent<CameraController>().farAwayCameraMode = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideHitZone = false;
        }
    }

    void BiteHit() // Animation event
    {
        GetComponents<AudioSource>()[1].Play();

        if (playerInsideHitZone)
        {
            var player = GameObject.FindWithTag("Player").GetComponent<Ship>();
            GetComponent<Animator>().StopPlayback();
            chaser.turnSpeed = 20;
            Invoke(nameof(StopChasing), 0.1f);
            player.GetComponentInChildren<Player>().FreezeMovement();
            Camera.main.GetComponent<CameraController>().farAwayCameraMode = true;
            // player.transform.DOMove(transform.position + GetComponent<SphereCollider>().center, 0.5f)
            player.transform.DOMove(transform.position + transform.rotation * GetComponent<SphereCollider>().center, 1f)
                .OnComplete(() => player.GetComponentInChildren<Renderer>(false).enabled = false);
            player.Invoke(nameof(Ship.Die), 3);
        }
        else
        {
            // Camera.main.GetComponent<CameraController>().farAwayCameraMode = false;
            Debug.Assert(chaser.turnSpeed != 0);
            chaser.turnSpeed = originalTurnSpeed;
            chaser.chaseSpeed = originalChaseSpeed;
            Invoke(nameof(ResumeSwimming), 1);
        }
    }

    void ResumeSwimming()
    {
        GetComponentInChildren<Animator>().Play("Swim");
    }
    
    void StopChasing()
    {
        chaser.enabled = false;
        GetComponent<Animator>().StopPlayback();
    }
}
