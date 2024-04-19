using H3VC.View;
using H3VC.VoiceRecoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.Presenter
{
    public static class PresenterMain{
        public static VoiceRecoderPresenter presenter;
       
        public static void Intialize(VoiceRecoder model) {
            VoiceRecoderView.SingleInstance
                            .Where(instance => instance is not null)
                            .Subscribe(view => { 
                                if(presenter == null) {
                                    presenter = new VoiceRecoderPresenter(model, view);
                                    return;
                                }
                                presenter.ResetView(view);

                            }); 
        }
    }
}
