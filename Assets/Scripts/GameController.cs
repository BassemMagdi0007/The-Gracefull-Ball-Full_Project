using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private float Score = 0.0f;
    private int decimalScore = 0;

    public float scrollSpeed = 10f;
    public float counter = 0f;

    public Text ScoreText;
    public Text gameOverText;
    public Text restartText;
    public Text Pause;
    public Text Escape;

    public GameObject[] obstacles;
    public Transform obstaclesSpawnPoint;
    public AudioSource audioSource;

    public static bool gameOver;
    private bool restart;


    void Start()
    {
        gameOver = false;
        restart = false;

        gameOverText.text = "";
        restartText.text = "";
        Pause.text = "";

        Time.timeScale = 1;
        Camera.main.GetComponent<GlitchEffect>().enabled = false;

        audioSource.mute = false;

        GenerateObstacles();
    }

    void Update()
    {
        Debug.Log(Score);
        if (Score >= 71.0f)
        {
            Camera.main.GetComponent<GlitchEffect>().enabled = true;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("HighScore") < decimalScore)
            {
                PlayerPrefs.SetInt("HighScore", decimalScore);
            }

            if (gameOver != true)
                SceneManager.LoadScene(0);
        }
            
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (PlayerPrefs.GetInt("HighScore") < decimalScore)
                {
                    PlayerPrefs.SetInt("HighScore", decimalScore);
                }

                Score = 0;
                ScoreText.text = "Score : " + ((int)Score).ToString();

                Application.LoadLevel(Application.loadedLevel);
            }
        }

        Score += 1 * Time.deltaTime;
        decimalScore = Mathf.RoundToInt(Score);

        Score += Time.deltaTime;
        ScoreText.text = "Score : " + ((int)Score).ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = 0;
            Pause.text = "Pause";
            audioSource.mute = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Time.timeScale = 1;
            Pause.text = "";
            audioSource.mute = false;
        }

        if (counter <= 0)
        {
            GenerateObstacles();
        }
        else
        {
            counter -= Time.deltaTime;
        }

        GameObject CurrentChild;
        for(int i =0; i< transform.childCount; i++)
        {
            CurrentChild = transform.GetChild(i).gameObject;
            ScrollObstcles(CurrentChild);
        }

        EndGame();
    }

    void GenerateObstacles()
    {
        GameObject newObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstaclesSpawnPoint.position, Quaternion.identity);
        newObstacle.transform.parent = transform;

        counter = 2f;
    }

    void ScrollObstcles( GameObject currentObstcle )
    {
        currentObstcle.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }

    public void EndGame()
    {
        if(gameOver == true)
        {
            gameOverText.text = "Game Over";
            restartText.text = "Press 'R' To Restart";
            Escape.text = "";
            Time.timeScale = 0;
            restart = true;
            audioSource.mute = true;
        }
    }
}
