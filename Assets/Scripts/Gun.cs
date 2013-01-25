using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Player m_player;
	public Transform m_muzzleExit;
	public GameObject m_bulletPrefab;
	
	public float m_shotDelay;
	public bool m_isAutomatic;
	public bool m_launchesProjectiles;
	
	public int m_ammoCapacity;
	public int m_currentAmmo;
	public int m_damage;
	
	bool m_firing = false;
	public bool m_hasFired = false;
	bool m_reloading = false;
	
	Vector3 m_defaultPosition;
	Vector3 m_reloadPosition;
	
	void Start() {
		m_currentAmmo = m_ammoCapacity;
		m_defaultPosition = transform.localPosition;
		m_reloadPosition = new Vector3(m_defaultPosition.x, m_defaultPosition.y - 0.5f, m_defaultPosition.z);
	}
	
	public void TryToReload() {
		StartCoroutine(Reload());
	}
	
	public void TryToFire(Ray ray) {
		if(m_launchesProjectiles) {
			StartCoroutine(Launch());
		}
		else {
			StartCoroutine(Fire(ray));
		}
	}
	
	IEnumerator Launch() {
		if(m_firing || (m_hasFired && !m_isAutomatic) || m_reloading || !HasAmmo()) {
			return false;
		}
		m_firing = true;
		m_hasFired = true;
		DecrementAmmo();
		
		var bullet = (GameObject)GameObject.Instantiate(m_bulletPrefab);
		bullet.transform.position = m_muzzleExit.transform.position;
		bullet.transform.rotation = m_muzzleExit.transform.rotation;
		bullet.GetComponent<Rocket>().m_player = m_player;
		
		if(m_shotDelay > 0) {
			yield return new WaitForSeconds(m_shotDelay);
		}
		
		m_firing = false;
	}
	
	IEnumerator Fire(Ray ray) {
		if(m_firing || (m_hasFired && !m_isAutomatic) || m_reloading || !HasAmmo()) {
			return false;
		}
		m_firing = true;
		m_hasFired = true;
		DecrementAmmo();
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
			target.ShotBy(m_player == null ? gameObject : m_player.gameObject, m_damage);
		}
		
		Destroy(bullet);
		
		if(m_shotDelay > 0) {
			yield return new WaitForSeconds(m_shotDelay);
		}
		
		m_firing = false;
	}
	
	IEnumerator Reload() {
		if(m_reloading) {
			yield break;
		}
		m_reloading = true;
		transform.localPosition = m_reloadPosition;
		yield return new WaitForSeconds(0.33f);
		m_player.m_inventory.ReloadCurrentGun();
		transform.localPosition = m_defaultPosition;
		m_reloading = false;
	}
	
	bool HasAmmo() {
		return m_currentAmmo > 0;
	}
	
	void DecrementAmmo() {
		m_currentAmmo--;
		if(m_currentAmmo < 0) {
			m_currentAmmo = 0;
		}
	}
}
