using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    public Transform homePos;
    public float speed;
    [SerializeField]
    private float maxRange = 0f;
    [SerializeField]
    private float minRange = 0f;

    NavMeshAgent agent;

    public float tempoEspera;
    float tempo;
    public Vector2 maxiPosition;
    public Vector2 miniPosition;

    public GameObject shot;
    public float timeBtwShots;
    public float startTimeBtwShots;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;

        tempo = tempoEspera;
        timeBtwShots = startTimeBtwShots;

        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if(Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome();
        }

        /*if(timeBtwShots <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }*/
    }

    void FollowPlayer()
    {
        anim.SetFloat("Horizontal", (target.position.x - transform.position.x));
        anim.SetFloat("Vertical", (target.position.y - transform.position.y));
        anim.SetBool("isRange", true);
        agent.isStopped = false;

        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        agent.SetDestination(target.position);
    }

    void GoHome()
    {
        anim.SetFloat("Horizontal", (homePos.position.x - transform.position.x));
        anim.SetFloat("Vertical", (homePos.position.y - transform.position.y));

        agent.SetDestination(homePos.position);
        
        if(Vector2.Distance(transform.position, homePos.position) <= 1)
        {
            agent.isStopped = true;
            transform.position = Vector2.MoveTowards(transform.position, homePos.transform.position, Time.deltaTime);
            
            if(Vector2.Distance(transform.position, homePos.position) == 0)
            {
                if(tempo <= 0)
                {
                    anim.SetBool("isRange", true);
                    homePos.position = new Vector2(Random.Range(maxiPosition.x, miniPosition.x), Random.Range(maxiPosition.y, miniPosition.y));
                    tempo = tempoEspera;
                    agent.isStopped = false;
                }
                else
                { 
                    anim.SetBool("isRange", false);
                    tempo -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "MyWeapon")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
}