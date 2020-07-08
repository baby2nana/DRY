using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
public static class GameObjectUtility
{
    public static bool HasComponent(this GameObject obj,Type type)
    {
        if(type == null)
        {
            return false;
        }

        Component component = obj.gameObject.GetComponent(type);
        if(component != null)
        {
            return true;
        }
        return false;
    }

    public static T AskComponent<T>(this GameObject obj) where T:Component
    {
        T comment = obj.GetComponent<T>();
        if(comment == null)
        {
            comment = obj.AddComponent<T>();
        }
        return comment;
    }
}
}
