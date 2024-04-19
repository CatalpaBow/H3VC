using H3VC.View;
using H3VC.VoiceRecoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.Presenter
{
    public class VoiceRecoderPresenter{
        private VoiceRecoder model;
        private VoiceRecoderView view;

        public VoiceRecoderPresenter(VoiceRecoder model,VoiceRecoderView view) {
            this.model = model;
            this.view = view;
            Intialize();
        }
        public void Intialize() {
            /* View to model */
            view.OnMuteChanged.Subscribe(isMute => model.isMute.Value = isMute).AddTo(view);
            
            //soundmode
            view.OnNextSoundMode.Subscribe(_ => model.NextSoundMode()).AddTo(view) ;
            view.OnPreviousSoundMode.Subscribe(_ => model.PreviousSoundMode()).AddTo(view);

            //Device
            view.OnNextDevice.Subscribe(_ => model.NextDevice()).AddTo(view);
            view.OnPreviousDevice.Subscribe(_ => model.PreviousDevice()).AddTo(view);

            //Volume
            view.OnDecreaseVolume.Subscribe(_ => model.DecreaseVolume());
            view.OnInceraseVolume.Subscribe(_ => model.IncreaseVolume());
            

            /* Model to View */
            model.soundMode.Subscribe(mode => view.ChangeSoundMode(mode.ToString())).AddTo(view);
            model.crntDevice.Subscribe(info => view.ChangeDeviceName(info.name)).AddTo(view);
            model.VoiceLevel.Subscribe(level => view.SetVoiceLevel(level)).AddTo(view);
            model.volume.Select(val => val*100)
                        .Subscribe(percent => view.ChangeVolumeNum((int)percent));
        }

        public void ResetView(VoiceRecoderView view) {
            this.view = view;
            Intialize();
        }
    }
}
