using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {

	Animator animator;
	
	public Player player;
	LootFactory lootFactory;
	string openedAnimator;
	string itemDropMethod;
	float itemDropDelay;
	private bool opened;

	// Use this for initialization
	void Start () {
		opened = false;
		findEnabledPlayer();
		lootFactory = GameObject.FindObjectOfType<LootFactory> ();
		animator = GetComponent<Animator> ();
		openedAnimator = "opened";
		itemDropMethod = "delayedItemDrop";
		itemDropDelay = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("player is " + player);
	}
	
	private void findEnabledPlayer(){
		Player[] players = (Player[]) GameObject.FindObjectsOfType(typeof(Player));
		foreach(Player p in players){
			if(p.playerEnabled){
				player = p;
			}
		}
	}
	
	public void openChest(){
		if(!opened){
			Invoke(itemDropMethod,itemDropDelay);		
			opened = true;
			animator.SetBool (openedAnimator, opened);
		}
		//Destroy (gameObject);
	}
	void delayedItemDrop(){
		int itemsToDrop = Random.Range(1,3);
		for(int i = 0; i < itemsToDrop; i++){
			lootFactory.determineDrop(1, transform.position);
		}
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
