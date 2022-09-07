using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DetailVariation : MonoBehaviour
{
    [SerializeField]
    Vector3 lowerScale;
    [SerializeField]
    Vector3 upperScale;        
    [SerializeField]
    Vector3 lowerPositionNudge;
    [SerializeField]
    Vector3 upperPositionNudge;    
    [SerializeField]
    float lowerSwaySpeed;    
    [SerializeField]
    float upperSwaySpeed;

    void Start()
    {
        transform.position.Set(
            transform.position.x + Random.Range(lowerPositionNudge.x, upperPositionNudge.x),
            transform.position.y + Random.Range(lowerPositionNudge.y, upperPositionNudge.y),
            transform.position.z + Random.Range(lowerPositionNudge.z, upperPositionNudge.z));

        transform.localScale.Set(
            Random.Range(lowerScale.x, upperScale.x),
            Random.Range(lowerScale.y, upperScale.y),
            Random.Range(lowerScale.z, upperScale.z));

        GetComponent<Animator>().SetFloat("SwaySpeed", Random.Range(lowerSwaySpeed, upperSwaySpeed));
    }
}
