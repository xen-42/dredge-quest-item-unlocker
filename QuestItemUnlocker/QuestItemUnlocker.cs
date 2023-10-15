using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Winch.Core;

namespace QuestItemUnlocker
{
	public class QuestItemUnlocker : MonoBehaviour
	{
		public void Awake()
		{
			WinchCore.Log.Debug($"{nameof(QuestItemUnlocker)} has loaded!");
			SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		}

		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			if (arg1.name == "Game")
			{
				foreach (var relicPOI in GameObject.FindObjectsOfType<HarvestPOI>().Where(x => x.harvestable is HarvestPOIDataModel poi && poi.items.Any(x => x.name.Contains("Relic"))))
				{
					relicPOI.AddStock(1, false);
				}
			}
		}

		public void Update()
		{
			// For some reason this says it gives you the item but it doesnt
			/*
			if (SceneManager.GetActiveScene().name == "Game")
			{
				for (int i = 1; i <= 5; i++)
				{
					if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), $"Alpha{i}")))
					{
						var item = $"Relic{i}";
						try
						{
							GameManager.Instance.ItemManager.AddItemById(item, GameManager.Instance.SaveData.Inventory);
							var msg = $"Added item {item}";
							WinchCore.Log.Info(msg);
							ShowNotification(NotificationType.ITEM_ADDED, msg);
						}
						catch (Exception e) 
						{
							var msg = $"Couldn't add item {item} : {e}";
							WinchCore.Log.Error(msg);
							ShowNotification(NotificationType.ERROR, msg);
						}
					}
				}
			}
			*/
		}

		public static void ShowNotificationWithColour(NotificationType notificationType, string text, string colourCode)
		{
			ShowNotification(notificationType, $"<color=#{colourCode}>{text}</color>");
		}

		public static void ShowNotification(NotificationType notificationType, string text)
		{
			GameEvents.Instance.TriggerNotification(notificationType, text);
		}
	}
}
