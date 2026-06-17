using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    Result,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public PlayerController playerController;
    public MouseLook mouseLook;
    public AudioManager audioManager;
    public ObstacleSpawner obstacleSpawner;
    public ScoreManager scoreManager;
    public HealthManager healthManager;
    public UIManager uiManager;

    [Header("Player Start")]
    public Transform playerTransform;
    public Vector3 playerStartPosition = new Vector3(0f, 0.05f, 0f);
    public Vector3 playerStartRotation = Vector3.zero;

    [Header("Runtime")]
    public GameState currentState = GameState.MainMenu;

    private bool hasEnded = false;

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
        EnterMainMenu();
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                return;
            }

            if (!hasEnded && audioManager != null && audioManager.IsMusicFinished())
            {
                EndGame(true);
            }
        }
        else if (currentState == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        hasEnded = false;
        currentState = GameState.Playing;

        ResetPlayer();

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        if (healthManager != null)
        {
            healthManager.ResetHealth();
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.ResetSpawner();
            obstacleSpawner.SetSpawnEnabled(true);
        }

        if (uiManager != null)
        {
            uiManager.ShowGameHUD();
            uiManager.RefreshHUD();
        }

        if (audioManager != null)
        {
            audioManager.PlayMusic();
        }

        SetPlayerControl(true);
    }

    public void PauseGame()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        currentState = GameState.Paused;

        Time.timeScale = 0f;

        if (audioManager != null)
        {
            audioManager.PauseMusic();
        }

        SetPlayerControl(false);

        if (uiManager != null)
        {
            uiManager.ShowPause();
        }
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
        {
            return;
        }

        currentState = GameState.Playing;

        Time.timeScale = 1f;

        if (audioManager != null)
        {
            audioManager.ResumeMusic();
        }

        SetPlayerControl(true);

        if (uiManager != null)
        {
            uiManager.HidePause();
        }
    }

    public void RestartGame()
    {
        StartGame();
    }

    public void ReturnToMainMenu()
    {
        EnterMainMenu();
    }

    public void GameOver()
    {
        if (hasEnded)
        {
            return;
        }

        EndGame(false);
    }

    public void EndGame(bool isClear)
    {
        if (hasEnded)
        {
            return;
        }

        hasEnded = true;
        currentState = isClear ? GameState.Result : GameState.GameOver;

        Time.timeScale = 1f;

        SetPlayerControl(false);

        if (obstacleSpawner != null)
        {
            obstacleSpawner.SetSpawnEnabled(false);
            obstacleSpawner.ClearObstacles();
        }

        if (audioManager != null)
        {
            if (isClear)
            {
                audioManager.StopMusic();
            }
            else
            {
                audioManager.StopMusic();
            }
        }

        int finalScore = scoreManager != null ? scoreManager.score : 0;
        float hitRate = scoreManager != null ? scoreManager.GetHitRate() : 0f;
        int maxCombo = scoreManager != null ? scoreManager.maxCombo : 0;

        if (uiManager != null)
        {
            uiManager.ShowResult(isClear, finalScore, hitRate, maxCombo);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void EnterMainMenu()
    {
        Time.timeScale = 1f;
        hasEnded = false;
        currentState = GameState.MainMenu;

        if (audioManager != null)
        {
            audioManager.StopMusic();
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.SetSpawnEnabled(false);
            obstacleSpawner.ClearObstacles();
            obstacleSpawner.ResetSpawner();
        }

        ResetPlayer();
        SetPlayerControl(false);

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        if (healthManager != null)
        {
            healthManager.ResetHealth();
        }

        if (uiManager != null)
        {
            uiManager.ShowMainMenu();
        }
    }

    private void ResetPlayer()
    {
        if (playerController != null)
        {
            playerController.ResetPlayer(playerStartPosition);
        }
        else if (playerTransform != null)
        {
            playerTransform.position = playerStartPosition;
        }

        if (playerTransform != null)
        {
            playerTransform.rotation = Quaternion.Euler(playerStartRotation);
        }
    }

    private void SetPlayerControl(bool enabled)
    {
        if (playerController != null)
        {
            playerController.SetMoveEnabled(enabled);
        }

        if (mouseLook != null)
        {
            mouseLook.SetLookEnabled(enabled);
        }
    }
}
