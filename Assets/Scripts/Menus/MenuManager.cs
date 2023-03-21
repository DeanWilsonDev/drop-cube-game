using UnityEngine;

namespace BlackPad.DropCube.Menus
{
    public class MenuManager : MonoBehaviour
    {

        [SerializeField] GameObject _mainMenuPanel;
        [SerializeField] GameObject _leaderboardPanel;
        Leaderboard _leaderboard;

        void Start()
        {
            _leaderboard = _leaderboardPanel.GetComponent<Leaderboard>();
            _leaderboard.GetLeaderboard();
        }
        
        public void BeginGame()
        {
            _mainMenuPanel.SetActive(false);
        }

        public void ShowLeaderboard(bool isFromMainMenu)
        {
            _mainMenuPanel.SetActive(false);
            _leaderboardPanel.SetActive(true);
            _leaderboard.IsFromMainMenu = isFromMainMenu;
        }

        public void ReturnToMainMenu()
        {
            _leaderboardPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }
    }
}
