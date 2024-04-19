using H3MP;
using H3MP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.H3MPWrapper
{
    public static class Players{
        public static string CurrentScene(int id) {
            if(id == IDSelf()) {
                return GameManager.scene;
            }
            PlayerManager mngr;
            if(H3MP.GameManager.players.TryGetValue(id, out mngr)) {
                return mngr.scene;
            }
            return "";
        }
        public static int IDSelf() {
            return H3MP.GameManager.ID;
        }
    }
}
