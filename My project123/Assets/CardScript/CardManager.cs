using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameManager gameManager;
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
        /*
                Debug.Log(cardIntList[0]);
                Debug.Log(cardIntList[1]);
                Debug.Log(cardIntList[2]);
        */
        Debug.Log(card[cardIntList[0]].itemName+ card[cardIntList[0]].skillLevel);
        Debug.Log(card[cardIntList[1]].itemName + card[cardIntList[1]].skillLevel);
        Debug.Log(card[cardIntList[2]].itemName + card[cardIntList[2]].skillLevel);

        itemName[0].text = card[cardIntList[0]].itemName;
        itemDescription[0].text = card[cardIntList[0]].description;
        itemName[1].text = card[cardIntList[1]].itemName;
        itemDescription[1].text = card[cardIntList[1]].description;
        itemName[2].text = card[cardIntList[2]].itemName;
        itemDescription[2].text = card[cardIntList[2]].description;
    }


    public void Clickbtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        Debug.Log(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
        

        switch (clickObject.GetComponentInChildren<Text>().text)
        {
            case "데미지 증가":
                card[0].skillLevel++;
                gameManager.DamageUp();


                break;
            case "최대 체력 증가":
                card[1].skillLevel++;
                gameManager.HpUp();

                break;
            case "이동 속도 증가":
                card[2].skillLevel++;
                gameManager.SpeedUp();

                break;
            case "공격 속도 증가":
                card[3].skillLevel++;
                gameManager.ShootSpeedUp();


                break;
            case "추가 공격":
                card[4].skillLevel++;
                gameManager.AddShoot();

                break;
            case "경험치 흭득량 증가":
                card[5].skillLevel++;
                gameManager.ExpUp();

                break;




        }




    }



}
