using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager.Team team;
    public int score;

    public AudioSource audioSource;
    public AudioClip clip;

    public Transform actualPos;

    private void Start()
    {
        if (team == GameManager.Team.Team1) GameManager.GetInstance().team1Goal = gameObject;
        else GameManager.GetInstance().team2Goal = gameObject;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (team == GameManager.Team.Team1) ScoreManager.GetInstance().team2Score++;
            else ScoreManager.GetInstance().team1Score++;
            
            audioSource.PlayOneShot(clip);
        }
    }
}
