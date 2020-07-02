using System.Collections;
using UnityEngine;
namespace GDGeek{
public abstract class Singleton <T> : MonoBehaviour where T : Singleton<T>
{
   static T instance_ = null;


    static public bool IsInitialized
    {
        get
        {
            if(instance_ == null)
            {
                T[] types = FindObjectsOfType<T>();
                if(types == null || types.Length == 0)
                {
                    return false;
                }
                instance_ = types[0];
                return true;
            }
            return false;
        }
    }


   public virtual void OnApplicationQuit()
   {
       instance_ = null;
   }

   static private T FindInstance()
   {
       T[] types = FindObjectsOfType<T>();
       if(types.Length > 1)
       {
           Debug.LogError("Single instance " + typeof(T).Name + " can not have multi instance!!!!");
       }

       if(types.Length == 1)
       {
           return types[0];
       }
       return null;
   } 

   static public T Instance
   {
        get
        {
            if(instance_  == null)
            {
                instance_ = FindInstance();
                
            }
            return instance_;
        }
   }
}
}

