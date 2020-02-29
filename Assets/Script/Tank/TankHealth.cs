using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class TankHealth : BaseHealth {

        public Slider sliderSelf;
        public Color fullHealthColor = Color.green;       // 满血状态时的血条颜色
        public Color zeroHealthColor = Color.red;         // 空血的血条状态颜色
        
        private Image _Fill;
        private HeadUI _SliderHead;

        private void Awake() {
            _SliderHead = HeadUI.Get;
            _Fill = sliderSelf.fillRect.GetComponent<Image>();
        }
        private void OnEnable() {
            _HealthValue = systemData.tankHealthValue;
            _SliderHead.slider.maxValue = _HealthValue;
            _SliderHead.slider.value = _HealthValue;
            sliderSelf.maxValue = _HealthValue;
            sliderSelf.value = _HealthValue;
            _CurrentHealth = _HealthValue;
            _SliderHead.fill.color = fullHealthColor;
            _Fill.color = fullHealthColor;
        }

        protected override void Start(){
            base.Start();
            _CurrentHealth = _HealthValue;
        }

        private void UpdateUIHealth() {
            _SliderHead.slider.value = _CurrentHealth;
            sliderSelf.value = _CurrentHealth;
            // 差值运算得到显示的过度颜色，从0~100之间取值
            _Fill.color = Color.Lerp(zeroHealthColor, fullHealthColor, _CurrentHealth / _HealthValue);
        }
        
        protected override void OnCollisionEnter(Collision collision) {
            string s = collision.gameObject.tag;

            if (s == "EnemyBullet") {
                SubHealthValue();
                UpdateUIHealth();
                if (_CurrentHealth <= 0) {
                    ScoreSystem.Get.Add(ScoreSystem.Get.score);
                    _SliderHead.fill.color = zeroHealthColor;
                    DeadMethod();
                }
            }
        }
        
    }
}

