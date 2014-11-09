using UnityEngine;
using System.Collections;

public class Fight : MonoBehaviour {
	
	public GameObject opponent;

	public AnimationClip attack;

	public int damage = 10;

	public float impactLength;

	public float range;

	public bool impacted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Attack if Spacebar is pressed
		if (Input.GetKey(KeyCode.Space) && inRange()) 
		{
			//Attack
			animation.CrossFade(attack.name);
			ClickToMove.attack = true;

			// Damage Opponent
			if(opponent != null){
				transform.LookAt(opponent.transform.position);

			}
		}

		// Stops Attack and Make Move enabled
		if (animation[attack.name.time] < 0.9 * animation[attack.name].length ) 
		{
			ClickToMove.attack = false;
			impacted = false;
		}

		impact();
	}

	void impact(){
		if(opponent != null && animation.IsPlaying(attack.name) && !impacted)
		{
			if(animation[attack.name].time > animation[attack.name].length * impactLength && animation[attack.name].time < 0.9 * animation[attack.name].length)
			{
				opponent.GetComponent<EnemyAI>().getHit(damage);
				impacted = true;
			}
		}
	}

	bool inRange(){

		if (Vector3.Distance(opponent.transform.position, transform.position) <= range)
		{
			return true;
		} 

		else 
		{
			return false;
		}
	}
	              
}


