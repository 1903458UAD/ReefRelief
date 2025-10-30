using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public LeaderboardManager manager;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        var entries = manager.GetLeaderboard();
        leaderboardText.text = "";

        for (int i = 0; i < entries.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {entries[i].playerName} - {entries[i].score}\n";
        }
    }
}
