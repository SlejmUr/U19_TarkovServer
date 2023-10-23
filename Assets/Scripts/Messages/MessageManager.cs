﻿using ComponentAce.Compression.Libs.zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovServerU19.BSGClasses;
using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    public class MessageManager
    {
        public static void ConnectionRequest(NetworkMessage msg)
        {
            Console.WriteLine(msg.channelId + " " + msg.msgType + " " + msg.conn.connectionId);

            var connectPackage = msg.ReadMessage<ConnectSentPacket>();
            Console.WriteLine(connectPackage.ToString());
            RejectConnection rejectConnection = new RejectConnection()
            {
                RealErrorCode = RejectConnection.LocationMissmatch
            };

            AcceptConnection acceptConnection = new AcceptConnection()
            {
                antiCheatPort = 0,
                speedLimitsEnabled = false,
                fixedDeltaTime = 0,
                decryptionEnabled = false,
                encryptionEnabled = false,
                bounds = new UnityEngine.Bounds(new UnityEngine.Vector3(0f, 0f, 0f), new UnityEngine.Vector3(5000f, 5000f, 5000f)),
                canRestart = false,
                MemberCategory = EMemberCategory.Default,
                GitVersion = new(),
                NetLogsLevel = ENetLogsLevel.Normal,
                voipSettings = BSGClasses.VOIP.VoipSettings.Default,
                gameTimeClass = new(DateTime.Now.AddMinutes(30), DateTime.Now.AddMinutes(50), 7f),
                sessionId = new byte[] { },
                CompressedInteractables = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new Dictionary<string, int>()), 0),
                CompressedCustomizationIds = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new string[] { }), 0),
                CompressedResources = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new Jsons.ResourceKey[] { }), 0),
                CompressedWeathers = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new WeatherClass[] { WeatherClass.CreateDefault() }), 0),
            };

            msg.conn.Send(148, rejectConnection);
        }

    }
}
