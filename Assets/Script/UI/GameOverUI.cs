using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete {
    public class GameOverUI : MonoBehaviour {
        public static GameOverUI Get{ get; private set; }

        public Button btnRestart;

        GameOverUI() {
            Get = this;
        }

        public void SetRestartBtn(State state,GameController gameController) {
            switch (state) {
                case State.TankMode:
                    btnRestart.onClick.AddListener(gameController.PlayTank);
                    break;
                case State.PlaneMode:
                    btnRestart.onClick.AddListener(gameController.PlayPlane);
                    break;
                case State.TurretMode:
                    btnRestart.onClick.AddListener(gameController.PlayTurret);
                    break;
            }
        }
        private void OnEnable() {
            WarningUI.Get.gameObject.SetActive(false);
        }
        private void OnDisable() {
            btnRestart.onClick.RemoveAllListeners();
        }
    }
}
