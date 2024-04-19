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
        public static PlayerAudioSpeakers speakers;
        public static VCUserList userList;
        public static AudioRelayServer server;

        public static void Intialize() {
            //UserList
            userList = new VCUserList();

            //Recoder
            BaseMicrophone mic = new VoiceRecoders.MicImpement.NAudioMic();
            recoder = new VoiceRecoder(mic);
            

            //Speaker
            speakers = new PlayerAudioSpeakers(userList);

            //AudioPipeLine
            NetworkAudioListener audioListner = new NetworkAudioListener();
            NetworkAudioStreamer networkAudioStreamer = new NetworkAudioStreamer();
            audioPipeline = new AudioPipeline(recoder,networkAudioStreamer,audioListner, speakers);

            //Server
            server = new AudioRelayServer();
        }
    }
}
