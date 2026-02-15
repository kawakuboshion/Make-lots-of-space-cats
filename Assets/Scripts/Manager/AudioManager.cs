using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _bgm;
    [SerializeField] private List<AudioClip> _se;
    [SerializeField] private AudioMixer _audioMixer = default;
    [SerializeField] private AudioSource _bgmAudioSource = default;
    [SerializeField] private AudioSource _seAudioSource = default;
    [SerializeField] private BGM _playingBGM;

    public static AudioManager Instance;

    public enum BGM
    {
        Title,
        Stage,
        Option,
        Result,
        Ranking,
    }

    public enum SE
    {
        OnClick,
        Change_AudioValue,
        Cat_Appeared,
        Cat_Disappeared,
        Cat_Anger,
        Anger,
        Happy,
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public float ConvertVolume2dB(float volume)
    {
        return Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);
    }

    public float ConvertDB2Volume(float db)
    {
        return Mathf.Clamp(Mathf.Pow(10, Mathf.Clamp(db, -80, 0) / 20f), 0, 1);
    }

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", ConvertVolume2dB(volume));
        Debug.Log($"マスターの音量を変更: {volume}");
    }
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGMVolume", ConvertVolume2dB(volume));
        Debug.Log($"BGMの音量を変更: {volume}");
    }

    public void SetSEVolume(float volume)
    {
        _audioMixer.SetFloat("SEVolume", ConvertVolume2dB(volume));
        Debug.Log($"SEの音量を変更: {volume}");
    }
    public void PlayBGM(BGM bgm)
    {
        if (_bgm[(int)bgm] == null) { return; }
        
        _bgmAudioSource.clip = _bgm[(int)bgm];
        _bgmAudioSource.Play();
        Debug.Log($"次のBGMを再生: {_bgm[(int)bgm].name}");
        _playingBGM = bgm;
    }

    public void PlaySE(SE se)
    {
        if (_se[(int)se] == null) { return; }
        
        _seAudioSource.PlayOneShot(_se[(int)se]);
        Debug.Log($"次のSEを再生: {_se[(int)se].name}");
    }

    public void PlaySE(SE se, bool loop)
    {
        if (_se[(int)se] == null) { return; }
        ;
        _seAudioSource.loop = loop;
        _seAudioSource.clip = _se[(int)se];
        _seAudioSource.Play();
        Debug.Log($"次のSEを再生: {_se[(int)se].name},Loop: {loop}");
    }

    public IEnumerator WaitSEFinigh()
    {
        yield return new WaitUntil(() => _seAudioSource.isPlaying);
        Debug.Log("SEの再生が終了");
    }

    public float GetMasterVolume()
    {
        _audioMixer.GetFloat("MasterVolume", out float masterVolume);
        return masterVolume;
    }

    public float GetBGMVolume()
    {
        _audioMixer.GetFloat("BGMVolume", out float bgmVolume);
        return bgmVolume;
    }

    public float GetSEVolume()
    {
        _audioMixer.GetFloat("SEVolume", out float seVolume);
        return seVolume;
    }

    public BGM GetBGM()
    {
        return _playingBGM;
    }

    public bool GetIsPlaying()
    {
        return _seAudioSource.isPlaying;
    }
}
