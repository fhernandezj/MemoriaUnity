using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CardScript : NetworkBehaviour
{
	public Material myMaterial, empty;
	public GameObject main;
	public bool canFlip = true;

	[SyncVar]
	public int coloreame = 0;

	void Update()
	{		

		if (coloreame == 1) {
			GetComponent<Renderer> ().material = myMaterial;
		} else {
			GetComponent<Renderer> ().material = empty;
		}
	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	public void CmdPaint()
	{
		if (coloreame == 1) {
			coloreame = 0;
		} else {
			coloreame = 1;
		}

		/*var card = gameObject;
		card.GetComponent<Renderer>().material = myMaterial;

		NetworkServer.Spawn (card);*/
	}

	void OnMouseDown(){
		Debug.Log (isClient);
		Debug.Log (isServer);

		if(!canFlip){
			return;
		}

		if (main.GetComponent<MainScript> ().jugador == 0 && isClient && !isServer) {//Host
			//return;
		}

		if (main.GetComponent<MainScript> ().jugador == 1 && isServer && isClient) {//Client
			//return;
		}

		if(main.GetComponent<MainScript> ().canOpen){
			coloreame = 1;
			main.GetComponent<MainScript> ().openCard (gameObject.name, myMaterial.name);
		}
	}   

}