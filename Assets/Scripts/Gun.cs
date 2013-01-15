using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Player m_player;
	public Transform m_muzzleExit;
	public GameObject m_bulletPrefab;
	
	public float m_shotDelay;
	public bool m_isAutomatic;
	
	bool m_firing = false;
	bool m_hasFired = false;
	
	// Update is called once per frame
	void Update () {
		CheckForFire();
	}
	
	void CheckForFire() {
		if(Input.GetButtonDown("Fire1") || Input.GetAxis ("Fire1") > 0.5f) {
			StartCoroutine(Fire ());
		}
		else {
			m_hasFired = false;
		}
	}
	
	IEnumerator Fire() {
		if(m_firing || (m_hasFired && !m_isAutomatic)) {
			return false;
		}
		m_firing = true;
		m_hasFired = true;
		var cam = Camera.main;
		var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
		RaycastHit rh;
		Vector3 dest;
		Shootable target = null;
		Physics.Raycast (ray, out rh, float.MaxValue);
		if(rh.collider != null) {
			dest = rh.point;
			target = rh.collider.gameObject.GetComponent<Shootable>();
		}
		else {
			dest = ray.origin + 1000 * ray.direction;
		}
		var bullet = (GameObject)GameObject.Instantiate(m_bulletPrefab);
		var lr = bullet.GetComponent<LineRenderer>();
		bullet.transform.parent = m_muzzleExit;
		bullet.transform.localPosition = Vector3.zero;
		bullet.transform.localRotation = Quaternion.identity;
		lr.SetPosition(1, bullet.transform.InverseTransformPoint(dest));
		lr.SetVertexCount(2);
		
		yield return new WaitForSeconds(0.1f);
		
		if(target != null) {
			target.ShotBy(m_player);
		}
		
		Destroy(bullet);
		
		if(m_shotDelay > 0) {
			yield return new WaitForSeconds(m_shotDelay);
		}
		
		m_firing = false;
	}
}
