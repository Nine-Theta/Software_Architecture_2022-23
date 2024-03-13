using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// An <see cref="I_Command"/> that checks if a given <see cref="AbstractInstanceFactory"/> can build an object at a mouse input location on a specific layer.
/// <para>If possible, it will make the factory build its set object at that location.</para>
/// </summary>
public class ConstructAtMouseRayCommand : I_Command
{
    private GameplayManager _receiver;

    private Vector2 _mousePos;

    private LayerMask _constructionLayer;

    private AbstractInstanceFactory _factoryBackup;
    private AbstractContainerObject _containerBackup;

    public ConstructAtMouseRayCommand(GameplayManager pReceiver, Vector2 pMousePos, LayerMask pConstructionLayer)
    {
        this._receiver = pReceiver;
        _mousePos = pMousePos;

        _factoryBackup = _receiver.ConstructionFactory;
        _constructionLayer = pConstructionLayer;
    }

    public bool Execute()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return false;

        if (_receiver.Credits - _factoryBackup.Containable.CreationCost < 0)
        {
            Debug.LogError("No Credits to build with!");
            return false;
        }

        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;
        //checks if the specific construction layer we need for the current factory is hit, or if it was a different one
        if (!Physics.Raycast(ray, out hit, 50, _constructionLayer) || (_factoryBackup.GetBuildLayer().value & (1 << hit.collider.gameObject.layer)) == 0)
            return false;

        Vector3 buildCoords = hit.point;

        if (hit.collider.CompareTag("Foundation"))
        {
            FoundationObject FO = hit.collider.GetComponent<FoundationObject>();

            if (!FO.BuildRequest())
                return false;
            buildCoords = FO.GetBuildPos;

            _containerBackup = _factoryBackup.CreateInstance(buildCoords, Quaternion.identity);

            (_containerBackup as TowerObject).SetFoundation(FO);
        }

        _receiver.Credits -= _factoryBackup.Containable.CreationCost;

        _containerBackup = _factoryBackup.CreateInstance(buildCoords, Quaternion.identity);

            return true;
    }

    public void Undo()
    {
        _receiver.Credits += _containerBackup.BaseData.CreationCost;
        _factoryBackup.DeleteInstance(_containerBackup);
    }
}
