using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject MenuPanel;
    void Start()
    {
        MenuPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OnOptionsClick()
    {
        StartPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnReturnButton()
    {
        MenuPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

}
