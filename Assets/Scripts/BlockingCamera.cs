using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    private List<StageComponent> blockingCamera = new List<StageComponent>();
    private List<StageComponent> currentlyTransparent= new List<StageComponent>();

    void Update()
    {
        blockingCamera.Clear();
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(player.transform.position, transform.position));
            //Physics.RaycastAll(transform.position, transform.forward, Vector3.Distance(player.transform.position, transform.position));
        
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.TryGetComponent<StageComponent>(out StageComponent stageComponent))
                blockingCamera.Add(stageComponent);
        }

        for (int i = 0; i < currentlyTransparent.Count; i++)
        {
            if (!blockingCamera.Contains(currentlyTransparent[i]))
            {
                currentlyTransparent[i].SetTransparent(false);
                currentlyTransparent.Remove(currentlyTransparent[i]);
            }
        }

        for (int i = 0; i < blockingCamera.Count; i++)
        {
            if(!currentlyTransparent.Contains(blockingCamera[i]))
            {
                blockingCamera[i].SetTransparent(true);
                currentlyTransparent.Add(blockingCamera[i]);
            }
        }

    }
}
