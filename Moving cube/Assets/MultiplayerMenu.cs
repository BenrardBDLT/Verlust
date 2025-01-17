using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class MultiplayerMenu : MonoBehaviour
{
    public TMP_InputField ipAddressInput;
    public UnityEngine.UI.Button hostButton;
    public UnityEngine.UI.Button joinButton;

    private void Start()
    {
        // Assign button click events
        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(JoinGame);
    }

    private void StartHost()
    {
        Debug.Log("Hosting game...");
        NetworkManager.singleton.StartHost();
    }

    private void JoinGame()
    {
        string ipAddress = ipAddressInput.text;

        if (!string.IsNullOrEmpty(ipAddress))
        {
            NetworkManager.singleton.networkAddress = ipAddress;
            NetworkManager.singleton.StartClient();
            Debug.Log("Connecting to " + ipAddress + "...");
        }
        else
        {
            Debug.Log("ConnectError");
        }
    }

    // Optionalndle successful connection events
}
