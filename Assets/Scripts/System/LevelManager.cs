
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button pauseMenuFirstButton;
    [SerializeField] private GameObject pauseMenu;

    ScoreKeeper scoreKeeper;
    PlayerController playerController;

    bool mouseInteractionEnabled = false;

    readonly Dictionary<GraphicRaycaster, bool> raycasterStates = new();

    bool isPaused = false;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Start()
    {
        Cursor.visible = false;
        SetMouseInteraction(false);
    }

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        bool canUseMenu =
            scene == "MainMenu" ||
            scene == "GameOver" ||
            isPaused;

        bool mouseMoved = canUseMenu &&
            Mouse.current != null &&
            Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f;

        bool gamepadUsed = false;

        if (canUseMenu && Gamepad.current != null)
        {
            gamepadUsed =
                Gamepad.current.dpad.ReadValue().sqrMagnitude > 0.01f ||
                Gamepad.current.leftStick.ReadValue().sqrMagnitude > 0.01f ||
                Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01f;

            foreach (var control in Gamepad.current.allControls)
            {
                if (control is ButtonControl button &&
                    button.wasPressedThisFrame)
                {
                    gamepadUsed = true;
                    break;
                }
            }
        }

        bool enableMouse = canUseMenu &&
            (gamepadUsed ? false :
            mouseMoved ? true : mouseInteractionEnabled);

        if (enableMouse == mouseInteractionEnabled)
            return;

        Cursor.visible = enableMouse;
        SetMouseInteraction(enableMouse);
    }

    void SetMouseInteraction(bool enabled)
    {
        mouseInteractionEnabled = enabled;

        foreach (GraphicRaycaster raycaster in
                 FindObjectsByType<GraphicRaycaster>(
                     FindObjectsInactive.Include,
                     FindObjectsSortMode.None))
        {
            if (!raycasterStates.ContainsKey(raycaster))
                raycasterStates[raycaster] = raycaster.enabled;

            raycaster.enabled = enabled
                ? raycasterStates[raycaster]
                : false;
        }
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 1f));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        if (pauseMenuFirstButton != null)
            EventSystem.current.SetSelectedGameObject(
                pauseMenuFirstButton.gameObject);

        DisableControls();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (pauseMenuFirstButton != null)
            EventSystem.current.SetSelectedGameObject(null);

        EnableControls();
    }

    public void OnGamePause(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    void DisableControls()
    {
        if (playerController != null)
        {
            playerController.DisableControls();
            playerController.enabled = false;
        }
    }

    void EnableControls()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
            playerController.EnableControls();
        }
    }
}