using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static GameManager GetInstance() => Instance;
    private void Awake()
    {
        Instance = this;
    }

    public enum Team
    {
        Team1, Team2
    }

    [field: SerializeField] public GameObject Ball {  get; private set; }

    //public static int maxPlayers;
    [SerializeField] private GameObject[] team1Players;
    [SerializeField] private GameObject[] team2Players;

    public GameObject team1Goal;
    public GameObject team2Goal;

    public bool ÑheckIfTheSameTeam(GameObject instigator, GameObject otherObject)
        => (team1Players.Contains(instigator) && team1Players.Contains(otherObject)) || (team2Players.Contains(instigator) && team2Players.Contains(otherObject));

    public GameObject GetAllyGoal(GameObject instigator)
        => team1Players.Contains(instigator) ? team1Goal : team2Goal;

    public GameObject GetEnemyGoal(GameObject instigator)
        => team2Players.Contains(instigator) ? team1Goal : team2Goal;

}
