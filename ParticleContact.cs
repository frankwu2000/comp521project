using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is to resolve physical simulation after two object collide.

public class ParticleContact{
	float penetration;
	public Particle[] particle = new Particle[2] ;
	float restitution;
	Vector3 contactNormal;


	public ParticleContact(Particle p1,Particle p2,float restitution,Vector3 contactNormal,float penetration){
		this.particle [0] = p1;
		this.particle [1] = p2;
		this.restitution = restitution;
		this.contactNormal = contactNormal;
		this.penetration = penetration;
	}

	public void Resolve(float duration){
		ResolveVelocity (duration);
		ResolveInterpenetration(duration);
	}

	public float SeperateVelocity(){
		Vector3 relativeVelocity = particle [0].velocity;
		if (particle [1] != null) {
			relativeVelocity -= particle [1].velocity;

		}
		return Vector3.Dot(relativeVelocity,contactNormal);
	}

	void ResolveVelocity(float duration){
		
		//find velocity in the direction of the contact
		float seperateVelocity = SeperateVelocity ();

		//check whether it needs to be resolved
		if (seperateVelocity > 0) {
			//no impluse require
			return;
		}

		float newSepVelocity = -seperateVelocity * restitution;

		//this part is to resolve resting collsion
//		Vector3 accCausedVelocity = particle[0].acceleration;
//
//		if (particle [1] != null) {
//			accCausedVelocity -= particle [1].acceleration;
//		}
//		float accCausedSeperateVelocity = Vector3.Dot(accCausedVelocity,contactNormal) * duration;
//
//		if (accCausedSeperateVelocity < 0) {
//			newSepVelocity += restitution * accCausedSeperateVelocity;
//
//			if (newSepVelocity < 0) {
//				newSepVelocity = 0;
//			}
//		}
		//end of resting collsion

		float deltaVelocity = newSepVelocity - seperateVelocity;

		//apply change in velocity to each object in proportion to inverse mass
		float totalInverseMass = 1/particle[0].mass;
		if (particle [1] != null) {
			totalInverseMass += 1/particle [1].mass;
		}

		//if all particles have huge mass, impulse has no effect
		if (totalInverseMass <= 0) {return;}

		//calculate impulse
		float impulse = deltaVelocity /totalInverseMass;

		//find the amount of impulse per unity of inverse mass
		Vector3 impulsePerIMass = contactNormal * impulse;

		//apply impulse
		particle[0].velocity = particle[0].velocity + impulsePerIMass / particle[0].mass;
		if (particle [1] != null) {
			particle [1].velocity = particle [1].velocity + impulsePerIMass / -particle [1].mass;
		}
	}

	//resolve penetration
	void ResolveInterpenetration(float duration){
		if(penetration <= 0 ){
			return;
		}
		float totalInverseMass = 1/particle[0].mass;
		if (particle [1] != null) {
			totalInverseMass += 1/particle [1].mass;
		}
		if (totalInverseMass <= 0) {return;}

		// Find the amount of penetration resolution per unit of inverse mass. 
		Vector3 movePerIMass = contactNormal *(-penetration / totalInverseMass);
		// Apply the penetration resolution. 
		particle[0].position=particle[0].position + movePerIMass * 1/particle[0].mass;
		if (particle[1]!=null)
		{
			particle[1].position=particle[1].position+movePerIMass* 1/particle[1].mass;
		}
	}
}
