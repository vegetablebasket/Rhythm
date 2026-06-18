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
    public float feedbackDuration = 0.7f;

    [Header("Feedback Style")]
    public Color goodColor = new Color(0.2f, 1f, 0.3f, 1f);
    public Color missColor = new Color(1f, 0.15f, 0.15f, 1f);

    public int goodFontSize = 48;
    public int missFontSize = 58;

    [Header("Miss Flash")]
    public Image missFlashImage;
    public float missFlashDuration = 0.25f;
    public float missFlashMaxAlpha = 0.45f;

    private Coroutine feedbackCoroutine;
    private Coroutine missFlashCoroutine;

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
        ClearFeedback();
    }

    public void ShowMainMenu()
    {
        SetPanel(mainMenuPanel, true);
        SetPanel(gameHUDPanel, false);
        SetPanel(pausePanel, false);
        SetPanel(resultPanel, false);

        ClearFeedback();
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

        ClearFeedback();

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

        bool isMiss = message == "Miss";

        feedbackCoroutine = StartCoroutine(ShowFeedbackRoutine(message, isMiss));

        if (isMiss)
        {
            ShowMissFlash();
        }
    }

    public void ClearFeedback()
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            feedbackCoroutine = null;
        }

        if (missFlashCoroutine != null)
        {
            StopCoroutine(missFlashCoroutine);
            missFlashCoroutine = null;
        }

        if (feedbackText != null)
        {
            feedbackText.text = "";
            feedbackText.transform.localScale = Vector3.one;
        }

        if (missFlashImage != null)
        {
            Color color = missFlashImage.color;
            color.a = 0f;
            missFlashImage.color = color;
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

    private IEnumerator ShowFeedbackRoutine(string message, bool isMiss)
    {
        feedbackText.text = message;

        if (isMiss)
        {
            feedbackText.color = missColor;
            feedbackText.fontSize = missFontSize;
        }
        else
        {
            feedbackText.color = goodColor;
            feedbackText.fontSize = goodFontSize;
        }

        float timer = 0f;

        Vector3 startScale = Vector3.one * 1.25f;
        Vector3 endScale = Vector3.one;

        feedbackText.transform.localScale = startScale;

        while (timer < feedbackDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / feedbackDuration;
            feedbackText.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        feedbackText.text = "";
        feedbackText.transform.localScale = Vector3.one;

        feedbackCoroutine = null;
    }

    private void ShowMissFlash()
    {
        if (missFlashImage == null)
        {
            return;
        }

        if (missFlashCoroutine != null)
        {
            StopCoroutine(missFlashCoroutine);
        }

        missFlashCoroutine = StartCoroutine(MissFlashRoutine());
    }

    private IEnumerator MissFlashRoutine()
    {
        Color color = missFlashImage.color;

        float timer = 0f;

        while (timer < missFlashDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / missFlashDuration;
            float alpha = Mathf.Lerp(missFlashMaxAlpha, 0f, t);

            color.a = alpha;
            missFlashImage.color = color;

            yield return null;
        }

        color.a = 0f;
        missFlashImage.color = color;

        missFlashCoroutine = null;
    }

    private void SetPanel(GameObject panel, bool active)
    {
        if (panel != null)
        {
            panel.SetActive(active);
        }
    }
}
