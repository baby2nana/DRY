﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
public class TaskWait : Task
{
   private float allTime_ ;
   private float time_;

    public void initImpl()
    {
        time_ = 0f;
    }
    
    public void updateImpl(float d)
    {
        time_  += d;
    }

    public bool isOverImpl()
    {
        if(time_ >= allTime_)
        {
            return true;
        }
        return false;
    }
   public TaskWait()
   {
       this.init = initImpl;
       this.update = updateImpl;
       this.isover = isOverImpl;
   }
    public void setAllTime(float allTime)
    {
        allTime_ = allTime;
    }

   public TaskWait(float d)
   {
       this.init = initImpl;
       this.update = updateImpl;
       this.isover = isOverImpl;
       this.setAllTime(d);
   }

   public static TaskWait Create(float time,TaskShutDown shutDown)
   {
       TaskWait wait = new TaskWait(time);
       TaskManager.PushBack(wait,shutDown);
       return wait;
   }

}
}
