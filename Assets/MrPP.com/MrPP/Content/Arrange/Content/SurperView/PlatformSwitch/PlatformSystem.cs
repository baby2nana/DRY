using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GDGeek;
namespace MrPP.SuperView
{


public class PlatformSystem : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        bool isLoaded = false;
        Debug.Log("How many count of scenes " + SceneManager.sceneCount);
        PlatformInfo.Type type = PlatformInfo.Instance.type;
        for(int i = 0; i< SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.name == type.ToString())
            {
                isLoaded = true;
            }
        }

        if(!isLoaded)
        {
            SceneManager.LoadScene(type.ToString(),LoadSceneMode.Additive);
        }

        PlatformOn[] platfromOns = FindObjectsOfType<PlatformOn>();
        foreach(var pt in platfromOns)
        {
        
            
            if(!pt.gameObject.HasComponent(PlatformInfo.Instance.stamp))
            {
                Destroy(pt.gameObject);
            }
        }

    }
}
}
