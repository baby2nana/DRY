using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace GDGeek
{


public class TaskRunnerInEditor : MonoBehaviour,ITaskRunner
{
    private List<Task> tasks_= new List<Task>();
    private List<Task> shutDown_ = new List<Task>();

    private Filter filter_ = new Filter(); 
    private bool running_= false;

    private DateTime time_ ;

    private float all_ = 0f;
   public void addTask(Task task)
   {
       task.init();
       tasks_.Add(task);
       if(!running_)
       {
           running_ = true;
           time_ = DateTime.UtcNow;
           EditorApplication.update += editorUpdate;
           all_ = 0f;
           editorUpdate();
       }
   }


   public void update(float d)
   {
       List<Task> tasks = new List<Task>();
       foreach(var task in shutDown_)
       {
           task.shutDown();
       }
       shutDown_.Clear();

       foreach(var task in tasks_)
       {
           task.update(d);
           if(!task.isOver())
           {
               tasks.Add(task);
           }else{
               shutDown_.Add(task);
           }
       }
       this.tasks_ = tasks;
   }
   
   public void editorUpdate()
   {
       DateTime time = DateTime.UtcNow;
       TimeSpan ts = time_ - time;
       float d = filter_.interval((float)(ts.Ticks)/ 10000000f);
       all_ += d;
       this.update (d);
       time_ = time;
       if(this.tasks_.Count == 0 && this.shutDown_.Count == 0)
       {
           EditorApplication.update -= editorUpdate;
           running_ = false;
       }
   }
}
}
#endif
