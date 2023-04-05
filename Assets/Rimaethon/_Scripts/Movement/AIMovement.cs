using System;
using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> targetPoints = new List<Transform>();
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    private float targetRadius = 2f;
   [SerializeField] private Transform currentTarget;
    private NavMeshAgent navMeshAgent;
    private ITypeDeterminer.ColorEnum _characterType;

    private void Awake()
    {
        _characterType = GetComponent<Character>().ColorType;
        
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomTarget();
    }

    private void Update()
    {
        if (ReachedTarget())
        {
            SetRandomTarget();
        }

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        navMeshAgent.SetDestination(currentTarget.position);
        navMeshAgent.speed = moveSpeed;
    }

    private bool ReachedTarget()
    {
        Debug.Log(Vector3.Distance(transform.position, currentTarget.position) + " And target radius is "+ targetRadius);
        return Vector3.Distance(transform.position, currentTarget.position) <= targetRadius;
        
    }

    private void SetRandomTarget()
    {
        int maxTries = 3;
        int numTries = 0;

        while (numTries < maxTries)
        {
            Transform randomTarget = Helpers.PickRandomFromList(
                SceneDataHolder.PooledBrickDictionary[BrickStatus.PooledBrickStatus.Active][_characterType]).transform;

            if (Vector3.Distance(transform.position, randomTarget.position) >= 4f )
            {
                currentTarget = randomTarget;
                return;
            }

            numTries++;
        }
        
        Debug.Log("Failed to find a suitable target after " + maxTries + " attempts.");
    }

    private void GoToRandomDoor(IPlatformAble.PlatformStates platform)
    {
        Transform randomDoor = Helpers.PickRandomFromList(SceneDataHolder.DoorsOnPlatforms[platform]).transform;
        currentTarget = randomDoor;
    }

}