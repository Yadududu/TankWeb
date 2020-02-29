using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class HeadUI : MonoBehaviour {

        public static HeadUI Get { get; private set; }
        public Slider slider;
        public Image headImage;
        public Image fill { get; private set; }

        private HeadUI() {
            Get = this;
        }
        private void Awake() {
            fill = slider.fillRect.GetComponent<Image>();
        }
    }

}
