using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    
public class StateBase 
{
    private string name_  = "";

    protected string fatherName_ = "";

    protected string defsubState_ = "";

    public delegate StateBase GetCurrState(string name);

    public GetCurrState getCurrState = null;

    public StateBase()
    {

    }

    public string name
    {
        get { 
            return name_;
            }

        set{ 
            name_ = value;
            }
    }

    public string fatherName
    {
        get { 
            return fatherName_;
            }

        set{ 
            fatherName_ = value;
            }
    }

    public string defsubState
    {
        get { 
            return defsubState_;
            }

        set{ 
            defsubState_ = value;
            }
    }

    public virtual void start()
    {

    }

    public virtual void over()
    {

    }

    public virtual string postEvent(FSMEvent evt)
    {
        return "";
    }

}

}
