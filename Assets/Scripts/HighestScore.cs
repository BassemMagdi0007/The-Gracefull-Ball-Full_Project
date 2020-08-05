using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestScore : MonoBehaviour
{
    public Text highScore;

    void Start()
    {
        highScore.text = "Highest Score : " + PlayerPrefs.GetInt("HighScore");
    }
}

