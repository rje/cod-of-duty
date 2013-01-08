using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour {
	
	public void ShotBy(Player p) {
		Die();
	}
	
	void Die() {
		Destroy (gameObject);
	}
}
