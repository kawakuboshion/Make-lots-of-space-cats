using UnityEngine;

public class OnClickEndGame : MonoBehaviour
{
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
