using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button easy;

    [SerializeField]
    private Button mid;

    [SerializeField]
    private Button hard;

    int rows = 0;
    int columns = 0;

    void Start()
    {
        easy.onClick.AddListener(() => startGame(2,2));

        mid.onClick.AddListener(() => startGame(2,4));

        hard.onClick.AddListener(() => startGame(4, 4));
    }

    void startGame(int r, int c)
    {
        rows = r;
        columns = c;
        SceneManager.LoadScene("gameScene2");
    }

    private void OnDisable()
    {
        Debug.Log(rows);
        PlayerPrefs.SetInt("rows", rows);
        PlayerPrefs.SetInt("columns", columns);
    }
}
