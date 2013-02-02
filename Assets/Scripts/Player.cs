using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float m_speed;
	public float m_jumpForce;
	public Transform m_head;
	
	public float m_headMin;
	public float m_headMax;
	
	public Inventory m_inventory;
	
	public float m_mouseSensitivity;
	
	public bool m_pauseInput;
	
	float m_currentHeadAngle;
	float m_sinceLastShot;
	
	public int m_maxHP;
	public int m_hp;
	
	public HealthEffect m_health;
	public bool m_isDead = false;
	
	public bool m_isGrounded = true;
	public bool m_leftGround = false;
	
	public AudioSource m_source;
	public AudioClip m_hurtClip;
	
	HUD m_hud;

	// Use this for initialization
	void Start () {
		m_currentHeadAngle = 0;
		m_hud = GameObject.FindGameObjectWithTag("hud").GetComponent<HUD>();
		m_hp = m_maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_pauseInput || m_isDead) {
			return;
		}
		UpdatePosition();
		CheckGunChange();
		CheckForReload();
		CheckForFire();
		UpdateHealth();
		
		var delta = new Vector3(Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"), 0);
		UpdateRotation(delta);
		UpdateHeadRotation(delta);
		UpdateAmmo();
	}
	
	
	void UpdateHealth() {
		m_sinceLastShot += Time.deltaTime;
		if(m_sinceLastShot > 3 && m_hp < m_maxHP) {
			m_hp += 1;
			
		}
		m_health.SetHealthPerc((float)m_hp / (float)m_maxHP);
	}
	
	void CheckForReload() {
		if(Input.GetKeyDown(KeyCode.R)) {
			m_inventory.GetCurrentGun().m_gun.TryToReload();
		}
	}
	
	void CheckForFire() {
		if(Input.GetButtonDown("Fire1") || Input.GetAxis ("Fire1") > 0.5f) {
			var cam = Camera.main;
			var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
			m_inventory.GetCurrentGun().m_gun.TryToFire(ray);
		}
		else {
			m_inventory.GetCurrentGun().m_gun.m_hasFired = false;
		}
	}
	
	void CheckGunChange() {
		if(Input.GetKeyDown (KeyCode.Alpha1)) {
			m_inventory.PrevGun();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			m_inventory.NextGun();
		}
	}
	
	void UpdateAmmo() {
		var gun = m_inventory.GetCurrentGun();
		m_hud.SetAmmoLabel(gun.GetLoadedAmmo(), gun.GetInventoryAmmo());
	}
	
	void UpdatePosition() {
		bool isMoving = false;
		var strafe = Input.GetAxisRaw ("Horizontal");
		var forward = Input.GetAxisRaw ("Vertical");
		var moveVec = (strafe * transform.right) + (forward * transform.forward);
		moveVec.Normalize();
		moveVec = moveVec * m_speed * Time.deltaTime;
		if(moveVec.magnitude > 0.01f) {
			isMoving = true;
		}
		
		if(isMoving && !m_source.isPlaying) {
			m_source.Play ();
		}
		else if(!isMoving && m_source.isPlaying) {
			m_source.Stop ();
		}
		
		transform.position += moveVec;
		
		if(Input.GetButtonDown ("Jump") && m_isGrounded && !m_leftGround) {
			m_isGrounded = false;
			m_leftGround = false;
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
		float invertY = PlayerPrefs.GetInt("y-axis-invert");
		var headAngles = m_head.localRotation.eulerAngles;
		m_currentHeadAngle -= invertY * delta.y / m_mouseSensitivity;
		if(m_currentHeadAngle < m_headMin) {
			m_currentHeadAngle = m_headMin;
		}
		else if(m_currentHeadAngle > m_headMax) {
			m_currentHeadAngle = m_headMax;
		}
		headAngles.x = m_currentHeadAngle;
		m_head.localRotation = Quaternion.Euler(headAngles);
	}
	
	void OnShotBy(ShotInfo si) {
		m_hp -= si.m_damage;
		m_hp = Mathf.Clamp (m_hp, 0, m_maxHP);
		m_sinceLastShot = 0;
		AudioSource.PlayClipAtPoint(m_hurtClip, transform.position);
		if(m_hp == 0 && !m_isDead) {
			Die(si.m_shooter);
		}
	}
	
	void Die(GameObject killer) {
		transform.LookAt(killer.transform.position);
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.AddForce((transform.position - killer.transform.position).normalized * 40.0f);
		m_isDead = true;
		StartCoroutine(FreezeAfterDelay(1.0f));
	}
	
	IEnumerator FreezeAfterDelay(float delay) {
		yield return new WaitForSeconds(delay);
		rigidbody.Sleep();
	}
	
	void OnCollisionStay(Collision c) {
		if(m_leftGround) {
			m_isGrounded = true;
			m_leftGround = false;
		}
	}
	
	void OnCollisionExit(Collision c) {
		m_leftGround = true;
	}
	
	public void UnpauseAfterDelay(float delay) {
		StartCoroutine(DelayedUnpause(delay));
	}
	
	IEnumerator DelayedUnpause(float delay) {
		yield return new WaitForSeconds(delay);
		m_pauseInput = false;
	}
}
