using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager Instance;
    public static ScoreManager GetInstance() => Instance;
    private void Awake()
    {
        Instance = this;
    }

    public int team1Score;
    public int team2Score;

    [SerializeField]
    private Text _scoreText1;
    private Text _scoreText2;


    private void Start()
    {
        _scoreText1.text = 0.ToString();
        _scoreText2.text = 0.ToString();
    }

    public void Update()
    {
        _scoreText1.text = team1Score.ToString();
        _scoreText2.text = team2Score.ToString();
    }
}
