using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//simple object movement controller

public class SnookerController: MonoBehaviour{
	Particle particle;
	public GameObject gethit;
	GameObject gethitClone;


	void Update(){
		particle = gameObject.GetComponent<MyPhysics> ().particle;
		float mass = particle.mass;
		float gravity = particle.mass;
		if (Input.GetKey(KeyCode.A)) {
			particle.AddForce (new Vector3(-80,0,0));


		}
		if (Input.GetKey(KeyCode.S)) {
			particle.AddForce (new Vector3(0,0,-80));


		}
		if (Input.GetKey(KeyCode.D)) {
			particle.AddForce (new Vector3(80,0,0));

		}
		if (Input.GetKey(KeyCode.W)) {
			particle.AddForce (new Vector3(0,0,80));

		}
		if (Input.GetKey (KeyCode.F)) {
			float flyforce = mass * gravity * 1.5f;
			particle.AddForce (new Vector3(0,flyforce,0));
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			RestartGame ();

		}
		if (Input.GetKeyDown (KeyCode.G)) {
			gethitClone = GameObject.Instantiate (gethit);
		}

		Vector3 direction = particle.forceAccum.normalized*5;

		Debug.DrawRay (transform.position, direction,Color.cyan);

	}

	void RestartGame(){
		particle.position = new Vector3 (0,0.7f,-10);
	}



}
