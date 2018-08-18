using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour {

	int num;
	int[] nums;

	Int number;

	string str;

	// Use this for initialization
	void Start () {
		num = 0;
		nums = new int[3] {1,1,2};
		number = new Int (5);
		str = "A";
		for (int i = 0; i < nums.Length; i++) {
			Debug.Log (nums[i]);
		}
		AddOne (num);
		AddOne (nums);
		AddOne (number);
		AddOne (str);
		Debug.Log (num);
		for (int i = 0; i < nums.Length; i++) {
			Debug.Log (nums[i]);
		}
		Debug.Log (number.i);
		Debug.Log (str);
	}

	void AddOne(int i){
		i = i + 1;
	}

	void AddOne(int[] nums){
		for (int i = 0; i < nums.Length; i++) {
			nums [i] = nums [i] + 1;
		}
	}

	void AddOne(Int i){
		i.i = i.i + 1;
	}

	void AddOne(string str){
		str = str + "1";
	}

	// Update is called once per frame
	void Update () {
	
	}
}

public class Int{
	public int i;
	public Int(int i){
		this.i = i;
	}
}
