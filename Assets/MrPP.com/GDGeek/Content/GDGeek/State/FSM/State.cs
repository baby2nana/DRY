using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


public class State : StateBase
{
    public delegate void Action();
    public delegate void EvtAction(FSMEvent evt);

    public delegate string StateAction(FSMEvent evt);

    private Dictionary<string ,StateAction> actionMap_ = new Dictionary<string, StateAction>();

    public void addAction(string evt,string nextState)
    {
        addAction(evt,delegate
        {
            return nextState;
        });
    }

    public void addAction(string evt,StateAction action)
    {
        if(!actionMap_.ContainsKey(evt))
        {
            actionMap_.Add(evt,action);
        }else
        {
            StateAction old = actionMap_[evt];
            actionMap_[evt] = delegate(FSMEvent e)
            {
                string ret = null;
                ret = old(e);
                if(!string.IsNullOrEmpty(ret))
                {
                    return ret;
                }
                return action(e);
            };
        }
    }
    public event Action onStart;
    public event Action onOver;

    public override string postEvent(FSMEvent evt)
    {
        string ret = "";
        if(actionMap_.ContainsKey(evt.msg))
        {
            ret = actionMap_[evt.msg](evt);
        }
        return ret;
    }
    public override void start()
    {
        if(onStart != null)
        {
            onStart.Invoke();
        }
    }

    public override void over()
    {
        if(onOver != null)
        {
            onOver.Invoke();
        }
    }

    public State()
    {

    }

    public State(string name)
    {
        this.name = name;
    }

    

}
}
