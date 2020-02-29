using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    //用来记录对象信息 并且 定时回收对象(可根据需要自己看着办修改)
    public class ObjectInfo : MonoBehaviour {
        public float lifetime = 0;
        public string poolName;

        private WaitForSeconds _WaitTime;

        private void Awake() {
            if (lifetime > 0) {
                _WaitTime = new WaitForSeconds(lifetime);
            }
        }

        private void OnEnable() {
            if (lifetime > 0) {
                StartCoroutine(CountDown(lifetime));
            }
        }

        //手动调用回收
        public void RemoveGameObject() {
            ObjectPoolManager.Instance.RemoveGameObject(poolName, gameObject);
        }

        IEnumerator CountDown(float lifetime) {
            yield return _WaitTime;
            ObjectPoolManager.Instance.RemoveGameObject(poolName, gameObject);
        }
    }
}

