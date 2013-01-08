using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour {
	
	public GameObject m_messageTarget;
	
	public void ShotBy(Player p) {
		m_messageTarget.SendMessage("OnShotBy", p, SendMessageOptions.RequireReceiver);
	}
}
