using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


public class TaskState
{
    protected static int index_ = 0;

    public static State Create(TaskFactory create,FSM fsm,State.StateAction nextState)
    {
        string over = "over" + index_.ToString();
        index_ ++;
        State state = new State();
        Task task = null;
        state.onStart += delegate{
            task = create();
            TaskManager.PushBack(task,delegate{
                fsm.post(over);
            });

            TaskManager.Run(task);
        };

        state.onStart += delegate
        {
            task.isover = delegate
            {
                return true;
            };
        };
        state.addAction(over,nextState);
        return state;
    }
    public static State Create(TaskFactory creater,FSM fsm,string nextState)
    {
        return Create(creater, fsm, delegate {
            return nextState;
        });
    }
}
}