using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete {
    public class SaveData : MonoBehaviour {
        public int score;
        public List<Commodity> comms = new List<Commodity>();

        public virtual void Save() {

        }
        public virtual void Load() {

        }
    }
}
