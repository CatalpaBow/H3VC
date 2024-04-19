using H3MP.Networking;
using H3VC.Network;
using System;
using UniRx;
using H3MP;
using System.Diagnostics;
using H3VC.Networks.NetworkImplement;
namespace H3VC.Servers{

    /// <summary>
    /// Receive audio data and relay it to clients.
    /// </summary>
    public class AudioRelayServer{
        public static AudioRelayServer Insntance => _instance ?? (_instance = new AudioRelayServer());
        private static AudioRelayServer _instance;
        
        private VoiceReceiver receiver;
        private IDisposable serverDisposer;
        public AudioRelayServer() {
            receiver = VoiceReceiver.Instance;
            H3MP.Mod.OnConnection += this.Start;
            H3MP.Networking.Server.OnServerClose += this.Stop;
        }
        public void Start() {
            if (!ThreadManager.host) {
                return;
            }
            H3VC.Mod.Logger.LogInfo("H3VC ServerStart");
            serverDisposer = receiver.OnVoiceReceived
                    .Subscribe(data => VoiceSender.ServerSend(data.Item1, data.Item2));

        }
        public void Stop() {
            H3VC.Mod.Logger.LogInfo("H3VC ServerClose");
            serverDisposer.Dispose();
        }


    }
}
