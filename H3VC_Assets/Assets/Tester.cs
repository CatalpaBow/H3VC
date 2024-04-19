using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tester : MonoBehaviour {
    [SerializeField]

    public void ToggleTest() {
        Debug.Log("Tesetd");
    }
    public void HogeFuga() {

    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.A)){
            ToggleTest();
        }
    }
}
