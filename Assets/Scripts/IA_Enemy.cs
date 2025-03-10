using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NewBehaviourScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;

    [SerializeField] private float _visionRange = 15;
    [SerializeField] private float _attackRange = 3;

    public enum EnemyState
    {
        Patrolling,

        Chasing,

        Attacking

    }
    

    public EnemyState currentState;

    [SerializeField] private Transform[] _patrolPoints;
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Patrolling;
        SetPatrolPoint();
    }



    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                    Patrol();
            break;

            case EnemyState.Chasing:
                    Chase();
            break;

            case EnemyState.Attacking:
                    Attack();
            break;


            
        }
    }


    bool InRange(float range)
    {
        return  Vector3.Distance(transform.position, _playerTransform.position)<range;
    }

    void SetPatrolPoint()
    {
        _agent.destination = _patrolPoints[Random.Range(0,_patrolPoints.Length)].position;
    }

    void Patrol()
    {

        if(InRange(_visionRange))
        {

            currentState = EnemyState.Chasing;
        }

        if(_agent.remainingDistance < 0.5f)
        {
            SetPatrolPoint();
        }



    }

    void Chase()
    {

        if(!InRange(_visionRange))
        {

            currentState = EnemyState.Patrolling;
        }

        if(InRange(_attackRange))
        {

            currentState = EnemyState.Attacking;
        }

        _agent.destination = _playerTransform.position;


    }

    void Attack()
    {

        Debug.Log("Atacando");

        currentState = EnemyState.Chasing;


    }
}
