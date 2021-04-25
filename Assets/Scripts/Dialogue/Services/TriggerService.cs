using Assets.Scripts.Common;
using Assets.Scripts.Dialogue.Helpers;
using UnityEngine;

namespace Assets.Scripts.Dialogue.Services
{
    public class TriggerService: ServiceBase
    {
        public TriggerService()
        {
            
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    DialogueTrigger trigger = hit.transform.GetComponent<DialogueTrigger>();
                    if (trigger != null && trigger.fileName != string.Empty)
                    {
                        DialogueManager.Internal.DialogueStart(trigger.fileName);
                    }
                }
            }
        }
    }
}
