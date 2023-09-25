using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildScript : MonoBehaviour
{
    public TowerFactory TFactory;

    public TowerScriptable testType;

    public Vector3 TowerBuildPosition;

    public bool PlaceTowers = false;

    public GameObject BuildTower(TowerScriptable pTowerType, Vector3 pPosition)
    {
        GameObject newTower = TFactory.CreateInstance(pTowerType, pPosition);
        newTower.transform.position = pPosition;
        newTower.transform.position += new Vector3(0,1,0);
        return newTower;
    }

    public void RemoveTower(GameObject pTower)
    {
        Destroy(pTower);
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //TowerBuildCommander.UndoCommand();
        }
    }

}
