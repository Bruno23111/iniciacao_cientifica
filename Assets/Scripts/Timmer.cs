using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    public Text timmerText;
    public static float remainingTime = 10;
    public static bool playWithTime = true;
    private bool startCountdown = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdownAfterDelay(2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            if(playWithTime)
            {
                if (remainingTime > 0)
                {
                    remainingTime -= Time.deltaTime;
                }
                else if (remainingTime < 0)
                {
                    remainingTime = 0;
                }
            }
            

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timmerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    IEnumerator StartCountdownAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        startCountdown = true;
    }
}
