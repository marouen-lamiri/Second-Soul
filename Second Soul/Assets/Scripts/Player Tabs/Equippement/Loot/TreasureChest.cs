using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {

	public AnimationClip openClip;
	public Animator animator;

	// Use this for initialization
	void Start () {
		animation.Play (openClip.name);
		animator.Play("treasurechest_openAvatar");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
