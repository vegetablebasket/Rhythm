using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameHUDPanel;
    public GameObject pausePanel;
    public GameObject resultPanel;

    [Header("HUD Text")]
    public Text healthText;
    public Text scoreText;
    public Text comboText;
    public Text feedbackText;

    [Header("Result Text")]
    public Text resultTitleText;
    public Text finalScoreText;
    public Text hitRateText;
    public Text maxComboText;

    [Header("Feedback Settings")]
    public float feedbackDuration = 0.8f;

    private Coroutine feedbackCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    public void ShowMainMenu()
    {
        SetPanel(mainMenuPanel, true);
        SetPanel(gameHUDPanel, false);
        SetPanel(pausePanel, false);
        SetPanel(resultPanel, false);
    }

    public void ShowGameHUD()
    {
        SetPanel(mainMenuPanel, false);
        SetPanel(gameHUDPanel, true);
        SetPanel(pausePanel, false);
        SetPanel(resultPanel, false);

        ClearFeedback();
        RefreshHUD();
    }

    public void ShowPause()
    {
        SetPanel(pausePanel, true);
    }

    public void HidePause()
    {
        SetPanel(pausePanel, false);
    }

    public void ShowResult(bool isClear, int finalScore, float hitRate, int maxCombo)
    {
        SetPanel(mainMenuPanel, false);
        SetPanel(gameHUDPanel, false);
        SetPanel(pausePanel, false);
        SetPanel(resultPanel, true);

        if (resultTitleText != null)
        {
            resultTitleText.text = isClear ? "Clear!" : "Game Over";
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + finalScore;
        }

        if (hitRateText != null)
        {
            hitRateText.text = "Hit Rate: " + hitRate.ToString("F1") + "%";
        }

        if (maxComboText != null)
        {
            maxComboText.text = "Max Combo: " + maxCombo;
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateCombo(int combo)
    {
        if (comboText != null)
        {
            comboText.text = "Combo: " + combo;
        }
    }

    public void ShowFeedback(string message)
    {
        if (feedbackText == null)
        {
            return;
        }

        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(ShowFeedbackRoutine(message));
    }

    public void ClearFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    public void RefreshHUD()
    {
        if (HealthManager.Instance != null)
        {
            UpdateHealth(HealthManager.Instance.currentHealth);
        }

        if (ScoreManager.Instance != null)
        {
            UpdateScore(ScoreManager.Instance.score);
            UpdateCombo(ScoreManager.Instance.combo);
        }
    }

    private IEnumerator ShowFeedbackRoutine(string message)
    {
        feedbackText.text = message;

        yield return new WaitForSecondsRealtime(feedbackDuration);

        feedbackText.text = "";
    }

    private void SetPanel(GameObject panel, bool active)
    {
        if (panel != null)
        {
            panel.SetActive(active);
        }
    }
}
