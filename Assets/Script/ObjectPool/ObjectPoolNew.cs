using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class ObjectPoolNew : MonoBehaviour {

        public GameObject tank;
        public GameObject plane;
        public GameObject turret;
        public GameObject enemy;
        public GameObject bulletPlayer;
        public GameObject bulletEnemy;
        public GameObject fireEffect;
        public GameObject bulletBoomEffect;
        public GameObject objBoomEffect;

        private ObjectPool _tankPool;
        private ObjectPool _planePool;
        private ObjectPool _turretPool;
        private ObjectPool _enemyPool;
        private ObjectPool _bulletPlayerPool;
        private ObjectPool _bulletEnemyPool;
        private ObjectPool _fireEffectPool;
        private ObjectPool _bulletBoomEffectPool;
        private ObjectPool _objBoomEffectPool;


        private void Start() {
            _tankPool = ObjectPoolManager.Instance.CreateObjectPool<TankPool>("TankPool");
            _tankPool.prefab = tank;
            _planePool = ObjectPoolManager.Instance.CreateObjectPool<PlanePool>("PlanePool");
            _planePool.prefab = plane;
            _turretPool = ObjectPoolManager.Instance.CreateObjectPool<TurretPool>("TurretPool");
            _turretPool.prefab = turret;
            _enemyPool = ObjectPoolManager.Instance.CreateObjectPool<EnemyPool>("EnemyPool");
            _enemyPool.prefab = enemy;
            _bulletPlayerPool = ObjectPoolManager.Instance.CreateObjectPool<BulletPlayerPool>("BulletPlayerPool");
            _bulletPlayerPool.prefab = bulletPlayer;
            _bulletEnemyPool = ObjectPoolManager.Instance.CreateObjectPool<BulletEnemyPool>("BulletEnemyPool");
            _bulletEnemyPool.prefab = bulletEnemy;
            _fireEffectPool = ObjectPoolManager.Instance.CreateObjectPool<FireEffectPool>("FireEffectPool");
            _fireEffectPool.prefab = fireEffect;
            _bulletBoomEffectPool = ObjectPoolManager.Instance.CreateObjectPool<BulletBoomEffectPool>("BulletBoomEffectPool");
            _bulletBoomEffectPool.prefab = bulletBoomEffect;
            _objBoomEffectPool = ObjectPoolManager.Instance.CreateObjectPool<ObjBoomEffectPool>("ObjBoomEffectPool");
            _objBoomEffectPool.prefab = objBoomEffect;
        }
    }
}
 
