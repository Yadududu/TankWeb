using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    //测试主程序
    public class PoolDemo : MonoBehaviour {
        private ObjectPool _TestPool;
        public GameObject prefab;

        public Button testButton;

        // Start is called before the first frame update
        void Start() {
            //m_TestPool = ObjectPoolManager.Instance.CreateObjectPool<CubePool>("CubePool");
            //m_TestPool.Prefab = Prefab;

            //TestButton.onClick.AddListener(() => {
            //    ObjectPoolManager.Instance.GetGameObject("CubePool", new Vector3(0, 0, 0), 2);
            //});

            _TestPool = ObjectPoolManager.Instance.CreateObjectPool<BulletPlayerPool>("BulletPlayerPool");
            _TestPool.prefab = prefab;

            testButton.onClick.AddListener(() => {
                ObjectPoolManager.Instance.GetGameObject("PlayerBulletPool", new Vector3(0, 0, 0), Quaternion.identity, 0);
            });
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

