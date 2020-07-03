using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
namespace MrPP.Network{
    
public class NetworkSystem : MonoBehaviour
{
    public Action onSessionReceive
    {
        get;
        set;
    }

    public void doSessionReceive()
    {
        if(onSessionReceive != null)
        {
            onSessionReceive.Invoke();
        }
    }

    private List<SessionInfo> sessions_ = new List<SessionInfo>();

    public class SessionInfo
    {
        public string name;
        public string ip;
        public string uuid;
    }

    public List<SessionInfo> sessions
    {
        get
        {
            return sessions_;
        }
    }

    [SerializeField]
    private NetworkDiscoveryReceiver _discovery;

    private void OnDestroy() 
    {
        _discovery.onReceiver -= receiveBroadcast;
    }

    private void receiveBroadcast(string fromAddress,string data)
    {
        string serverIp = fromAddress.Substring(fromAddress.LastIndexOf(':') + 1);
        BroadcastData db = JsonUtility.FromJson<BroadcastData>(data);
        this.sessions_.Add(new SessionInfo(){ip = serverIp,name = db.name,uuid = db.uuid});
        doSessionReceive();
    }
    

    public void startBroadcast()
    {
        _discovery.Initialize();
        _discovery.StartAsServer();
    }

    public void stopBroadcast()
    {
        _discovery.StopBroadcast();
    }

    public void startHost()
    {
        NetworkManager.singleton.serverBindToIP = true;
        NetworkManager.singleton.StartHost();
    }

    public void stopHost()
    {
        NetworkManager.singleton.StopHost();
    }

    public void startClient()
    {
        //NetworkManager.singleton.networkAddress = Configure
        NetworkManager.singleton.StartClient();
    }

    public void stopClient()
    {
        NetworkManager.singleton.StopClient();
    }

    public void startListening()
    {
        _discovery.Initialize();
        _discovery.StartAsClient();
    }

    public void stopListening()
    {
        _discovery.StopBroadcast();
    }
    //[Serializable]
    public class BroadcastData
    {
        //[SerializeField]
        public string name;
       // [SerializeField]
        public string uuid;
    }
    private void Awake() {
        if(_discovery == null)
        {
            _discovery = this.gameObject.GetComponent<NetworkDiscoveryReceiver>();
        }
        _discovery.onReceiver += receiveBroadcast;
    }


    public void testSessin(string id)
    {
        this.sessions_.Add(new SessionInfo(){ ip = "192.168.8.8",name = "jinbao MacBook Pro 13",uuid = id});
        Debug.Log("count is " + this.sessions_.Count);
        doSessionReceive();
    }

    public bool running
    {
        get{
            return _discovery.running; 
        }
    }
}

}