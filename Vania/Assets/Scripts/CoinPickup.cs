﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(100);
    } 

}
