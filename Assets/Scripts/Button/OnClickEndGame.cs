using UnityEngine;

public class OnClickEndGame : MonoBehaviour
{
    public void EndGame()
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
