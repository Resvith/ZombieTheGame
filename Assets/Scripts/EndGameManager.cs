using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Text FinalScore;

    private Player player;
    void Start()
    {
        GameOverPanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerDead += EndGame;
    }

    private void EndGame()
    {
        print("Game Over");
        GameOverPanel.SetActive(true);
        FinalScore.text = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text;
        InputController inputController = player.GetComponent<InputController>();
        WeaponController weaponController = player.GetComponentInChildren<WeaponController>();
        FirstPersonCameraController firstPersonCameraController = player.GetComponentInChildren<FirstPersonCameraController>();
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
