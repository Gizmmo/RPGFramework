using UnityEngine;
using System.Collections.Generic;
using System;

public class MessagingManager : MonoBehaviour {

	public ScriptingObjects MyWaypoints;

	//Static singleton property
	public static MessagingManager Instance { 
		get;
		private set;
	}

	//public property for manager
	private List<Action> subscribers = new List<Action>();

	void Awake() {
		Debug.Log ("Messaging Manager Started");

		//First check if there are any other conflicting instances
		if(Instance != null && Instance != this) {
			//Destroy other instances if its not the same
			Destroy (gameObject);
		}

		//Save our current singleton instance
		Instance = this;

		//Make sure that the instance is not destroyed between scenes
		DontDestroyOnLoad(gameObject);
	}

	//The Subscribe methods for manager
	public void Subscribe(Action subscriber) {
		Debug.Log ("Subscriber registered");
		subscribers.Add (subscriber);
	}

	//The Unsubscribe method for manager
	public void UnSubscribe(Action subscriber) {
		Debug.Log ("Subscriber unregistered");
		subscribers.Remove (subscriber);
	}

	//Clear subscribers method for manager
	public void ClearAllSubscribers () {
		subscribers.Clear ();
	}

	public void Broadcast() {
		Debug.Log("Broadcast requested, No of Subscribers = " + subscribers.Count);
		foreach (Action subscriber in subscribers) {
			subscriber();
		}
	}
}
