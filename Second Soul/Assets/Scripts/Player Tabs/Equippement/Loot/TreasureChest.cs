using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {

	Animator animator;
	
	public Player player;
	LootFactory lootFactory;
	string openedAnimator;
	private bool opened;

	// Use this for initialization
	void Start () {
		opened = false;
		findEnabledPlayer();
		lootFactory = GameObject.FindObjectOfType<LootFactory> ();
		animator = GetComponent<Animator> ();
		openedAnimator = "opened";
		//animation.Play (openClip.name);
		//animator.Play("treasurechest_openAvatar");
		//player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("player is " + player);
	}
	
	private void findEnabledPlayer(){
		Player[] players = (Player[]) GameObject.FindObjectsOfType(typeof(Player));
		foreach(Player p in players){
			if(p.enabled){
				player = p;
			}
		}
	}
	
	public void openChest(){
		if(!opened){
			int itemsToDrop = Random.Range(1,3);
			for(int i = 0; i < itemsToDrop; i++){
				lootFactory.determineDrop(1, transform.position);
			}
			
			opened = true;
			animator.SetBool (openedAnimator, opened);
		}
		//Destroy (gameObject);
	}
	
	void OnMouseDrag(){
		player.treasureChest = this;
	}
	
	void OnMouseUp(){
		player.treasureChest = null;
	}	
	
	void OnMouseOver(){
		player.treasureChest = this;
	}
	
	void OnMouseExit(){
		player.treasureChest = null;
	}
}
