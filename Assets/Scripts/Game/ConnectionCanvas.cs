using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCanvas : MonoBehaviour
{
    public void Connect()
    {
        ClientGameNetPortal.StartClient(GameNetPortal.Instance, "127.0.0.1", 7777);
    }

    public void Host()
    {
        GameNetPortal.Instance.StartHost("127.0.0.1", 7777);
    }
}
