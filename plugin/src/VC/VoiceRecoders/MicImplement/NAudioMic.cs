using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace H3VC.VoiceRecoders.MicImpement{
    /// <summary>
    /// Implementation with NAudio that .NET audio library.
    /// <see href="https://github.com/naudio/NAudio">Github Link</see>
    /// </summary>
    public class NAudioMic : BaseMicrophone
    {
        public override IObservable<float[]> OnAudioReady => OnAudioReadySubject;
        private Subject<float[]> OnAudioReadySubject;

        private float[] floatSamples;
        private WaveInEvent waveIn;
        public NAudioMic() {
            OnAudioReadySubject = new Subject<float[]>();
            floatSamples = new float[2048];
            var deviceNumber = 0;
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = deviceNumber;
            waveIn.WaveFormat = new WaveFormat(48000, 1);
            waveIn.BufferMilliseconds = 10;
            waveIn.DataAvailable += DataAvailableHandler;
        }

        private void DataAvailableHandler(object sender, WaveInEventArgs e) {
            try {
                int recodedPartLength = Bit16ToNormizedFloat(ref floatSamples, e);
                float[] recodedSamples = CutRecodedSamples(recodedPartLength)
                                                    .ToArray();
                OnAudioReadySubject.OnNext(recodedSamples);
            } catch (Exception ex) {
                Mod.Logger.LogError(ex);
            }
        }
        private int Bit16ToNormizedFloat(ref float[] floatSamples, WaveInEventArgs e) {
            int index = 0;
            for (index = 0; index < e.BytesRecorded; index += 2) {
                // サンプルデータを16ビットで扱うため、バイトデータをshort型に変換する
                // 下位ビットが先に配列に格納されているため、バイトの組み合わせをシフト演算でshortに変換する
                short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);

                // short型の値を正規化する（-1.0～1.0の範囲に変換する）
                /// https://pspunch.com/pd/article/bit_depth/
                float sample32 = sample / 32768f;
                floatSamples[index / 2] = sample32;
            }
            return index / 2;
        }

        private float[] CutRecodedSamples(int recodedPartLength) {
            float[] recodedSamples = new float[recodedPartLength];
            Array.Copy(floatSamples, recodedSamples, recodedPartLength);
            return recodedSamples;
        }

        public override void StartRecoding() {
            waveIn.StartRecording();
        }
        public override MicDeviceInfo ChangeDevice(int newDeviceNumber) {
            waveIn.StopRecording();
            waveIn.Dispose();

            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = newDeviceNumber;
            waveIn.WaveFormat = new WaveFormat(48000, 1);
            waveIn.BufferMilliseconds = 10;
            waveIn.DataAvailable += DataAvailableHandler;

            waveIn.StartRecording();
            return new MicDeviceInfo(newDeviceNumber, WaveIn.GetCapabilities(newDeviceNumber).ProductName);
        }

        public override IEnumerable<MicDeviceInfo> ShowDevices() {
            for (int i = 0; i < WaveIn.DeviceCount; i++) {
                yield return new MicDeviceInfo(i, WaveIn.GetCapabilities(i).ProductName);
            }
        }

        public override int DeviceCount() {
            return WaveIn.DeviceCount;
        }


        /*
        private IEnumerable<float[]> SplitSamples(float[] samples) {
            float[] splitedSamples = new float[256];
            int head = 0;

            int count = Mathf.CeilToInt((float)samples.Length / splitedSamples.Length);
            for (int i = 0; i < count; i++) {
                Array.Copy(samples, head, splitedSamples, 0, splitedSamples.Length);
                head += splitedSamples.Length;
                yield return splitedSamples;
            }
            int reamainLength = samples.Length - head;
            float[] remainSamples = new float[reamainLength];
            Array.Copy(samples, head, remainSamples, 0, reamainLength);
            yield return remainSamples;
        }
        */
    }
}
