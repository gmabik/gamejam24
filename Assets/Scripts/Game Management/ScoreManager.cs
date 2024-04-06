using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
