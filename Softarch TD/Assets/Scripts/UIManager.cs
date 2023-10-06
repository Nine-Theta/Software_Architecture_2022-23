using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button ConstructButton;
    public Button TowerButton;
    public Button FoundationButton;
    public Button UpgradeButton;

    public List<GameObject> ConstructionTab;
    public List<GameObject> TowerTab;
    public List<GameObject> FoundationTab;

    public void SelectTowerButton(Button pSelected)
    {
        for (int i = 0; i < TowerTab.Count; i++)
        {
            TowerTab[i].GetComponent<Button>().interactable = true;
        }

        if (pSelected == null) return;
        pSelected.interactable = false;
    }

    public void ActiveConstructTab(bool pActive)
    {
        for (int i = 0; i < ConstructionTab.Count; i++)
        {
            ConstructionTab[i].SetActive(pActive);
        }
        ConstructButton.interactable = !pActive;
    }

    public void ActiveTowerTab(bool pActive)
    {
        setTowerTab(pActive);
        FoundationButton.interactable = pActive;
    }

    public void SelectFoundationButton()
    {
        setTowerTab(false);
        SelectTowerButton(null);

        FoundationButton.interactable = false;
    }

    private void setTowerTab(bool pActive)
    {
        for (int i = 0; i < TowerTab.Count; i++)
        {
            TowerTab[i].SetActive(pActive);
        }

        TowerButton.interactable = !pActive;
    }
}
