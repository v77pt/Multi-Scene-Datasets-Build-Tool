using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableReader<T, U> : Singleton<U>, ITableManager where T : ITableItem
{
	// abstract functions need tobe implements.
	public abstract ETableType TableType();
	public abstract string TableName();
	public object TableData { get { return mItemArray; } }

	// the data arrays.
	T[] mItemArray;
	Dictionary<int, int> mKeyItemMap = new Dictionary<int, int>();

	// constructor.
	internal TableReader()
	{
		
	}

	// constructor.
	public void ReadFromFile(string Path)
	{
		// load from excel txt file.
		mItemArray = TableParser.Parse<T>(Path,TableType());

		// build the key-value map.
		for (int i = 0; i < mItemArray.Length; i++)
			mKeyItemMap[mItemArray[i].Key()] = i;
	}

	// get a item base the key.
	public T GetItem(int key)
	{
		int itemIndex;
		if (mKeyItemMap.TryGetValue(key, out itemIndex))
			return mItemArray[itemIndex];
		return default(T);
	}

	// get the item array.
	public T[] GetAllItem()
	{
		return mItemArray;
	}
}
