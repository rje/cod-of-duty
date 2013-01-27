using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GunType {
	None = -1,
	Pistol,
	Rifle,
	RocketLauncher
}

[System.Serializable]
public class GunInfo {
	public GunType m_type;
	public GameObject m_gunGameObject;
	public Gun m_gun;
	public int m_ammo;
	
	public int GetLoadedAmmo() {
		return m_gun.m_currentAmmo;
	}
	
	public int GetInventoryAmmo() {
		return m_ammo;
	}
	
	public void Reload() {
		var emptySlots = m_gun.m_ammoCapacity - m_gun.m_currentAmmo;
		if(m_ammo == -1) {
			m_gun.m_currentAmmo += emptySlots;
		}
		else {
			var amt = Mathf.Min (emptySlots, m_ammo);
			m_gun.m_currentAmmo += amt;
			m_ammo -= amt;
		}
	}
}

public class Inventory : MonoBehaviour {
	public List<GunInfo> m_allGuns;
	public List<GunInfo> m_activeGuns;
	public int m_currentIndex;
	
	public AudioSource m_source;
	public AudioClip m_reloadClip;
	
	void Awake() {
		ResetInventory();
	}
	
	public void GotLoot(GunType type, int ammo) {
		foreach(var gun in m_activeGuns) {
			if(gun.m_type == type) {
				if(gun.m_ammo != -1) {
					gun.m_ammo += ammo;
				}
				return;
			}
		}
		
		DisableGun (m_currentIndex);
		AddGunToList(type);
		m_currentIndex = m_activeGuns.Count - 1;
		EnableGun (m_currentIndex);
	}
	
	void AddGunToList(GunType type) {
		foreach(var gun in m_allGuns) {
			if(gun.m_type == type) {
				m_activeGuns.Add (gun);
				return;
			}
		}
	}
	
	public void ResetInventory() {
		DisableAllGuns();
		m_activeGuns.Clear ();
		m_activeGuns.Add (m_allGuns[0]);
		m_currentIndex = 0;
		EnableGun(m_currentIndex);
	}
	
	public void NextGun() {
		DisableGun(m_currentIndex);
		m_currentIndex++;
		if(m_currentIndex >= m_activeGuns.Count) {
			m_currentIndex -= m_activeGuns.Count;
		}
		EnableGun(m_currentIndex);
	}
	
	public void PrevGun() {
		DisableGun(m_currentIndex);
		m_currentIndex--;
		if(m_currentIndex < 0) {
			m_currentIndex += m_activeGuns.Count;
		}
		EnableGun(m_currentIndex);
	}
	
	void DisableAllGuns() {
		foreach(var gi in m_allGuns) {
			gi.m_gunGameObject.SetActive(false);
		}
	}
	
	void DisableGun(int idx) {
		m_activeGuns[idx].m_gunGameObject.SetActive(false);
	}
	
	void EnableGun(int idx) {
		m_source.PlayOneShot(m_reloadClip);
		m_activeGuns[idx].m_gunGameObject.SetActive (true);
		m_activeGuns[idx].m_gun.ResetChecks();
	}
	
	public GunInfo GetCurrentGun() {
		return m_activeGuns[m_currentIndex];
	}
	
	public void ReloadCurrentGun() {
		m_source.PlayOneShot(m_reloadClip);
		GetCurrentGun().Reload();
	}
}
