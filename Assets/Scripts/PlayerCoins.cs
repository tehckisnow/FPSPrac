using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour
{
    public TextMeshProUGUI coinCount;
    public string displayText = "Coins: ";
    public int coins = 0;

    public void AddCoin()
    {
        coins++;
        updateCoinCount();
    }

    public void updateCoinCount()
    {
        coinCount.SetText(displayText + coins.ToString());
        Debug.Log(coins);
    }

    // Start is called before the first frame update
    void Start()
    {
        updateCoinCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
