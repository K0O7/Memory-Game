using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    [SerializeField]
    private GameObject attempts;

    [SerializeField]
    private GameObject score;

    [SerializeField]
    private Button resetBtn;

    [SerializeField]
    private Button backBtn;

    [SerializeField]
    private InputActionMap action;

    //[SerializeField]
    //private InputActionMap backAction;

    private List<Button> btns = new List<Button>();

    public List<Sprite> sprites = new List<Sprite>();

    private List<Sprite> gameCards = new List<Sprite>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessFront, secondGuessFront;

    private void Awake()
    {
        action["restart"].Enable();
        action["back"].Enable();
        Debug.Log("awake");
        action["restart"].performed += OnRestart;
        action["back"].performed += OnBack;
    }

    private void OnRestart(InputAction.CallbackContext obj)
    {
        Debug.Log("restart");
        Restart();
    }

    private void OnBack(InputAction.CallbackContext obj)
    {
        Back();
    }


    void Start()
    {
        GetButtons();
        AddListeners();
        AddGameCards();
        Shuffle(gameCards);
        gameGuesses = gameCards.Count / 2;
    }

    //private void Update()
    //{
    //    Debug.Log("update?");
    //    if (restartAction.triggered)
    //    {
    //        Debug.Log("reser");
    //        Restart();
    //    }
            
    //    if (backAction.triggered)
    //    {
    //        Debug.Log("back");
    //        Back();
    //    }
            
    //}

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");

        for(int i = 0; i< objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGameCards()
    {
        int lopper = btns.Count;
        int index = 0;

        for(int i = 0; i < lopper; i++)
        {
            if(index == lopper / 2)
            {
                index = 0;
            }
            gameCards.Add(sprites[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickACard());
        }

        resetBtn.onClick.AddListener(() => Restart());

        backBtn.onClick.AddListener(() => Back());
    }
    public void PickACard()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(name);

            firstGuessFront = gameCards[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gameCards[firstGuessIndex];

        }else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(name);

            secondGuessFront = gameCards[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gameCards[secondGuessIndex];

            StartCoroutine(ChceckIfTheFrontsMach());
        }
    }

    IEnumerator ChceckIfTheFrontsMach()
    {

        countGuesses++;
        attempts.GetComponent<TMPro.TextMeshProUGUI>().text = "Attempts: " + countGuesses.ToString();
        yield return new WaitForSeconds(1f);
        
        if (firstGuessFront == secondGuessFront)
        {
            yield return new WaitForSeconds(.1f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            ChceckIfTheGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.1f);

        firstGuess = secondGuess = false;
    }

    void ChceckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        score.GetComponent<TMPro.TextMeshProUGUI>().text = "Hits: " + countCorrectGuesses.ToString();
        if (countCorrectGuesses == gameGuesses)
        {
            Win();
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Restart() {
        SceneManager.LoadScene("gameScene2");
    }

    void Back()
    {
        SceneManager.LoadScene("menuScene");
    }

    void Win()
    {
        SceneManager.LoadScene("winScene");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("score", float.Parse(countCorrectGuesses.ToString()) / float.Parse(countGuesses.ToString()) *100);
    }
}
