using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private GameController gameController = null;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(gameController.StageIncrement(3f, false));
            }
        }
        catch (System.NullReferenceException ex)
        {
            Debug.Log("Could not find a collision object: " +  ex);
        }
    }
}
