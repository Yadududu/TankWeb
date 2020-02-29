using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class TurretHealth : BaseHealth {
         
        public Color fullHealthColor = Color.green;       // 满血状态时的血条颜色
        public Color zeroHealthColor = Color.red;         // 空血的血条状态颜色
        
        private HeadUI _SliderHead;

        private void Awake() {
            _SliderHead = HeadUI.Get;
        }
        private void OnEnable() {
            _HealthValue = systemData.turretHealthValue;
            _SliderHead.slider.maxValue = _HealthValue;
            _SliderHead.slider.value = _HealthValue;
            _CurrentHealth = _HealthValue;
            _SliderHead.fill.color = fullHealthColor;
        }
        protected override void Start() {
            base.Start();
            _CurrentHealth = _HealthValue;
        }
        protected override void OnCollisionEnter(Collision collision) {
            string s = collision.gameObject.tag;

            if (s == "EnemyBullet") {
                SubHealthValue();
                _SliderHead.slider.value = _CurrentHealth;
                if (_CurrentHealth <= 0) {
                    ScoreSystem.Get.Add(ScoreSystem.Get.score);
                    _SliderHead.fill.color = zeroHealthColor;
                    DeadMethod();
                }
            }
        }

    }
}

