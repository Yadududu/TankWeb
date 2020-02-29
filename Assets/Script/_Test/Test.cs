using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public float mun;

    void Start() {

    }
    
    void Update() {
        Ray ray = new Ray(transform.position,transform.forward);
        Debug.DrawRay(ray.origin, ray.direction*5, Color.red);

        //检测单个碰撞
        //RaycastHit hit;
        //if (Physics.Raycast(ray,out hit)) {
        //    Debug.Log(hit.collider.name);
        //}

        //检测多个碰撞
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction * 5);
        foreach (RaycastHit h in hits) {
            Debug.Log(h.collider.name);
        }

        Debug.Log(Mathf.Clamp(mun, -10, 45));
    }
}
