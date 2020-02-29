using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class RelativeObj : MonoBehaviour {

        public GameObject[] positive;
        public GameObject[] negative;

        private void OnEnable() {
            foreach (GameObject obj in positive) {
                obj.SetActive(true);
            }
            foreach (GameObject obj in negative) {
                obj.SetActive(false);
            }
        }
        private void OnDisable() {
            foreach (GameObject obj in positive) {
                obj.SetActive(false);
            }
            foreach (GameObject obj in negative) {
                obj.SetActive(true);
            }
        }
    }
}

