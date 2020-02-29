using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Complete {
	public class BaseHealth : MonoBehaviour {

        public SystemData systemData;
        public UnityEvent onDead;

        protected int _HealthValue = 1;       // 预设生命值
        protected float _CurrentHealth;  		// 当前生命值
        protected ObjectInfo _ObjectInfo;

        protected virtual void Start() {
            _CurrentHealth = _HealthValue;
            _ObjectInfo = GetComponent<ObjectInfo>();
        }

        protected void SubHealthValue() {
			_CurrentHealth -= 1;
		}
		protected void AddHealthValue() {
			_CurrentHealth += 1;
		}
        protected virtual void OnCollisionEnter(Collision collision) {
            string s = collision.gameObject.tag;
            if (s == "EnemyBullet") {
                SubHealthValue();
                if (_CurrentHealth <= 0) DeadMethod();
            }
        }
        public virtual void DeadMethod() {
            ObjectPoolManager.Instance.GetGameObject("ObjBoomEffectPool", gameObject.transform.position - new Vector3(0f, 1.4f, 0f), Quaternion.identity, 1);
            _ObjectInfo.RemoveGameObject();
            onDead.Invoke();
        }

	}
}

