using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Team team;

    public enum Team
    {
        Team1,
        Team2
    }
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (team == Team.Team1) ScoreManager.GetInstance().team2Score++;
            else ScoreManager.GetInstance().team1Score++;
        }
    }
}
