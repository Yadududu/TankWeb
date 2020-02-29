using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
	public class AutoAttack : MonoBehaviour {

		public SystemData systemData;
        
		protected float _FireRate;
        protected float _NextFire;
        
        protected virtual void AutoAttackMethod() {

        }

		protected void ActiveAudio(){
            GetComponent<AudioSource>().Play();
		}
	}

}
