using UnityEngine;

public class OnClickSellEnergy : MonoBehaviour
{
    [SerializeField] private float _energyToSell = 10f; // 売るエネルギーの量
    [SerializeField] private float _moneyPerEnergy = 5f; // エネルギー1単位あたりの金額
    GameManager _gameManager = GameManager.Instance;
    public void SellEnergy()
    {
        if (_gameManager != null)
        {
            Debug.Log("Attempting to sell energy...");
            // エネルギーが十分にあるか確認
            if (_gameManager.GetEnergy() >= _energyToSell)
            {
                _gameManager.RemoveEnergy(_energyToSell);
                _gameManager.AddMoney(_energyToSell * _moneyPerEnergy);
                Debug.Log($"Sold {_energyToSell} energy for {_energyToSell * _moneyPerEnergy} money.");
            }
            else
            {
                Debug.Log("Not enough energy to sell.");
            }
        }
        else
        {
            _gameManager = GameManager.Instance;
        }
    }
}
