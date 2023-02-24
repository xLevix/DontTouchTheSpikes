using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private GameObject end;
    GameObject player;
    private GameObject scoreView;
    TMP_Text score;
    private GameObject info;
    TMP_Text restart;
    private GameObject menu;
    private GameObject game;
    GameObject spikesContainer;
    private GameObject birdMenu;
    private GameObject birdGame;
    GameObject jumpButton;
    private AudioSource[] audios;

    void Start()
    {
        player = GameObject.Find("BirdGame");
        end = GameObject.Find("Death");
        scoreView = GameObject.Find("ScoreView");
        score = scoreView.GetComponent<TMP_Text>();
        menu = GameObject.Find("Menu");
        game = GameObject.Find("Game");
        spikesContainer = GameObject.Find("SpikesContainer");
        birdMenu = GameObject.Find("BirdMenu");
        birdGame = GameObject.Find("BirdGame");
        jumpButton = GameObject.Find("JumpButton");
        audios = end.GetComponents<AudioSource>();
        audios[1].PlayDelayed(2);
        Debug.Log(audios.Length);
        end.SetActive(false);
    }
    

    void Update()
    {
        if (player.GetComponent<CollisionController>().deathFlag == 1)
        {
            end.SetActive(true);
            //wyswietlenie wyniku oraz highscore nawet przy restrarcie gry
            if (player.GetComponent<CollisionController>().score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", player.GetComponent<CollisionController>().score);
            }
            score.text = "Score: " + player.GetComponent<CollisionController>().score + "\nHigh Score: " + PlayerPrefs.GetInt("HighScore", 0);
            
            if (Input.GetButtonDown("Fire1")) 
            {
                //zrestartowanie gry oraz kluczowych zmiennych
                player.GetComponent<CollisionController>().deathFlag = 0;
                player.GetComponent<CollisionController>().score = 0;
                spikesContainer.GetComponent<SpikeController>().animCount = 0;
                end.SetActive(false);
                birdGame.SetActive(false);
                game.SetActive(false);
                menu.SetActive(true);
                birdMenu.SetActive(true);
                jumpButton.SetActive(true);
                menu.GetComponent<MenuController>()._anim.SetLayerWeight(1,1);
            }
        }
    }
}


