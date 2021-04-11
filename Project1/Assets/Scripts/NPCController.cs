using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    //private Animator anim;
    public Transform homePos;
    public float speed;

    public float tempoEspera;
    float tempo;
    public Vector2 maxiPosition;
    public Vector2 miniPosition;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        tempo = tempoEspera;
    }

    // Update is called once per frame
    void Update()
    {
        GoHome();
    }

    void GoHome()
    {
        //anim.SetFloat("Horizontal", (homePos.position.x - transform.position.x));
        //anim.SetFloat("Vertical", (homePos.position.y - transform.position.y));
        
        transform.position = Vector3.MoveTowards(transform.position, homePos.transform.position, speed * Time.deltaTime);
            
        if(Vector2.Distance(transform.position, homePos.position) == 0)
        {
            if(tempo <= 0)
            {
                //anim.SetBool("isRange", true);
                homePos.position = new Vector2(Random.Range(maxiPosition.x, miniPosition.x), Random.Range(maxiPosition.y, miniPosition.y));
                tempo = tempoEspera;
            }
            else
            { 
                //anim.SetBool("isRange", false);
                tempo -= Time.deltaTime;
            }
        }
    }
}
