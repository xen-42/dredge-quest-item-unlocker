using UnityEngine;

namespace QuestItemUnlocker
{
	public class Loader
	{
		/// <summary>
		/// This method is run by Winch to initialize your mod
		/// </summary>
		public static void Initialize()
		{
			var gameObject = new GameObject(nameof(QuestItemUnlocker));
			gameObject.AddComponent<QuestItemUnlocker>();
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}