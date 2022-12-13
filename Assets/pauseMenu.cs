using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    GameObject _menu;
    private void Awake()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu");
        _menu.SetActive(false);
    }
    //Exit button in menu
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //restart button in menu
    public void RestartB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        //menu
        if (!_menu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(true);
        }
        else if (_menu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(false);
        }
    }
}
