using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager healthMan;
    public float waitToHurt = 2f;
    public bool isTouching;

    [SerializeField]
    private int damageToGive = 10;

    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if(waitToHurt <= 0)
            {
                healthMan.HurtPlayer(damageToGive);//Duas Formas de HealthManager
                waitToHurt = 1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            Vector2 difference = other.transform.position - transform.position;
            other.transform.position = new Vector2(other.transform.position.x + difference.x, other.transform.position.y + difference.y);
            other.gameObject.GetComponent<HealthManager>().HurtPlayer(damageToGive);//Duas Formas de HealthManager
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isTouching = false;
        waitToHurt = 2f;
    }
}
