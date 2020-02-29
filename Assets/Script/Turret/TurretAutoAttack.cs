using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TurretAutoAttack : AutoAttack {

        public static TurretAutoAttack Get { get; private set; }
        public Transform gun;
        
        private Vector3 _Target;

        private TurretAutoAttack() {
            Get = this;
        }
        private void OnEnable() {
            transform.localPosition = new Vector3(91.6f, 0.03f, 88.2f);
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            gun.tag = "Untagged";
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<TurretMove>().enabled = false;
            GetComponent<TurretAttack>().enabled = false;
            Camera.main.GetComponent<CameraController>().SetCameraMode(CameraMode.Camera2D);
        }
        private void OnDisable() {
            transform.localPosition = new Vector3(91.6f, 0.03f, 88.2f);
            transform.localEulerAngles = Vector3.zero;
            gun.localEulerAngles = Vector3.zero;
            gun.tag = "Player";
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<TurretMove>().enabled = true;
            GetComponent<TurretAttack>().enabled = true;
            this.enabled = false;
            Camera.main.GetComponent<CameraController>().SetCameraMode(CameraMode.Turret);
        }
        private void Start() {
            _FireRate = systemData.turretFireRate;
        }
        private void OnTriggerStay(Collider collider) {
            if (collider.gameObject.tag == "Enemy") {
                _Target = collider.transform.position;
                AutoAttackMethod();
            }
        }
        protected override void AutoAttackMethod() {
            if (Time.time > _NextFire) {
                _NextFire = Time.time + _FireRate;

                gun.LookAt(_Target);
                //Gun.transform.rotation = Quaternion.LookRotation(Target - Gun.transform.position);
                
                //Debug.DrawLine(transform.position, Target, Color.blue);
                //Quaternion.Slerp(Gun.transform.rotation, Quaternion.LookRotation(Target - Gun.transform.position), NextFire);

                if (gun.localEulerAngles.x > 0) {
                    gun.rotation = Quaternion.Euler(0f, gun.localEulerAngles.y, 0f);
                }
                ActiveAudio();
                ObjectPoolManager.Instance.GetGameObject("BulletPlayerPool", gun.TransformPoint(new Vector3(0, 1.88f, 2.96f)), gun.rotation, 0);
                ObjectPoolManager.Instance.GetGameObject("FireEffectPool", gun.TransformPoint(new Vector3(0, 1.88f, 2.96f)), Quaternion.Euler(90.0f, gun.localEulerAngles.y, 0.0f), 2);
            }

        }
    }
}
