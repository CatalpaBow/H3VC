using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
namespace H3VC.src.VC.VoiceRecoders
{
    public class MicLevelMeter
    {
        /*
        private IMicrophone voiceSrc;
        public float crntLevel { get; private set; }
        private Subject<float> OnCrntLevelUpdatedStream;
        public IObservable<float> OnCrntLevelUpdated => OnCrntLevelUpdatedStream;
        //このdBでlevelMeter表示の下限に到達する
        [SerializeField]
        private float dB_Min = -80.0f;
        //このdBでlevelMeter表示の上限に到達する
        [SerializeField]
        private float dB_Max = -0.0f;

        public MicLevelMeter(IMicrophone src)
        {
            voiceSrc = src;
            OnCrntLevelUpdatedStream = new Subject<float>();
            Intialize();
        }
        private void Intialize()
        {
            voiceSrc.OnAudioReady
                .Select(samples => samples.Average())
                .Select(ToDecibel)
                .Select(Normalization)
                .Subscribe(OnCrntLevelUpdatedStream.OnNext);
        }
        float Normalization(float dB)
        {
            //入力されたdBをdB_MaxとdBMin値で切り捨て
            float modified_dB = dB;
            if (modified_dB > dB_Max) { modified_dB = dB_Max; } else if (modified_dB < dB_Min) { modified_dB = dB_Min; }

            //0～１で正規化(dB_Min=0.0f, dB_Max=1.0f)
            float fillAountValue = 1.0f + modified_dB / (dB_Max - dB_Min);
            return fillAountValue;
        }
        float ToDecibel(float sampleAverage)
        {
            return 20.0f * Mathf.Log10(sampleAverage);
        }

        */
    }
}
