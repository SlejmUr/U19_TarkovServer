using ComponentAce.Compression.Libs.zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovServerU19.BSGClasses;
using TarkovServerU19.Http;
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

            var interactables = HTTP_Client.POSTString("/ts/GetLocationInteractables", connectPackage.LocationId);
            var customizables = HTTP_Client.POSTString("/ts/GetAccountCustomiztationIds", connectPackage.ProfileId);

            var Interactables_List = JsonConvert.DeserializeObject<Dictionary<string, int>>(interactables);
            var custom_List = JsonConvert.DeserializeObject<string[]>(customizables);

            AcceptConnection acceptConnection = new AcceptConnection()
            {
                antiCheatPort = 0,
                speedLimitsEnabled = false,
                fixedDeltaTime = 0.016666668f,
                decryptionEnabled = false,
                encryptionEnabled = false,
                bounds = new UnityEngine.Bounds(new UnityEngine.Vector3(0f, 0f, 0f), new UnityEngine.Vector3(5000f, 5000f, 5000f)),
                canRestart = false,
                MemberCategory = EMemberCategory.Default,
                GitVersion = new GitVersion(),
                NetLogsLevel = ENetLogsLevel.Normal,
                voipSettings = BSGClasses.VOIP.VoipSettings.Default,
                gameTimeClass = new GameTimeClass(DateTime.Now.AddMinutes(30), DateTime.Now.AddMinutes(50), 7f),
                sessionId = new byte[] { 0x00, 0xFF, 0xAA },
                CompressedInteractables = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new Dictionary<string, int>(Interactables_List)), 0),
                CompressedCustomizationIds = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(custom_List), 0),
                CompressedResources = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new ResourceKey[] { }), 0),
                CompressedWeathers = SimpleZlib.CompressToBytes(JsonConvert.SerializeObject(new WeatherClass[] { WeatherClass.CreateDefault() }), 0),
            };
            msg.conn.Send(148, rejectConnection);
        }

    }
}
