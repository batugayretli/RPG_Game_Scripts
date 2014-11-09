using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	// PROPERTIES
	public float speed;
	public float range; // Detection Range
	public CharacterController controller;

	public Transform player; // Position of the Player

	// Animations
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;

	private int health;

	// Use this for initialization
	void Start () {
		health = 100; // Default Enemy Health
	}
	
	// Update is called once per frame
	void Update () {

		if (!isDead()) 
		{
			// Search for Player
			if (InRange ()) {
				chase();
			}
			else{
				animation.CrossFade(idle.name); // Idle Animation
			}
		}
		else
		{
			animation.Play (die.name);
		}

		Debug.Log(health);
	}

	// Checks the Player in Range
	bool InRange(){

		if (Vector3.Distance (transform.position,player.position) < range){
			return true;
		}
		else {
			return false;
		}
	}

	public void getHit(int damage){
		health = health - damage;
		if (health < 0){
			health = 0;
		}
	}

	// If Died
	void dieMethod(){

		animation.Play (die.name);
		
		if(animation[die.name].time > animation[die.name].length * 0.9)
		{
			Destroy(gameObject); // Removes the Object
		}
	}
	
	// Check whether it is dead or not
	bool isDead()
	{
		if (health <= 0)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	// Chase the Player
	void chase(){
		// Look the Player
		transform.LookAt(player.position);

		// Move(Charge) toward the Player
		controller.SimpleMove(transform.forward * speed);
		animation.CrossFade(run.name); // Run Animation

	}
	
	void OnMouseOver(){

		player.GetComponent<Fight>().opponent = gameObject;
	}
	          
}
