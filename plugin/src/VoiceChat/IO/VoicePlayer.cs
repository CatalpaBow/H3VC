using H3VC.Data;
using System;
using UnityEngine;
using UnityOpus;
namespace H3VC.IO
{
    [RequireComponent(typeof(AudioSource))]
    public class VoicePlayer : MonoBehaviour
    {
        const NumChannels channels = NumChannels.Mono;
        const SamplingFrequency frequency = SamplingFrequency.Frequency_48000;
        const int audioClipLength = 1024 * 6;
        AudioSource source;
        int head = 0;
        float[] audioClipData;

        void OnEnable() {
            source = GetComponent<AudioSource>();
            source.spatialBlend = 1;
            source.clip = AudioClip.Create("Loopback", audioClipLength, (int)channels, (int)frequency, false);
            source.loop = true;
            DontDestroyOnLoad(this);
        }

        void OnDisable() {
            source.Stop();
        }

        public void Play(PCMSegment sgmnt) {
            Play(sgmnt.pcmBuffer, sgmnt.pcmLength);
        }
        public void Play(float[] buffer, int length) {
            int pcmLength = length;
            float[] pcm = buffer;
            if (audioClipData == null || audioClipData.Length != pcmLength) {
                // assume that pcmLength will not change.
                audioClipData = new float[pcmLength];
            }
            Array.Copy(pcm, audioClipData, pcmLength);
            source.clip.SetData(audioClipData, head);
            head += pcmLength;
            if (!source.isPlaying && head > audioClipLength / 2) {
                source.Play();
            }
            head %= audioClipLength;
        }
        public void SetInScene() {
            source.spatialBlend = 0;
        }
    }
}
