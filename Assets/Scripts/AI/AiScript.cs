using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiScript : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Animator animator;

    public GameManager.Team team;

    public GameManager.BotRole role;

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

        if(Vector3.Distance(Ball.transform.position, EnemyGoal.transform.position) < Vector3.Distance(transform.position, EnemyGoal.transform.position)) canAttack = true; // checks if bot should kick the ball
        else canAttack = false;

        if (role == GameManager.BotRole.Defender) //sets behaviour pattern for defenders
        {
            Defend();
            if (Vector3.Distance(agent.destination, transform.position) < 5f) { animator.SetBool("isRunning", false); animator.SetBool("isDefending", true); }
            else { animator.SetBool("isRunning", true); animator.SetBool("isDefending", false); }
            if (distanceBallToPlayer <= distanceToBallToAttack && !Ball.GetComponent<BallScript>().isBeingKicked) Attack();
            return;
        }
        else
        {
            if (agent.isStopped) animator.SetBool("isRunning", false);
            else animator.SetBool("isRunning", true);
            animator.SetBool("isDefending", false);
        }
        //here behaviour for attackers
        FollowBall();

        if (distanceBallToPlayer <= distanceToBallToAttack && canAttack && !Ball.GetComponent<BallScript>().isBeingKicked) Attack();

        else if (Ball.GetComponent<BallScript>().isBeingKicked) //if ball can't be attacked as it's on cooldown, bots move a bit to a random direction so they dont get stuck with the ball; also adds randomness to gameplay
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f; 
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Kick");

        Vector3 directionToTarget = (EnemyGoal.transform.position - transform.position).normalized;

        // Check if the path to the attack target intersects with any other bots
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, Mathf.Infinity)) //if the kick would send the ball to an enemy, changes direction of a kick; helps with them getting stuck and adds some more interesting behaviour
        {
            AiScript hitBot = hit.collider.gameObject.GetComponent<AiScript>();
            if (hitBot != null && hitBot != this)
            {
                // If the path intersects with another bot, adjust direction away from it
                Vector3 newDirection = Vector3.Reflect(directionToTarget, hit.normal);
                Ball.GetComponent<Rigidbody>().AddForce(newDirection * Random.Range(kickPowerMin, kickPowerMax));
                return;
            }
        }
        Ball.GetComponent<BallScript>().isBeingKicked = true;
        Ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * Random.Range(kickPowerMin, kickPowerMax));
    }

    private void FollowBall()
    {
        Vector3 positionOffset = Ball.transform.position - EnemyGoal.transform.position; //calculates such a position, from which bot could kick into an enemy goal
        agent.SetDestination(Ball.transform.position + positionOffset.normalized); 
    }

    private void Defend()
    {
        Vector3 position = Vector3.Lerp(Ball.transform.position, AllyGoal.GetComponent<Goal>().actualPos.position, CoefToDefend); // goes in between the ball and the goal
        position += new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)); // adds a bit of randomness and helps with 2 defenders pushing each other
        agent.SetDestination(position);
    }
}
