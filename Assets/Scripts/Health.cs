using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    [SyncVar(hook = "OnHealthChange")]
    public float health = 100;

    public float maxHealth = 100;

    private Slider m_HealthSlider;

    void Start()
    {
        if (isLocalPlayer)
            m_HealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    void Update()
    {
        if (isLocalPlayer && health <= 0)
        {
            GameObject.Find("EndText").GetComponent<Text>().text = "You Lose!";
            GameObject.Find("GameEnd").GetComponent<CanvasGroup>().alpha = 1;

            CmdGameEnd();
        }
    }

    [Command]
    private void CmdGameEnd()
    {
        RpcGameEnd();
    }

    [ClientRpc]
    private void RpcGameEnd()
    {
        if (!isLocalPlayer)
        {
            GameObject.Find("EndText").GetComponent<Text>().text = "You Win!";

            GameObject.Find("GameEnd").GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private void OnHealthChange(float currentHealth)
    {
        if (isLocalPlayer)
        {
            health = currentHealth;
            m_HealthSlider.value = currentHealth / maxHealth;
        }
    }

    //[ClientRpc]
    //private void RpcHealth(float serverHealth)
    //{
        //health = serverHealth;
    //}

    public void TakeDamage(float damage)
    {
        health -= damage;

        //RpcHealth(health);
    }
}
