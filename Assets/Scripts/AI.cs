using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AI : Character, IAnimateAble
{
    public Transform stairExitPosition;
    private List<Transform> pointsToVisit;
    public float minDistance = 0.5f;
    public float speed = 5.0f;
    private int _characterID;
    private int _brickCountGoal;
    private NavMeshAgent navAgent;
    private Transform nextPoint;
    private Animator _animator;
    private bool _isRunning = false;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private int _brickCountOfAI;
    private bool IsAIGoingToStairs;

    void Start()
    {
        _brickCountGoal = Random.Range(3, 8);
        navAgent = GetComponent<NavMeshAgent>();
        nextPoint = GetRandomPoint();
        

        _animator = GetComponentInChildren<Animator>();
        navAgent.SetDestination(nextPoint.position);
        Debug.Log(nextPoint);
    }

    void LateUpdate()
    {
        _brickCountOfAI = gameObject.GetComponent<BrickStacker>()._brickCount;
        if (Vector3.Distance(transform.position, nextPoint.position) < minDistance)
        {
            // remove the visited point
            if (pointsToVisit.Contains(nextPoint))
            {
                // remove the visited point
                pointsToVisit.Remove(nextPoint);
            }
            
            if (_brickCountOfAI >= _brickCountGoal)
            {
                Debug.Log("Yes I should fucking go to stairs ");
                navAgent.SetDestination(stairExitPosition.position);
                _brickCountGoal = Random.Range(3, 8);
                IsAIGoingToStairs=true;
            }
            else if (_brickCountOfAI >= 0)
            {
                if (pointsToVisit.Count > 0)
                {
                    
                    Debug.Log("Okay I need to find more bricks since "+_brickCountOfAI);
                    nextPoint = GetRandomPoint();
                    navAgent.SetDestination(nextPoint.position);
                }
            }
            
        
        }


        if (IsAIGoingToStairs && _brickCountOfAI == 0)
        {
            if (pointsToVisit.Count > 0)
            {
                    
                Debug.Log("Okay I need to find more bricks since "+_brickCountOfAI);
                nextPoint = GetRandomPoint();
                navAgent.SetDestination(nextPoint.position);
                IsAIGoingToStairs = false;
            }
        }

        Animate();
    }


    // Get a random point from the pointsToVisit list
    private Transform GetRandomPoint()
    {
        pointsToVisit = GameManager.AITargets[_characterID];
        int index = Random.Range(0,pointsToVisit.Count );
        return pointsToVisit[index];
    }

    
    
    private void Animate()
    {
        if (navAgent.velocity.magnitude > 0)
        {
            _isRunning = true;
            _animator.SetBool(IsRunning,_isRunning);
        }
        else
        {
            _isRunning = false;
            _animator.SetBool(IsRunning,_isRunning);
        }
    }

    protected override void Move()
    {
        
    }
}

