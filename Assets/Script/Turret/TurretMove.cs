using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete {
    public class TurretMove : BaseMove {
        
		protected override void Start () {
			Speed = systemData.turretSpeed;
		}
		protected override void Update () {
			MoveMethod();
        }

		protected override void MoveMethod(){
			
			float mouseX = Input.GetAxis("Mouse X") * Speed;
			float mouseY = Input.GetAxis("Mouse Y") * Speed;
			
			//要么上下观察，要么左右观察
			if (Mathf.Abs(mouseX) > Mathf.Abs(mouseY)){
                Camera.main.transform.eulerAngles += new Vector3(0, mouseX, 0);
				transform.localRotation = transform.localRotation * Quaternion.Euler(0, mouseX, 0);
			}
			else{
                Camera.main.transform.eulerAngles += new Vector3(-mouseY, 0, 0);//摄像机绕x轴旋转的方向跟鼠标y移动方向相反
                // Debug.Log(Camera3D.transform.eulerAngles.x);
                if (Camera.main.transform.eulerAngles.x <= 350 & Camera.main.transform.eulerAngles.x > 180) {
                    Camera.main.transform.rotation = Quaternion.Euler(new Vector3(-10, Camera.main.transform.eulerAngles.y, 0));
                }
                if (Camera.main.transform.eulerAngles.x >= 45 & Camera.main.transform.eulerAngles.x <= 180) {
                    Camera.main.transform.rotation = Quaternion.Euler(new Vector3(45, Camera.main.transform.eulerAngles.y, 0));
                }
                //Camera.main.transform.eulerAngles =new Vector3(Mathf.Clamp(Camera.main.transform.eulerAngles.x, 10, 45), Camera.main.transform.eulerAngles.y,0);
            }
		}
	}
}

