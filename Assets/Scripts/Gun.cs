using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Player m_player;
	public Transform m_muzzleExit;
	public GameObject m_bulletPrefab;
	
	// Update is called once per frame
	void Update () {
		CheckForFire();
	}
	
	void CheckForFire() {
		if(Input.GetMouseButtonDown(0)) {
			StartCoroutine(Fire ());
		}
	}
	
	IEnumerator Fire() {
		var cam = Camera.main;
		var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
		RaycastHit rh;
		Vector3 dest;
		Shootable target = null;
		Physics.Raycast (ray, out rh, float.MaxValue);
		if(rh.collider != null) {
			dest = rh.point;
			Debug.Log (rh.collider.name);
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
	}
}
