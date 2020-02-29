using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransformPoint : MonoBehaviour {

    public Transform cube1;
    public Transform cube2;
    public Transform cube3;
    
    private void OnEnable() {
        //Debug.Log(cube1.TransformPoint(cube2.localPosition));
        Debug.Log(transform.TransformVector(new Vector3(0,0,2)));
        Debug.Log(cube2.position);
    }
    
}
