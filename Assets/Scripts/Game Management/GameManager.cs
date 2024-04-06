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

    //public static int maxPlayers;
    [SerializeField] private GameObject[] team1Players;
    [SerializeField] private GameObject[] team2Players;

    public GameObject team1Goal;
    public GameObject team2Goal;

    public bool checkIfTheSameTeam(GameObject instigator, GameObject otherObject)
        => (team1Players.Contains(instigator) && team1Players.Contains(otherObject)) || (team2Players.Contains(instigator) && team2Players.Contains(otherObject));
}
