using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankingText;
    [SerializeField] private bool _isShowRanking = false;
    private static List<float> _scores = new List<float>();
    private static int _maxRankingCount = 5;

    void Start()
    {
        if (_isShowRanking)
        {
            ShowRanking();
        }
    }

    public void AddScore(float score)
    {
        _scores.Add(score);
        _scores.Sort((a, b) => b.CompareTo(a)); // スコアを降順にソート
        if (_scores.Count > _maxRankingCount)
        {
            _scores.RemoveAt(_scores.Count - 1); // ランキングの最大数を超えた場合、最下位のスコアを削除
        }
    }

    public void ShowRanking()
    {
        string rankingText = "ランキング\n\n";
        if(_scores.Count == 0)
        {
            rankingText += "ランキングはまだありません。";
        }
        for (int i = 0; i < _scores.Count; i++)
        {
            rankingText += $"{i + 1} : {_scores[i]:F0}点\n";
        }
        _rankingText.text = rankingText;
    }
}
