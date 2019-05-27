using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public float distance;

	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		distance = Vector3.Distance(player1.transform.position,player2.transform.position);
	}
}
