using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public struct BuildTower
    {
        public TowerScriptable TowerType;
        public Vector3 Location;

        public BuildTower(TowerScriptable pTowerType, Vector3 pLocation)
        {
            TowerType = pTowerType;
            Location = pLocation;
        }
    }
}
