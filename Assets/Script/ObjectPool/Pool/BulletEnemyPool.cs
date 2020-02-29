using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class BulletEnemyPool : ObjectPool {
        public override GameObject Get(Vector3 pos, Quaternion rot, float lifetime) {
            GameObject obj;
            obj = base.Get(pos, rot, lifetime);

            return obj;
        }
    }
}

