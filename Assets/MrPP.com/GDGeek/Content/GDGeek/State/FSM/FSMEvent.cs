using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


public class FSMEvent
{
   public Tuple<string ,object> data_ = new Tuple<string, object>(null,null);

   public FSMEvent()
   {

   }

   public FSMEvent(Tuple<string,object> data)
   {
       this.data_ = data;
   }

   public FSMEvent(string msg) {
       this.data_= new Tuple<string, object>(msg,null);
   }

   public FSMEvent(string msg, object obj)
   {
       this.data_ = new Tuple<string, object>(msg,obj);
   }

   public string msg
    {
        get
        {
            return this.data_.Item1;
        }
        set
        {
            this.data_ = new Tuple<string, object>(value,data_.Item2);
        }
    }

    public object obj
    {
        get{
            return this.data_.Item2;
        }
        set
        {
            this.data_ = new Tuple<string, object>(data_.Item1,value);
        }
    }
}

}
