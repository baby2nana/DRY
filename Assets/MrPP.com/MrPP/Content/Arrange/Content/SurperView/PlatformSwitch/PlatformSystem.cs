using System.Linq;
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
        Debug.Log("count of plarfrom" + platfromOns.Count());
        foreach(var pt in platfromOns)
        {
        
            Debug.Log("PlatformInfo.Instance.stamp is .." + PlatformInfo.Instance.stamp.ToString());
            if(!pt.gameObject.HasComponent(PlatformInfo.Instance.stamp))
            {
                Destroy(pt.gameObject);
            }
        }

    }
}
}
