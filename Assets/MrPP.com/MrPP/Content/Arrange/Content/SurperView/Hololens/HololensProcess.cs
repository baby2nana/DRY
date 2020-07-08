using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
using MrPP.Network;
namespace MrPP.SuperView
{


public class HololensProcess : MonoBehaviour
{

    private FSM fsm_ = new FSM();

    #if UNITY_EDITOR
    [SerializeField]
    private string stateName;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        stateName = fsm_.getCurrSubState().name;
    }
    #endif

    private State singleState()
    {
        State state = new State();

        return state;
    }

    private State aloneState()
    {
        State state = TaskState.Create(delegate
        {
            Task task = new TaskWait(0.3f);
            TaskManager.PushFront(task,delegate{
                NetworkState.Instance?.doAlone();
            });
            return task;
        },fsm_,"single");

        return state;
    }
    public void doSessionReceive()
    {
        this.fsm_.post("session");
    }

    public void doStart()
    {
        this.fsm_.post("start");
    }
    private State beginState()
    {
        State state = new State();

        state.onStart += delegate{
            NetworkState.Instance?.doListening();
        };

        state.addAction("start","alone");
        state.addAction("session","join");

        return state;

    }

    private State joinState()
    {
        State state = TaskState.Create(delegate{

            Task task = new TaskWait(0.3f);
            TaskManager.PushBack(task,delegate{
                List<NetworkSystem.SessionInfo> sessions = NetworkState.Instance.sessions;
                if(sessions.Count > 0 && NetworkState.Instance.isRunning)
                {
                    //configure
                    NetworkState.Instance?.doJoin();
                }
            });
            return new Task();
        },fsm_,"scanning");

        return state;
    }
    // Start is called before the first frame update
    void Start()
    {

        if(NetworkState.IsInitialized)
        {
            NetworkState.Instance.onSessionReceive += doSessionReceive;
        }

        fsm_.addState("begin",beginState());
        
        fsm_.addState("alone",aloneState());
        fsm_.addState("single",singleState());

        fsm_.addState("join",joinState());
        // fsm_.addState("scanning",scanningState());
        // fsm_.addState("wait",waitState());
        // fsm_.addState("running",runningState());

        fsm_.init("begin");
    }

    
}
}
