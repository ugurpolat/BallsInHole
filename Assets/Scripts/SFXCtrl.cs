using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;

    public GameObject sfx_coin_pickup;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void ShowCoinSparkle( Vector3 pos)
    {
        Instantiate(sfx_coin_pickup, pos, Quaternion.identity);
    }
}
