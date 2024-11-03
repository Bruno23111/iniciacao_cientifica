using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource persistentAudioSource;

    void Start()
    {
        // Obtenha o componente Button do botão
        Button button = GetComponent<Button>();

        // Adicione um listener para o clique do botão
        button.onClick.AddListener(PlayButtonClickSound);
    }

    void PlayButtonClickSound()
    {
        // Verifique se há um áudio a ser reproduzido
        if (persistentAudioSource.clip != null)
        {
            // Reproduza o som do clique do botão
            persistentAudioSource.PlayOneShot(persistentAudioSource.clip);
        }
    }
}
