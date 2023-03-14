using System;
using System.Collections.Generic;
using BlackPad.DropCube.Data;
using Dan.Main;
using TMPro;
using UnityEngine;

namespace BlackPad.DropCube.Menus
{
  public class Leaderboard : MonoBehaviour
  {
    [SerializeField] List<TextMeshProUGUI> _names;
    [SerializeField] List<TextMeshProUGUI> _scores;
    [SerializeField] StringListVariable _badWords;
    const string PublicKey =
      "dae94930060331e38f1ac7724ed273dabd0bdf46f36cb09c7e8c50348e78b3e7";

    public void GetLeaderboard()
    {
      LeaderboardCreator.GetLeaderboard(PublicKey, msg =>
      {
        for (int i = 0; i < _names.Count; i++)
        {
          _names[i]
            .text = msg[i]
            .Username;

          _scores[i]
            .text = msg[i]
            .Score.ToString();
        }
      });
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