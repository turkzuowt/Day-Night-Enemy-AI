using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {
	public GameObject[] buildings;

    public List<string> ebuildingOneCost;
    public List<string> ebuildingTowCost;
    public List<string> ebuildingThreeCost;

    static public List<string> buildingOneCost;
    static public List<string> buildingTowCost;
    static public List<string> buildingThreeCost;

	private List<string>[] resourceCost = new List<string>[3];

	private BuildingPlacement buildingPlacement;

    private Rect buildingListWindowRect = new Rect(10, 50, 100, 400);
    private bool buildingListWindowShow = true;

    private string lastTooltip = "";

    private List<PlaceableBuilding> currentBuildingsOnMap;

	// Use this for initialization
	void Start () {
		buildingPlacement = GetComponent<BuildingPlacement>();

		buildingOneCost = ebuildingOneCost;
		buildingTowCost = ebuildingTowCost;
		buildingThreeCost = ebuildingThreeCost;

		resourceCost[0] = buildingOneCost;
		resourceCost[1] = buildingTowCost;
		resourceCost[2] = buildingThreeCost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
        if (buildingListWindowShow)
        {
            buildingListWindowRect = GUI.Window(2, buildingListWindowRect,
                                                BuidingListGUIMathod, "Building");
        }
	}
    void BuidingListGUIMathod(int windowId)
    {
        GUILayout.BeginArea(new Rect(15, 30, 70, 400));

        GUILayout.BeginVertical();

        for (int i = 0; i < buildings.Length; i++)
        {
            if (GUILayout.Button(new GUIContent(buildings[i].name, "MouseOver"), GUILayout.Height(50)))
            {
                buildingPlacement.CancelCurrentSelected();
				if(CostResource(i))
				{
                    buildingPlacement.SetItem(buildings[i]);
				}
            }
        }

        GUILayout.EndVertical();

        GUILayout.EndArea();

        if (Event.current.type == EventType.repaint && GUI.tooltip != lastTooltip)
        {
            if (lastTooltip != "")
                //Debug.LogWarning("OnMouseOut");
                InventoryGUI.overGUI = false;

            if (GUI.tooltip != "")
                //Debug.LogWarning("OnMouseOver");
                InventoryGUI.overGUI = true;

            lastTooltip = GUI.tooltip;
        }
    }

    public bool CostResource(int index)
	{
		int meetRequired = 0;
		List<int> indexList = new List<int>();

        if (InventoryGUI.inventoryNameDictionary.Keys.Count > 0)
        {
            for(int i = 0; i < InventoryGUI.inventoryNameDictionary.Keys.Count; ++i)
            {
                string item = InventoryGUI.inventoryNameDictionary[i];
                for(int j = 0; j < resourceCost[index].Count; ++j)
                {
                    if(resourceCost[index][j] == item)
                    {
                        meetRequired++;
                        indexList.Add(i);
                        if(meetRequired == resourceCost[index].Count)
                        {
                            foreach(int k in indexList)
                            {
                                InventoryGUI.dictionaryAmounts[k]--;
                            }
                            return true;
                        }
                    }
                }
            }
        }
		return false;
	}

    public void addBuilding(PlaceableBuilding go)
    {
        currentBuildingsOnMap.Add(go);
    }
    public void removeBuilding(PlaceableBuilding go)
    {
        currentBuildingsOnMap.Remove(go);
    }
}
