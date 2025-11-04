using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrashGameManager : MonoBehaviour
{
    #region Variables
    private int score;
    public float time;
    private string playerName;
    private string player1Name;
    private string player2Name;
    public bool wheelsActive;
    private bool player1Flag;
    private bool player2Flag;

    [Header("Gameplay elements")]
    [SerializeField] private GameObject cannon1;
    [SerializeField] private GameObject cannon2;
    [SerializeField] private GameObject pegContainer;

    [Header("Leaderboard elements")]
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
    [SerializeField] private GameObject colourWheel;
    [SerializeField] private GameObject animalWheel;
    [SerializeField] private TextMeshProUGUI currentText_1;
    [SerializeField] private TextMeshProUGUI currentText_2;
    #endregion

    private void Start()
    {
        score = 0;
        time = 60f;
        player1Canvas.SetActive(true);
        player2Canvas.SetActive(true);
        leaderboardCanvas_1.SetActive(false);
        leaderboardCanvas_2.SetActive(false);
        colourWheel.SetActive(false);
        animalWheel.SetActive(false);
        wheelsActive = false;
        player1Flag = false;
        player2Flag = false;
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
            bool shotActive = false;
            string[] validateTags = { "Ball", "Ball_1", "Ball_2" };

            foreach (string tag in validateTags)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                foreach (GameObject obj in objs)
                {
                    if (obj.activeInHierarchy)
                    {
                        shotActive = true;
                        break;
                    }
                }
                if (shotActive) break;
            }

            if (!shotActive && !wheelsActive)
            {
                ShowWheels();
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void ShowWheels()
    {
        cannon1.SetActive(false);
        cannon2.SetActive(false);
        pegContainer.SetActive(false);

        colourWheel.SetActive(true);
        animalWheel.SetActive(true);
        wheelsActive = true;
    }

    private void HideWheels()
    {
        colourWheel.SetActive(false);
        animalWheel.SetActive(false);
    }

    private void ShowLeaderboard()
    {
        HideWheels();
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
        currentText_1.text = $"{playerName} {score}";
        currentText_2.text = $"{playerName} {score}";
        ShowLeaderboard();
    }

    public void SetName(string name, int playerNo)
    {
        if (playerNo == 1)
        {
            player1Name = name;
        }
        if (playerNo == 2)
        {
            player2Name = name;
        }
        if (player1Name != null && player2Name != null)
        {
            playerName = $"{player1Name} {player2Name}";
            EndGame();
        }
    }

    public void ReloadScene(int playerNo)
    {
        if (playerNo == 1)
        {
            player1Flag = true;
        }
        else if (playerNo == 2)
        {
            player2Flag = true;
        }

        if (player1Flag && player2Flag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
