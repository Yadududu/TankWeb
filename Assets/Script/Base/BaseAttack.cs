using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class BaseAttack : MonoBehaviour {

        private bool _DoubleBullet;
        protected virtual void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                AttackControl(_DoubleBullet);
                ActiveAudio();
            }
        }
        public void SetDoubleBullet(bool b) {
            _DoubleBullet = b;
        }
        protected virtual void AttackControl(bool doubleBullet) {
            if (doubleBullet) {
                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(1.6f, 0.64f, 2)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(1.6f, 0.64f, 2)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);

                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(-1.6f, 0.64f, 2)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(-1.6f, 0.64f, 2)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
            } else {
                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(0, 0.57f, 1.41f)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(0, 0.57f, 1.41f)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
            }
        }
        protected void ActiveAudio() {
            GetComponent<AudioSource>().Play();
        }
    }
}
