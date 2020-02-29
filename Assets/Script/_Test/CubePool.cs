using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    //用来对游戏里的对象进行特殊处理 (这里是改变颜色)
    public class CubePool : ObjectPool {
        public override GameObject Get(Vector3 pos, Quaternion rot, float lifetime) {
            GameObject obj;
            obj = base.Get(pos, rot, lifetime);

            obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

            return obj;
        }
    }

}
