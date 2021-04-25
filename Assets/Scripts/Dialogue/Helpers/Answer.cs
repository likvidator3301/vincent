using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Dialogue.Helpers
{
	[System.Serializable]
	class Answer
	{
		public string text;
		public int toNode;
		public bool exit;
	}
}