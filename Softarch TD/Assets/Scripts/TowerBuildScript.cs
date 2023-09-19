using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildScript : MonoBehaviour
{
    public TowerFactory TFactory;

    public TowerScriptable testType;

    public Vector3 TowerBuildPosition;

    public Commander TowerBuildCommander =  new Commander();

    public GameObject BuildTower()
    {
        GameObject newTower = TFactory.CreateTower(testType);
        newTower.transform.position = TowerBuildPosition;
        newTower.transform.position += new Vector3(0,1,0);
        return newTower;
    }

    public void RemoveTower(GameObject pTower)
    {
        Destroy(pTower);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
            RaycastHit hit;            
            Physics.Raycast(ray, out hit);
            if (hit.collider == null) return;
            TowerBuildPosition = hit.point;
            TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TowerBuildCommander.UndoCommand();
        }
    }

}
