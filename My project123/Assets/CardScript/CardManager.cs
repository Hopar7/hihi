using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public ItemData[] card;

    
    public Text[] itemName;
    public Text[] itemDescription;


    public void CardSet()
    {
        List<int> cardIntList = new List<int>();
        int currentNumber = Random.Range(0, card.Length);

        for (int i = 0; i < 3;)
        {
            if (cardIntList.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, card.Length);
            }
            else
            {
                cardIntList.Add(currentNumber);
                i++;
            }
        }

        Debug.Log(cardIntList[0]);
        Debug.Log(cardIntList[1]);
        Debug.Log(cardIntList[2]);

        itemName[0].text = card[cardIntList[0]].itemName;
        itemDescription[0].text = card[cardIntList[0]].description;
        itemName[1].text = card[cardIntList[1]].itemName;
        itemDescription[1].text = card[cardIntList[1]].description;
        itemName[2].text = card[cardIntList[2]].itemName;
        itemDescription[2].text = card[cardIntList[2]].description;
    }
    





}
