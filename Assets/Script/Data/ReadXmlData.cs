using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


namespace Complete {
    //读取商品信息,实例化到商店
    public class ReadXmlData : MonoBehaviour {

        public GameObject commPerfab;
        public Transform commParent;
        public XMLSave saveData;

        private GameObject _InstancePerfab;
        private List<Data> _DataList = new List<Data>();
        private Commodity _Comm;

        private void Start() {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/StreamingAssets/TankData.xml");
            XmlNode rootNode = xmlDoc.FirstChild;
            XmlNodeList nodeList = rootNode.ChildNodes;
            foreach (XmlNode partialNode in nodeList) {
                XmlNodeList dataNodeList = partialNode.ChildNodes;
                foreach (XmlNode dataNode in dataNodeList) {
                    Data data = new Data();
                    XmlAttributeCollection col = dataNode.Attributes;
                    data.name = col["name"].Value;
                    data.price = col["price"].Value;
                    data.bullet = col["bullet"].Value;
                    data.image = col["image"].Value;
                    data.mesh = col["model"].Value;
                    data.material = col["material"].Value;
                    _DataList.Add(data);
                }
            }
            //foreach (Data data in _dataList) {
            //    Debug.Log(data.ToString());
            //}
            for (int i = 0; i < _DataList.Count; i++) {
                _InstancePerfab = Instantiate(commPerfab, commParent);
                _Comm = _InstancePerfab.GetComponent<Commodity>();
                _Comm.imageHead.sprite = Resources.Load(_DataList[i].image, typeof(Sprite)) as Sprite;
                _Comm.txtPrice.text = _DataList[i].price;
                _Comm.mesh = Resources.Load(_DataList[i].mesh, typeof(GameObject)) as GameObject;
                _Comm.material = Resources.Load(_DataList[i].material, typeof(Material)) as Material;
                _Comm.doubleBullet = _DataList[i].bullet == "2" ? true : false;
                _Comm.modeName = _DataList[i].name;
                saveData.comms.Add(_Comm);
            }

            saveData.Load();
        }
    }
    public class Data {
        public string name;
        public string price;
        public string bullet;
        public string image;
        public string mesh;
        public string material;

        public override string ToString() {
            return "name:" + name + ",price:" + price + ",image:" + image;
        }
    }
}
    