using UnityEngine;

public class OnClickPlayBGM : MonoBehaviour
{
    [SerializeField] private AudioManager.BGM _bgm;
    public void OnClick()
    {
        AudioManager.Instance.PlayBGM(_bgm);
    }
}
