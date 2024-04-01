using BepInEx;
using BepInEx.Logging;
// TODO: Change 'YourName' to your name. 
namespace H3VC
{
    // TODO: Change 'YourPlugin' to the name of your plugin
    [BepInPlugin("CatalpaBow.H3VC", "H3VC", "0.1.0")]
    [BepInProcess("h3vr.exe")]
    public partial class Mod : BaseUnityPlugin
    {
        /* == Quick Start == 
         * Your plugin class is a Unity MonoBehaviour that gets added to a global game object when the game starts.
         * You should use Awake to initialize yourself, read configs, register stuff, etc.
         * If you need to use Update or other Unity event methods those will work too.
         *
         * Some references on how to do various things:
         * Adding config settings to your plugin: https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/4_configuration.html
         * Hooking / Patching game methods: https://harmony.pardeike.net/articles/patching.html
         * Also check out the Unity documentation: https://docs.unity3d.com/560/Documentation/ScriptReference/index.html
         * And the C# documentation: https://learn.microsoft.com/en-us/dotnet/csharp/
         */

        private void Awake() {
            Logger = base.Logger;
            H3VC.VoiceChat.VoiceChat.Intialize();

        }


        // The line below allows access to your plugin's logger from anywhere in your code, including outside of this file.ThreadManager.host
        // Use it with 'YourPlugin.Logger.LogInfo(message)' (or any of the other Log* methods)
        internal new static ManualLogSource Logger { get; private set; }

        private void Update() {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.PageDown)) {
                Mod.Logger.LogInfo("TestStart");
                TestPacket.Send();
            }

            if(Input.GetKeyDown(KeyCode.Home)) {
                H3VC.Mod.Logger.LogInfo("VCTest");
                Test.TestStart();
            }
            if (Input.GetKeyDown(KeyCode.PageUp)) {
                
                Mod.Logger.LogInfo("TestConnectionStart");
                string ip = "192.168.1.50";
                int port = 7863;
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip),port);
                H3MP.Mod.OnConnectClicked(endPoint);
                
                /*
                MicrophoneRecorder recoder = new MicrophoneRecorder();
                PCMEncoder encoder = new PCMEncoder();
                OpusDecoder decoder = new OpusDecoder();
                PCMPlayer player = new GameObject().AddComponent<PCMPlayer>();
                Observable.EveryUpdate()
                          .SelectMany(_ => recoder.Recode())
                          .SelectMany(encoder.Encode)
                          .Select(decoder.Decode)
                          .Subscribe(sgmnt => {
                              player.Play(sgmnt.pcmBuffer, sgmnt.pcmLength);
                          });
                */
            }
#endif
        }
    }

}
