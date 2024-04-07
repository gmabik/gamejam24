using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public AudioClip clip;
    
    public TMP_Text countText;
    
    private static GameManager Instance;
    public static GameManager GetInstance() => Instance;
    private void Awake()
    {
        Instance = this;
        StartCoroutine(Countdown(3));
    }

    public enum Team
    {
        Team1, Team2
    }

    public enum BotRole
    {
        Attacker,
        Defender
    }

    [field: SerializeField] public GameObject Ball { get; private set; }

    //public static int maxPlayers;
    [SerializeField] private GameObject[] team1Players;
    [SerializeField] private GameObject[] team2Players;

    public GameObject team1Goal;
    public GameObject team2Goal;

    public GameObject mainPlayer;

    public bool heckIfTheSameTeam(GameObject instigator, GameObject otherObject)
        => (team1Players.Contains(instigator) && team1Players.Contains(otherObject)) || (team2Players.Contains(instigator) && team2Players.Contains(otherObject));

    public GameObject GetAllyGoal(GameObject instigator)
        => team1Players.Contains(instigator) ? team1Goal : team2Goal;

    public GameObject GetEnemyGoal(GameObject instigator)
        => team2Players.Contains(instigator) ? team1Goal : team2Goal;

    private void RoleManagement(GameObject[] team)
    {
        bool teamHasPlayer = false;
        foreach (GameObject player in team)
        {
            if (player.GetComponent<PlayerMovement>() != null)
            {
                teamHasPlayer = true;
                break;
            }
        }

        var sorted = team.OrderBy(item => Vector3.Distance(item.transform.position, Ball.transform.position)).ToArray();

        int attackersAmountNeeded = teamHasPlayer ? 1 : 2;
        int attackersAmountActual = 0;

        int y = 0;
        for (int i = 0; i < 2; i++)
        {
            y++;
            if (sorted[i].TryGetComponent<AiScript>(out AiScript ai))
            {
                ai.role = BotRole.Attacker;
                attackersAmountActual++;
            }
            if (attackersAmountActual == attackersAmountNeeded) break;
        }

        for (int i = y; i < sorted.Length; i++)
        {
            if (sorted[i].TryGetComponent<AiScript>(out AiScript ai))
            {
                ai.role = BotRole.Defender;
            }
        }
    }

    IEnumerator Countdown(int seconds)
    {
        Time.timeScale = 0f;
        int count = seconds;
        AudioSource source = gameObject.GetComponent<AudioSource>();
        while (count > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            count--;
            source.PlayOneShot(clip);
            countText.text = count.ToString();
        }
        
        
        countText.text = "GO";
        source.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Destroy(countText.gameObject);
    }

    private void Update()
    {
        RoleManagement(team1Players);
        RoleManagement(team2Players);
    }
}
