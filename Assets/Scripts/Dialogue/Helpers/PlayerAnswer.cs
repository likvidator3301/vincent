using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Dialogue.Helpers
{
	[System.Serializable]
	public class PlayerAnswer
	{
		public string text;
		public int toNode;
		public bool exit;
	}
}