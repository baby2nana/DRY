using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GDGeek;

namespace MrPP.SuperView
{


    public class PlaneFinderReceiver : Singleton<PlaneFinderReceiver>
    {
        public Action OnFound { get; set; }
        public Action OnLost { get; set; }

        public void doFound()
        {
            OnFound?.Invoke();
        }

        public void doLost()
        {
            OnLost?.Invoke();
        }
    }
}
