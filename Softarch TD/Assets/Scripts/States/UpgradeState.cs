using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UpgradeState", menuName = "States/UpgradeState")]
public class UpgradeState : AbstractProcessorState
{
    [SerializeField]
    private LayerMask _upgradeLayer;


    public UpgradeState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 50, _upgradeLayer))
        {
            context.UIManager.HideUpgradeUI();
            return;
        }


        TowerObject tower = hit.collider.gameObject.GetComponent<TowerObject>();

        context.UIManager.DisplayUpgradeUI(tower);

        /* TODO:
        * Upgrade UI
        * Check for Credits
        * Send Upgrade Command
        */
    }

    private void UpgradeTower()
    {
        //UpgradeCommand
    }
}
