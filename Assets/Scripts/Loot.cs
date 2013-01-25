using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {
	
	public GunType m_type;
	public int m_ammoCount;
	public float m_rotationSpeed;
	
	float m_angle;
	
	public void ApplyForce() {
		var rand = new Vector3(Random.Range (-5.0f, 5.0f), 1.0f, Random.Range (-5.0f, 5.0f));
		rigidbody.AddForce(rand * 10.0f);
	}
	
	void Update() {
		m_angle += m_rotationSpeed * Time.deltaTime;
		while(m_angle >= 360.0f) {
			m_angle -= 360.0f;
		}
		transform.rotation = Quaternion.Euler(0, m_angle, 0);
	}
	
	void OnCollisionEnter(Collision collision) {
		var inventory = collision.gameObject.GetComponent<Inventory>();
		if(inventory != null) {
			inventory.GotLoot(m_type, m_ammoCount);
			Destroy (gameObject);
		}
	}
}
