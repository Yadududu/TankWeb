using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//就一个向上运动的脚本
public class MoveUp : MonoBehaviour {
    void Update() {
        transform.position += Vector3.up * Time.deltaTime * 2;
    }
}
