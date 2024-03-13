using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Upgrade state for the <see cref="InputProcessor"/>.
/// <para>In this state the processor will select a <see cref="TowerObject"/> for the <see cref="GameplayManager"/> that has already been build on MouseClick, and show show their upgradeUI through the <see cref="UIManager"/>.</para> 
/// </summary>
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
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 50, _upgradeLayer))
        {
            context.GetUIManager.HideUpgradeUI();
            return;
        }


        TowerObject tower = hit.collider.gameObject.GetComponent<TowerObject>();

        context.GetUIManager.DisplayUpgradeUI(tower);

        context.GetGameplayManager.SetSelectedTower(tower);
    }
}
