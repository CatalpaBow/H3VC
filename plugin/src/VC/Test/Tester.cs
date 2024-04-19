using FistVR;
using H3VC.VC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.Test
{
    public static class Tester{
        

        public static void Assert(bool isSucsess) {
            if (isSucsess) {
                Mod.Logger.LogInfo("Test is Sucsesed:");
                return;
            }
            Mod.Logger.LogError("Test is Failed");
        }

        public static void UserListTest() {
            H3VC.Mod.Logger.LogDebug("-----------" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-----------");
            H3VC.Mod.Logger.LogDebug("UserListCount:" + VCMain.userList.crntUserIDs.Count);
            H3VC.Mod.Logger.LogDebug("H3MPPlayersCount:" + H3MP.GameManager.players.Count);
            Assert( VCMain.userList.crntUserIDs.Count == H3MP.GameManager.players.Count);

        }
        public static void Spkr_CountTest() {
            H3VC.Mod.Logger.LogDebug("-----------" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-----------");
            Mod.Logger.LogDebug("spkeakerListCount:" + VCMain.speakers.speakerList.Count);
            Mod.Logger.LogDebug("UserListCount:" + VCMain.userList.crntUserIDs.Count);
            Assert(VCMain.speakers.speakerList.Count == H3MP.GameManager.players.Count);
        }

    }
}
