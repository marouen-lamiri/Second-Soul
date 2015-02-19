using UnityEngine;
using System.Collections;

public class FlockingAgent : MonoBehaviour {

	public Transform target;
	public float MaxVelocity;
	public float MaxAngularVelocity;
	
	public Vector3 Velocity { get; private set; }
	public float AngularVelocity { get; private set; }

	FlockingBehaviour [] behaviours;
	void Start()
	{
		
		behaviours = gameObject.GetComponents<FlockingBehaviour> ();
		ResetVelocities ();
	}
	
	void FixedUpdate()
	{
		UpdateVelocities(Time.deltaTime);
		
		UpdatePosition(Time.deltaTime);
		UpdateRotation(Time.deltaTime);
	}
	
	public void ResetVelocities()
	{
		Velocity = Vector3.zero;
		AngularVelocity = 0f;
	}
	
	private void UpdateVelocities(float deltaTime)
	{
		// throw new NotImplementedException();

		for (int i=0; i<behaviours.Length; ++i) {
			Velocity += behaviours[i].Acceleration*Time.fixedDeltaTime;
			if(behaviours[i].HaltTranslation){
				Velocity = Vector3.zero;
				return;
			}
		}
		Velocity += new Vector3 (5, 0, 5);
		Velocity = Vector3.ClampMagnitude (Velocity, MaxVelocity);
	}
	
	private void UpdatePosition(float deltaTime)
	{
		//throw new NotImplementedException();
		transform.position += Velocity * Time.fixedDeltaTime;
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}
	
	private void UpdateRotation(float deltaTime)
	{
		if(Velocity.sqrMagnitude > 0f)
			transform.rotation = Quaternion.LookRotation(Velocity.normalized, Vector3.up);
	}
}
