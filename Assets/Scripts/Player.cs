using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float m_speed;
	public float m_jumpForce;
	public Transform m_head;
	
	public float m_headMin;
	public float m_headMax;
	
	public float m_mouseSensitivity;
	
	float m_currentHeadAngle;

	// Use this for initialization
	void Start () {
		m_currentHeadAngle = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition();
		
		var delta = new Vector3(Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"), 0);
		UpdateRotation(delta);
		UpdateHeadRotation(delta);
	}
	
	void UpdatePosition() {
		var strafe = Input.GetAxisRaw ("Horizontal");
		var forward = Input.GetAxisRaw ("Vertical");
		var moveVec = (strafe * transform.right) + (forward * transform.forward);
		moveVec.Normalize();
		moveVec = moveVec * m_speed * Time.deltaTime;
		
		transform.position += moveVec;
		
		if(Input.GetButtonDown ("Jump")) {
			rigidbody.AddForce(transform.up * m_jumpForce);
		}
	}
	
	void UpdateRotation(Vector3 delta) {
		var angles = transform.rotation.eulerAngles;
		var amt = delta.x;
		amt /= m_mouseSensitivity;
		angles.y += amt;
		transform.rotation = Quaternion.Euler(angles);
	}
	
	void UpdateHeadRotation(Vector3 delta) {
		var headAngles = m_head.localRotation.eulerAngles;
		m_currentHeadAngle -= delta.y / m_mouseSensitivity;
		if(m_currentHeadAngle < m_headMin) {
			m_currentHeadAngle = m_headMin;
		}
		else if(m_currentHeadAngle > m_headMax) {
			m_currentHeadAngle = m_headMax;
		}
		headAngles.x = m_currentHeadAngle;
		m_head.localRotation = Quaternion.Euler(headAngles);
	}
}
