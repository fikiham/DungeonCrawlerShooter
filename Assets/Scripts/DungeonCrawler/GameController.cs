using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform sack;

    public float gameTimer;
    [SerializeField] int killCount;

    int damageLevel = 1;
    int firerateLevel = 1;

    bool gamePaused = false;
    public bool gameEnd = false;
    public bool playerDied = false;

    [Header("UI")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text killText;

    [SerializeField] GameObject EndScreen;
    [SerializeField] TMP_Text EndText;

    [SerializeField] GameObject PauseScreen;

    [SerializeField] GameObject LevelupScreen;
    [SerializeField] Button HealButton;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text firerateText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        HandleUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                PauseScreen.SetActive(true);
            }
        }

        if (playerDied)
            gameEnd = true;

        if (gameEnd)
        {
            PauseGame();
            EndScreen.SetActive(true);
            EndText.text = playerDied ? "noob lmao" : "you win blablabla";
        }
    }

    void HandleUI()
    {
        timerText.text = gameTimer.ToString("F2");
        killText.text = killCount.ToString();
    }

    public void AddKillCount()
    {
        killCount++;
    }

    public void IncreasingLevel()
    {
        PauseGame();
        LevelupScreen.SetActive(true);
        HealButton.interactable = Player_Health.Instance.health < Player_Health.Instance.maxHealth;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("Home");
    }

    public void RestartGame()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }

    public void BuffDamage()
    {
        Player_Action.Instance.bulletdamage += 50;
        damageLevel++;
        damageText.text = damageLevel.ToString();
    }

    public void BuffFireRate()
    {
        Player_Action.Instance.defaultFirerate += 1;
        firerateLevel++;
        firerateText.text = firerateLevel.ToString();
    }

    public void HealPlayer()
    {
        Player_Health.Instance.Heal(10);
    }
}
