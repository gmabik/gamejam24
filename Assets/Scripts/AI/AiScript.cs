using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameManager.Team team; 
    
    [Header("Attack")]
    [SerializeField] private float distanceToBallToAttack;

    [Header("Follow Ball")]
    [SerializeField] private float distanceFromGoalToFollow;

    [Header("Defend")]
    [SerializeField] private GameObject areaOfDefend;
    [SerializeField] private float distanceFromGoalToDefend;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }
}
