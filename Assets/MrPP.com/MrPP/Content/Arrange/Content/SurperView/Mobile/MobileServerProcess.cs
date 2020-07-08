using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using MrPP.Network;
namespace MrPP.SuperView
{


public class MobileServerProcess : MonoBehaviour
{

    private FSM fsm_ = new FSM();

    #if UNITY_EDITOR
    [SerializeField]
    private string stateName_;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        stateName_ = fsm_.getCurrSubState().name;
    }

    #endif


    private State runningState()
    {
        State state = new State();

        return state;
    }
    private State beginState()
    {
        State state = new State();

        state.onStart += delegate{
            NetworkState.Instance.doBroadcast();
        };

        state.addAction("start",delegate{

            return "running";
        });
        return state;
    }
    private void doFound()
    {
        fsm_.post("found");
    }


    private State readyState()
    {
        State state = new State();
        state.addAction("found","begin");
        return state;
    }


    // Start is called before the first frame update
    void Start()
    {

        if(PlaneFinderReceiver.IsInitialized)
        {
            PlaneFinderReceiver.Instance.OnFound += doFound;
        }
        fsm_.addState("ready",readyState());
        fsm_.addState("begin",beginState());
        fsm_.addState("running",runningState());

        fsm_.init("ready");
    }

   
}
}
