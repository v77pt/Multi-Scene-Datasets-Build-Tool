﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITableItem
{
    int Key();
}

public interface ITableManager
{
    string TableName();
    object TableData { get; }
}

public abstract class TableManager<T, U> : Singleton<U>, ITableManager where T : ITableItem
{
    // abstract functions need tobe implements.
	public abstract ETableType TableType();
    public abstract string TableName();
    public object TableData { get { return mItemArray; } }

    // the data arrays.
    T[] mItemArray;
    Dictionary<int, int> mKeyItemMap = new Dictionary<int, int>();

    // constructor.
    internal TableManager()
    {
        // load from excel txt file.
		mItemArray = TableParser.Parse<T>(TableName(),TableType());

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