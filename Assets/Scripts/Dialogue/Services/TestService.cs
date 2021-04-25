using UnityEngine;
using System.Collections;
using Assets.Scripts.Dialogue.Helpers;

public class TestService : MonoBehaviour
{

	// пример, получения имени файла диалога и запуска процесса

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				DialogueTrigger tr = hit.transform.GetComponent<DialogueTrigger>();
				if (tr != null && tr.fileName != string.Empty)
				{
					DialogueManager.Internal.DialogueStart(tr.fileName);
				}
			}
		}
	}
}