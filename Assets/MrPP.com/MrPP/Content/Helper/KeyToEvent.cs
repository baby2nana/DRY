using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MrPP.Helper
{


public class KeyToEvent : MonoBehaviour
{
    public KeyCode _key;

    public UnityEvent _action;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_key))
        {
            if(_action != null)
            {
                _action.Invoke();
            }
            
        }
    }
}
}
