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

    public int team1Score = 0;
    public int team2Score = 0;

    [SerializeField] private Text _scoreText1;
    [SerializeField] private Text _scoreText2;

    public void Update()
    {
        _scoreText1.text = team1Score.ToString();
        _scoreText2.text = team2Score.ToString();
    }
}
