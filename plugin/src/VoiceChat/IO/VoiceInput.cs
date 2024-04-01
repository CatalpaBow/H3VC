﻿using System;
using UniRx;
using UnityEngine;
namespace H3VC.IO
{
    public class VoiceInput
    {
        const int samplingFrequency = 48000;
        const int lengthSeconds = 1;

        AudioClip clip;
        int head = 0;
        float[] processBuffer = new float[512];
        float[] microphoneBuffer = new float[lengthSeconds * samplingFrequency];

        public IObservable<float[]> OnVoiceReady => OnVoiceReadyStream;
        private Subject<float[]> OnVoiceReadyStream;
        private IDisposable disposer;
        public VoiceInput() {
            OnVoiceReadyStream = new Subject<float[]>();
        }


        public void Start(int interval) {
            clip = Microphone.Start(null, true, lengthSeconds, samplingFrequency);
            disposer = Observable.Interval(TimeSpan.FromMilliseconds(16))
                      .Subscribe(_ => NextSegment());

        }
        public void Stop() {
            disposer.Dispose();
        }

        public float GetRMS() {
            float sum = 0.0f;
            foreach (var sample in processBuffer) {
                sum += sample * sample;
            }
            return Mathf.Sqrt(sum / processBuffer.Length);
        }

        public void NextSegment() {
            var position = Microphone.GetPosition(null);
            if (position < 0 || head == position) {
                return;
            }

            clip.GetData(microphoneBuffer, 0);
            while (GetDataLength(microphoneBuffer.Length, head, position) > processBuffer.Length) {
                var remain = microphoneBuffer.Length - head;
                if (remain < processBuffer.Length) {
                    Array.Copy(microphoneBuffer, head, processBuffer, 0, remain);
                    Array.Copy(microphoneBuffer, 0, processBuffer, remain, processBuffer.Length - remain);
                } else {
                    Array.Copy(microphoneBuffer, head, processBuffer, 0, processBuffer.Length);
                }

                OnVoiceReadyStream.OnNext(processBuffer);

                head += processBuffer.Length;
                if (head > microphoneBuffer.Length) {
                    head -= microphoneBuffer.Length;
                }
            }
        }

        static int GetDataLength(int bufferLength, int head, int tail) {
            if (head < tail) {
                return tail - head;
            } else {
                return bufferLength - head + tail;
            }
        }
    }
}
