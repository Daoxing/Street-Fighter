using UnityEngine;
using System.Collections;

public class BannerController : MonoBehaviour {

	private Animator animator;
	private AudioSource audioPlayer;

	private bool animating=true;
	// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
		audioPlayer=GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playVoice(AudioClip voice){
		GameUtils.playSound (voice,audioPlayer);
	}

	public void showRoundFight(){
		animating = true;
		animator.SetTrigger ("SHOW_ROUNDONE_BANNER");
	}

	public void showYouLose(){
		animating = true;
		animator.SetTrigger ("SHOW_YOU_LOSE");
	}

	public void showYouWin(){
		animating = true;
		animator.SetTrigger ("SHOW_YOU_WIN");
	}

	public void animationEnded(){
		animating = false;
	}

	public bool isAnimating{
		get{
			return animating;
		}
			
	}
}
