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
        [SerializeField] AudioManager _audioManager;
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
            _audioManager.GameMusicSource.Play();
        }
        
        public void QuitGame()
        {
            _levelManager.Kill();
            _playerManager.Kill();
            _pointsManager.Reset();
            _audioManager.GameMusicSource.Stop();
        }

        public void RestartGame()
        {
            QuitGame();
            _audioManager.GameMusicSource.Stop();
            BeginGame();
        }

        public void EndGame()
        {
            _pointsManager.SetScoreText();
            _menuManager.ShowGameOverScreen();
        }


    }
}
