using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

namespace Complete {
    //使用XML保存游戏信息
    public class XMLSave : SaveData {

        public Text txtTip;
        private string _FilePath;

        private void Awake() {
            _FilePath = Application.dataPath + "/StreamingAssets/SaveData.xml"; //创建XML文件的存储路径
        }
        public override void Save() {
            XmlDocument xmlDoc = new XmlDocument(); //创建XML文档
            XmlElement root = xmlDoc.CreateElement("root");//创建根节点，即最上层节点
            root.SetAttribute("name", "saveFile"); //设置根节点中的值
            XmlElement score = xmlDoc.CreateElement("score");
            score.InnerText = ScoreSystem.Get.totalScore.ToString();
            root.AppendChild(score);//添加子节点
            XmlElement setting = xmlDoc.CreateElement("setting");
            setting.InnerText = SettingUI.Get.num.ToString();
            root.AppendChild(setting);//添加子节点

            XmlElement commodity;
            XmlElement name;
            XmlElement buyBtn;
            XmlElement selectBtn;

            foreach (Commodity comm in comms) {
                commodity = xmlDoc.CreateElement("commodity");

                name = xmlDoc.CreateElement("name");
                name.InnerText = comm.modeName;

                buyBtn = xmlDoc.CreateElement("buyBtn");
                buyBtn.InnerText = comm.buyKeyDownSign ? "1" : "0";

                selectBtn = xmlDoc.CreateElement("selectBtn");
                selectBtn.InnerText = comm.selectKeyDownSign ? "1" : "0";

                commodity.AppendChild(name);
                commodity.AppendChild(buyBtn);
                commodity.AppendChild(selectBtn);
                root.AppendChild(commodity);
            }

            xmlDoc.AppendChild(root);
            xmlDoc.Save(_FilePath);

            if (File.Exists(Application.dataPath + "/StreamingAssets/SaveData.xml")) {
                txtTip.text = "保存成功";
                txtTip.color = new Color(0, 1, 0.65f, 1);
            } else {
                txtTip.text = "保存失败";
                txtTip.color = new Color(0, 1, 0.65f, 1);
            }
        }
        private void Update() {
            if (txtTip!=null) {
                if (txtTip.color.a > 0.2f) {
                    txtTip.color = Color.Lerp(txtTip.color, new Color(0, 1, 0.65f, 0), Time.deltaTime * 0.8f);
                } else {
                    txtTip.color = new Color(0, 0, 0, 0);
                }
            }
            
        }
        public override void Load() {
            StartCoroutine(LoadData());
        }

        IEnumerator LoadData() {
            var _uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, "SaveData.xml"));
            UnityWebRequest request = UnityWebRequest.Get(_uri.AbsoluteUri);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.error);

                ScoreSystem.Get.Add(100);
                comms[0].Init();
                comms[0].btnBuy.onClick.Invoke();
                comms[0].btnSelect.onClick.Invoke();
                Debug.Log("存档文件不存在");
            } else {
                //StringReader sr = new StringReader(request.downloadHandler.text);
                //string result = sr.ReadToEnd();
                //sr.Close();
                //Debug.Log(result);

                //用XML读取文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(request.downloadHandler.text);

                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("commodity");
                if (nodeList.Count != 0) {
                    for (int i = 0; i < nodeList.Count; i++) {
                        if (comms[i].modeName == nodeList[i].ChildNodes[0].InnerText) {
                            comms[i].Init();
                            comms[i].buyKeyDownSign = nodeList[i].ChildNodes[1].InnerText == "0" ? false : true;
                            if (comms[i].buyKeyDownSign) comms[i].btnBuy.onClick.Invoke();
                            comms[i].selectKeyDownSign = nodeList[i].ChildNodes[2].InnerText == "0" ? false : true;
                            if (comms[i].selectKeyDownSign) comms[i].btnSelect.onClick.Invoke();
                        }
                    }
                }

                XmlNodeList score = xmlDoc.GetElementsByTagName("score");
                ScoreSystem.Get.Init();
                ScoreSystem.Get.Add(int.Parse(score[0].InnerText));
                Debug.Log(ScoreSystem.Get.totalScore);
                XmlNodeList setting = xmlDoc.GetElementsByTagName("setting");
                SettingUI.Get.Cover(int.Parse(setting[0].InnerText));
            }
            request.Dispose();
        }
    }
}

