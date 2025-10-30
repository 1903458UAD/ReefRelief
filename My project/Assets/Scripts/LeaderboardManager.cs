using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public int maxEntries = 10;
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    private const string SaveKey = "LocalLeaderboard";

    private void Start()
    {
        LoadLeaderboard();
    }

    public void AddEntry(string name, int score)
    {
        leaderboard.Add(new LeaderboardEntry(name, score));
        leaderboard = leaderboard.OrderByDescending(e => e.score).Take(maxEntries).ToList();
        SaveLeaderboard();
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return leaderboard;
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new LeaderboardWrapper(leaderboard));
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            leaderboard = JsonUtility.FromJson<LeaderboardWrapper>(json).entries;
        }
    }

    [System.Serializable]
    private class LeaderboardWrapper
    {
        public List<LeaderboardEntry> entries;

        public LeaderboardWrapper(List<LeaderboardEntry> entries)
        {
            this.entries = entries;
        }
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int score;

        public LeaderboardEntry(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }

}
