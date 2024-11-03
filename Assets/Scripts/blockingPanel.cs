using UnityEngine;
using System.Collections;

public class BlockingController : MonoBehaviour
{
    public GameObject blockingPanel; // Referência ao GameObject que cobre toda a tela

    IEnumerator DisableInputForTwoSeconds()
    {
        // Ativa o painel que bloqueia a tela
        blockingPanel.SetActive(true);

        // Aguarda 2 segundos
        yield return new WaitForSeconds(2f);

        // Desativa o painel que bloqueia a tela após 2 segundos
        blockingPanel.SetActive(false);
    }

    void Start()
    {
        // Inicia a rotina para desativar a entrada por 2 segundos
        StartCoroutine(DisableInputForTwoSeconds());
    }
}
