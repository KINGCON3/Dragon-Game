using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StartNetworkScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("NetworkType") == "Host") NetworkManager.Singleton.StartHost();
        else NetworkManager.Singleton.StartClient();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
