using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject nameObject;
    public GameObject bodyObject;
    public Animator animator;
 

    void Start(){
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameObject.GetComponent<TextMeshProUGUI>().text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0){
            if (!GameData.gameRunning)
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            else { 
                EndDialogue();
                return;
            }
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of dialogue");
    }

    IEnumerator TypeSentece(string sentence)
    {
        bodyObject.GetComponent<TextMeshProUGUI>().text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            bodyObject.GetComponent<TextMeshProUGUI>().text += letter;
            yield return null;
        }
    }

}
