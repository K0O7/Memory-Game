using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject score;

    [SerializeField]
    private Button backBtn;

    int totalscore = 0;

    private void Start()
    {
        totalscore = (int)PlayerPrefs.GetFloat("score");
        backBtn.onClick.AddListener(() => SceneManager.LoadScene("menuScene"));
        score.GetComponent<TMPro.TextMeshProUGUI>().text = "Total Score: " + totalscore.ToString() + "/100";
    }
}
