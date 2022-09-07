using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField]
    private float knockbackForce = 0;

    private Vector3 knockbackDir = new Vector3();

    private void Awake()
    {
        knockbackDir = transform.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
            StartCoroutine(player.Stun(knockbackForce));
        }
    }
}
