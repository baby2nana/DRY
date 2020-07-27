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
        Debug.Log("front....");
        TaskInit oInit = task.init;
        task.init = delegate()
        {
            func();
            Debug.Log("front init...");
            oInit();
        };
    }

    public static void PushBack(Task task,TaskShutDown func)
    {
        Debug.Log("back....");
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
            #if UNITY_EDITOR
            if(!Application.isPlaying)
            {
                
				Debug.Log("running in editor...");
                return editorRunner;
            }
            #endif
            if(partRunner_ != null)
            {
                Debug.Log("part runner is ...");
                return partRunner_;
            }
            return globalRunner;
        }
    }
    
    #if UNITY_EDITOR
    public ITaskRunner editorRunner
    {

        get
        {
            TaskRunnerInEditor runner = this.gameObject.AskComponent<TaskRunnerInEditor>();
            return runner;
        }
    }
    #endif
    public ITaskRunner globalRunner{
			get{
				TaskRunner runner = this.gameObject.AskComponent<TaskRunner> ();
				return runner;
			}
	}
    public static void Run(Task task)
    {
        Debug.Log("run...");
        TaskManager.GetInstance().runner.addTask(task);
    }
}
}
