using UnityEngine;
namespace BlackPad.DropCube.Menus
{
  public class MenuManager : MonoBehaviour
  {
    [SerializeField]
    GameObject _mainMenuScreen;

    [SerializeField]
    GameObject _leaderboardScreen;

    [SerializeField] GameObject _gameOverScreen;
    
    Leaderboard _leaderboard;

    void Start()
    {
      _leaderboard = GetComponent<Leaderboard>();
      _leaderboard.GetLeaderboard();
    }

    public void BeginGame()
    {
      _mainMenuScreen.SetActive(false);
      _mainMenuScreen.SetActive(false);
      _gameOverScreen.SetActive(false);
    }

    public void ShowLeaderboard()
    {
      _mainMenuScreen.SetActive(false);
      _gameOverScreen.SetActive(false);
      _leaderboardScreen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
      _leaderboardScreen.SetActive(false);
      _gameOverScreen.SetActive(false);
      _mainMenuScreen.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
      _leaderboardScreen.SetActive(false);
      _mainMenuScreen.SetActive(false);
      _gameOverScreen.SetActive(true);
    }
  }
}