using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   public string levelName;

    public void Mudar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
   
    }
}
