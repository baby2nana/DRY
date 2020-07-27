using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{


public class Filter
{
   private int num_ = 0;

   private float[] filters_= new float[15];

   public Filter()
   {
       for(int i = 0; i < filters_.Length; i++)
       {
           filters_[i] = -1f;
       }
   }

   public float interval(float d)
   {
       this.filters_[num_] = d;
       num_++;
       if(num_ >= 15)
       {
           num_ = 0;
       }

       float all = 0f;
       for(int i = 0; i < filters_.Length; i++)
       {
           if(filters_[i] > 0)
           {
               all += filters_[i];
           }else{
               all += d;
           }
       }

       return (all/15);
   }



}
}
