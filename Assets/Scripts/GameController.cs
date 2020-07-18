using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private float Score = 0.0f;

    public Text ScoreText;
    public Text gameOverText;
    public Text restartText;
    public Text Pause;

    public GameObject[] obstacles;
    public float scrollSpeed = 10f;
    public Transform obstaclesSpawnPoint;
    public AudioSource audioSource;
    public float counter = 0f;

    public static bool gameOver;
    private bool restart;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        restart = false;

        gameOverText.text = "";
        restartText.text = "";
        Pause.text = "";

        Time.timeScale = 1;

        GenerateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
            
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

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
            Time.timeScale = 0;
            restart = true;
        }
    }
}
