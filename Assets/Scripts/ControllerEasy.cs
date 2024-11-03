using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEasy : MonoBehaviour
{
    [SerializeField]
    private Sprite bjImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firtGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firtGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    public GameObject gameOver;
    public GameObject perdeu;

    public Text scoreTextMedium;

    public Text waveText;
    



    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/Cartas/CartasPuzzles");
    }

    void Start()
    {
        HeartScript.vida = 3;
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        StartCoroutine(FlipCardsAfterDelay(2f));
        restart();

    }

    void Update()
    {
        scoreTextMedium.text = "Score: " + Mathf.Round(ScoreManager.scoreCountEasy);
        ScoreManager.AtualizarMaiorScore(ScoreManager.scoreCountEasy);
        waveText.text = "Fase : " + Mathf.Round(waveController.waveEasy) + " Concluída";
        checkIfPlayerLose();

    }
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bjImage;
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log ("Voce esta apertando o botao numero " +name);
        if (!firtGuess)
        {

            firtGuess = true;
            firtGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firtGuessIndex].name;

            btns[firtGuessIndex].image.sprite = gamePuzzles[firtGuessIndex];
            btns[firtGuessIndex].enabled = false;
        }
        else if (!secondGuess)
        {

            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfThePuzzleMatch());
            btns[firtGuessIndex].enabled = true;
        }
    }
    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(0.3f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.1f);

            btns[firtGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
        
            ScoreManager.scoreCountEasy += 30;
            Timmer.remainingTime += 5;
            CheckIfTheGameFinished();
        }
        else
        {
            yield return new WaitForSeconds(.1f);
            btns[firtGuessIndex].image.sprite = bjImage;
            btns[secondGuessIndex].image.sprite = bjImage;
            if(HeartScript.vida != 0)
            {
                HeartScript.vida--;
            }

            if (ScoreManager.scoreCountEasy != 0)
            {
                ScoreManager.scoreCountEasy -= 10;
            }

        }

        yield return new WaitForSeconds(.1f);
        firtGuess = secondGuess = false;

    }

    void CheckIfTheGameFinished()
    {
        countCorrectGuesses++;


        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("game finished");
            Debug.Log("Voce tentou " + countGuesses + "vez/es");
            waveController.waveEasy += 1;
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }

    }

    void checkIfPlayerLose()
    {
        if (Timmer.remainingTime == 0)
        {
            perdeu.SetActive(true);

        }

        if (HeartScript.vida == 0)
        {
            perdeu.SetActive(true);
            Timmer.remainingTime = 0;
        }

    }

    void restart()
    {
        Time.timeScale = 1;
        Timmer.remainingTime = 30;
    }


    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    IEnumerator FlipCardsAfterDelay(float delay)
    {
        foreach (Button btn in btns)
        {
            btn.image.sprite = gamePuzzles[btns.IndexOf(btn)];
        }

        yield return new WaitForSeconds(delay);


        foreach (Button btn in btns)
        {
            btn.image.sprite = bjImage;
        }
    }

}