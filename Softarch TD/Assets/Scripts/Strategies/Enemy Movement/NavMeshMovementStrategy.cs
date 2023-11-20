using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NavMeshMovementStrategy", menuName = "Strategy/Movement/Navmesh"), RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementStrategy : AbstractMovementStrategy
{
    public override void Initialize(EnemyObject pEnemy, float pMoveSpeed)
    {
        NavMeshAgent agent;

        if (!pEnemy.gameObject.TryGetComponent<NavMeshAgent>(out agent))
        {
           agent = pEnemy.gameObject.AddComponent<NavMeshAgent>();
        }

        agent.speed = pMoveSpeed;

        //Required for good pathfinding at higher speeds
        agent.acceleration = 100;
        //Enemy should not care about other enemies on the path
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public override void MoveTo(EnemyObject pEnemy, Vector3 pDestiniation)
    {
        pEnemy.GetComponent<NavMeshAgent>().SetDestination(pDestiniation);
    }

}
