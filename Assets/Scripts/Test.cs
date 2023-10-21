using System;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19
{
    public class Test : NetworkBehaviour
    {
        public static void LogMe(NetworkBehaviour obj, NetworkReader rdr)
        {
            Debug.Log("L: " + rdr.Length);
            var bytes = rdr.ReadBytesAndSize();
            Debug.Log("Bytes: " + BitConverter.ToString(bytes));
        }
    }
}
