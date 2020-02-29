using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class PlaneAttack : BaseAttack {
        
        protected override void AttackControl(bool doubleBullet) {
            if (doubleBullet) {
                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(1.4f, 0f, 3f)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(1.4f, 0f, 3f)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);

                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(-1.4f, 0f, 3)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(-1.4f, 0f, 3)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
            } else {
                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", transform.TransformPoint(new Vector3(0f, 0f, 4)), transform.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(0f, 0f, 4)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
            }
        }
    }
}

