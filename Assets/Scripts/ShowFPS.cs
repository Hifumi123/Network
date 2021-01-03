using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public Text fpsText;

    private float m_Timer = 0;

    private int m_Count = 0;

    void Start()
    {
        fpsText.text = "0 FPS";
    }

    void Update()
    {
        if (m_Timer >= 1)
        {
            float fps = m_Count / m_Timer;

            fpsText.text = Mathf.FloorToInt(fps) + " FPS";

            m_Timer = Time.deltaTime;

            m_Count = 1;
        }
        else
        {
            m_Timer += Time.deltaTime;

            m_Count++;
        }
    }
}
