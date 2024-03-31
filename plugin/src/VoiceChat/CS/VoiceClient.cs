using H3MP.Networking;
using H3VC.Converter;
using H3VC.Data;
using H3VC.IO;
using H3VC.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
namespace H3VC.CS
{
    public class VoiceClient
    {
        public static VoiceClient Instance() => _instance ?? (_instance = new VoiceClient());
        private static VoiceClient _instance;
        private VoiceInput input;
        private VoiceEncoder encoder;

        private VoiceReceiver receiver;
        private VoiceDecoder decoder;
        private VoicePlayerFacade player;
        private List<IDisposable> disposerList;

        public VoiceClient() {
            input = new VoiceInput();
            encoder = new VoiceEncoder();
            receiver = VoiceReceiver.Instance;

            decoder = new VoiceDecoder();
            player = new VoicePlayerFacade();
            disposerList = new List<IDisposable>();


        }


        public void Start() {
            Mod.Logger.LogInfo("H3VC ClientStart...");

            var disposer = input.OnVoiceReady
                 .SelectMany(encoder.Encode)
                 .Subscribe(sgmnt => {
                     if (ThreadManager.host) {
                         H3VC.Mod.Logger.LogInfo("VoiceSended From Server");
                     } else {
                         VoiceSender.ClientSend(sgmnt);
                         //H3VC.Mod.Logger.LogInfo("VoiceSended From Client");
                     }
                 })
                 .AddTo(disposerList);
            /*
            input.OnMuted
                 .Subscribe(sender.SendMuteState());
            input.OnSoundRangeChanged
                 .Subscribe(VoiceSender.SendSoundRange());
            */
            receiver.OnVoiceReceived
                    .Subscribe(data => {
                        if (ThreadManager.host) {
                            H3VC.Mod.Logger.LogInfo("VoiceReceived!!!");
                        }
                        var opusCopied = new OpusSegment(data.Item2.data, data.Item2.length);
                        var pcmData = decoder.Decode(opusCopied);
                        player.Play(data.Item1, pcmData);
                    })
                    .AddTo(disposerList);
            /*
            receiver.OnSoundRangeChanged
                    .Subscribe(player.ChangeSoundVoice);
            receiver.OnMuteChanged
                    .Subscribe(player.ChangeMute);
            */
            input.Start(16);
        }
        public void Stop() {
            disposerList.ForEach(disposer => disposer.Dispose());
        }
    }
}
