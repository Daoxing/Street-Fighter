using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour {
	public int roundTime=10;
	private float lastTimeUpdate=0;

	public BannerController banner;
	private bool gameStarted=false;
	private bool gameEnded=false;


	public Fighter player1;
	public Fighter player2;


	public AudioSource musicPlayer;
	public AudioClip backgroundMusic;
	// Use this for initialization
	void Start () {
		banner.showRoundFight ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameStarted&&!banner.isAnimating){
			gameStarted=true;
			player1.enable=true;
			player2.enable=true;
			GameUtils.playSound(backgroundMusic,musicPlayer);
		}
		if (!gameEnded && gameStarted) {
			if (roundTime > 0 && Time.time - lastTimeUpdate > 1) {
				roundTime--;
				lastTimeUpdate = Time.time;
				if(roundTime<=0){
					player1.enable=false;
					player2.enable=false;
					if (player1.healthPercent >player2.healthPercent) {
						banner.showYouWin ();
						gameEnded = true;
					} else if (player1.healthPercent <player2.healthPercent) {
						banner.showYouLose ();
						gameEnded = true;
					}
				}
				if(roundTime==0){
					expireTime();
				}
			}

			if (player1.healthPercent <= 0) {
				banner.showYouLose ();
				gameEnded = true;
				player1.enable=false;
				player2.enable=false;
			} else if (player2.healthPercent <= 0) {
				banner.showYouWin ();
				gameEnded = true;
				player1.enable=false;
				player2.enable=false;
			}
		}
	}

	private void expireTime(){
		if(player1.healthPercent>player2.healthPercent){
			player2.health=0;
		}else{
			player1.health=0;
		}
	}
}
