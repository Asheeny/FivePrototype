using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponent : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]
    int activeFromStage = 1;
    [SerializeField]
    GameObject visibleObject = null;
    [SerializeField]
    GameObject transparentObject = null;

    private bool showInEditor = false;
    private Animator anim = null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(showInEditor)
            Gizmos.DrawCube(transform.position, transform.localScale * 1.01f);  
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if(anim != null)
            anim.SetFloat("SpawnSpeed", Random.Range(0.5f, 2f));
    }

    public int GetActiveFromStage()
    {
        return activeFromStage;
    }

    public void SetTransparent(bool transparency)
    {
        if (transparentObject == null)
            return;
        transparentObject.SetActive(transparency);
        visibleObject.SetActive(!transparency);
    }

    public void ShowComponentInEditor(bool value)
    {
        showInEditor = value;
    }
}
