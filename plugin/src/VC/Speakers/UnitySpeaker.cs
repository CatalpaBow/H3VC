using H3VC.Data;
using System;
using UnityEngine;
using UnityOpus;
namespace H3VC.Speakers
{
    [RequireComponent(typeof(AudioSource))]
    public class UnitySpeaker : MonoBehaviour
    {
        const NumChannels channels = NumChannels.Mono;
        const SamplingFrequency frequency = SamplingFrequency.Frequency_48000;
        const int audioClipLength = 48000;
        const int delaySamplesLength = (int)(audioClipLength * 0.01 * 8);
        AudioSource source;
        int head = 0;
        float[] audioClipData;

        void OnEnable() {
            source = GetComponent<AudioSource>();
            source.spatialBlend = 1;
            source.clip = AudioClip.Create("Voice", audioClipLength, (int)NumChannels.Mono, (int)frequency, false);
            source.loop = true;
        }
        public void Intialize(int channels) {
            source = GetComponent<AudioSource>();
            source.spatialBlend = 1;
            source.clip = AudioClip.Create("Voice", audioClipLength, channels, (int)frequency, false);
            source.loop = true;
        }

        void OnDisable() {
            source.Stop();
        }

        public void Play(PCMSegment sgmnt) {
            Play(sgmnt.pcmBuffer, sgmnt.pcmLength);
        }
        public void Play(float[] pcm, int pcmLength) {
            Log();
            if (audioClipData == null || audioClipData.Length != pcmLength) {
                // assume that pcmLength will not change.
                audioClipData = new float[pcmLength];
                H3VC.Mod.Logger.LogInfo("audioClipDataSize is changed:" + pcmLength);
            }

            //int remainSamples = source.clip.samples - head;

            Array.Copy(pcm, audioClipData, pcmLength);
            source.clip.SetData(audioClipData, head);

            head += pcmLength;
            if (!source.isPlaying && head > delaySamplesLength) {
                source.Play();
            }

            /* 再生速度のズレの補正をする */
            if ( (source.timeSamples + delaySamplesLength/2) > head || source.timeSamples < (head - delaySamplesLength - delaySamplesLength/2) ) {
                int modifedPos = head - delaySamplesLength;
                source.timeSamples = modifedPos < 0 ? source.clip.samples + modifedPos : modifedPos;
            }
            head %= audioClipLength;
        }
        public void SetInScene() {
            source.spatialBlend = 0;
        }
        public void SetPositional(bool isPositional) {
            if (isPositional) {
                source.spatialBlend = 1;
                return;
            }
            source.spatialBlend = 0;
        }



        private int previousTimeSamples = 0;
        private int previousDistance = 0;
        public void Log() {
            /*
            if(audioClipData == null){
                return;
            }
            if(previousTimeSamples == source.timeSamples) {
                return;
            }
            int distance = head > source.timeSamples ? head - source.timeSamples : source.clip.samples - source.timeSamples + head;

            Mod.Logger.LogInfo("\nHead:" + head + "\nTimeSamples:" + source.timeSamples + "\nDistance:" + distance + "\nTimeSamplesDelta:" + (source.timeSamples-previousTimeSamples) +"\nDistanceDelta:" + (distance - previousDistance));
            previousDistance = distance;
            previousTimeSamples = source.timeSamples;
            */
        }
    }
}
