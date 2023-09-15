using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NavMeshMovementStrategy", menuName = "Strategy/Movement/Navmesh"), RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementStrategy : AbstractMovementStrategy
{
    private NavMeshAgent bbbb;
    

    public override void MoveStep(Vector3 pDestiniation)
    {
        throw new System.NotImplementedException();
    }

}
