using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek
{

public class Task
{
    public TaskInit init = delegate(){};
    public TaskShutDown shutDown = delegate(){};
    public TaskUpdate update = delegate(float d){};
    public TaskIsOver isOver = delegate()
    {
        return true;
    };
}
}
