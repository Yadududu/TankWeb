using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class EnemyMove : BaseMove {
        
        private void OnEnable() {
            Speed = systemData.enemyMoveSpeed;
        }
        protected override void Update() {
            MoveMethod();
        }
        protected override void MoveMethod() {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        private void OnCollisionStay() {
            Turn();
        }
        private void Turn() {
            int num = Random.Range(0, 4);
            num = num * 90;
            if (num == transform.localEulerAngles.y) {
                Turn();
                return;
            }
            transform.localEulerAngles = new Vector3(0.0f, num, 0.0f);
        }
    }
}

