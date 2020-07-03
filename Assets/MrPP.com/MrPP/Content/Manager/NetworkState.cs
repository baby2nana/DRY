using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.Network
{


public class NetworkState : MonoBehaviour
{
    [SerializeField]
    private NetworkSystem _system;

    public NetworkSystem.SessionInfo findSession(int uuid)
    {
        return _system.findSessino(uuid);
    }

    #if UNITY_EDITOR
    [SerializeField]
    private string stateName_;

    private void Update() {
        stateName_ = fsm_.getCurrSubState().name;;
    }
    #endif

    public Action onSessionReceive
    {
        get{
           return  _system.onSessionReceive ;
        }
        set{
            _system.onSessionReceive = value;
        }
    }
    
    private FSM fsm_ = new FSM();
    // Start is called before the first frame update

    private StateBase beginState()
    {
        StateBase state = new StateBase();
        //state.
        return state;
    }
    void Start()
    {
        fsm_.addState("begin",beginState());
        // fsm_.addState("host",hostState());
        // fsm_.addState("broadcast",broadcastState(),"host");
        // fsm_.addState("running",runningState(),"host");

        // fsm_.addState("alone",aloneState());

        // fsm_.addState("listening",listeningState());
        // fsm_.addState("client",clientState());

        fsm_.init("begin");

    }

}

}