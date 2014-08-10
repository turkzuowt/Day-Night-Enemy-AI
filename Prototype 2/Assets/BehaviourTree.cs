using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Allows me to use lists
using Pathfinding; // Added this from to allow access to Pathfinding
//using AI_Agent; // Added this from to allow access to AI_Agent class

public enum BEHAVIOUR_RESULT
{
	SUCCESS = 0,
	FAIL
};

public class BehaviourTree : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public interface IBehaviour
{
	BEHAVIOUR_RESULT execute(AI_Agent AIagent); //Seeker OR AI_Pather OR CharacterController??? Otherwise would be NavmeshAgent if we were using a Unity Navmesh
}

public class Composite
{
	void addChild(IBehaviour a_behaviour)
	{
		m_children.Add(a_behaviour);
	}

	public List<IBehaviour> m_children;
}

public class Selector : Composite, IBehaviour
{
	public BEHAVIOUR_RESULT execute(AI_Agent AIagent)
	{
		foreach (IBehaviour behaviour in m_children)
		{
			if(behaviour.execute(AIagent) == BEHAVIOUR_RESULT.SUCCESS)
				return BEHAVIOUR_RESULT.SUCCESS;
		}
		return BEHAVIOUR_RESULT.FAIL;		 
	}
}

public class Sequence : Composite, IBehaviour
{
	public BEHAVIOUR_RESULT execute(AI_Agent AIagent)
	{
		foreach (IBehaviour behaviour in m_children)
		{
			if(behaviour.execute(AIagent) == BEHAVIOUR_RESULT.FAIL)
				return BEHAVIOUR_RESULT.FAIL;
		}
		return BEHAVIOUR_RESULT.SUCCESS;
	}
}