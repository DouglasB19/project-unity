using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class AreaTransitions : MonoBehaviour
{
    private GameObject area;
    public GameObject anotherArea;

    public string name;

    private CameraController cam;
    public Vector2 newMinPos;
    public Vector2 newMaxPos;

    bool start = false;
    
    bool isFadeIn = false;

    float alpha = 0f;

    float fadeTime = 1f;

    //public Vector3 movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();   

        area = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }*/

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Animator>().enabled = false;
            other.GetComponent<PlayerController>().enabled = false;
            FadeIn();

            yield return new WaitForSeconds(fadeTime);

            other.transform.position = anotherArea.transform.GetChild(0).transform.position;
            cam.minPosition = newMinPos;
            cam.maxPosition = newMaxPos;

            FadeOut();
            other.GetComponent<Animator>().enabled = true;
            other.GetComponent<PlayerController>().enabled = true;

            StopAllCoroutines();
            StartCoroutine(area.GetComponent<TextCanvas>().ShowArea(name));
            //other.transform.position += movePlayer;
        }
    }

    void OnGUI()
    {
        if(!start)
        {
            return;
        }
        
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if(isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);

            if(alpha < 0)
            {
                start = false;
            }
        }
    }

    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }

    void FadeOut()
    {
        isFadeIn = false;
    }
}
