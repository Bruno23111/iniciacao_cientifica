using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Refer�ncia est�tica ao �nico AudioManager
    private static AudioManager instance;

    // Refer�ncia ao componente de �udio
    private AudioSource audioSource;

    // Som de fundo
    public AudioClip somDeFundo;

    // M�todo est�tico para acessar o AudioManager
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Selecionar" || SceneManager.GetActiveScene().name == "Creditos")
        {
            // Define o volume do �udio para um valor menor
            audioSource.volume = 0.4f; 
        }
        else
        {
            audioSource.volume = 1.0f;
        }
    }
    void Awake()
    {
        // Garante que apenas um AudioManager exista em toda a execu��o do jogo
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

        // Inicializa o componente de �udio
        audioSource = GetComponent<AudioSource>();

        // Verifica se o componente de �udio foi encontrado
        if (audioSource == null)
        {
            // Se n�o for encontrado, adiciona o componente de �udio ao objeto
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Define o som de fundo e configura para reproduzir em loop
        audioSource.clip = somDeFundo;
        audioSource.loop = true;
        

        // Inicia a reprodu��o do som de fundo
        audioSource.Play();
    }
}
