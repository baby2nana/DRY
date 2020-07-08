using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.SuperView
{

    public class PlaneFinderSender : Singleton<PlaneFinderSender>
    {
        private bool antomaticHitTest_ = false;
        private bool antomaticHit_ = false;


        public void onInteractiveHitTest()
        {

        }
        public void onAutomaticHitTest()
        {
            antomaticHitTest_ = true;
        }

        private void doFound()
        {
            PlaneFinderReceiver.Instance?.doFound();
        }

        private void doLost()
        {
            PlaneFinderReceiver.Instance?.doLost();
        }

        private void LateUpdate()
        {
            if(antomaticHit_ != antomaticHitTest_)
            {
                antomaticHit_ = antomaticHitTest_;

                if(antomaticHit_)
                {
                    Debug.Log("onFound ...");
                    doFound();
                }else{
                    doLost();
                }
                antomaticHitTest_ = false;
            }
        }
    }
}
