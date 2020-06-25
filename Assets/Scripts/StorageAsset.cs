using UnityEngine;
using UnityEditor;

public class StorageAsset
{
	[MenuItem("Assets/Create/Levels")]
	public static Storage CreateAsset()
	{
		return ScriptableObjectUtility.CreateAsset<Storage>();
	}
}