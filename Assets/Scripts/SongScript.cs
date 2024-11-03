using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Referência estática ao único AudioManager
    private static AudioManager instance;

    // Referência ao componente de áudio
    private AudioSource audioSource;

    // Som de fundo
    public AudioClip somDeFundo;

    // Método estático para acessar o AudioManager
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Selecionar" || SceneManager.GetActiveScene().name == "Creditos")
        {
            // Define o volume do áudio para um valor menor
            audioSource.volume = 0.4f; 
        }
        else
        {
            audioSource.volume = 1.0f;
        }
    }
    void Awake()
    {
        // Garante que apenas um AudioManager exista em toda a execução do jogo
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        // Garante que o objeto persista entre as cenas
        DontDestroyOnLoad(gameObject);

        // Inicializa o componente de áudio
        audioSource = GetComponent<AudioSource>();

        // Verifica se o componente de áudio foi encontrado
        if (audioSource == null)
        {
            // Se não for encontrado, adiciona o componente de áudio ao objeto
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Define o som de fundo e configura para reproduzir em loop
        audioSource.clip = somDeFundo;
        audioSource.loop = true;
        

        // Inicia a reprodução do som de fundo
        audioSource.Play();
    }
}
