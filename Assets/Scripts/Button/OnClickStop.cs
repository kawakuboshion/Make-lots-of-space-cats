using UnityEngine;

public class OnClickStop : MonoBehaviour
{
    public void Stop()
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
        Time.timeScale = 0f;
    }
}
