using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += AddFunction;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= AddFunction;
    }

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("IPAddress").GetComponent<InputField>().text;
        NetworkManager.singleton.StartClient();
    }

    public void Stop()
    {
        NetworkManager.singleton.StopHost();
    }

    private void OfflineUISet()
    {
        GameObject.Find("CreateHost").GetComponent<Button>().onClick.AddListener(StartHost);
        GameObject.Find("LinkHost").GetComponent<Button>().onClick.AddListener(StartClient);
    }

    private void OnlineUISet()
    {
        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(Stop);
    }

    public void AddFunction(Scene scene, LoadSceneMode mode)
    {
        Scene current = SceneManager.GetActiveScene();

        if (current.buildIndex == 0)
            OfflineUISet();
        else
            OnlineUISet();
    }
}
