using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HubController : MonoBehaviour {
	public Fighter player1;
	public Fighter player2;

	public Text Player1Tag;
	public Text Player2Tag;


	public Scrollbar leftBar;
	public Scrollbar rightBar;

	public BattleController battlecontroller;
	public Text timerTag;
	// Use this for initialization
	void Start () {
		Player1Tag.text = player1.fighterName;
		Player2Tag.text = player2.fighterName;
	}
	
	// Update is called once per frame
	void Update () {
		if(leftBar.size>player1.healthPercent){
			leftBar.size-=0.01f;
		}
		if(rightBar.size>player2.healthPercent){
			rightBar.size-=0.01f;
		}

		timerTag.text = battlecontroller.roundTime.ToString ();
	
	}
}
