using UnityEngine;
using System.Collections;

public class Level03CallInNuke : MonoBehaviour {
	
	public Player m_player;
	public GameObject m_nukeGameObject;
	public Transform m_nukeDest;
	public Transform m_nukeStart;
	public UseTrigger m_laptop;
	public ParticleEmitter m_nukesplosion;
	public AudioClip m_nukeSound;

	IEnumerator CallInNuke() {
		DisableMother();
		DisableAllDeathCod();
		m_player.m_pauseInput = true;
		m_laptop.m_finished = true;
		var nuke = (GameObject)GameObject.Instantiate(m_nukeGameObject);
		nuke.transform.position = m_nukeStart.position;
		var startingPos = nuke.transform.position;
		var percPerSecond = 1.0f / 8.0f;
		var time = 0.0f;
		while(time < 8.0f) {
			time += Time.deltaTime;
			var perc = time * percPerSecond;
			nuke.transform.position = Vector3.Lerp (startingPos, m_nukeDest.position, perc);
			m_player.transform.LookAt(nuke.transform.position);
			yield return null;
		}
		ExplodeNuke();
		AudioSource.PlayClipAtPoint(m_nukeSound, m_nukeDest.position);
		Destroy (nuke);
		yield return new WaitForSeconds(1.0f);
		m_player.m_isDead = true;
	}
	
	void DisableMother() {
		var mother = (MotherCod)GameObject.FindObjectOfType(typeof(MotherCod));
		mother.m_stopAttacking = true;
	}
	
	void DisableAllDeathCod() {
		var deathCods = (DeathCod[])GameObject.FindObjectsOfType(typeof(DeathCod));
		foreach(var cod in deathCods) {
			cod.m_stopAttacking = true;
		}
	}
	
	void ExplodeNuke() {
		m_nukesplosion.emit = true;
	}
}
