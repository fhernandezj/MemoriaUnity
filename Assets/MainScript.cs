using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainScript : NetworkBehaviour {

	[SyncVar]
	public string carta1, carta2;
	private string sprite1, sprite2;
	[SyncVar]
	public bool canOpen;
	[SyncVar]
	public int puntosHost, puntosClient;
	[SyncVar]
	public int jugador;
	public UnityEngine.UI.Text turno, ph, pc;

	// Use this for initialization
	void Start () {
		canOpen = true;
		carta1 = "";
		carta2 = "";
		sprite1 = "";
		sprite2 = "";
		puntosHost = 0;
		puntosClient = 0;
		jugador = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (puntosHost + puntosClient == 8) {			
			if (puntosHost == puntosClient) {
				turno.text = "Empate!";
			} else {
				if (puntosHost > puntosClient) {
					turno.text = "Gana Host!";
				} else {
					turno.text = "Gana Client!";
				}
			}
		} else {
			if (jugador == 0) {
				turno.text = "Turno de: Host";
			} else {
				turno.text = "Turno de: Client";
			}
		}
		ph.text = "Puntaje Host: " + puntosHost;
		pc.text = "Puntaje Client: " + puntosClient;
	}

	public void openCard(string carta, string sprite){
		if (carta1 == "") {
			carta1 = carta;
			sprite1 = sprite;
		} else {
			carta2 = carta;
			sprite2 = sprite;
			canOpen = false;

			if (sprite1 == sprite2) {
				if (jugador == 0) {//Host
					puntosHost++;
				} else {//Client
					puntosClient++;
				}
				GameObject.Find (carta1).GetComponent<CardScript> ().canFlip = false;
				GameObject.Find (carta2).GetComponent<CardScript> ().canFlip = false;
				resetParams ();
			} else {
				StartCoroutine (ResetCards());
			}

		}
	}

	void resetParams(){
		canOpen = true;
		carta1 = "";
		carta2 = "";
		sprite1 = "";
		sprite2 = "";
		if (jugador == 0) {
			jugador = 1;
		} else {
			jugador = 0;
		}
	}


	IEnumerator ResetCards(){
		yield return new WaitForSeconds (1);
		GameObject.Find (carta1).GetComponent<CardScript> ().coloreame = 0;
		GameObject.Find (carta2).GetComponent<CardScript> ().coloreame = 0;
		resetParams ();
	}
}
