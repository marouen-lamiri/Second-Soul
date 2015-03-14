using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T: IHeapItem<T>  {

	T[] list;
	int currentCount;

	public Heap(int maxSize){
		list = new T[maxSize];
	}

	public void Add(T item){
		item.HeapIndex = currentCount;
		list[currentCount] = item;
		sortUpwards(item);
		currentCount++;
	}

	public T removeFirst(){
		T firstItem = list[0];
		currentCount--;
		list[0] = list[currentCount];
		list[0].HeapIndex = 0;
		sortDownwards(list[0]);
		return firstItem;
	}

	public void UpdateItem(T item){
		sortUpwards(item);
	}

	public int Count {
		get {
			return currentCount;
		}
	}

	public bool Contains(T item){
		return Equals (list[item.HeapIndex], item);
	}

	void sortDownwards(T item){
		while (true) {
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;
			
			if (childIndexLeft < currentCount) {
				swapIndex = childIndexLeft;
				
				if (childIndexRight < currentCount) {
					if (list[childIndexLeft].CompareTo(list[childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}
				
				if (item.CompareTo(list[swapIndex]) < 0) {
					swap (item,list[swapIndex]);
				}
				else {
					return;
				}
				
			}
			else {
				return;
			}
		}
	}

	void sortUpwards(T item){
		int parentIndex = (item.HeapIndex-1)/2; //parent index = (n-1)/2
		while(true){
			T parentItem = list[parentIndex];
			if(item.CompareTo(parentItem) > 0){
				swap (item, parentItem);
			}
			else{
				break;
			}
			parentIndex = (item.HeapIndex-1)/2;
		}
	}

	void swap (T itemA, T itemB){
		list[itemA.HeapIndex] = itemB;
		list[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

public interface IHeapItem<T> : IComparable<T>{
	int HeapIndex {
		get;
		set;
	}
}