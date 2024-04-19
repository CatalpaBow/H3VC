using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
namespace H3VC.View{
    public class VoiceRecoderView : MonoBehaviour{
        public static ReactiveProperty<VoiceRecoderView> SingleInstance = new ReactiveProperty<VoiceRecoderView>();

        public IObservable<Unit> OnViewDestroied;
        public IObservable<bool> OnMuteChanged;

        public IObservable<Unit> OnPreviousDevice;
        public IObservable<Unit> OnNextDevice;

        public IObservable<Unit> OnPreviousSoundMode;
        public IObservable<Unit> OnNextSoundMode;

        public IObservable<Unit> OnInceraseVolume;
        public IObservable<Unit> OnDecreaseVolume;

        //Mute
        [SerializeField]
        private Toggle muteToggle;

        //SoundMode
        [SerializeField]
        private Text soundModeName;
        [SerializeField]
        private Button soundModePrevious;
        [SerializeField]
        private Button soundModeNext;

        //Device
        [SerializeField]
        private Text deviceNameText;
        [SerializeField]
        private Button devicePrevious;
        [SerializeField]
        private Button deviceNext;

        [SerializeField]
        private GameObject levelMeter;

        [SerializeField]
        private Text volumeNum;
        [SerializeField]
        private Button decreaseVolume;
        [SerializeField]
        private Button increaseVolume;
        public void OnDestroy() {
            H3VC.Mod.Logger.LogInfo("Destroied!");
        }

        public void Awake() {
            OnMuteChanged =  muteToggle.OnValueChangedAsObservable();

            OnPreviousDevice = devicePrevious.OnClickAsObservable();
            OnNextDevice = deviceNext.OnClickAsObservable();

            OnPreviousSoundMode = soundModePrevious.OnClickAsObservable();
            OnNextSoundMode =  soundModeNext.OnClickAsObservable();
            
            OnDecreaseVolume = decreaseVolume.OnClickAsObservable();
            OnInceraseVolume = increaseVolume.OnClickAsObservable();

            SingleInstance.Value = this;


        }

        public void ChangeSoundMode(string modeName) {
            soundModeName.text = modeName;
        }

        public void ChangeDeviceName(string deviceName) { 
            deviceNameText.text = deviceName;
        }
        public void ChangeVolumeNum(int val) {
            volumeNum.text = val.ToString();
        }
        public void SetVoiceLevel(float val) {
            levelMeter.transform.localScale = new Vector3(val, 1, 1) ;
        }

    }
}
