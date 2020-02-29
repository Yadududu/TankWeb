using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    //用来管理多个对象池
    public class ObjectPoolManager : MonoBehaviour {
        public static ObjectPoolManager Instance { get; private set; }

        private Dictionary<string, ObjectPool> _PoolDic;

        private Transform _RootPoolTrans;

        public ObjectPoolManager() {
            Instance = this;

        }
        private void Awake() {
            _PoolDic = new Dictionary<string, ObjectPool>();
            // 根对象池
            GameObject go = new GameObject("ObjcetPoolManager");
            _RootPoolTrans = go.transform;
        }
        // 创建一个新的对象池
        public T CreateObjectPool<T>(string poolName) where T : ObjectPool, new() {
            if (_PoolDic.ContainsKey(poolName)) {
                return _PoolDic[poolName] as T;
            }

            GameObject obj = new GameObject(poolName);
            obj.transform.SetParent(_RootPoolTrans);
            T pool = new T();
            pool.Init(poolName, obj.transform);
            _PoolDic.Add(poolName, pool);
            return pool;
        }

        public GameObject GetGameObject(string poolName, Vector3 position, Quaternion rotation, float lifetTime) {
            if (_PoolDic.ContainsKey(poolName)) {
                return _PoolDic[poolName].Get(position, rotation, lifetTime);
            }
            return null;
        }

        public void RemoveGameObject(string poolName, GameObject go) {
            if (_PoolDic.ContainsKey(poolName)) {
                _PoolDic[poolName].Remove(go);
            }
        }

        // 销毁所有对象池
        public void Destroy() {
            _PoolDic.Clear();
            GameObject.Destroy(_RootPoolTrans);

        }
    }

}
