using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum State {Idle, Patrolling, Working, Returning, Shooting}

    public bool hasKey = false;
    public int score;
    public int scoreWithKey;
    public float fov;
    public float weaponCooldown;
    public GameObject bullet;
    public float workTime;

    private Transform group;
    private GameObject keyEffect;
    private State state = State.Idle;
    private State origState = State.Idle;
    private Transform player;
    private NavMeshAgent agent;
    private float nextShot = 0f;
    private float bulletOffset;
    private float terminalRotation;
    private float returnTime;
    private Vector3 returnPos;
    private EnemyGroupController groupCtrl;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        group = transform.parent;
        keyEffect = transform.GetChild(0).gameObject;
        transform.up = group.position - transform.position;
        player = FindObjectOfType<PlayerController>().transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        bulletOffset = 1.1f * transform.localScale.y;
        returnPos = transform.position;
        groupCtrl = group.GetComponent<EnemyGroupController>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        if (state != State.Shooting)
        {
            origState = state;
        }
        
        if (CanSeePlayer(out Vector2 playerDir))
        {
            scoreKeeper.TryTriggerAlarm();
            agent.isStopped = true;
            transform.up = playerDir;
            state = State.Shooting;
            if (Time.timeSinceLevelLoad > nextShot)
            {
                Instantiate(bullet, transform.position + transform.up * bulletOffset, transform.rotation);
                GetComponent<AudioSource>().Play();
                nextShot = Time.timeSinceLevelLoad + weaponCooldown;
            }
        }
        else
        {
            agent.isStopped = false;
            state = origState;
        }

        if (agent.pathPending)
        {
            return;
        }

        switch (state)
        {
            case State.Patrolling:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.ResetPath();
                    returnTime = Time.timeSinceLevelLoad + workTime;
                    transform.rotation = Quaternion.Euler(0f, 0f, terminalRotation + 180f);
                    state = State.Working;
                }
                break;
            case State.Working:
                if (Time.timeSinceLevelLoad > returnTime)
                {
                    agent.SetDestination(returnPos);
                    state = State.Returning;
                }
                break;
            case State.Returning:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.ResetPath();
                    transform.up = group.position - transform.position;
                    state = State.Idle;
                    groupCtrl.NextPatrol();
                }    
                break;
            default:
                break;
        }

        if (agent.hasPath && !agent.isStopped)
        {
            transform.up = (Vector2)(agent.steeringTarget - transform.position);
        }
    }

    private bool CanSeePlayer(out Vector2 playerDir)
    {
        if (player == null)
        {
            playerDir = Vector2.zero;
            return false;
        }

        playerDir = player.position - transform.position;
        if (playerDir.magnitude < bulletOffset)
        {
            return true;
        }
        if (Vector2.Angle(transform.up, playerDir) < 0.5f * fov)
        {
            Collider2D rayHit = Physics2D.Raycast(transform.position + bulletOffset * transform.up,
                                                    playerDir, playerDir.magnitude).collider;
            if (rayHit != null)
            {
                if (rayHit.GetComponent<PlayerController>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ReceiveKey()
    {
        hasKey = true;
        score = scoreWithKey;
        keyEffect.SetActive(true);
    }

    public void StartPatrol(Transform target)
    {
        if (state == State.Shooting)
        {
            origState = State.Patrolling;
        }
        else
        {
            state = State.Patrolling;
        }
        agent.SetDestination(target.position);
        terminalRotation = target.rotation.eulerAngles.z;
    }

    public void Die()
    {
        if (hasKey)
        {
            groupCtrl.gate.Unlock();
        }
        scoreKeeper.UpdateScore(score);
        groupCtrl.Remove(this);
        if (state != State.Idle && origState != State.Idle)
        {
            groupCtrl.NextPatrol();
        }
        Destroy(gameObject);
    }
}
