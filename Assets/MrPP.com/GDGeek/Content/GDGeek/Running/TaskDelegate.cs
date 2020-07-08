using System.Collections;
using System.Collections.Generic;
namespace GDGeek
{

   public delegate void TaskInit();

   public delegate void TaskShutDown();
   public delegate void TaskUpdate(float d);

   public delegate bool TaskIsOver();

   public delegate Task TaskFactory();
}
