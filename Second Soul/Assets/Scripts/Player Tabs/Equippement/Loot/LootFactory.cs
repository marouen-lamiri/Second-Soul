using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class LootFactory : MonoBehaviour {
	
	public static Player player;
	public static ItemHolder itemHolderPrefab;
	
	private static Dictionary<System.Type, LootWeights> items;
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
		items = new Dictionary<System.Type, LootWeights>();
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
		addItemToDict(typeof(Chest), 3);
		addItemToDict(typeof(Axe), 3);
		addItemToDict(typeof(Ring), 3);
		addItemToDict(typeof(HealthPotion), 3);
		addItemToDict(typeof(ManaPotion), 3);
	}
	
	void addItemToDict(System.Type type, int weight){
		weightSum += weight;
		items.Add(type, new LootWeights(weight, weightSum));
	}
	
	public static void determineDrop(float dropRate, Vector3 originPostion){
		int dropRoll = Random.Range(1,100);
		
		if(dropRoll <= (int)(dropRate * 100)){
			System.Type itemType = null;
			
			Vector3 dropPosition = originPostion;
			int xRoll = Random.Range(-200, 200);
			int zRoll = Random.Range(-200, 200);
			dropPosition.x += (xRoll / 100);
			dropPosition.z += (zRoll / 100);
			
			int itemRoll = Random.Range(1, weightSum);
			Debug.Log ("item roll is: " + itemRoll);
			
			foreach(KeyValuePair<System.Type, LootWeights> item in items)
			{
				itemType = item.Key;
				LootWeights itemWeight = item.Value;
				Debug.Log (item.Key.ToString() + ":" + itemWeight.weight + ":" + itemWeight.csum);
				if (itemRoll <= itemWeight.csum){
					break;
				}
			}
			//ItemHolder itemHolder = Network.Instantiate(itemHolderPrefab, dropPosition, Quaternion.identity,8)as ItemHolder;
			ItemHolder itemHolder = Instantiate(itemHolderPrefab, dropPosition, Quaternion.identity)as ItemHolder;
			itemHolder.item = (Item) System.Activator.CreateInstance(itemType);
		}
	}

}
