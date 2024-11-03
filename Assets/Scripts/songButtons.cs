using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource persistentAudioSource;

    void Start()
    {
        // Obtenha o componente Button do bot�o
        Button button = GetComponent<Button>();

        // Adicione um listener para o clique do bot�o
        button.onClick.AddListener(PlayButtonClickSound);
    }

    void PlayButtonClickSound()
    {
        // Verifique se h� um �udio a ser reproduzido
        if (persistentAudioSource.clip != null)
        {
            // Reproduza o som do clique do bot�o
            persistentAudioSource.PlayOneShot(persistentAudioSource.clip);
        }
    }
}
