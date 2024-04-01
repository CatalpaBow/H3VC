using H3VC.Network;
using System;
using UniRx;
namespace H3VC.CS
{
    public class VoiceServer
    {
        public static VoiceServer Insntance() => _instance ?? (_instance = new VoiceServer());
        private static VoiceServer _instance;

        private VoiceReceiver receiver;
        private IDisposable disposable;
        public VoiceServer() {
            receiver = VoiceReceiver.Instance;
        }
        public void Start() {
            disposable = receiver.OnVoiceReceived
                    .Subscribe(data => VoiceSender.ServerSend(data.Item1, data.Item2));

        }
        public void Stop() {
            disposable.Dispose();
        }

    }
}
