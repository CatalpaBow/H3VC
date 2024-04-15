using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using H3MP;
using H3MP.Scripts;
namespace H3VC.VCUsers
{
    public class VCUserList{
        public IReadOnlyReactiveCollection<int> crntUserIDs => _crntUsers;
        private ReactiveCollection<int> _crntUsers;
        private static VCUserList _instance;
        public static VCUserList Instance = _instance ?? (_instance = new VCUserList());

        public IObservable<int> OnJoined => crntUserIDs.ObserveAdd().Select(evnt => evnt.Value);
        public IObservable<int> OnLeaved => crntUserIDs.ObserveRemove().Select(evnt => evnt.Value);

        public VCUserList() {
            _crntUsers = [.. GameManager.players.Keys];

            Observable.FromEvent<GameManager.OnPlayerAddedDelegate, PlayerManager>(
                h => h.Invoke,
                h => GameManager.OnPlayerAdded += h,
                h => GameManager.OnPlayerAdded -= h)
                .Select(plMngr => plMngr.ID)
                .Subscribe(_crntUsers.Add);

            Observable.FromEvent<H3MP.Mod.OnPlayerRemovedDelegate, PlayerManager>(
                h => h.Invoke,
                h => H3MP.Mod.OnPlayerRemoved += h,
                h => H3MP.Mod.OnPlayerRemoved -= h)
                .Select(plMngr => plMngr.ID)
                .Subscribe(id => _crntUsers.Remove(id));
        }

    }
}
