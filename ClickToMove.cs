using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {
	
	// VARIABLES

	public float speed;
	private Vector3 position;
	public CharacterController controller;

	public static bool attack;

	public AnimationClip run;
	public AnimationClip idle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(!attack)
		{
			if (Input.GetMouseButton (0)) { // 0: Left Mouse Button
					// Locate where the player clicked on the terrain
					locatePosition ();
			}
			
			// Move Player to the position
			movePosition ();
		}
	}

	// METHODS

	void locatePosition(){
		// It uses Physical Ray and Collide it with terrain in order to get the position
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); // Ray From Main Camera

		// Updates Position if Ray from Main Camera Collides
		if(Physics.Raycast(ray,out hit,1000))
		{
			if(hit.collider.tag != "Player"){ // Unables to click onself
			
				position = new Vector3( hit.point.x, hit.point.y, hit.point.z);
			}
		}
	}

	void movePosition(){

		// Game Object on the Move
		if(Vector3.Distance(transform.position,position) > 1)
		{
			Quaternion newRotation = Quaternion.LookRotation(position-transform.position, Vector3.forward);
			
			// Locks these Axis
			newRotation.x = 0f;
			newRotation.z = 0f;

			// Rotates
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

			// Then Moves
			controller.SimpleMove(transform.forward * speed);

			animation.CrossFade(run.name); // Run Animation NOTE: CroosFade() smooths the Animation Transition instead of Play()
		}
		// Game Object Stop
		else
		{
			animation.CrossFade(idle.name); // Idle Animation
		}

	}
}
