using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerInput;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    private InputManager inputManager;

    private bool gamePaused = false;

    [Header("UI Components")]
    [SerializeField] private GameObject PlayerCollections;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject InventoryMenu;

    private void Start()
    {
        ToggleCursor(false);

        inputManager = GetComponent<InputManager>();

        inputManager.onFootActions.PauseGame.performed += ctx => ToggleMainMenu(true);
        inputManager.onFootActions.ToggleInventory.performed += ctx => ToggleInventory(true);
    }

    public void UpdatePromptText(string message)
    {
        promptText.text = message;
    }

    public void ToggleCursor(bool toggleSwtich)
    {
        Cursor.visible = toggleSwtich;

        switch (toggleSwtich)
        {
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void PauseGame(bool toggle)
    {
        if (toggle)
        {
            gamePaused = !gamePaused;

            ToggleCursor(false);

            Time.timeScale = 1;
        }
        else
        {
            gamePaused = !gamePaused;

            ToggleCursor(true);

            Time.timeScale = 0;
        }
    }

    public void ToggleMainMenu(bool toggle)
    {
        PauseGame(!toggle);

        PlayerCollections.SetActive(toggle);
        PauseMenu.SetActive(toggle);

        InventoryMenu.SetActive(!toggle);
    }

    public void ToggleInventory(bool toggle)
    {
        PauseGame(!toggle);

        PlayerCollections.SetActive(toggle);
        InventoryMenu.SetActive(toggle);

        PauseMenu.SetActive(!toggle);
    }
}
