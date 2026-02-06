using UnityEngine;

public class OnClickSellEnergy : MonoBehaviour
{
    [SerializeField] private float energyToSell = 10f; // 売るエネルギーの量
    [SerializeField] private float moneyPerEnergy = 5f; // エネルギー1単位あたりの金額
    GameManager gm = GameManager.Instance;
    public void SellEnergy()
    {
        if (gm != null)
        {
            // エネルギーが十分にあるか確認
            if (gm.GetEnergy() >= energyToSell)
            {
                gm.RemoveEnergy(energyToSell);
                gm.AddMoney(energyToSell * moneyPerEnergy);
                Debug.Log($"Sold {energyToSell} energy for {energyToSell * moneyPerEnergy} money.");
            }
            else
            {
                Debug.Log("Not enough energy to sell.");
            }
        }
    }
}
