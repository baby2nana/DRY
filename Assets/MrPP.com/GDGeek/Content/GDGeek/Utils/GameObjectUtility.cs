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
}
}
