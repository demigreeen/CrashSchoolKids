using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public AudioSource fallSound;
    [SerializeField] public AudioSource goalSound;
    private Transform pointRespawn;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            fallSound.Play();
            fallSound.pitch = Mathf.Lerp(fallSound.pitch, 1.4f, 0.3f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            goalSound.Play();
            pointRespawn = other.GetComponentInChildren<Point>().gameObject.transform;
            transform.position = pointRespawn.position;
        }
    }
}
