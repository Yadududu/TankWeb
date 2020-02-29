using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class Player : MonoBehaviour {

        public MeshFilter[] children;

        private MeshFilter _Mesh;
        private MeshRenderer _MeshRenderer;

        private void Awake() {
            _Mesh = GetComponent<MeshFilter>();
            _MeshRenderer = GetComponent<MeshRenderer>();
        }

        public void ChangeModel(GameObject setMesh, Material setMeterial) {
            _Mesh.mesh = setMesh.GetComponent<MeshFilter>().sharedMesh;
            _MeshRenderer.material = setMeterial;
        }
        public void ChangeModelChildren(GameObject setMesh, Material setMeterial) {
            MeshFilter[] mf = setMesh.GetComponentsInChildren<MeshFilter>();
            for (int i=0;i< mf.Length;i++) {
                children[i].mesh = mf[i].sharedMesh;
                children[i].GetComponent<MeshRenderer>().material = setMeterial;
            }
        }
    }
}
