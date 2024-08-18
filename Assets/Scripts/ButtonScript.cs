using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public GameObject startHostButton;
    public GameObject startClientButton;


    // Start is called before the first frame update
    void Start()
    {
        Button hostButton = startHostButton.GetComponent<Button>();
        hostButton.onClick.AddListener(startHost);
        Button clientButton = startClientButton.GetComponent<Button>();
        clientButton.onClick.AddListener(startClient);

    }

    void startHost()
    {
        PlayerPrefs.SetString("NetworkType", "Host");
        Debug.Log("Loading game as host");
        SceneManager.LoadScene(1);
    }

    void startClient()
    {
        PlayerPrefs.SetString("NetworkType", "Client");
        Debug.Log("Loading game as client");
        SceneManager.LoadScene(1);
    }

}
