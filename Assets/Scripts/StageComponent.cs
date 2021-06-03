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
}
