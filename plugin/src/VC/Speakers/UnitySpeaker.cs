using H3VC.Data;
using Steamworks;
using System;
using System.Linq;
using UnityEngine;
using UnityOpus;
namespace H3VC.Speakers
{
    [RequireComponent(typeof(AudioSource))]
    public class UnitySpeaker : MonoBehaviour {
        const NumChannels channels = NumChannels.Mono;
        const SamplingFrequency frequency = SamplingFrequency.Frequency_48000;
        const int audioClipLength = 48000;
        const int delaySamplesLength = (int)(audioClipLength * 0.01 * 8);
        public AudioSource source { get;private set; }
        public int head { get; private set; } = 0;
        float[] samplesBuf;

        void Awake() {
            Intialize(1);
        }
        public void Intialize(int channels) {
            source = GetComponent<AudioSource>();
            source.spatialBlend = 1;
            source.clip = AudioClip.Create("Voice", audioClipLength, channels, (int)frequency, false);
            source.loop = true;
        }

        public void DontPlaySound(bool isMute) {
            if (isMute) {
                source.volume = 0f;
                return;
            }
            source.volume = 1.0f;
        }


        void OnDisable() {
            source.Stop();
        }

        public void Play(PCMSegment sgmnt) {
            Play(sgmnt.pcmBuffer,sgmnt.pcmLength);
        }
        public void Play(float[] samples,int samplesLength) {
            
            if (samplesBuf == null || samplesBuf.Length != samplesLength + 2) {
                // assume that pcmLength will not change.
                samplesBuf = new float[samplesLength + 2];
                H3VC.Mod.Logger.LogInfo("samplesBufSize is changed:" + samplesLength + 2);
            }
            Array.Copy(samples, 0, samplesBuf,1,samplesLength);
            samplesBuf[0] = samplesBuf[1];
            samplesBuf[samplesBuf.Length-1] = samplesBuf[samplesBuf.Length - 2];

            source.clip.SetData(samplesBuf, head);
            head += samplesBuf.Length; 
            head %= source.clip.samples;

            //Play if the head has advanced enough.
            if (!source.isPlaying && head > delaySamplesLength) {
                H3VC.Mod.Logger.LogInfo("Played");
                source.Play();
            }
            int distance = 0;
            if (source.timeSamples > head) {
                distance = head + source.clip.samples - source.timeSamples;
            } else {
                //ループしていない
                distance = head - source.timeSamples;
            }

            if (distance > delaySamplesLength + delaySamplesLength / 2) {
                int modifedPos = head - delaySamplesLength;
                source.timeSamples = modifedPos < 0 ? source.clip.samples + modifedPos : modifedPos;
            }

            /* 旧補正
            if ( (source.timeSamples + delaySamplesLength/2) > head || source.timeSamples < (head - delaySamplesLength - delaySamplesLength/2) ) {
                int modifedPos = head - delaySamplesLength;
                source.timeSamples = modifedPos < 0 ? source.clip.samples + modifedPos : modifedPos;
            }
            */

            Log();
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
        private int previousHead = 0;
        public void Log() {

            if (!source.isPlaying) {
                return;
            }
            /*
            if(previousTimeSamples == source.timeSamples) {
                return;
            }
            */
            /*
            //int distance = head > source.timeSamples ? head - source.timeSamples : source.clip.samples - source.timeSamples + head;
            int distance;
            if (source.timeSamples > head) {
                distance = head + source.clip.samples - source.timeSamples;
            } else {
                //ループしていない
                distance = head - source.timeSamples;
            }
            Mod.Logger.LogInfo("\nHead:" + head + "\nTimeSamples:" + source.timeSamples + "\nDistance:" + distance + "\nTimeSamplesDelta:" + (source.timeSamples-previousTimeSamples) + "\nHeadDelta:" + (head - previousHead) + "\nDistanceDelta:" + (distance - previousDistance));
            previousDistance = distance;
            previousTimeSamples = source.timeSamples;
            previousHead = head;
            */
            
        }
    }
}
