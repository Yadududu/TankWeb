using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class PlaneMove : BaseMove {

        public Transform Head;
        public Transform LeftAirfoil;
        public Transform RightAirfoil;
        public Transform LeftTailAirfoil;
        public Transform RightTailAirfoil;

        private float _UpForce;
        private float _TailUpForce;
        private float _WLiftSpeed;
        private float _SLiftSpeed;
        private float _ALiftSpeed;
        private float _DLiftSpeed;
        private Rigidbody _Rb;

        protected override void Start() {
            _Rb = GetComponent<Rigidbody>();
            _UpForce = systemData.planeUpForce;
            _TailUpForce = systemData.planeTailUpForce;
            Speed = systemData.planeSpeed;
            _WLiftSpeed = systemData.plane_W_LiftSpeed;
            _SLiftSpeed = systemData.plane_S_LiftSpeed;
            _ALiftSpeed = systemData.plane_A_LiftSpeed;
            _DLiftSpeed = systemData.plane_D_LiftSpeed;
        }

        protected override void Update() {
            MoveMethod();
        }

        protected override void MoveMethod() {
            _Rb.AddForceAtPosition(transform.forward * Speed, Head.position);

            _Rb.AddForceAtPosition(transform.up * _UpForce, LeftAirfoil.position);
            _Rb.AddForceAtPosition(transform.up * _UpForce, RightAirfoil.position);

            _Rb.AddForceAtPosition(transform.up * _TailUpForce, LeftTailAirfoil.position);
            _Rb.AddForceAtPosition(transform.up * _TailUpForce, RightTailAirfoil.position);

            //俯冲
            if (Input.GetKey(KeyCode.W)) {
                _Rb.AddForceAtPosition(transform.up * _WLiftSpeed, LeftTailAirfoil.position);
                _Rb.AddForceAtPosition(transform.up * _WLiftSpeed, RightTailAirfoil.position);
            }
            //爬升
            else if (Input.GetKey(KeyCode.S)) {
                _Rb.AddForceAtPosition(transform.up * -_SLiftSpeed, LeftTailAirfoil.position);
                _Rb.AddForceAtPosition(transform.up * -_SLiftSpeed, RightTailAirfoil.position);
            }
            //左翻滚
            else if (Input.GetKey(KeyCode.A)) {
                _Rb.AddForceAtPosition(transform.up * -_ALiftSpeed, LeftTailAirfoil.position);
                _Rb.AddForceAtPosition(transform.up * _ALiftSpeed, RightTailAirfoil.position);
            }
            //右翻滚
            else if (Input.GetKey(KeyCode.D)) {
                _Rb.AddForceAtPosition(transform.up * _DLiftSpeed, LeftTailAirfoil.position);
                _Rb.AddForceAtPosition(transform.up * -_DLiftSpeed, RightTailAirfoil.position);
            }
        }
    }
}


