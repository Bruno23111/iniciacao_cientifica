using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Aloca imagem das cartas de tras
    [SerializeField]
    private Sprite bjImage;
    
    //Aloca imagem de cada carta
    public Sprite[] puzzles;

    //Lista das imagens das cartas
    public List<Sprite> gamePuzzles = new List<Sprite>();

    //Lista de botoes no jogo
    public List<Button> btns = new List<Button>();

    //Váriaveis que definem se as 2 cartas estao viradas
    private bool firtGuess , secondGuess;

    //Váriaveis das tentativas 
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    //Váriavel para controlar as cartas, se sao iguais ou nao
    private int firtGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    //Variavel para controlar o canva GameOver
    public GameObject gameOver;

    //Metodo iniciado logo ao abrir a cena. Ele busca na pasta Resources/Sprites/Cartas/CartasPuzzles,
    //Onde as cartas dos animais foram deixadas.
    void Awake(){
        puzzles = Resources.LoadAll<Sprite> ("Sprites/Cartas/CartasPuzzles");
    }

    //Metodo similar ao Awake, Chamado ao iniciar o Jogo
    void Start(){
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle (gamePuzzles);
        gameGuesses = gamePuzzles.Count/2;
    }
    
    //Metood que aloca as imagens para cada button
    void GetButtons(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

        for(int i = 0; i < objects.Length; i++){
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bjImage;
        }
    }

    //Metodo que Adiciona os Puzzles
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
        if(!firtGuess){

            firtGuess = true;
            firtGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firtGuessIndex].name;

            btns[firtGuessIndex].image.sprite = gamePuzzles[firtGuessIndex];
            btns[firtGuessIndex].enabled = false;
        }
        else if(!secondGuess){

            
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

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

            CheckIfTheGameFinished();
        }
        else{
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
            gameOver.SetActive(true);
            Debug.Log("game finished");
            Debug.Log("Voce tentou " +countGuesses+ "vez/es");
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
