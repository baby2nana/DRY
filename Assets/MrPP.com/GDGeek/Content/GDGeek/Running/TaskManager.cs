using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


public class TaskManager : Singleton<TaskManager>
{
    public static TaskManager GetInstance()
    {
        return Singleton<TaskManager>.GetOrCreateInstance;
    }
    
    public static void PushFront(Task task,TaskInit func)
    {

        TaskInit oInit = task.init;
        task.init = delegate()
        {
            func();
            oInit();
        };
    }

    public static void PushBack(Task task,TaskShutDown func)
    {
        TaskShutDown oShutDown = task.shutDown;
        task.shutDown = delegate()
        {
            func();
            oShutDown();
        };
    }

    private TaskRunner partRunner_ = null;

    public TaskRunner partRunner
    {
        set
        {
            this.partRunner_ = value as TaskRunner;

        }
    }

    public ITaskRunner runner
    {
        get
        {
            if(partRunner_ != null)
            {
                return partRunner_;
            }
            return globalRunner;
        }
    }
    
    public ITaskRunner globalRunner{
			get{
				TaskRunner runner = this.gameObject.AskComponent<TaskRunner> ();
				return runner;
			}
	}
    public static void Run(Task task)
    {
        TaskManager.GetInstance().runner.addTask(task);
    }
}
}
