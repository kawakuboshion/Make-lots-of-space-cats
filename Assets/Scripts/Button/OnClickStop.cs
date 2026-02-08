using UnityEngine;

public class OnClickStop : MonoBehaviour
{
    public void Stop()
    {
        Time.timeScale = 0f;
    }
}
