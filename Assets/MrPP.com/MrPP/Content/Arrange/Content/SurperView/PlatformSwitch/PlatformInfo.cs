using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.SuperView
{


public class PlatformInfo : Singleton<PlatformInfo>
{
    public enum Type
    {
        Desktop,
        Hololens,
        Hololens2,
        Mobile,
        Unknow,
    }

    private Type type_ = Type.Hololens2;
    public Type type{
        get
        {
            return type_ ;
        }
    }

    public System.Type stamp
    {
        get
        {
            System.Type ty = System.Type.GetType("MrPP.SuperView" + type_.ToString());
            return  ty;
        }
    }
}

}
