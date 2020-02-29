using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TankBullet : BaseBullet {

        private void OnEnable() {
            _BulletSpeed = systemData.tankBulletSpeed;
        }
        protected override void OnCollisionEnter(Collision collision) {
            
            string s = collision.gameObject.tag;
            if (s == "Enemy") {
                ScoreSystem.Get.Gain();
            }
            base.OnCollisionEnter(collision);
        }
    }
}

