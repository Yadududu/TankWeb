using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class SettingUI : MonoBehaviour {

        public static SettingUI Get { get; private set; }
        public SystemData systemData;
        public Toggle easy;
        public Toggle normal;
        public Toggle hard;
        [HideInInspector]
        public int num = 1;

        private SettingUI() {
            Get = this;
        }
        public void Setting() {
            if (easy.isOn) {
                systemData.Easy();
                num = 1;
            } else if (normal.isOn) {
                systemData.Normal();
                num = 2;
            } else if (hard.isOn) {
                systemData.Hard();
                num = 3;
            }
        }
        public void Cover(int i) {
            switch (i) {
                case 1:
                    easy.isOn = true;
                    normal.isOn = false;
                    hard.isOn = false;
                    systemData.Easy();
                    num = i;
                    break;
                case 2:
                    easy.isOn = false;
                    normal.isOn = true;
                    hard.isOn = false;
                    systemData.Normal();
                    num = i;
                    break;
                case 3:
                    easy.isOn = false;
                    normal.isOn = false;
                    hard.isOn = true;
                    systemData.Hard();
                    num = i;
                    break;
            }
        }
    }
}

