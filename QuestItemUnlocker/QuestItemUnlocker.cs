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
				// The family crest cannot be discarded so only replenish it if the quest is not completed and they don't already have it
				if (!GameManager.Instance.QuestManager.GetIsQuestStepCompleted("Hermitage_ReturnCrest") && !HasItem("quest-crest"))
				{
					ReplenishStock("Family Crest");
				}

				// These can be discarded too
				ReplenishStock("Dog Tag");

				// Relics can be discarded so we can replenish them all without potentially clogging the players inventory
				ReplenishStock("Relic");
			}
		}

		private void ReplenishStock(string name)
		{
			foreach (var dredgePOI in GameObject.FindObjectsOfType<HarvestPOI>()
			   .Where(x => x.harvestable is HarvestPOIDataModel poi && poi.items
			   .Any(x => x.name.Contains(name))))
			{
				if (dredgePOI.Stock < 1)
				{
					dredgePOI.AddStock(1, false);
				}
			}
		}

		private bool HasItem(string id)
		{
			var inInventory = GameManager.Instance.SaveData.Inventory.spatialItems.FirstOrDefault(x => x.id == id) != null;
			var inStorage = GameManager.Instance.SaveData.Storage.spatialItems.FirstOrDefault(x => x.id == id) != null;
			return inInventory || inStorage;
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
