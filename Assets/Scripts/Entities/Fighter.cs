using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {
	public enum PlayerType{
		HUMAN,AI
	}
	public static float MAX_HEALTH=100f;
	public float health=MAX_HEALTH;
	public string fighterName;
	public Fighter oponent;
	public PlayerType player;
	public FighterStates currentState;
	public bool enable=false;
	protected Animator animator;
	private Rigidbody myBody;
	private AudioSource audioplayer;

	private float random;
	private float randomSetTime;
	// Use this for initialization
	void Start () {
		myBody=GetComponent<Rigidbody>();
		animator = GetComponent<Animator> ();
		audioplayer=GetComponent<AudioSource> ();
	
	}

	public void playSound(AudioClip sound){
		GameUtils.playSound (sound,audioplayer);
	}

	void UpdateHumanInput(){
		if (Input.GetAxis ("Horizontal") > 0.1) {
			animator.SetBool ("WALK", true);
		} else {
			animator.SetBool ("WALK", false);
		}

		if (Input.GetAxis ("Horizontal") <- 0.1) {
			if(oponent.attacking){
				animator.SetBool("WALK_BACK",false);
				animator.SetBool("DEFEND",true);
			}else{
				animator.SetBool("WALK_BACK",true);
				animator.SetBool("DEFEND",false);
			}
		} else {
			animator.SetBool ("WALK_BACK", false);
		}

		if (Input.GetAxis ("Vertical") <-0.1) {
			animator.SetBool ("DUCK", true);
		} else {
			animator.SetBool ("DUCK", false);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			animator.SetTrigger("JUMP");
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("space");
			animator.SetTrigger("PUNCH");
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			animator.SetTrigger("KICK");
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			animator.SetTrigger("HADOKEN");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(enable){
			if(player==PlayerType.HUMAN){
				UpdateHumanInput ();
			}else{
				UpdateAiInput();
			}
		}
	
		if (oponent != null) {
			animator = GetComponent<Animator> ();
			Debug.LogError("SETTING NON OPONENT HEALTH:  "+healthPercent);
			animator.SetFloat ("oponent_health", healthPercent);
		} else {
			Debug.LogError("SETTING NON OPONENT HEALTH");
			animator.SetFloat ("oponent_health", 1.0f);
		}

		if(health<=0&&currentState!=FighterStates.DEAD){
			animator.SetTrigger("DEAD");
		}
	
	}

	public void UpdateAiInput(){
		animator.SetBool ("defending",defending);
		animator.SetBool ("invulnerable",invulnerable);
		animator.SetBool ("enable",enable);
		animator.SetBool ("oponent_attacking",oponent.attacking);

		animator.SetFloat ("distanceToOponent",getDistanceToOponent());

		if(Time.time-randomSetTime>1){
			random=Random.value;
			randomSetTime=Time.time;
		}
		Debug.Log ("RANDOM RESULT: "+random);
		animator.SetFloat ("random",random);
	}


	public float getDistanceToOponent(){
		return Mathf.Abs (transform.position.x-oponent.transform.position.x);
	}

	public float healthPercent{
		get{
			return health/MAX_HEALTH;
		}
	}

	public Rigidbody body{
		get{
			return this.myBody;
		}
	}
	public virtual void hurt(float damage){
		//if(!invulnerable){
		if(defending){
			damage*=0.2f;
		}
			if(health>=damage){
				health-=damage;
			}else{
				health=0;
			}
			if(health>0){
				animator.SetTrigger("TAKE_HIT");
			}
		//}
	}

	public bool invulnerable{
		get{
			return currentState==FighterStates.TAKE_HIT
				||currentState==FighterStates.TAKE_HIT_DEFEND
					||currentState==FighterStates.DEAD;
		}
	}

	public bool attacking{
		get{
			return currentState==FighterStates.ATTACK;
		}
	}

	public bool defending{
		get{
			return currentState==FighterStates.DEFEND
				||currentState==FighterStates.TAKE_HIT_DEFEND;
		}
	}


}
