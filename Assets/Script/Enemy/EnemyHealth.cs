using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class EnemyHealth : BaseHealth {
        
        private void OnEnable() {
            _HealthValue = systemData.enemyHealthValue;
            _CurrentHealth = _HealthValue;
        }

        protected override void OnCollisionEnter(Collision collision) {
            string s = collision.gameObject.tag;
            if (s == "Bullet") {
                SubHealthValue();
                if (_CurrentHealth <= 0) DeadMethod();
            }
        }

    }
}

