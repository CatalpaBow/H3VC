using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H3VC.AudioPipelines;
using H3VC.VoiceRecoders;
using H3VC.Speakers;
using H3VC.VCUsers;
using H3VC.Networks;
using H3VC.Servers;
namespace H3VC.VC
{
    public static class VCMain{
        public static AudioPipeline audioPipeline;
        public static VoiceRecoder recoder;
        public static PlayerAudioSpeakers speakerList;
        public static VCUserList userList;
        public static AudioRelayServer server;

        public static void Intialize() {
            //Recoder
            Microphone mic = new VoiceRecoders.MicImpement.NAudioMic();
            recoder = new VoiceRecoder(mic);

            //UserList
            userList = new VCUserList();

            //Speaker
            speakerList = new PlayerAudioSpeakers(userList);

            //AudioPipeLine
            NetworkAudioListener audioListner = new NetworkAudioListener();
            NetworkAudioStreamer networkAudioStreamer = new NetworkAudioStreamer();
            audioPipeline = new AudioPipeline(recoder,networkAudioStreamer,audioListner, speakerList);
            //Server
            server = new AudioRelayServer();


        }
    }
}
