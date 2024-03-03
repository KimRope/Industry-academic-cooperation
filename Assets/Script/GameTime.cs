using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    bool timeZero = false;
    public float PlayTime;
    public Animator TimerAnim;
    public Animator GameOverAnim;
    public Text scoreText;
    public Text GaOvscoreText;
    float currentTime;

    public PlayMusic PlayMusic_cs;
    // Start is called before the first frame update
    void Start()
    {
        TimerAnim.SetFloat("TimeSpeed", 1 / PlayTime);
        currentTime = PlayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeZero) { 
        currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                GaOvscoreText.text = scoreText.text;
                GameOverAnim.SetBool("GameOver", true);
                PlayMusic_cs.RingSound();
                timeZero = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ReGameBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
