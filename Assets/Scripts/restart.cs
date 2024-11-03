using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoReiniciar : MonoBehaviour
{
    public string levelName;
    // Método para reiniciar o jogo
    public void ReiniciarJogo()
    {
        // Zera os scores
        ScoreManager.scoreCountEasy = 0;
        ScoreManager.scoreCountMedium = 0;
        ScoreManager.scoreCountHard = 0;

        // Salva os scores zerados
        PlayerPrefs.SetInt("ScoreEasy", 0);
        PlayerPrefs.SetInt("ScoreMedium", 0);
        PlayerPrefs.SetInt("ScoreHard", 0);

        
        // Carrega a cena do jogo novamente
        SceneManager.LoadScene(levelName);
    }
}