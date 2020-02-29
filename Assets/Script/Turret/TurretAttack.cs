using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TurretAttack : BaseAttack {

        protected override void AttackControl(bool doubleBullet) {
            ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(0, 1.88f, 2.96f)), transform.rotation, 0);
            ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(0, 1.88f, 2.96f)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
        }
        protected override void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                AttackControl(false);
                ActiveAudio();
            }
        }

    }
}
