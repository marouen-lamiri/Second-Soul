using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

public class LootWeights : IComparable<LootWeights> {
	public int weight;
	public int csum;

	public LootWeights(int weight){
		this.weight = weight;
		this.csum = 0;
	}

	public LootWeights(int weight, int csum){
		this.weight = weight;
		this.csum = csum;
	}
	
	public int CompareTo(LootWeights lw)
	{
		if(lw == null)
		{
			return 1;
		}
		
		//Return the difference in power.
		return weight - lw.weight;
	}
}
