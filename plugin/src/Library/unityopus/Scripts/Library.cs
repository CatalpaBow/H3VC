using System;
using System.IO;
using System.Runtime.InteropServices;
namespace UnityOpus
{
    public enum SamplingFrequency : int
    {
        Frequency_8000 = 8000,
        Frequency_12000 = 12000,
        Frequency_16000 = 16000,
        Frequency_24000 = 24000,
        Frequency_48000 = 48000,
    }

    public enum NumChannels : int
    {
        Mono = 1,
        Stereo = 2,
    }

    public enum OpusApplication : int
    {
        VoIP = 2048,
        Audio = 2049,
        RestrictedLowDelay = 2051,
    }

    public enum OpusSignal : int
    {
        Auto = -1000,
        Voice = 3001,
        Music = 3002
    }

    public enum ErrorCode
    {
        OK = 0,
        BadArg = -1,
        BufferTooSmall = -2,
        InternalError = -3,
        InvalidPacket = -4,
        Unimplemented = -5,
        InvalidState = -6,
        AllocFail = -7,
    }

    public static class Library
    {
        public const int maximumPacketDuration = 5760;
        private static IntPtr dllHandle;

        static Library() {
            //dllPath
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string finalPath = Directory.GetParent(path).FullName + "\\UnityOpus.dll";

            //LoadLibrary
            dllHandle = LoadLibrary(finalPath);
            if (dllHandle == IntPtr.Zero) {

            }
            //SetFncPointer
            OpusEncoderCreate = GetFunctionPointer<OpusEncoderCreateDelegate>("OpusEncoderCreate");
            OpusEncode = GetFunctionPointer<OpusEncodeDelegate>("OpusEncode");
            OpusEncodeFloat = GetFunctionPointer<OpusEncodeFloatDelegate>("OpusEncodeFloat");
            OpusEncoderDestroy = GetFunctionPointer<OpusEncoderDestroyDelegate>("OpusEncodeFloat");
            OpusEncoderSetBitrate = GetFunctionPointer<OpusEncoderSetBitrateDelegate>("OpusEncoderSetBitrate");
            OpusEncoderSetComplexity = GetFunctionPointer<OpusEncoderSetComplexityDelegate>("OpusEncoderSetComplexity");
            OpusEncoderSetSignal = GetFunctionPointer<OpusEncoderSetSignalDelegate>("OpusEncoderSetSignal");
            OpusDecoderCreate = GetFunctionPointer<OpusDecoderCreateDelegate>("OpusDecoderCreate");
            OpusDecode = GetFunctionPointer<OpusDecodeDelegate>("OpusDecode");
            OpusDecodeFloat = GetFunctionPointer<OpusDecodeFloatDelegate>("OpusDecodeFloat");
            OpusDecoderDestroy = GetFunctionPointer<OpusDecoderDestroyDelegate>("OpusDecoderDestroy");
            OpusPcmSoftClip = GetFunctionPointer<OpusPcmSoftClipDelegate>("OpusPcmSoftClip");

            //Excute FreeLibrary if process is exit.
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
        }


        private static T GetFunctionPointer<T>(string functionName) where T : Delegate {
            IntPtr funcPtr = GetProcAddress(dllHandle, functionName);
            return (T)Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(T));
        }
        private static void ProcessExitHandler(object sender, EventArgs e) {
            FreeLibrary(dllHandle);
        }

        public static readonly OpusEncoderCreateDelegate OpusEncoderCreate;
        public delegate IntPtr OpusEncoderCreateDelegate(
            SamplingFrequency samplingFrequency,
            NumChannels channels,
            OpusApplication application,
            out ErrorCode error);

        public static readonly OpusEncodeDelegate OpusEncode;
        public delegate int OpusEncodeDelegate(
            IntPtr encoder,
            short[] pcm,
            int frameSize,
            byte[] data,
            int maxDataBytes);

        public static OpusEncodeFloatDelegate OpusEncodeFloat;
        public delegate int OpusEncodeFloatDelegate(
            IntPtr encoder,
            float[] pcm,
            int frameSize,
            byte[] data,
        int maxDataBytes);

        public static OpusEncoderDestroyDelegate OpusEncoderDestroy;
        public delegate int OpusEncoderDestroyDelegate(
            IntPtr encoder);

        public static OpusEncoderSetBitrateDelegate OpusEncoderSetBitrate;
        public delegate int OpusEncoderSetBitrateDelegate(
            IntPtr encoder,
            int bitrate);

        public static OpusEncoderSetComplexityDelegate OpusEncoderSetComplexity;
        public delegate int OpusEncoderSetComplexityDelegate(
            IntPtr encoder,
            int complexity);

        public delegate int OpusEncoderSetSignalDelegate(
            IntPtr encoder,
            OpusSignal signal);
        public static OpusEncoderSetSignalDelegate OpusEncoderSetSignal;


        public delegate IntPtr OpusDecoderCreateDelegate(
            SamplingFrequency samplingFrequency,
            NumChannels channels,
            out ErrorCode error);
        public static OpusDecoderCreateDelegate OpusDecoderCreate;


        public static OpusDecodeDelegate OpusDecode;
        public delegate int OpusDecodeDelegate(
            IntPtr decoder,
            byte[] data,
            int len,
            short[] pcm,
            int frameSize,
            int decodeFec);

        public static OpusDecodeFloatDelegate OpusDecodeFloat;
        public delegate int OpusDecodeFloatDelegate(
            IntPtr decoder,
            byte[] data,
            int len,
            float[] pcm,
            int frameSize,
            int decodeFec);

        public delegate void OpusDecoderDestroyDelegate(
            IntPtr decoder);
        public static OpusDecoderDestroyDelegate OpusDecoderDestroy;

        public delegate void OpusPcmSoftClipDelegate(
            float[] pcm,
            int frameSize,
            NumChannels channels,
            float[] softclipMem);
        public static OpusPcmSoftClipDelegate OpusPcmSoftClip;


        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
    }
}
