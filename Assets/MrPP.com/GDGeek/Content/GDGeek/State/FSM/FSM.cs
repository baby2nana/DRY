using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{
    
public class FSM
{
    private Dictionary<string,StateBase> states_ = new Dictionary<string, StateBase>();

    private List<StateBase> currStates_ = new List<StateBase>();

    private bool debug_ = false;

    private string[] currStateName
    {
        get;
        set;
    }

    public FSM(bool debug = false)
    {
        this.debug_ = debug;
        StateBase root = new StateBase();
        root.name = "root";
        this.states_["root"] = root;
        this.currStates_.Add(root);
    }

    public FSM(string name,bool debug = false)
    {

        this.debug_ = debug;
        StateBase root = new StateBase();
        root.name = "root";
        this.states_["root"] = root;
        this.currStates_.Add(root);
    }

    public void addState(StateBase state)
    {
        this.addState(state.name,state);
    }

    public void addState(string name,StateBase state)
    {
        this.addState(name,state,"");
    }

    public void addState(StateBase state,string fatherName)
    {
        this.addState(state.name,state,fatherName);
    }

    public void addState(string stateName,StateBase state,string fatherName)
    {
        if(fatherName == "")
        {
            fatherName = "root";
        }else{
            state.fatherName = fatherName;

        }

        state.getCurrState = delegate (string name)
        {
            for(int i = 0 ; i < this.currStates_.Count; i++)
            {
                StateBase  tempState = this.currStates_[i] as StateBase;
                if(tempState.name == name)
                {
                    return tempState;
                }
            }
            return null;
        };

        if(this.states_.ContainsKey(state.fatherName))
        {
            if(string.IsNullOrEmpty(this.states_[state.fatherName].defsubState))
            {
                this.states_[state.fatherName].defsubState = stateName;
            }
        }

        state.name = stateName;
        this.states_[stateName] = state;


    }

    public void addState(string stateName,object obj)
    {
        throw new NotImplementedException();
    }

    public StateBase getCurrSubState()
    {
        return this.currStates_[this.currStates_.Count - 1];
    }

    private string[] printCurrState()
    {
        string[] states = new string[this.currStates_.Count];

        for(int i = 0; i < this.currStates_.Count; i++)
        {
            states[i] = this.currStates_[i].name;
        }

        return states;
    }

    public StateBase getCurrState(string name)
    {
        for(int i = 0; i< this.currStates_.Count; i++)
        {
            if(this.currStates_[i].name == name)
            {
                return this.currStates_[i];
            }
        }

        return null;
    }


    public void translation(string stateName)
    {
        if(!this.states_.ContainsKey(stateName))
        {
            return ;
        }

        StateBase target = this.states_[stateName];

        while(!string.IsNullOrEmpty(target.defsubState) && this.states_.ContainsKey(target.defsubState))
        {
            target = this.states_[target.defsubState];
        }

        if(target == this.currStates_[this.currStates_.Count - 1])
        {
            target.over();
            target.start();
            return;
        }

        StateBase publicState = null;
        List<StateBase> stateList = new List<StateBase>();

        StateBase tempState = target;
        string fatherName = target.fatherName;

        //do loop
        while(tempState != null)
        {
            
            foreach(var state in this.currStates_)
            {
                if(state == tempState)
                {
                    publicState = state;
                    break;
                }
            }

            //end
            if(publicState != null)
            {
                break;
            }

            stateList.Insert(0,tempState);
            if(fatherName != "")
            {
                tempState = this.states_[fatherName] as StateBase;
                fatherName = tempState.fatherName;
            }else{
                tempState = null;
            }
        }

        if(publicState == null)
        {
            return;
        }

        List<StateBase> newCurrState = new List<StateBase>();
        bool under = true;
        //析构状态
        foreach(var state in this.currStates_)
        {
            if(state == publicState)
            {
                under = false;
            }

            if(under)
            {
                state.over();
            }else{
                newCurrState.Insert(0,state);
            }
        }
        //构建状态
        foreach(var state in this.currStates_)
        {
            state.start();
            newCurrState.Add(state);
        }

        this.currStates_ = newCurrState;

        //存储当前所有状态名字
        this.currStateName = printCurrState();


    }
    public void init(string stateName)
    {
        //init the first state it.
        this.translation(stateName);
    }

    public void shutdown()
    {
        foreach(StateBase state in this.currStates_)
        {
            state.over();
        }

        this.currStates_ = null;
    }

    public void post(string msg)
    {
        //FSMEvent evt = new FSMEvent(msg);
        FSMEvent evt = new FSMEvent();
        evt.msg = msg;

        this.postEvent(evt);
    }

    public void post(string msg, object obj)
    {
        FSMEvent evt = new FSMEvent(msg,obj);
        this.postEvent(evt);
    }

    public void post(FSMEvent evt)
    {
        this.postEvent(evt);
    }

    private void postEvent(FSMEvent evt)
    {
        for(int i= 0 ; i< this.currStates_.Count; i++)
        {
            StateBase state = this.currStates_[i];
            
            if(debug_)
            {
                Debug.Log("msg_post" + evt.msg + "state_name" + state.name);
            }

            string stateName = state.postEvent(evt) as string;

            if(stateName != "")
            {
                this.translation(stateName);
                break;
            }
        }
    }


}

}