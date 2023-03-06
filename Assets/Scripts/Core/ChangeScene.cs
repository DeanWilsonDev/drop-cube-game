using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackPad.DropCube.Core
{
  public class ChangeScene : MonoBehaviour
  {
    public void LoadSceneByName(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int index)
    {
      SceneManager.LoadScene(index);
    }
  }
}