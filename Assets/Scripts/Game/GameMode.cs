using BlackPad.DropCube.Level;
using BlackPad.DropCube.Menus;
using BlackPad.DropCube.Player;
using UnityEngine;


namespace BlackPad.DropCube.Game
{
    public class GameMode : MonoBehaviour
    {

        PlayerManager _playerManager;
        LevelManager _levelManager;
        PointsManager _pointsManager;
        CameraManager _cameraManager;
        [SerializeField] MenuManager _menuManager;

        void Awake()
        {
            _playerManager = GetComponent<PlayerManager>();
            _levelManager = GetComponent<LevelManager>();
            _pointsManager = GetComponent<PointsManager>();

            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                _cameraManager = mainCamera
                    .gameObject
                    .GetComponentInParent<CameraManager>();
            }
        }

        public void BeginGame()
        {
            _playerManager.Initialize();
            _levelManager.Initialize();
            _pointsManager.Initialize();
            _cameraManager.Initialize();
            _menuManager.BeginGame();
        }
        
        public void QuitGame()
        {
            _levelManager.Kill();
            _playerManager.Kill();
            _pointsManager.Reset();
        }

        public void RestartGame()
        {
            QuitGame();
            BeginGame();
        }

        public void EndGame()
        {
            _pointsManager.SetScoreText();
            _menuManager.ShowGameOverScreen();
        }


    }
}
