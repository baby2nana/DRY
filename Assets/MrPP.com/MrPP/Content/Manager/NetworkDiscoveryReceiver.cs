using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MrPP.Network
{
    
public class NetworkDiscoveryReceiver : NetworkDiscovery
{
    public delegate void Receiver(string fromAddress,string data);

    public event Receiver onReceiver;

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if(onReceiver != null)
        {
            onReceiver(fromAddress,data);
        }
    }

}


}

