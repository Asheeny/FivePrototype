using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public void Select(int level)
    {
        switch (level)
        {
            case 0:
                Loader.Load(Loader.Scene.Level_Select);
                break;
            case 1:
                Loader.Load(Loader.Scene.Level_1);
                break;
            case 2:
                Loader.Load(Loader.Scene.Level_2);
                break;
            case 3:
                Loader.Load(Loader.Scene.Level_3);
                break;
            case 4:
                Loader.Load(Loader.Scene.Level_4);
                break;
            case 5:
                Loader.Load(Loader.Scene.Level_5);
                break;
            case 6:
                Application.Quit();
                break;
            default:
                throw new System.NullReferenceException("No level found");
        }
    }
}
