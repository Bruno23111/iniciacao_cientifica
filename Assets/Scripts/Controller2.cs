using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
   [SerializeField]
    private Sprite bjImage;
    
    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firtGuess , secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firtGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    public GameObject gameOver;

    void Awake(){
        puzzles = Resources.LoadAll<Sprite> ("Sprites/Cartas/CartasCores");
    }

    void Start(){
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle (gamePuzzles);
        gameGuesses = gamePuzzles.Count/2;
    }
    
    void GetButtons(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

        for(int i = 0; i < objects.Length; i++){
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bjImage;
        }
    }

    void AddGamePuzzles(){
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++){
            if(index == looper / 2){
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners(){
        foreach(Button btn in btns){
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle() {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log ("Voce esta apertando o botao numero " +name);
        if(!firtGuess){

            firtGuess = true;
            firtGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firtGuessIndex].name;

            btns[firtGuessIndex].image.sprite = gamePuzzles[firtGuessIndex];
            btns[firtGuessIndex].enabled = false;
        }else if(!secondGuess){

            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfThePuzzleMatch());
            btns[firtGuessIndex].enabled = true;
        }
    }
    IEnumerator CheckIfThePuzzleMatch(){
        yield return new WaitForSeconds(0.3f);
        if(firstGuessPuzzle == secondGuessPuzzle){
            yield return new WaitForSeconds(.1f);

            btns[firtGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            ScoreManager.scoreCount += 100;

            CheckIfTheGameFinished();
        }else{
            yield return new WaitForSeconds(.1f);
            btns[firtGuessIndex].image.sprite = bjImage;
            btns[secondGuessIndex].image.sprite = bjImage;

        }
        yield return new WaitForSeconds(.1f);
        firtGuess = secondGuess = false;

    }

    void CheckIfTheGameFinished(){
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses){
            Debug.Log("game finished");
            Debug.Log("Voce tentou " +countGuesses+ "vez/es");
            gameOver.SetActive(true);
        }
    }

    void Shuffle(List<Sprite> list){
        for(int i = 0; i < list.Count;i++){
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }   

}
