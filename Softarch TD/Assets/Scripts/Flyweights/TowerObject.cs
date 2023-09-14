using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField]
    private TowerScriptable _baseData;
    [SerializeField]
    private GameObject _model;

    public void Initialize(TowerScriptable pData, GameObject pModel)
    {
        _baseData = pData;
        _model = pModel;
    }

}
