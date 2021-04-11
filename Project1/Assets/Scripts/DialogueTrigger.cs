using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogBox;

    public Dialogue dialogue;

    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(dialogBox.activeInHierarchy)
            {
                FindObjectOfType<DialogueController>().DisplayNextSentence();
            }
            else
            {
                FindObjectOfType<DialogueController>().StartDialogue(dialogue);
                dialogBox.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interaction"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interaction"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
