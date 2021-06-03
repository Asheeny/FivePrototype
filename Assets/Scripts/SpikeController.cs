using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField]
    float KnockbackForce = 0;

    void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                Vector3 dir = (player.transform.position - transform.position).normalized;
                StartCoroutine(player.Stun(dir, KnockbackForce));
            }
        }
        catch (System.NullReferenceException ex)
        {
            Debug.Log("Could not find a collision object: " + ex);
        }
    }
}
