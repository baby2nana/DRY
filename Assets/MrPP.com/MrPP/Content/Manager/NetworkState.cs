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

    public void doBroadcast()
    {
        fsm_.post("broadcast");
    }

    public void doListening()
    {
        fsm_.post("listening");
    }
    private State beginState()
    {
        State state = new State();
        state.addAction("broadcast","broadcast");
        state.addAction("listening","listening");
        return state;
    }

    private State hostState()
    {
        State state = new State();
        state.onStart += delegate
        {
            _system.startHost();
        };

        //state.addAction("client","client");
        state.onOver += delegate
        {
            _system.stopHost();
        };
        return state;
    }

    public void doRunning()
    {
        fsm_.post("running");
    }
    private State broadcastState()
    {
        State state = new State();
        state.onStart += delegate
        {
            _system.startBroadcast();
        };

        state.addAction("running","running");
        state.onOver += delegate
        {
            _system.stopBroadcast();
        };
        return state;
    }

    private State runningState()
    {
        State state = new State();
        state.addAction("broadcast","broadcast");
        return state;
    }
    public void doJoin()
    {
        fsm_.post("join");
    }

    public void doAlone()
    {
        fsm_.post("alone");
    }
    private State listeningState()
    {
        State state = new State();
        state.onStart += delegate
        {
            _system.startListening();
        };

        state.addAction("join","client");
        state.addAction("alone","alone");

        state.onOver += delegate
        {
            _system.stopListening();
        };
        return state;
    }

    private State clientState()
    {
        State state = new State();
        state.onStart += delegate
        {
            _system.startClient();
        };

        state.onOver += delegate
        {
            _system.stopClient();
        };
        return state;
    }

    private State aloneState()
    {
        State state = new State();
        state.onStart += delegate
        {
            _system.startHost();
        };

        state.onOver += delegate
        {
            _system.stopHost();
        };
        return state;
    }
    void Start()
    {
        fsm_.addState("begin",beginState());
        fsm_.addState("host",hostState());
        fsm_.addState("broadcast",broadcastState(),"host");
        fsm_.addState("running",runningState(),"host");

        fsm_.addState("alone",aloneState());

        fsm_.addState("listening",listeningState());
        fsm_.addState("client",clientState());

        fsm_.init("begin");

    }

}

}