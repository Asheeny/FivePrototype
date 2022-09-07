using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StageVisualiser : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField]
    private int StageInLevel = 0;
    [SerializeField]
    private bool Show = true;

    [SerializeField]
    private GameObject Stage = null;

    private void OnValidate()
    {
        if (Stage == null)
            return;

        foreach (StageComponent c in Stage.GetComponentsInChildren<StageComponent>())
        {
            if (!Show)
            { c.ShowComponentInEditor(false); continue; }
                   
            if (c.GetActiveFromStage() == StageInLevel)
            {
                c.ShowComponentInEditor(true);
            }
            else
            {
                c.ShowComponentInEditor(false);
            }
        }
    }
}
