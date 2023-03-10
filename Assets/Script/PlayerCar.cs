using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
	[SerializeField] private GameObject LeftBackLight;
	[SerializeField] private GameObject RightBackLight;

	public Transform tireTransformFL;
	public Transform tireTransformFR;
	public Transform tireTransformRL;
	public Transform tireTransformRR;

	public WheelCollider colliderFR;
	public WheelCollider colliderFL;
	public WheelCollider colliderRR;
	public WheelCollider colliderRL;

	public Transform wheelTransformFL;
	public Transform wheelTransformFR;
	public Transform wheelTransformRL;
	public Transform wheelTransformRR;

	public float highestSpeed = 350.0f;
	public float lowSpeedSteerAngle = 0.1f;
	public float highSpeedStreerAngle = 35.0f;

	public float decSpeed = 50.0f;

	public float currentSpeed;
	public float maxSpeed = 350.0f;
	public float maxRevSpeed = 100.0f;

	public int maxTorque = 10;

	private float prevSteerAngle;
	private Rigidbody rigid;

	WheelFrictionCurve ForRRwheel;
	WheelFrictionCurve SideRRwheel;
	WheelFrictionCurve ForRLwheel;
	WheelFrictionCurve SideRLwheel;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	void Start()
	{
		rigid.centerOfMass = new Vector3(0, -1, 0); // 무게중심이 높으면 차가 쉽게 전복된다
		ForRRwheel = colliderRR.forwardFriction;
		SideRRwheel = colliderRR.sidewaysFriction;
		ForRLwheel = colliderRL.forwardFriction;
		SideRLwheel = colliderRL.sidewaysFriction;
	}

	void FixedUpdate()
	{
		Control();
	}

	void Update()
	{
		tireTransformFL.Rotate(Vector3.up, colliderFL.steerAngle - prevSteerAngle, Space.World);
		tireTransformFR.Rotate(Vector3.up, colliderFR.steerAngle - prevSteerAngle, Space.World);
		prevSteerAngle = colliderFR.steerAngle;
		BackLightOnOff();
		ResetCar();
	}

	void BackLightOnOff()
	{
		if (currentSpeed == 0)
		{
			LeftBackLight.SetActive(true);
			RightBackLight.SetActive(true);
		}
		else
		{
			LeftBackLight.SetActive(false);
			RightBackLight.SetActive(false);
		}
	}

	void ResetCar()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
			colliderRR.motorTorque = 0;
			colliderRL.motorTorque = 0;
		}
	}

	void Control()
	{
		currentSpeed = 2 * 3.14f * colliderRL.radius * colliderRL.rpm * 60 / 1000;
		currentSpeed = Mathf.Round(currentSpeed);

		if (currentSpeed <= 0 && currentSpeed > -maxSpeed)
		{
			colliderRR.motorTorque = -maxTorque * Input.GetAxis("Vertical") * 5;
			colliderRL.motorTorque = -maxTorque * Input.GetAxis("Vertical") * 5;
		}
		else if (currentSpeed >= 0 && currentSpeed < maxRevSpeed)
		{
			colliderRR.motorTorque = -maxTorque * Input.GetAxis("Vertical") * 5;
			colliderRL.motorTorque = -maxTorque * Input.GetAxis("Vertical") * 5;
		}
		else
		{
			colliderRR.motorTorque = 0;
			colliderRL.motorTorque = 0;
		}

		if (!Input.GetButton("Vertical"))
		{
			colliderRR.brakeTorque = decSpeed;
			colliderRL.brakeTorque = decSpeed;
		}
		else
		{
			colliderRR.brakeTorque = 0;
			colliderRL.brakeTorque = 0;
		}

		float speedFactor = rigid.velocity.magnitude / highestSpeed;
		float steerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedStreerAngle, 1 / speedFactor);
		//float steerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedStreerAngle, 1.0f);
		steerAngle *= Input.GetAxis("Horizontal");

		colliderFR.steerAngle = steerAngle;
		colliderFL.steerAngle = steerAngle;

		Drift();
		WheelRotate();
	}

	void Drift()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			SideRRwheel.stiffness = 1.0f;
			colliderRR.sidewaysFriction = SideRRwheel;

			ForRRwheel.stiffness = 1.0f;
			colliderRR.forwardFriction = ForRRwheel;

			SideRLwheel.stiffness = 1.0f;
			colliderRL.sidewaysFriction = SideRLwheel;

			ForRLwheel.stiffness = 1.0f;
			colliderRL.forwardFriction = ForRLwheel;

			if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
			{
				//colliderRR.attachedRigidbody.MoveRotation(Quaternion.Euler(0.0f, angle, 0.0f));
				//colliderRL.attachedRigidbody.MoveRotation(Quaternion.Euler(0.0f, angle, 0.0f));
			}

			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				wheelTransformFL.Rotate(wheelTransformFL.rotation.x, 0.0f, wheelTransformFL.rotation.z);
				wheelTransformFR.Rotate(wheelTransformFL.rotation.x, 0.0f, wheelTransformFL.rotation.z);
			}

			if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
			{
				//colliderRR.attachedArticulationBody.parentAnchorRotation = Quaternion.Euler(0.0f, prevSteerAngle, 0.0f);
				//colliderRL.attachedArticulationBody.parentAnchorRotation = Quaternion.Euler(0.0f, prevSteerAngle, 0.0f);
			}

			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				wheelTransformFL.Rotate(wheelTransformFL.rotation.x, 0.0f, wheelTransformFL.rotation.z);
				wheelTransformFR.Rotate(wheelTransformFL.rotation.x, 0.0f, wheelTransformFL.rotation.z);
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			SideRRwheel.stiffness = 1.0f;
			colliderRR.sidewaysFriction = SideRRwheel;

			ForRRwheel.stiffness = 1.0f;
			colliderRR.forwardFriction = ForRRwheel;

			SideRLwheel.stiffness = 1.0f;
			colliderRL.sidewaysFriction = SideRLwheel;

			ForRLwheel.stiffness = 1.0f;
			colliderRL.forwardFriction = ForRLwheel;
		}
	}

	void WheelRotate()
	{
		wheelTransformFL.Rotate(colliderFL.rpm / 180 * 360 * Time.fixedDeltaTime, 0, 0);
		wheelTransformFR.Rotate(colliderFR.rpm / 180 * 360 * Time.fixedDeltaTime, 0, 0);
		wheelTransformRL.Rotate(colliderRL.rpm / 180 * 360 * Time.fixedDeltaTime, 0, 0);
		wheelTransformRR.Rotate(colliderRR.rpm / 180 * 360 * Time.fixedDeltaTime, 0, 0);
	}
}
