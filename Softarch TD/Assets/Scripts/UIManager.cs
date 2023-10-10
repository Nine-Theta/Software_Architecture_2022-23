using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button TowerButton;
    public Button FoundationButton;
    public Button UpgradeButton;

    public List<GameObject> TowerTab;

    public void SelectTowerButton(Button pSelected)
    {
        for (int i = 0; i < TowerTab.Count; i++)
        {
            TowerTab[i].GetComponent<Button>().interactable = true;
        }

        if (pSelected == null) return;
        pSelected.interactable = false;
    }

    public void TowerTabSetActive(bool pActive)
    {
        for (int i = 0; i < TowerTab.Count; i++)
        {
            TowerTab[i].SetActive(pActive);
        }

        TowerButton.interactable = !pActive;

        FoundationButton.interactable = pActive;
    }
}
