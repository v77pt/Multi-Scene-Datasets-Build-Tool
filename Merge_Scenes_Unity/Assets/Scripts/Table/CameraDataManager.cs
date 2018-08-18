using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Table
{
	//副本信息

	public class CameraData : ITableItem {
		
		public int CameraID;
		public string FileName;
		public float X;
		public float Y;
		public float Z;
		public float W;
		public float P;
		public float Q;
		public float R;

		public double FocalLength;
		public double RadialDistortion;

		public int Key () { return CameraID; }	
	}

	public class CameraDataManager : TableManager<CameraData, CameraDataManager>
	{
		public override ETableType TableType ()
		{
			return ETableType.CameraData;
		}

		public override string TableName() { return "CameraData"; }
	}

	public class CameraLabel : ITableItem {

		public int CameraID;
		public string FileName;
		public float X;
		public float Y;
		public float Z;
		public float W;
		public float P;
		public float Q;
		public float R;
		public float S;

		public int Key () { return CameraID; }	
	}

	public class CameraTableReader : TableReader<CameraLabel, CameraTableReader>{

		public override ETableType TableType ()
		{
			return ETableType.CameraData;
		}

		public override string TableName() { return "CameraTableReader"; }

		public void SaveCameraDataToCSV(Table.CameraLabel[] newLabel, string fullPath){

			Debug.Log ("Saving : " + newLabel[0].FileName);

			fullPath = "D:/UnityWorkSpace/PointCloudLibrary/Assets/Resources/Table/SavedData/" + fullPath + "_new.txt";

			System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);  
			if (!fi.Directory.Exists)  
			{  
				fi.Directory.Create();  
			}  
			System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,   
				System.IO.FileAccess.Write);  
			System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);  
			string data = "FileName\tX\tY\tZ\tW\tP\tQ\tR";  

			sw.WriteLine(data);  
			sw.WriteLine(data); 

			for (int i = 0; i < newLabel.Length; i++) //写入各行数据  
			{  
				data = "";  

				data =  String.Format("{0}\t{1:F6}\t{2:F6}\t{3:F6}\t{4:F6}\t{5:F6}\t{6:F6}\t{7:F6}",newLabel [i].FileName,newLabel [i].X,newLabel [i].Y,newLabel [i].Z,newLabel [i].W,newLabel [i].P,newLabel [i].Q,newLabel [i].R);
				
				sw.WriteLine(data);  
			}  
			sw.Close();  
			fs.Close();  
			Debug.Log ("Done.");
		}

		public void SaveTransDataToCSV(Transform obj, string fullPath){

			Debug.Log ("Saving : " + fullPath);

			Table.CameraLabel tansData = TransToCameraLable (obj);

			fullPath = "D:/UnityWorkSpace/PointCloudLibrary/Assets/Resources/Table/SavedData/" + fullPath + ".txt";

			System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);  
			if (!fi.Directory.Exists)  
			{  
				fi.Directory.Create();  
			}  
			System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,   
				System.IO.FileAccess.Write);  
			System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);  
			string data = "FileName\tX\tY\tZ\tW\tP\tQ\tR\tS";  

			sw.WriteLine(data);  
			sw.WriteLine(data); 

			data = "";  

			data =  String.Format("{0}\t{1:F6}\t{2:F6}\t{3:F6}\t{4:F6}\t{5:F6}\t{6:F6}\t{7:F6}\t{8:F6}",tansData.FileName,tansData.X,tansData.Y,tansData.Z,tansData.W,tansData.P,tansData.Q,tansData.R, tansData.S);

			sw.WriteLine(data);  

			sw.Close();  
			fs.Close();  
			Debug.Log ("Done.");
		}

		Table.CameraLabel TransToCameraLable(Transform obj){
			Table.CameraLabel tansData = new Table.CameraLabel ();
			tansData.FileName = obj.name;
			tansData.X = obj.position.x;
			tansData.Y = -obj.position.y;
			tansData.Z = obj.position.z;
			tansData.W = obj.rotation.w;
			tansData.P = obj.rotation.x;
			tansData.Q = -obj.rotation.y;
			tansData.R = obj.rotation.z;
			tansData.S = obj.localScale.x;

			return tansData;
		}

	}

}
