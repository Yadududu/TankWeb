using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class CreateEnemy : MonoBehaviour {

        public SystemData systemData;

        private float _CreateRate;
        private float _NextCreate;
        private int _Direction;
        private List<GameObject> _PrefabPool;
        private int _Uplimit = 2;

        private void OnEnable() {
            _CreateRate = systemData.enemyCreateRate;
            _Uplimit = systemData.enemyUplimit;
        }
        private void Start() {
            _PrefabPool = new List<GameObject>();
        }
        private void Update() {
            if ((Time.time > _NextCreate) & (_PrefabPool.Count < _Uplimit)) {

                _Direction = Random.Range(0, 4);
                _Direction = _Direction * 90;

                _NextCreate = Time.time + _CreateRate;
                GameObject _prefabInstance = ObjectPoolManager.Instance.GetGameObject("EnemyPool", transform.position, Quaternion.Euler(0.0f, _Direction, 0.0f), 0);
                _prefabInstance.GetComponent<EnemyHealth>().onDead.AddListener(()=>Remove(_prefabInstance));
                _PrefabPool.Add(_prefabInstance);
            }
        }
        private void Remove(GameObject go) {
            go.GetComponent<EnemyHealth>().onDead.RemoveAllListeners();
            _PrefabPool.Remove(go);
        }
        private void RemoveAll() {
            foreach (GameObject o in _PrefabPool) {
                o.GetComponent<EnemyHealth>().onDead.RemoveAllListeners();
                o.GetComponent<ObjectInfo>().RemoveGameObject();
            }
            _PrefabPool.Clear();
        }
        private void OnDisable() {
            RemoveAll();
        }
    }
}

