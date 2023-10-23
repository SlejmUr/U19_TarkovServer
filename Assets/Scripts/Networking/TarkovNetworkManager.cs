using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19.Networking
{
    public class TarkovNetworkManager : NetworkManager
    {
        public override void OnClientConnect(NetworkConnection conn)
        {
            Debug.Log("OnClientConnect");
            base.OnClientConnect(conn);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("OnClientDisconnect");
            base.OnClientDisconnect(conn);
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            Debug.Log("OnServerConnect");
            base.OnServerConnect(conn);
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("OnClientError " + errorCode);
            base.OnClientError(conn, errorCode);
        }
    }
}
