using UnityEngine;
using System.Xml;

namespace Assets.Scripts.Dialogue.Helpers
{
	public class DialogueGenerator : MonoBehaviour
	{
		public string fileName = "Example";
		public DialogueNode[] node;

		public void Generate()
		{
			var path = @"C:\Users\Виктор Капкаев\Documents\GitHub\vincent\Assets\Scripts\Dialogue\Resources\" + fileName + ".xml";
			XmlNode userNode;
			XmlElement element;

			var xmlDoc = new XmlDocument();
			var rootNode = xmlDoc.CreateElement("dialogue");
			var attribute = xmlDoc.CreateAttribute("name");
			attribute.Value = fileName;
			rootNode.Attributes.Append(attribute);
			xmlDoc.AppendChild(rootNode);

			for (int j = 0; j < node.Length; j++)
			{
				userNode = xmlDoc.CreateElement("node");
				attribute = xmlDoc.CreateAttribute("id");
				attribute.Value = j.ToString();
				userNode.Attributes.Append(attribute);
				attribute = xmlDoc.CreateAttribute("npcText");
				attribute.Value = node[j].npcText;
				userNode.Attributes.Append(attribute);

				for (int i = 0; i < node[j].playerAnswer.Length; i++)
				{
					element = xmlDoc.CreateElement("answer");
					element.SetAttribute("text", node[j].playerAnswer[i].text);
					if (node[j].playerAnswer[i].toNode > 0) element.SetAttribute("toNode", node[j].playerAnswer[i].toNode.ToString());
					if (node[j].playerAnswer[i].exit) element.SetAttribute("exit", node[j].playerAnswer[i].exit.ToString());
					userNode.AppendChild(element);
				}

				rootNode.AppendChild(userNode);
			}

			xmlDoc.Save(path);
			Debug.Log(this + " Создан XML файл диалога [ " + fileName + " ] по адресу: " + path);
		}
	}
}