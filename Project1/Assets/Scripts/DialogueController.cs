using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    //
    private Queue<string> names;
    //

    private Queue<string> sentences;

    public GameObject dialogBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;

    //
    string nan;
    //

    string sentence;

    float fadeTime = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        //
        names = new Queue<string>();
        //

        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //nameText.text = dialogue.name;

        //
        names.Clear();
        foreach (string nan in dialogue.name)
        {
            names.Enqueue(nan);
        }
        //

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //
        if(sentences.Count == 0 && names.Count == 0)
        {
            EndDialogue();
            return;
        }
        nan = names.Dequeue();
        nameText.text = nan;
        //

        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(fadeTime);
        }
    }

    public void EndDialogue()
    {
        dialogBox.SetActive(false);
    }
}
