using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//1. Within Range (FOUND)
public class WithinRange : IBehaviour
{
	// Like a constructor where I can initialise 
	public WithinRange(float a_range)
	{
		m_rangeSquard = a_range * a_range;
	}

	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		float distSquard = Mathf.Pow(Vector3.Distance(a_enemyAgent.getPosition(), a_enemyAgent.getTarget().transform.position), 2);

		if(distSquard <= m_rangeSquard)
			return BEHAVIOUR_RESULT.SUCCESS;
		return BEHAVIOUR_RESULT.FAIL;
	}

	float m_rangeSquard;
}

//2. Set Target points to walk too (SET TO THE HOUSE)
public class SetTarget : IBehaviour
{
	public SetTarget(GameObject a_target)
	{
		m_target = a_target;
	}

	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		//Vector3 enemyPosition = a_enemyAgent.getPosition();
		GameObject target1;// = m_target; new Vector3(0,0,0); // Make this the position of the house

		// Set the input target position which will be the house
		target1 = m_target;

		// Do a distance check
		//float dist = Vector3.Distance(enemyPosition, target1.transform.position);

		// Set the actual target
		a_enemyAgent.setTarget(target1);

		return BEHAVIOUR_RESULT.SUCCESS;
	}

	GameObject m_target;
}

//3. Seek Target (MOVE TOWARDS THE TARGET POINT)
public class SeekTarget : IBehaviour
{
	public SeekTarget(float a_speed)
	{
		m_speed = a_speed;
	}

	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		Vector3 enemyPosition = a_enemyAgent.getPosition();
		Vector3 direction = Vector3.Normalize(a_enemyAgent.getTarget().transform.position - enemyPosition);

		// Move the enemy towards the target
		a_enemyAgent.setPosition(enemyPosition + direction * m_speed * Time.deltaTime);
		
		return BEHAVIOUR_RESULT.SUCCESS;
	}

	float m_speed;
}

//4. Catch enemy (MOVE TOWARDS PLAYER)
public class CatchEnemy : IBehaviour
{
	public CatchEnemy(float a_viewDistance, float a_catchingSpeed)
	{
		m_catchingSpeed = a_catchingSpeed;
		m_viewDistance = a_viewDistance;
	}
	
	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		// AI_Agents position
		Vector3 enemyPosition = a_enemyAgent.getPosition();

		// The players position
		Vector3 playerPosition = CharController.transform.position;

		// Do a distance check
		float dist = Vector3.Distance(enemyPosition, playerPosition);
		Vector3 dir = Vector3.Normalize(playerPosition - enemyPosition);

		// If the dist between them is less the view distance then move towards the enemy
		if(dist <= m_viewDistance)
		{
			a_enemyAgent.setPosition(enemyPosition + dir * m_catchingSpeed * Time.deltaTime);
		}

		return BEHAVIOUR_RESULT.SUCCESS;
	}

	float m_catchingSpeed;
	float m_viewDistance;
	CharacterController CharController;
}

//5. Attack Player (ATTACK)
public class AttackPlayer : IBehaviour
{
	public AttackPlayer(float a_attackDistance)
	{
		m_attackDistance = a_attackDistance;
	}
	
	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		// AI_Agents position
		Vector3 enemyPosition = a_enemyAgent.getPosition();
		
		// The players position
		Vector3 playerPosition = CharController.transform.position;
		
		// Do a distance check
		float dist = Vector3.Distance(enemyPosition, playerPosition);

		// If the dist between them is less the view distance then move towards the enemy
		if(dist <= m_attackDistance)
		{
			//Play the attack animation
			if(!enemyTransform.animation.isPlaying)
			{
				enemyTransform.animation.Play ("attack");
			}
				
			enemyTransform.LookAt(playerPosition);
		}
		
		return BEHAVIOUR_RESULT.SUCCESS;
	}

	float m_attackDistance;
	CharacterController CharController;
	Transform enemyTransform;
}

//6. Destroy obstacle (SMASH THE OBSTACLE BLOCK YOUR THE WAY THROUGH TO THE HOUSE)
public class DestroyObstacle : IBehaviour
{
	//public DestroyObstacle(float a_attackDistance)
	//{
	//	m_attackDistance = a_attackDistance;
	//}
	
	public BEHAVIOUR_RESULT execute(AI_Agent a_enemyAgent)
	{
		// AI_Agents position
		Vector3 enemyPosition = a_enemyAgent.getPosition();
		
		// The players position
		Vector3 playerPosition = CharController.transform.position;
		
		// Do a distance check
		float dist = Vector3.Distance(enemyPosition, playerPosition);
		
		// If the dist between them is less the view distance then move towards the enemy
		if(dist <= m_attackDistance)
		{
			//Play the attack animation
			if(!enemyTransform.animation.isPlaying)
			{
				enemyTransform.animation.Play ("attack");
			}
			
			enemyTransform.LookAt(playerPosition);
		}
		
		return BEHAVIOUR_RESULT.SUCCESS;
	}
	
	float m_attackDistance;
	CharacterController CharController;
	Transform enemyTransform;
}

// The following code allows me to change the animation and also always face towards the player
//if(!tr.animation.isPlaying)
//{
//	tr.animation.Play ("attack");
//}

//tr.LookAt(target.position);


public class Behaviours : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/////////////////BEHAVIOUR TREE CREATION AND INITIALISATION//////////////////
		//AI_Agent m_enemyAgent;
		//m_enemyAgent = new Agent();
		
		//IBehaviour seek;
		//IBehaviour gotoHouse;
		//IBehaviour within;
		//IBehaviour catchAgent;
		
		//seek = new SeekTarget(2); //set the speed of the enemy agent

		//gotoHouse = new SetTarget();
		//within = WithinRange(0.05f); //if within range of lookout point then seek towards next target
		//catchAgent = CatchEnemy(this, 2.5, 10); //create a new behaviour to catch enemy if spotted passing in the distance he can see  //pass in the agent position also
		
		//Sequence seq;
		//seq->addChild(catchAgent);
		//seq->addChild(within);
		//seq->addChild(gotoLookoutPoint);
		
		//Selector root = new Selector();
		//root->addChild(seq);
		//root->addChild(seek);
		
		//m_behaviour = root;
		
		//m_enemyAgent->setBehaviour(m_behaviour);
		//m_enemyAgent.setBehaviour(m_behaviour);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
