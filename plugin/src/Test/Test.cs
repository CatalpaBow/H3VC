using H3VC.Converter;
using H3VC.IO;
using System.Linq;
using UniRx;
using UnityEngine;
namespace H3VC
{
    public static class Test
    {
        public static VoiceInput input;
        public static VoiceEncoder encoder;

        public static VoiceDecoder decoder;
        public static VoicePlayer player;

        public static void TestStart() {
            //Input
            input = new VoiceInput();
            encoder = new VoiceEncoder();


            //Output
            var gmObj = new GameObject();
            player = gmObj.AddComponent<VoicePlayer>();
            player.SetInScene();
            decoder = new VoiceDecoder();

            input.Start(16);
            input.OnVoiceReady
                 .SelectMany(encoder.Encode)
                 .Select(decoder.Decode)
                  .Subscribe(sgmnt => {
                      player.Play(sgmnt);
                  });

        }
    }
}
