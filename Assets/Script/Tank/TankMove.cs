using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TankMove : BaseMove {
        
        private const int _UP = 0;
        private const int _RIGHT = 1;
        private const int _DOWN = 2;
        private const int _LEFT = 3;
        
        private bool _Mode3D = false;
        private int _State = 0;
        private Vector3 _PlayerRotation;
        private float _MoveHorizontal;
        private float _MoveVertical;

        protected override void Start() {
            Speed = systemData.tankMoveSpeed;
        }
        private void OnDisable() {
            SetRotation(_UP);
            _Mode3D = false;
            transform.rotation = Quaternion.Euler(0.0f, _PlayerRotation.y, 0.0f);
        }
        protected override void Update() {
            _MoveHorizontal = Input.GetAxis("Horizontal");
            _MoveVertical = Input.GetAxis("Vertical");
            MoveMethod();

            if (Input.GetKeyDown(KeyCode.Q)) {
                SetMode();
            }
        }
        private void SetMode() {
            if (_Mode3D == true) {
                _Mode3D = false;
                transform.rotation = Quaternion.Euler(0.0f, _PlayerRotation.y, 0.0f);
            } else {
                _Mode3D = true;
                _PlayerRotation = transform.localEulerAngles;
            }
        }

        protected override void MoveMethod(){
            if (_Mode3D == true) {

                Vector3 move = new Vector3(0f, 0f, _MoveVertical);
                Vector3 rota = new Vector3(0f, _MoveHorizontal, 0f);

                transform.Translate(move * Time.deltaTime * Speed);
                transform.Rotate(rota * Time.deltaTime * 50);

            } else {

                if (_MoveHorizontal == 1) {
                    SetRotation(_RIGHT);
                } else if (_MoveHorizontal == -1) {
                    SetRotation(_LEFT);
                } else if (_MoveVertical == 1) {
                    SetRotation(_UP);
                } else if (_MoveVertical == -1) {
                    SetRotation(_DOWN);
                }
            }
        }

        private void SetRotation(int newState) {
            int rotateValue = (newState - _State) * 90;
            Vector3 transformValue = new Vector3();

            switch (newState) {
                case _UP:
                    transformValue = Vector3.forward * Time.deltaTime;
                    break;
                case _DOWN:
                    transformValue = Vector3.back * Time.deltaTime;
                    break;
                case _LEFT:
                    transformValue = Vector3.left * Time.deltaTime;
                    break;
                case _RIGHT:
                    transformValue = Vector3.right * Time.deltaTime;
                    break;
            }

            transform.Rotate(Vector3.up, rotateValue);
            transform.Translate(transformValue * Speed, Space.World);
            _State = newState;
        }
    }
}

