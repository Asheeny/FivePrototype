using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private GameController controller;

    void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(controller.StageIncrement(0f, false));
            }
        }
        catch (System.NullReferenceException ex)
        {
            Debug.Log("Could not find a collision object: " +  ex);
        }
    }
}
