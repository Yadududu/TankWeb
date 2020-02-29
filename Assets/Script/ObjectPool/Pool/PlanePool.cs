using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class PlanePool : ObjectPool {
        // Start is called before the first frame update
        public override GameObject Get(Vector3 pos, Quaternion rot, float lifetime) {
            GameObject obj;
            obj = base.Get(pos, rot, lifetime);

            return obj;
        }
    }
}


