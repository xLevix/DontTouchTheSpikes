using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    GameObject bird;
    GameObject jumpButton;
    GameObject points;
    public int score = 0;
    TMP_Text TextComponent;
    public int spikeFlag = 0;
    public int deathFlag = 0;
    private GameObject game;
    private AudioSource[] audio;

    void Start()
    {
        game = GameObject.Find("Game");
        audio = game.GetComponents<AudioSource>();
        bird=GameObject.Find("BirdGame");
        jumpButton=GameObject.Find("JumpButton");
        points=GameObject.Find("Points");
        TextComponent = points.GetComponent<TMP_Text>();
    }
    
    IEnumerator DisableCollisionForSeconds(float seconds)
    {
        bird.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(seconds);
        bird.GetComponent<PolygonCollider2D>().enabled = true;
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("UpperWall") && !coll.gameObject.CompareTag("DownWall"))
        {
            StartCoroutine(DisableCollisionForSeconds(0.2f));
        }
        
        Vector2 force = bird.GetComponent<Rigidbody2D>().velocity;
        bird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bird.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force.x, 1f), ForceMode2D.Impulse);
        //w przypadku kolizji z przeszkoda gora lub dol lub spike
        if (coll.gameObject.CompareTag("UpperWall") || coll.gameObject.CompareTag("DownWall")  || coll.gameObject.CompareTag("Spike"))
        {
            //wylacz przycisk skoku, wlacz duza grawitacje by ptak szybko spadl
            jumpButton.SetActive(false);
            bird.GetComponent<Rigidbody2D>().gravityScale = 3;
            bird.transform.Rotate(0, 0, 90);
            if (coll.gameObject.CompareTag("DownWall"))
            {
                //zatrzymaj ptaka na ziemii
                bird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                bird.GetComponent<Rigidbody2D>().gravityScale = 0;
                bird.transform.Rotate(0, 0, 30);
            }

            foreach (AudioSource a in audio)
            {
                a.Stop();
            }
            
            deathFlag = 1; //ustaw flage ze ptak umarl
            TextComponent.text= "";
        }
        else
        {
            //w przypadku kolizji ze sciana lewa lub prawo, ustaw przeciwny kierunek zwortu do aktualnego
            Vector3 scale = bird.transform.localScale;
            bird.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            //dodaj punkt jezeli ptak jest zywy
            if (deathFlag==0)
            {
                score++;
                
                if (score>11 && audio[1].isPlaying)
                {
                    audio[1].Stop();
                    audio[2].Play();
                }

                
                if (score>5 && audio[0].isPlaying)
                {
                    audio[0].Stop();
                    audio[1].Play();
                }
            }
            //wyswietl punkty
            TextComponent.text = score.ToString();
            spikeFlag = 1;
        }
    }
}
