using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class Commodity : MonoBehaviour {
        public Image imageHead;
        public Text txtPrice;
        public Button btnBuy;
        public Button btnSelect;

        [HideInInspector]
        public string modeName;
        [HideInInspector]
        public bool buyKeyDownSign;
        [HideInInspector]
        public bool selectKeyDownSign;
        [HideInInspector]
        public GameObject mesh;
        [HideInInspector]
        public Material material;
        [HideInInspector]
        public bool doubleBullet;

        private ScoreSystem _ScoreSystem;
        private bool _initSign = false;

        private void Awake() {
            if (!_initSign) {
                Init();
            }
        }
        public void Init() {
            _initSign = true;
            _ScoreSystem = ScoreSystem.Get;
            _ScoreSystem.onTotalScoreChange.AddListener(ScoreChange);
            btnBuy.onClick.AddListener(BtnBuyEvent);
            btnSelect.onClick.AddListener(BtnSelectEvent);
        }

        private void ScoreChange() {
            if (_ScoreSystem.totalScore >= int.Parse(txtPrice.text)) {
                btnBuy.interactable = true;
            } else {
                btnBuy.interactable = false;
            }
        }

        private void BtnBuyEvent() {
            btnBuy.gameObject.SetActive(false);
            btnSelect.gameObject.SetActive(true);
            _ScoreSystem.Sub(int.Parse(txtPrice.text));
            buyKeyDownSign = true;
        }

        private void BtnSelectEvent() {
            GameController gc = GameController.Get;
            if (modeName.Contains("tank")) {
                gc.tankBtn.interactable = true;
                gc.planeBtn.interactable = false;
                gc.turretBtn.interactable = false;
            } else if (modeName.Contains("plane")) {
                gc.tankBtn.interactable = false;
                gc.planeBtn.interactable = true;
                gc.turretBtn.interactable = false;
            } else if (modeName.Contains("turret")) {
                gc.tankBtn.interactable = false;
                gc.planeBtn.interactable = false;
                gc.turretBtn.interactable = true;
            }
            HeadUI.Get.headImage.sprite = imageHead.sprite;
            gc.ChangePlayerModel(mesh,material, doubleBullet,this);
            selectKeyDownSign = true;
        }
    }
}

