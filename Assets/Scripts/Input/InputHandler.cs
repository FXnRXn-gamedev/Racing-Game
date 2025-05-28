using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace FXnRXn
{
	public class InputHandler : MonoBehaviour
	{
		public static InputHandler Instance { get; private set; }

		private void Awake() => Instance = this;


		#region Variable

		
		
		
		
		
		
		
		
		private float m_gas;
		public float gasInput
		{
			get => m_gas;
			set => m_gas = value;
		}

		private float m_brake;
		public float brakeInput
		{
			get => m_brake;
			set => m_brake = value;
		}

		private Vector2 m_steering;
		public Vector2 steeringInput
		{
			get => m_steering;
			set => m_steering = value;
		}


		#endregion


		#region Setting up the value

		public void OnAccelerate(InputValue button)
		{
			if (button.isPressed)
			{
				gasInput = 1;
			}
			if (!button.isPressed)
			{
				gasInput = 0;
			}
		}
		
		public void OnBrake(InputValue button)
		{
			if (button.isPressed)
			{
				brakeInput = 1;
				if (PlayerCarController.Instance.brakeAssist)
					PlayerCarController.Instance.GetPlayerRigid().constraints = RigidbodyConstraints.FreezeRotationX |
						RigidbodyConstraints.FreezeRotationZ;
			}
			if (!button.isPressed)
			{
				brakeInput = 0;
				if (PlayerCarController.Instance.brakeAssist)
					PlayerCarController.Instance.GetPlayerRigid().constraints = RigidbodyConstraints.None;
			}
		}

		public void OnSteering(InputValue value)
		{
			steeringInput = value.Get<Vector2>();
		}

		#endregion



	}
}

