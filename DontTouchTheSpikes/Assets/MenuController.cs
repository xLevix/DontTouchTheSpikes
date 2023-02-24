using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameObject _birdy;
    public Animator _anim;
    GameObject _game;
    GameObject _menu;
    GameObject _birdyGame;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        _birdy = GameObject.Find("BirdMenu");
        _birdyGame = GameObject.Find("BirdGame");
        _birdyGame.SetActive(false);
        _birdy.GetComponent<Rigidbody2D>().gravityScale = 0;
        _anim = _birdy.GetComponent<Animator>();
        _anim.SetLayerWeight(1, 1);
        _game = GameObject.Find("Game");
        _menu = GameObject.Find("Menu");
        _game.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //rozpoczecie gry
            Vector3 birdyPos = _birdy.transform.position;
            _anim.SetLayerWeight(1,0);  //wylaaczanie animacji menu
            _birdyGame.transform.position = birdyPos;
            _birdyGame.GetComponent<Rigidbody2D>().gravityScale = 0.4f; //wlaczenie grawitacji ptaka
            _menu.gameObject.SetActive(false); //wylaczenie menu
            _game.gameObject.SetActive(true); //wlaczenie canvasu gry
            _birdyGame.SetActive(true); //wlaczenie ptaka do gry
            _birdyGame.transform.position = new Vector3(0,0,0); //wyzerowanie pozycji ptaka
            _birdyGame.transform.localScale = new Vector3(0.5f,0.5f,1); //ustawienie kierunku ptaka
            _birdy.gameObject.SetActive(false); //wylaczenie ptaka z menu
            _birdyGame.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}
