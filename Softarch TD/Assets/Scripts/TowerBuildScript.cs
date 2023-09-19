using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildScript : MonoBehaviour
{
    public TowerFactory TFactory;

    public TowerScriptable testType;

    public void BuildTower(Command.BuildTower pTowerCommand)
    {
        GameObject newTower = TFactory.CreateTower(pTowerCommand.TowerType);
        newTower.transform.position = pTowerCommand.Location;
        newTower.transform.position += new Vector3(0,1,0);
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
            BuildTower(new Command.BuildTower(testType, hit.point));
        }
    }

}
