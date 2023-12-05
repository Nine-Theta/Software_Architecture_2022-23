using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface _mesh;

    [SerializeField]
    private Transform _navigationTarget;

    [SerializeField]
    private NavMeshAgent _navigationTester;

    private NavMeshPath paff;

    private void Start()
    {
        GetSceneValues();
    }

    public void GetSceneValues()
    {
        _mesh = SceneSettings.Instance.GetNavMeshSurface();
        _navigationTarget = SceneSettings.Instance.GetSceneBase().transform;
        _navigationTester = SceneSettings.Instance.GetSceneNavigiationTester();
    }

    public bool IsLayoutValid()
    {
        //A roundabout way of doing this, but PathStatus will always be PathComplete if not asigned to an agent
        NavMeshPath path = new NavMeshPath();
        _navigationTester.CalculatePath(_navigationTarget.position, path);
        _navigationTester.path = path;
        _navigationTester.isStopped = true;

        switch (_navigationTester.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                Debug.Log("Valid Path from Test agent to Target");
                return true;
            case NavMeshPathStatus.PathPartial:
                Debug.LogWarning("Only a Partial Path from Test agent to Target");
                break;
            case NavMeshPathStatus.PathInvalid:
                Debug.LogError("Invalid Path from Test agent to Target");
                break;
        }

        return false;
    }


    [Button]
    public void TestNavigation()
    {
        IsLayoutValid();
    }
}
