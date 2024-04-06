using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameManager.Team team;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
