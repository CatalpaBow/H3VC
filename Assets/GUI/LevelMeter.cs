using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class LevelMeter : MonoBehaviour
{
    //更新する対象のlevelMeter(uGUI Image)
    Image levelMeterImage = null;

    //このdBでlevelMeter表示の下限に到達する
    [SerializeField]
    private float dB_Min = -80.0f;

    //このdBでlevelMeter表示の上限に到達する
    [SerializeField]
    private float dB_Max = -0.0f;

    void Awake() {
        //更新する対象のImageを取得
        levelMeterImage = GetComponent<Image>();
    }

    void Update() {
    }

    /// <summary>
    /// dB_Minとdb_Maxに基づいてdBをfillAmount値に変換
    /// </summary>
    /// <param name="dB">dB値</param>
    /// <returns>fillAmount値</returns>
    float dB_ToFillAmountValue(float dB) {
        //入力されたdBをdB_MaxとdBMin値で切り捨て
        float modified_dB = dB;
        if (modified_dB > dB_Max) { modified_dB = dB_Max; } else if (modified_dB < dB_Min) { modified_dB = dB_Min; }

        //fillAmount値に変換(dB_Min=0.0f, dB_Max=1.0f)
        float fillAountValue = 1.0f + (modified_dB / (dB_Max - dB_Min));
        return fillAountValue;
    }

}