using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    //用来新建和回收游戏里的对象(prefab) 如 setActive(false)啥的
    public class ObjectPool {

        // 需要缓存的对象
        private GameObject _Prefab;
        public GameObject prefab { get { return _Prefab; } set { _Prefab = value; } }

        protected Transform _Parent;

        private Queue<GameObject> _PoolQueue;

        private string _PoolName;

        // 最大容量
        private int _MaxCount;

        protected const int _DefaultMaxCount = 30;


        public ObjectPool() {
            _MaxCount = _DefaultMaxCount;
            _PoolQueue = new Queue<GameObject>();
        }

        public virtual void Init(string poolName, Transform transform) {
            _PoolName = poolName;
            _Parent = transform;
        }

        public virtual GameObject Get(Vector3 pos, Quaternion rot, float lifetime) {
            if (lifetime < 0) {
                return null;
            }
            GameObject returnObj;
            if (_PoolQueue.Count > 0) {
                returnObj = _PoolQueue.Dequeue();
            } else {
                // 池中没有可分配对象了，新生成一个
                returnObj = GameObject.Instantiate<GameObject>(prefab);
                returnObj.transform.SetParent(_Parent);
                returnObj.SetActive(false);
            }
            // 使用PrefabInfo脚本保存returnObj的一些信息
            ObjectInfo info = returnObj.GetComponent<ObjectInfo>();
            if (info == null) {
                info = returnObj.AddComponent<ObjectInfo>();
            }

            info.poolName = _PoolName;
            if (lifetime > 0) {
                info.lifetime = lifetime;
            }

            returnObj.transform.position = pos;
            returnObj.transform.rotation = rot;
            returnObj.SetActive(true);

            return returnObj;
        }

        // "销毁对象" 其实是回收对象
        public virtual void Remove(GameObject obj) {
            if (_PoolQueue.Contains(obj)) {
                return;
            }

            if (_PoolQueue.Count > _MaxCount) {
                // 对象池已满 直接销毁
                GameObject.Destroy(obj);
            } else {
                // 放入对象池
                _PoolQueue.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        public virtual void Destroy() {
            _PoolQueue.Clear();
        }


    }
}
   