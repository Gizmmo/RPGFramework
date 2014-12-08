using UnityEngine;
using System.Collections;

public class MessagingClientBroadcaster : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		MessagingManager.Instance.Broadcast ();
	}
}
