using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private List<Button> closeButtons;

    public void Initalize()
    {
        foreach (var item in closeButtons)
        {
            item.onClick.AddListener(Close);
        }

        openButton.onClick.AddListener(Open);

        Close();

        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Quit);
    }

    private void Restart()
    {
        GameData.Instance.Clear();

        var scene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(scene.buildIndex);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }
    private void Close()
    {
        gameObject.SetActive(false);
    }
}
