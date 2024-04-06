using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiScript : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameManager.Team team;

    [Header("Attack")]
    [SerializeField] private float distanceToBallToAttack;
    [SerializeField] private float kickPowerMin;
    [SerializeField] private float kickPowerMax;

    [Header("Follow Ball")]
    [SerializeField] private float distanceFromGoalToFollow;

    [Header("Defend")]
    [SerializeField] private float CoefToDefend;

    private GameObject Ball;
    private GameObject AllyGoal;
    private GameObject EnemyGoal;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Ball = GameManager.GetInstance().Ball;
        AllyGoal = GameManager.GetInstance().GetAllyGoal(gameObject);
        EnemyGoal = GameManager.GetInstance().GetEnemyGoal(gameObject);
    }

    bool canAttack;
    private void Update()
    {
        transform.LookAt(new Vector3(Ball.transform.position.x, transform.position.y, Ball.transform.position.z));
        //print(agent.destination);
        float distanceBallToPlayer = Vector3.Distance(Ball.transform.position, gameObject.transform.position);
        float distanceBallToAllyGoal = Vector3.Distance(Ball.transform.position, AllyGoal.transform.position);

        if(Vector3.Distance(Ball.transform.position, EnemyGoal.transform.position) < Vector3.Distance(transform.position, EnemyGoal.transform.position)) canAttack = true;
        else canAttack = false;

        if (distanceBallToPlayer <= distanceToBallToAttack && canAttack && !Ball.GetComponent<BallScript>().isBeingKicked) Kick();

        else if (Ball.GetComponent<BallScript>().isBeingKicked)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f; // Change 5f to the desired distance to move away
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
        }

        else if (distanceBallToAllyGoal > distanceFromGoalToFollow) FollowBall();

        else Defend();
    }

    private void Kick()
    {
        Ball.GetComponent<BallScript>().isBeingKicked = true;
        Ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * Random.Range(kickPowerMin, kickPowerMax));
    }

    private void FollowBall()
    {
        Vector3 positionOffset = Ball.transform.position - EnemyGoal.transform.position;
        agent.SetDestination(Ball.transform.position + positionOffset.normalized);
    }

    private void Defend()
    {
        Vector3 position = Vector3.Lerp(Ball.transform.position, AllyGoal.transform.position, CoefToDefend);
        agent.SetDestination(position);
    }
}
