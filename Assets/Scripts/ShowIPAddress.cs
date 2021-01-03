using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShowIPAddress : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = NetworkManager.singleton.networkAddress;
    }

    void Update()
    {
        
    }
}
