using H3MP.Networking;
using H3VC.CS;
namespace H3VC.VoiceChat
{
    public static class VoiceChat
    {

        public static void Intialize() {
            H3MP.Mod.OnConnection += StartVC;
            Client.OnDisconnect += VoiceClient.Instance().Stop;
            Server.OnServerClose += VoiceServer.Insntance().Stop;
        }


        public static void StartVC() {
            VoiceClient.Instance().Start();
            if (ThreadManager.host) {
                Mod.Logger.LogInfo("H3VC ServerStart...");
                VoiceServer.Insntance().Start();
            }

        }
    }
}
