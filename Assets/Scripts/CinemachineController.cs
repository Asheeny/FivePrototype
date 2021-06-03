using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam1 = null;
    [SerializeField]
    private CinemachineVirtualCamera cam2 = null;

    public void SwitchPriority(bool overworld)
    {
        if(overworld)
        {
            cam1.Priority = 0;
            cam2.Priority = 1;
        }
        else
        {
            cam1.Priority = 1;
            cam2.Priority = 0;
        }
    }
}
