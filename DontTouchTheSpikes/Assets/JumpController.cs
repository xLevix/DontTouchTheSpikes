using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    GameObject bird;
    public Animator animator;
    CollisionController _collisionController;
    private int direction = 1;
    
    [SerializeField] private AudioSource jumpSound;
    
    void Start()
    {
        bird = GameObject.Find("BirdGame");
        animator = bird.GetComponent<Animator>();
        _collisionController = bird.GetComponent<CollisionController>();
    }
    
    void Update()
    {
        //jezeli wynik gracza jest parzysty to zmieniamy kierunek na prawo i odwrotnie
        if (_collisionController.score%2==0)
        {
            direction = 1;
        }else
        {
            direction = -1;
        }
        
        //plynna transformacja pozycji miedzy skierowaniem w gory, w stanie spoczynku i w dol na podstawie velocity, tak by wygladało to na lagodne przejscie
        if (bird.GetComponent<Rigidbody2D>().velocity.y < 0.5f && bird.GetComponent<Rigidbody2D>().velocity.y > -0.5f)
        {
            bird.transform.rotation = Quaternion.Slerp(bird.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
        } else if (bird.GetComponent<Rigidbody2D>().velocity.y > 0.6f)
        {
            bird.transform.rotation = Quaternion.Slerp(bird.transform.rotation, Quaternion.Euler(0, 0, 20 * direction), Time.deltaTime * 5);
        }
        else if (bird.GetComponent<Rigidbody2D>().velocity.y < -0.6f)
        {
            bird.transform.rotation = Quaternion.Slerp(bird.transform.rotation, Quaternion.Euler(0, 0, -20 * direction), Time.deltaTime * 5);
        }
    }
    
    public void Jump(){
        //na klik, zrestuj sily na ptaka oraz dodaj nowa sile w gore i lewo lub prawo
        bird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bird.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.5f*direction, 2f), ForceMode2D.Impulse);
        jumpSound.Play();
        bird.transform.localScale = new Vector3(0.5f*direction, 0.5f, 1);
    }
    
    public void DoAnimation(){
        //na klik wykonaj animacje latania
        animator.Play("flyingAnimation",0,0.0f);
    }
}


