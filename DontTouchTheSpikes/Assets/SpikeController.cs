using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikeController : MonoBehaviour
{
    GameObject[] spikes;
    private GameObject _gameObject;
    private CollisionController _collisionController;
    GameObject spikesContainer;
    Animator animator;
    public int animCount = 0;
    int spikeStart = 0;
    int spikeEnd = 6;
    int x = 3;

    private void Awake()
    {
        _gameObject = GameObject.Find("BirdGame");
        _collisionController = _gameObject.GetComponent<CollisionController>();
    }

    void Start()
    {
        spikes = GameObject.FindGameObjectsWithTag("Spike");
        spikesContainer = GameObject.Find("SpikesContainer");
        animator = spikesContainer.GetComponent<Animator>();
    }
    
    void Update()
    {
        //jezeli nastapila kolizja, wylosuj wolne miejsce z kolcow i przestaw animacje 
        if (_collisionController.spikeFlag==1)
        {
            SpikeForward();
            _collisionController.spikeFlag = 0;
        }
    }
    
    void SpikeForward()
    {
        //losowanie liczb 1-3, ktore oznaczaja ilosc wolne miejsca
        animCount++;

        if (animCount%6==0 && x>1)
        {
            x--;
        }

        int freeSpace = Random.Range(x, 3);
        ArrayList randomSpikeList = new ArrayList();
        //okreslenie czy zmiana dotyczy lewej czy prawej strony (0-7) to lewa strona, (8-15) to prawa strona
        if (animCount%2==0)
        {
            spikeStart = 9;
            spikeEnd = 14;
        }
        else
        {
            spikeStart = 1;
            spikeEnd = 6;
        }
        
        //dla kazdego wolnego miejsca losujemy czy ma byc to 1 wolne czy 2 wolne miejsca
        for (int i = 0; i < freeSpace; i++)
        {
            int randomSpike = Random.Range(spikeStart, spikeEnd);
            int freeSpikes = Random.Range(0, 1);
            int spikeTwo = randomSpike + freeSpikes;
            randomSpikeList.Add(randomSpike);
            randomSpikeList.Add(spikeTwo);
        }
        
        //te spike ktore zostaly wylosowane jako wolne sa wylaczane 
        for (int i = 0; i < spikes.Length; i++)
        {
            if (randomSpikeList.Contains(i))
            {
                spikes[i].SetActive(false);
            }
            else
            {
                spikes[i].SetActive(true);
            }
        }

        //granie animacji dla lewej lub prawej strony
        if (animCount==1)
        {
            animator.Play("SpikeGo");
        }
        else if (animCount%2!=0)
        {
            animator.Play("MoveRight");
        }
        else
        {
            animator.Play("MoveLeft");
        }
        
    }
}

