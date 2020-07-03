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
}

}