using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{   
    //Aloca cada carta
    [SerializeField]
    private Transform puzzleField;

    //Variavel para definir o numero de cartas
    public int numCards;

    [SerializeField]
    private GameObject btn;
    /*Esse método chamado ao iniciar a cena irá criar as cartas que serao untilizadas no jogo.
    O número de cartas é defininido na própria Unity, facilitando assim, a criação de novas fases.
    Além disso, o método cria um nome para cada carta, de 0 ao número escolhido na Unity. Esses nomes estão
    sendo utilizados no script GameController!*/
    void Awake() {
        for(int i = 0; i < numCards; i++) {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }
    }
}
