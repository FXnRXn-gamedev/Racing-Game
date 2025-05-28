using System;
using UnityEngine;

namespace FXnRXn
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerCarController : MonoBehaviour
	{
		public static PlayerCarController Instance { get; private set; }

		private void Awake() => Instance = this;

		#region Variable

		[Header("----------		Setting :		----------")] 
		[Space(25)]
		public bool												brakeAssist = false;
		
		[Space(25)]
		[SerializeField] private WheelCollider[]				wheelColliders;
		[SerializeField] private GameObject[]					wheels;
		[SerializeField] private float							driveTorque = 100;
		[SerializeField] private float							brakeTorque = 500;
		[SerializeField] private float							downForce = 100;
		[SerializeField] private float							steerAngle = 30;

		private float											forwardTorque;
		private Rigidbody										rigidbody;
		private InputHandler									inputHandler;

		#endregion

		public Rigidbody GetPlayerRigid() => rigidbody;
		
		private void Start()
		{
			if(rigidbody == null) rigidbody = GetComponent<Rigidbody>();
			if(inputHandler == null) inputHandler = InputHandler.Instance;
		}

		private void Update()
		{
			if(!inputHandler) return;

			Drive(inputHandler.gasInput, inputHandler.brakeInput, inputHandler.steeringInput);
			AddDownForce();
		}
		
		//--------------------------------------------------------------------------------------------------------------

		private void Drive(float acceleration, float brake, Vector2 steer)
		{
			forwardTorque = acceleration * driveTorque;
			brake *= brakeTorque;
			steer.x *= steerAngle;
			
			// Wheel meshed move with wheel colliders
			for (int i = 0; i < wheels.Length; i++)
			{
				Vector3 wheelPosition;
				Quaternion wheelRotation;
				
				wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
				wheels[i].transform.position = wheelPosition;
				wheels[i].transform.rotation = wheelRotation;
			}
			
			// Wheel colliders move the car
			for (int i = 0; i < wheelColliders.Length; i++)
			{
				wheelColliders[i].motorTorque = forwardTorque;
				wheelColliders[i].brakeTorque = brake;
				if (i < 2)
				{
					wheelColliders[i].steerAngle = steer.x;
				}
				
			}
		}

		private void AddDownForce()
		{
			wheelColliders[0].attachedRigidbody.AddForce(-transform.up * downForce * wheelColliders[0].attachedRigidbody.linearVelocity.magnitude);
		}
	}
}


