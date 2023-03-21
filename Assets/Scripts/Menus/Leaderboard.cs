using System;
using System.Collections.Generic;
using BlackPad.DropCube.Data;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace BlackPad.DropCube.Menus
{
  public class Leaderboard : MonoBehaviour
  {
    [SerializeField] List<TextMeshProUGUI> _names;
    [SerializeField] List<TextMeshProUGUI> _scores;
    [SerializeField] StringListVariable _badWords;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject _newEntryPanel;
    
    const string PublicKey =
      "dae94930060331e38f1ac7724ed273dabd0bdf46f36cb09c7e8c50348e78b3e7";

    public bool IsFromMainMenu { get; set; }
    
    void Start()
    {
      GetLeaderboard();
    }
    
    public void GetLeaderboard()
    {
      LeaderboardCreator.GetLeaderboard(PublicKey, msg =>
      {
        int loopLength = msg.Length < _names.Count
          ? msg.Length
          : _names.Count;
        for (int i = 0; i < loopLength; i++)
        {
          _names[i]
            .text = msg[i]
            .Username;

          _scores[i]
            .text = msg[i]
            .Score.ToString();
        }
      });

      _newEntryPanel.SetActive(!IsFromMainMenu);
    }

    public void SetLeaderboardEntry(string username, int score)
    {
      LeaderboardCreator.UploadNewEntry(PublicKey, username, score, _ =>
      {
        if (Array.IndexOf(_badWords.value.ToArray(), name) != -1) return;
        GetLeaderboard();
      });
    }
    
  }
}