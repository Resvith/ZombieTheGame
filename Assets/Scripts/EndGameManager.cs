using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Text FinalScore;

    private Player _player;


    void Start()
    {
        GameOverPanel.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _player.OnPlayerDead += EndGame;
    }

    private void EndGame()
    {
        print("Game Over");
        GameOverPanel.SetActive(true);
        FinalScore.text = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text;
        InputController inputController = _player.GetComponent<InputController>();
        WeaponController weaponController = _player.GetComponentInChildren<WeaponController>();
        FirstPersonCameraController firstPersonCameraController = _player.GetComponentInChildren<FirstPersonCameraController>();
        inputController.enabled = false;
        weaponController.enabled = false;
        firstPersonCameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
