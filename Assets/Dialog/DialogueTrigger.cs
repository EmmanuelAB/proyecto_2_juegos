using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Dialogue d = new Dialogue();
        d.name = "Hi peter";
        d.sentences = new string[] {
            "hola pedro ",
            "klalsdknaksdn ",
            "kjaskdmaasdsd ",

        };
        FindObjectOfType<DialogueManager>().StartDialogue(d);
    }
}
