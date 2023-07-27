using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    [SerializeField] protected OverlapAttack _overlapAttack;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public int scoreValue;
    public NavMeshPath navMeshPath;
    [SerializeField] private float _changePositionTime;
    [SerializeField] protected float _moveDistance;
    public bool isVisbleTarget = false;
    
    [Header("States")]
    public State startState;
    public State stayState;
    public State moveState;
    
    [Header("Actual state")]
    public Vector3 currentMovePosition;
    public State currentState;
    
    public float ChangePositionTime => _changePositionTime;
    public float MoveDistance => _moveDistance;
    public Weapon Weapon => _weapon;
    public OverlapAttack OverlapAttack => _overlapAttack;
    
    [Inject]
    private void Construct(GameLevelController gameLevelController)
    {
        Debug.Log("Construct Enemy");
        _gameLevelController = gameLevelController;
    }
    
    private void OnEnable()
    {
        _overlapAttack.OnPlayerMelleAttack += RotateToObject;
    }

    private void OnDisable()
    {
        _overlapAttack.OnPlayerMelleAttack -= RotateToObject;
    }

    public override void Start()
    {
        base.Start();
        if (!agent)
            agent = GetComponent<NavMeshAgent>();
        agent.speed = _moveSpeed;
        navMeshPath = new NavMeshPath();
        SetState(startState);
    }

    public virtual void Update()
    {
        if (!currentState.isFinished)
            currentState.Run();

        if (_weapon)
        {
            _weapon.FindClosestUnit();
            if (_weapon._closestUnit && !isVisbleTarget)
            {
                isVisbleTarget = true;
                currentMovePosition = transform.position;
                MoveTo(currentMovePosition);
            
            }
            else if (!_weapon._closestUnit && isVisbleTarget) 
                isVisbleTarget = false;
        }
    }

    public void SetState(State state)
    {
        currentState = Instantiate(state);
        currentState.enemy = this;
        currentState.Initialize();
    }
    
    protected override void Death()
    {
        base.Death();
        _gameLevelController.AddScore(scoreValue);
        _gameLevelController.EnemyDeath(transform);
    }

    public virtual void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public virtual void NavRandomPoint(float distance, Transform target)
    {
        bool getCurrentPoint = false;
        while (!getCurrentPoint)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += target.position;
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas);

            agent.CalculatePath(navMeshHit.position, navMeshPath);
            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                getCurrentPoint = true;
                currentMovePosition = navMeshHit.position;
            }
        }
        MoveTo(currentMovePosition);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _moveDistance);
    }
#endif
}
