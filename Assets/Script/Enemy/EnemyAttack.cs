using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class EnemyAttack : AutoAttack {
        
        private int _AttackDistance;
        private RaycastHit _Hit;
        private Ray _Ray;
        private CapsuleCollider _Collider;
        
        private void OnEnable() {
            _AttackDistance = systemData.enemyAttackDistance;
            _FireRate = systemData.enemyFireRate;
        }
        private void Start() {
            _Collider = GetComponent<CapsuleCollider>();
        }

        private void Update() {
            AutoAttackMethod();
        }

	    protected override void AutoAttackMethod(){
            _Ray = new Ray(transform.position + new Vector3(0f, _Collider.center.y, 0f), transform.forward);
            Debug.DrawRay(_Ray.origin, _Ray.direction * _AttackDistance, Color.red);

            if (Physics.Raycast(_Ray, out _Hit)) {
                if (_Hit.distance < _AttackDistance) {
                    if (_Hit.collider.gameObject.tag == "Player") {
                        
                        if (Time.time > _NextFire) {
                            _NextFire = Time.time + _FireRate;
                            ObjectPoolManager.Instance.GetGameObject("BulletEnemyPool", transform.TransformPoint(new Vector3(0, 0.57f, 1.97f)), transform.rotation, 0);
                            ObjectPoolManager.Instance.GetGameObject("FireEffectPool", transform.TransformPoint(new Vector3(0, 0.57f, 1.97f)), Quaternion.Euler(90.0f, transform.localEulerAngles.y, 0.0f), 2);
                            ActiveAudio();
                        }
                    }
                }
            }

            
         } 
    }
}

