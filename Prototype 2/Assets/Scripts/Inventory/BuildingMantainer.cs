using UnityEngine;
using System.Collections;

public class BuildingMantainer : MonoBehaviour
{
    //private LineRenderer line;
    public float maxHealth = 20f;
    private float currHealth;

    /** LineBlender health bar */
    private Vector3 oriPosStart; 
    private Vector3 currPosStart; 
    private Vector3 posEnd; 

    private float extension = 0f;
    private float colliderHeight = 0f;

    /** GUI health bar */
	private Vector3 drawPos1;
	private Vector3 drawPos2;

	void OnGUI()
	{
		GUI.backgroundColor = new Color(0f, 1f, 0f, 1f);

		drawPos1 = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, colliderHeight, 0) - new Vector3(extension * 0.5f, 0, 0));
		drawPos2 = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, colliderHeight, 0) + Camera.main.transform.right * extension * 0.5f);

		float rate;
		rate = currHealth / maxHealth;

		float left = drawPos1.x;
		float top = Screen.height - drawPos1.y;
		float width = (drawPos2.x - drawPos1.x) * rate;
		float height = 16;

		if(width > 0.5f)
		{
		    GUI.Button(new Rect(left, top, width, height), "");
		}
	}

    // Use this for initialization
    void Start()
    {
		currHealth = maxHealth;

        // Create a health bar object, and attach it to building 
        GameObject healthBar = new GameObject();
        healthBar.name = "HealthBar";
        healthBar.transform.parent = transform;
        healthBar.transform.localPosition = Vector3.zero;
        healthBar.transform.localScale = Vector3.one;

        // Add LineRenderer component
        /*line = healthBar.AddComponent<LineRenderer>();
        line.material = Resources.Load("Highlight") as Material;
        line.SetColors(Color.green, Color.green);
        line.SetWidth(0.5f, 0.5f);
        line.useWorldSpace = false;*/


        Collider collider;
		if(collider = transform.GetComponent<BoxCollider>())
        {
            extension = ((BoxCollider)collider).size.x;
			colliderHeight = ((BoxCollider)collider).size.y;
            oriPosStart = new Vector3(extension , 2, extension * 0.5f);
            posEnd = new Vector3(0, 2, extension * 0.5f);
        }
		else if(collider = transform.GetComponent<CapsuleCollider>())
        {
			extension = ((CapsuleCollider)collider).radius * 2;
			colliderHeight = ((CapsuleCollider)collider).height;
            oriPosStart = new Vector3(extension , 5, extension);
            posEnd = new Vector3(0, 5, extension);
        }

		currPosStart = oriPosStart;

        //line.SetPosition(0, currPosStart);
        //line.SetPosition(1, posEnd);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(Input.GetKey(KeyCode.Space) == true &&
			transform.GetComponent<PlaceableBuilding>().IsSelected)
		{
			BeAttacked(1f);
		}
		if(Input.GetKey(KeyCode.R) == true &&
			transform.GetComponent<PlaceableBuilding>().IsSelected)
		{
			BeRepared(1f);
		}

		//float rate;

		//rate = currHealth / maxHealth;

		//currPosStart = new Vector3(oriPosStart.x * rate, oriPosStart.y, oriPosStart.z);
        //line.SetPosition(0, currPosStart);
        //line.SetPosition(1, posEnd);
    }

    public void BeAttacked(float dmg)
    {
		if(currHealth > 0f && currHealth <= maxHealth)
            currHealth -= dmg;
		else if(currHealth <= 0)
        { 
			GameObject.Destroy(transform.gameObject);
        }
    }
    public void BeRepared(float health)
    {
		if(currHealth > 0f && currHealth < maxHealth)
            currHealth += health;
		else if(currHealth >= maxHealth)
			currHealth = maxHealth;
    }
}
