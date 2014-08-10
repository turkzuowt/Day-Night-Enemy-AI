using UnityEngine;
using System.Collections;
//using BehaviourTree;

public class AI_Agent : MonoBehaviour {

	IBehaviour m_behaviour;
	Vector3 m_position;
	public GameObject m_target;

	// Constructor to initialise variables
	public AI_Agent()
	{
		m_position = transform.position;
		m_target.transform.position = new Vector3(0,0,0);
	}

	public Vector3 getPosition()
	{
		return m_position;
	}

	public GameObject getTarget()
	{
		return m_target;
	}

	public void setPosition(Vector3 a_position)
	{
		m_position = a_position;
	}

	public void setTarget(GameObject a_target)
	{
		m_target = a_target;
	}

	public void setBehaviour(IBehaviour a_behaviour)
	{
		m_behaviour = a_behaviour;
	}

	public virtual void update(float a_deltaTime)
	{
		if(m_behaviour != null)
			m_behaviour.execute(this);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
