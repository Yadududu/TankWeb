using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Complete {

    public enum State {
        GeneralMode,
        TankMode,
        PlaneMode,
        TurretMode,
    }

    public class GameController : MonoBehaviour {

        public static GameController Get { get; private set; }
        [Header("UI")]
        public Button tankBtn;
        public Button planeBtn;
        public Button turretBtn;
        [Header("Music")]
        public AudioClip menuMusic;
        public AudioClip backGroundMusic;
        
        public UnityEvent onGameStart;
        public UnityEvent onGameOver;

        private Transform _EnemyLifeSpot1;
        private Transform _EnemyLifeSpot2;
        private GameObject _PlayerInstance;
        public State _State { get; private set; }
        private CameraController _CameraController;
        private GameObject _Mesh;
        private Material _Material;
        private Commodity _Comm;
        private Commodity _LastComm;
        private bool _DoubleBullet;
        private AudioSource _AudioSource;

        private GameController() {
            Get = this;
            _State = State.GeneralMode;
        }
        private void Start() {
            _CameraController = Camera.main.GetComponent<CameraController>();
            _AudioSource = GetComponent<AudioSource>();
            _EnemyLifeSpot1 = transform.Find("CreateEnemy (1)");
            _EnemyLifeSpot2 = transform.Find("CreateEnemy (2)");
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (_State != State.GeneralMode) {
                    WarningUI.Get.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                if (_State == State.TankMode) {
                    CameraMode cm = _CameraController.GetCameraMode() == CameraMode.Camera2D ? CameraMode.Tank : CameraMode.Camera2D;
                    _CameraController.SetCameraMode(cm);
                }
            }
        }
        public void PlayTank() {
            _State = State.TankMode;
            _PlayerInstance = ObjectPoolManager.Instance.GetGameObject("TankPool", new Vector3(100, 1.09f, 40), Quaternion.identity, 0);
            Play();
            _PlayerInstance.GetComponent<Player>().ChangeModel(_Mesh, _Material);
            _CameraController.SetCameraMode(CameraMode.Camera2D, _PlayerInstance);
            
        }
        public void PlayPlane() {
            _State = State.PlaneMode;
            _PlayerInstance = ObjectPoolManager.Instance.GetGameObject("PlanePool", new Vector3(73.2f, 5.8f, 45), Quaternion.identity, 0);
            Play();
            _PlayerInstance.GetComponent<Player>().ChangeModel(_Mesh, _Material);
            _CameraController.SetCameraMode(CameraMode.Plane, _PlayerInstance);
            
        }
        public void PlayTurret() {
            _State = State.TurretMode;
            _PlayerInstance = ObjectPoolManager.Instance.GetGameObject("TurretPool", new Vector3(91.6f, 0.03f, 88.2f), Quaternion.identity, 0);
            Play();
            _PlayerInstance.GetComponent<Player>().ChangeModelChildren(_Mesh, _Material);
            _CameraController.SetCameraMode(CameraMode.Turret, _PlayerInstance);
        }
        private void Play() {
            _AudioSource.clip = backGroundMusic;
            _AudioSource.Play();
            onGameStart.Invoke();
            EnemyLifeSpotControl(true);
            ScoreSystem.Get.Replace();
            _PlayerInstance.GetComponent<BaseHealth>().onDead.AddListener(GameOver);
            _PlayerInstance.GetComponent<BaseAttack>().SetDoubleBullet(_DoubleBullet);
        }
        private void EnemyLifeSpotControl(bool live) {
            if (_EnemyLifeSpot1 != null) _EnemyLifeSpot1.gameObject.SetActive(live);
            if (_EnemyLifeSpot2 != null) _EnemyLifeSpot2.gameObject.SetActive(live);
        }
        private void GameOver() {
            onGameOver.Invoke();
            GameOverUI.Get.SetRestartBtn(_State, this);
            GameOverUI.Get.gameObject.SetActive(true);
            End();
        }
        public void BackMenu() {
            _PlayerInstance.GetComponent<ObjectInfo>().RemoveGameObject();
            End();
        }
        private void End() {
            _State = State.GeneralMode;
            _PlayerInstance.GetComponent<BaseHealth>().onDead.RemoveListener(GameOver);
            _CameraController.SetCameraMode(CameraMode.Camera2D);
            EnemyLifeSpotControl(false);
            _AudioSource.clip = menuMusic;
            _AudioSource.Play();
        }
        public void ChangePlayerModel(GameObject setMesh, Material setMeterial,bool doubleBullet, Commodity comm) {
            _Mesh = setMesh;
            _Material = setMeterial;
            _DoubleBullet = doubleBullet;
            //判断是否第一次按下按钮
            if (_Comm != null) _LastComm = _Comm;
            _Comm = comm;
            _Comm.btnSelect.interactable = false;
            if (_LastComm != null) {
                _LastComm.btnSelect.interactable = true;
                _LastComm.selectKeyDownSign = false;
            }
        }
        public void Exit() {
            Application.Quit();
        }
    }
}
