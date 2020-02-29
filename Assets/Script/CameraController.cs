using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Complete {
    public enum CameraMode {
        Camera2D,
        Tank,
        Plane,
        Turret,
    }

    public class CameraController : MonoBehaviour {

        public UnityEvent onChangeMode;
        
        private CameraMode _CameraMode = CameraMode.Camera2D;
        private GameObject _PlayerInstance;

        private float _Height = 2.25f;      //相机距离人物高度
        private float _Distance = 5f;       //相机距离人物距离
        private float _Speed = 4f;         //相机跟随速度
        
        private void Update() {
            if (_PlayerInstance != null) {
                switch (_CameraMode) {
                    case CameraMode.Camera2D:
                        //使用的是正交相机
                        Camera.main.orthographic = true;
                        Camera.main.transform.position = _PlayerInstance.transform.position + new Vector3(0.0f, 50.0f, 0.0f);
                        Camera.main.transform.eulerAngles = new Vector3(90, 0, 0);
                        break;
                    case CameraMode.Tank:
                        Camera.main.orthographic = false;
                        //将人物的相对坐标转换为世界坐标
                        Camera.main.transform.position = _PlayerInstance.transform.TransformPoint(new Vector3(0, 1.86f, -1.46f));
                        Camera.main.transform.rotation = _PlayerInstance.transform.rotation;
                        break;
                    case CameraMode.Plane:
                        Camera.main.orthographic = false;
                        Transform player = _PlayerInstance.transform;
                        //得到这个目标位置
                        Vector3 v3 = player.position + Vector3.up * _Height - player.forward * _Distance;
                        //相机位置
                        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, v3, _Speed * Time.deltaTime);
                        //相机时刻看着人物
                        transform.LookAt(player);
                        break;
                    case CameraMode.Turret:
                        Camera.main.orthographic = false;
                        Camera.main.transform.position = _PlayerInstance.transform.TransformPoint(new Vector3(0.02f, 3.117f, -0.294f));
                        //Camera.main.transform.rotation = _PlayerInstance.transform.rotation;
                        break;
                }
            }
        }

        public void SetCameraMode(CameraMode mode) {
            _CameraMode = mode;
        }
        public void SetCameraMode(CameraMode mode, GameObject player) {
            _CameraMode = mode;
            _PlayerInstance = player;
        }
        public CameraMode GetCameraMode() {
            return _CameraMode;
        }
    }
}

