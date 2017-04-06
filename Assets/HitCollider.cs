using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {
	public string PunchName;
	public Fighter owner;
	public float damage;
	void OnTriggerEnter(Collider other){
		Fighter somebody=other.gameObject.GetComponent<Fighter>();
		if (owner.attacking) {
			if (somebody != null && somebody != owner) {
				somebody.hurt (damage);
			}
		}
	}
}
