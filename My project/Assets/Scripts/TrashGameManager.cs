using UnityEngine;
using TMPro;

public class TrashGameManager : MonoBehaviour
{
    #region Variables
    private int score;
    private float time;
    private string playerName;

    [SerializeField] private TextMeshProUGUI scoreText_1;
    [SerializeField] private TextMeshProUGUI scoreText_2;
    [SerializeField] private TextMeshProUGUI timeText_1;
    [SerializeField] private TextMeshProUGUI timeText_2;
    [SerializeField] private LeaderboardManager leaderboard;
    [SerializeField] private LeaderboardUI leaderboardUI_1;
    [SerializeField] private LeaderboardUI leaderboardUI_2;
    [SerializeField] private GameObject player1Canvas;
    [SerializeField] private GameObject player2Canvas;
    [SerializeField] private GameObject leaderboardCanvas_1;
    [SerializeField] private GameObject leaderboardCanvas_2;
    #endregion

    private void Start()
    {
        score = 0;
        time = 60f;
        player1Canvas.SetActive(true);
        player2Canvas.SetActive(true);
        leaderboardCanvas_1.SetActive(false);
        leaderboardCanvas_2.SetActive(false);
    }

    private void Update()
    {
        scoreText_1.text = score.ToString();
        scoreText_2.text = score.ToString();

        if (time > 0)
        {
            time -= Time.deltaTime;
            timeText_1.text = Mathf.Ceil(time).ToString();
            timeText_2.text = Mathf.Ceil(time).ToString();
        }
        else
        {
            ShowLeaderboard();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void ShowLeaderboard()
    {
        player1Canvas.SetActive(false);
        player2Canvas.SetActive(false);
        leaderboardCanvas_1.SetActive(true);
        leaderboardCanvas_2.SetActive(true);
        leaderboardUI_1.UpdateUI();
        leaderboardUI_2.UpdateUI();        
    }

    public void EndGame()
    {
        leaderboard.AddEntry(playerName, score);
    }

    public void SetName(string name)
    {
        if (playerName == null)
        {
            playerName = name;
        }
        else
        {
            playerName = playerName + name;
        }
    }
}
