using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text maiorScoreText;
    private static int maiorScore;

    public static int scoreCountEasy;

    public Text scoreTextMedium;
    public static int scoreCountMedium;

    public Text scoreTextHard;
    public static int scoreCountHard;


    void Start()
    {
        // Recupere o maior score salvo em PlayerPrefs
        maiorScore = PlayerPrefs.GetInt("MaiorScore", 0);
    }

    // Método estático para atualizar o maior score
    public static void AtualizarMaiorScore(int novoScore)
    {
        if (novoScore > maiorScore)
        {
            maiorScore = novoScore;

            // Salve o novo maior score em PlayerPrefs
            PlayerPrefs.SetInt("MaiorScore", maiorScore);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        maiorScoreText.text = "Maior Score: " + Mathf.Round(maiorScore);
    }
}

