using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TriggerGridCollider : MonoBehaviour {

		public GameObject GridCollider;
		private void OnTriggerEnter(Collider other) {
			if(other.tag == "Player"){
				GridCollider.SetActive(true);
			}
		}
		private void OnTriggerExit(Collider other) {
			if(other.tag == "Player"){
				GridCollider.SetActive(false);
			}
		}
	}
}
