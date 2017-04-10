using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallController : MonoBehaviour {
	Particle particle;
	GameObject gethitClone;
	
	// Update is called once per frame
	void Update () {
		

		particle = gameObject.GetComponent<MyPhysics> ().particle;
		float mass = particle.mass;
		float gravity = particle.mass;
		if (Input.GetKey (KeyCode.S)) {
			particle.AddForce (new Vector3 (-100, 0, 0));


		}
		if (Input.GetKey (KeyCode.D)) {
			particle.AddForce (new Vector3 (0, 0, -100));


		}
		if (Input.GetKey (KeyCode.W)) {
			particle.AddForce (new Vector3 (100, 0, 0));

		}
		if (Input.GetKey (KeyCode.A)) {
			particle.AddForce (new Vector3 (0, 0, 100));

		}
		if (Input.GetKey (KeyCode.F)) {
			float flyforce = mass * gravity * 1.5f;
			particle.AddForce (new Vector3 (0, flyforce, 0));
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			RestartGame ();

		}
		
	}




		void RestartGame(){
			particle.position = new Vector3 (-20,10,-10);
		}

}
