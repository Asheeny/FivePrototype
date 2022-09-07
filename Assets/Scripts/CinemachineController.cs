using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera followCam = null;
    [SerializeField]
    private CinemachineVirtualCamera overCam = null;

    public void SwitchPriority(bool overworld)
    {
        if(overworld)
        {
            followCam.Priority = 0;
            overCam.Priority = 1;
        }
        else
        {
            followCam.Priority = 1;
            overCam.Priority = 0;
        }
    }
}
