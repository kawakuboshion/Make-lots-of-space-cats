using UnityEngine;

public class OnClickResume : MonoBehaviour
{
    public void Resume()
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
        Time.timeScale = 1f;
    }
}
