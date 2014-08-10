using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingPlacement : MonoBehaviour {
	
	public float scrollSensitivity;
	
	private PlaceableBuilding placeableBuilding;
	private Transform currentBuilding;
	private bool hasPlaced = false;
	
	public LayerMask buildingsMask;
	
	private PlaceableBuilding placeableBuildingOld;

    public Material highlightMat;
    static private Material currMat;
	
	// Update is called once per frame
	void Update () {
        Vector3 placementLocation = new Vector3(0,0,0);
        Ray ray_BuildingLocate = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, 0);

		if (currentBuilding != null && !hasPlaced) 
		{

            // Get gameobject of current building 
            int index = currentBuilding.name.IndexOf("(");
            string name = currentBuilding.name.Remove(index, 7);
            GameObject go = currentBuilding.transform.FindChild(name).gameObject;

			// Player can't move
			InventoryGUI.overGUI = true;

            // Record current material
            if (!currMat)
            {
                currMat = go.renderer.material;
            }
            // Set highlight material
            go.renderer.material = highlightMat;

            // Calculate the desired building placement position
            float dist = 500;
            if (ground.Raycast(ray_BuildingLocate, out dist) == true)
            {
                placementLocation = ray_BuildingLocate.origin + ray_BuildingLocate.direction * dist;
                currentBuilding.position = new Vector3(placementLocation.x, 0, placementLocation.z);
            }
			
			if (Input.GetMouseButtonDown(0)) {
				if (IsLegalPosition() && currMat) {
                    go.renderer.material = currMat;
                    currMat = null;
					hasPlaced = true;
					currentBuilding.gameObject.layer = LayerMask.NameToLayer("Default");
					currentBuilding.collider.isTrigger = false;
                    //transform.GetComponent<BuildingManager>().addBuilding(placeableBuilding);
				}
			}

            if (Input.GetMouseButtonDown(1))
            {
                CancelCurrentSelected();
                // Player can move
                InventoryGUI.overGUI = false;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f )
            {
                //currentBuilding.Rotate(0, 90, 0, Space.World);

                // Only rotate the mesh component
                go.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f )
            {
                //currentBuilding.Rotate(0, -90, 0, Space.World);

                // Only rotate the mesh component
                go.transform.Rotate(0, -90, 0, Space.World);
            }

            if (IsLegalPosition() && !hasPlaced)
            {
                go.renderer.material = highlightMat;
            }
            else if (!IsLegalPosition() && !hasPlaced)
            {
                go.renderer.material = null;
            }
		}
		else if (currentBuilding != null && hasPlaced)
		{
			if(Input.GetMouseButton(0))
			{
                /* Wait for 0.5 second, then turn off overGUI;
                 * We can not turn off overGUI in this frame, because player update()
				 * is generating after this update
				 */
				StartCoroutine(DelayImplement(0.5f, () =>
				{
					InventoryGUI.overGUI = false;
					currentBuilding = null;
				} ));
			}
		}
		else 
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray_BuildingLocate, out hit, Mathf.Infinity) && hit.collider.gameObject.tag == "Building")
                {
					if (placeableBuildingOld != null) 
					{
						placeableBuildingOld.SetSelected(false);
					}
					hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
					placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();
				}
				else 
				{
					if (placeableBuildingOld != null) 
					{
						placeableBuildingOld.SetSelected(false);
					}
				}
			}
		}
	}


	bool IsLegalPosition()
	{
        /*if (placeableBuilding.colliders.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }*/

		Collider[] collidersArray = Physics.OverlapSphere(placeableBuilding.transform.position, 1, ~(1 << LayerMask.NameToLayer("IgnorePlace")));

		List<Collider> colliders = new List<Collider>(collidersArray);
		colliders.RemoveAll(x => x.gameObject == this.gameObject);
		colliders.RemoveAll(x => x.name == "Ground");

		return colliders.Count == 0;

		/*foreach (Collider c in placeableBuilding.colliders)
		{
			CapsuleCollider collider = c as CapsuleCollider;

			if (collider == null) continue;

			Vector3 halfHeight = new Vector3(0, collider.height / 2, 0);
			return !Physics.CheckCapsule(placeableBuilding.transform.position - halfHeight, placeableBuilding.transform.position + halfHeight, collider.radius);
		}*/

		return true;
	}
	
	public void SetItem(GameObject b) {
		hasPlaced = false;
		currentBuilding = ((GameObject)Instantiate(b)).transform;
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
		currentBuilding.gameObject.layer = LayerMask.NameToLayer("IgnorePlace");
		currentBuilding.collider.isTrigger = true;
	}

    public void CancelCurrentSelected()
    {
        if (currentBuilding != null && !hasPlaced)
        {
            Destroy(currentBuilding.gameObject);
        }
    }

    /// <summary>
    /// Wait for waitTimes, then implement act()
    /// </summary>
    /// <param name="waitTimes"></param>
    /// <param name="act"></param>
    /// <returns></returns>
    private IEnumerator DelayImplement(float waitTimes, Action act)
	{
		yield return new WaitForSeconds(waitTimes);
		act();
	}
}
