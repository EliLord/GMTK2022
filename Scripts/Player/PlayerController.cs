using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController characterController;
	[SerializeField] private float rollDistance;
	[SerializeField] private float rollDuration;
	[SerializeField] private UnityEvent rolled;
	[SerializeField] private AttackHandler attack;

	public static bool canMove;
	
	private PlayerControls playerControls;
	private float playerMoveSpeed = 10f;
	private float playerRollSpeed = 20f;

	private Transform playerTransform;
	private bool rolling;
	private bool attacking;

	private void Start()
	{
		canMove = true;
		
		playerControls = new PlayerControls();
		playerControls.Player.Enable();
		playerTransform = transform;
		
		playerControls.Player.Roll.performed += RollPressed;
		playerControls.Player.Attack.performed += AttackOnperformed;
	}

	private void AttackOnperformed(InputAction.CallbackContext obj)
	{
		if (!canMove)
			return;
		
		if (!rolling && !attacking)
		{
			attacking = true;

			attack.Attack(PlayerLoadoutUI.Instance.CurrentWeaponUI.WeaponInfo, transform.gameObject);

			attacking = false;
		}
	}

	private void RollPressed(InputAction.CallbackContext obj)
	{
		if (!canMove)
			return;
		
		if (!rolling && !attacking)
		{
			Vector2 movementVector;
			if (playerControls.Player.Move.inProgress)
			{
				movementVector = playerControls.Player.Move.ReadValue<Vector2>();
			}
			else
			{
				Vector3 playerRotation = playerTransform.rotation.eulerAngles;
				movementVector = DegreeToVector2(playerRotation.y);
			}

			Vector3 rollMovementVector = ConvertMovementVectorTo3D(movementVector) * rollDistance;
			StartCoroutine(Roll(rollMovementVector));
		}
	}

	private IEnumerator Roll(Vector3 rollVector)
	{
		rolling = true;
		rollVector *= 1 / rollDuration;
		float currentRollTime = rollDuration;
		while (currentRollTime > 0)
		{
			characterController.Move(rollVector * Time.deltaTime);
			currentRollTime -= Time.deltaTime;
			
			yield return null;
		}
		
		rolled?.Invoke();

		rolling = false;
	}

	private Vector3 ConvertMovementVectorTo3D(Vector2 movementVector)
	{
		return new Vector3(movementVector.x, 0, movementVector.y);
	}

	private void Update()
	{
		if (!canMove)
			return;
		
		if (!rolling && playerControls.Player.Move.inProgress)
		{
			Vector2 movementVector = playerControls.Player.Move.ReadValue<Vector2>();
			MovePlayer(movementVector);
			RotateToFaceInput(movementVector);
		}
	}

	private void MovePlayer(Vector2 movementVector)
	{
		characterController.Move(new Vector3(movementVector.x, 0, movementVector.y) * playerMoveSpeed *
		                         Time.deltaTime);
	}

	private void RotateToFaceInput(Vector2 movementVector)
	{
		float angle = Mathf.Atan2(movementVector.y, -movementVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.up);
		playerTransform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
	}
	
	private static Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}
  
	private static Vector2 DegreeToVector2(float degree)
	{
		Vector2 flippedInput = RadianToVector2(degree * Mathf.Deg2Rad);
		return new Vector2(flippedInput.y, flippedInput.x);
	}
}