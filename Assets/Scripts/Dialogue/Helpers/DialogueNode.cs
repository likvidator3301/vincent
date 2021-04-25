using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Dialogue.Helpers
{
	[System.Serializable]
	public class DialogueNode
	{
		public string npcText;
		public PlayerAnswer[] playerAnswer;
	}
}