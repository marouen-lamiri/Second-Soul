using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class LootFactory : MonoBehaviour {
	
	public static Player player;
	public static ItemHolder itemHolderPrefab;
	
	private static Dictionary<string, LootWeights> items;
	private static int weightSum;
	
	//Item i = System.Activator.CreateInstance(System.Type.GetType("Chest"));
	
	//Item item= new Item();
	//item =  createType("chest", item);
	
	
	/*item createType(str s, Item){
	object result = Convert.ChangeType(item, Type.GetType("System.Int32"))
	item.init();
		return new (s);
	}*/
	// Use this for initialization
	void Start () {
		items = new Dictionary<string, LootWeights>();
		initializeItemsAndWeights();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setFactoryVariables(ItemHolder i, Player p){
		itemHolderPrefab = i;
		player = p;
	}
	
	void initializeItemsAndWeights(){
		weightSum = 0;
		addItemToDict("Chest", 3);
		addItemToDict("Axe", 3);
		addItemToDict("Ring", 3);
	}
	
	void addItemToDict(string type, int weight){
		weightSum += weight;
		items.Add(type, new LootWeights(weight, weightSum));
	}
	
	public static void determineDrop(float dropRate, Vector3 enemyPostion){
		int dropRoll = Random.Range(1,100);
		
		if(dropRoll <= (int)(dropRate * 100)){
			string itemType = "";
			
			Vector3 dropPosition = enemyPostion;
			int xRoll = Random.Range(-100, 100);
			int yRoll = Random.Range(-100, 100);
			dropPosition.x += (xRoll / 100);
			dropPosition.y += (yRoll / 100);
			
			int itemRoll = Random.Range(1, weightSum);
			Debug.Log ("item roll is: " + itemRoll);
			
			foreach(KeyValuePair<string, LootWeights> item in items)
			{
				itemType = item.Key;
				LootWeights itemWeight = item.Value;
				Debug.Log (item.Key + ":" + itemWeight.weight + ":" + itemWeight.csum);
				if (itemRoll <= itemWeight.csum){
					break;
				}
			}
			ItemHolder itemHolder = Instantiate(itemHolderPrefab, dropPosition, Quaternion.identity)as ItemHolder;
			itemHolder.item = (Item) System.Activator.CreateInstance(System.Type.GetType(itemType));
		}
	}

}
