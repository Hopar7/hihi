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


    public GameObject[] CardStarUi;
    public GameObject[] CardStarUi2;
    public GameObject[] CardStarUi3;

    public GameObject[] RedUi;

    private int star5card=0;

    public Text[] itemName;
    public Text[] itemDescription;

    public GameObject levelUpUi;
    public void CardSet()
    {
        
        for (int i=0;i<4;i++)
        {
            CardStarUi[i].SetActive(false);
            CardStarUi2[i].SetActive(false);
            CardStarUi3[i].SetActive(false);
        }
        
        for(int i=0;i<3;i++)
        {
            RedUi[i].SetActive(false);
        }



        List<int> cardIntList = new List<int>();
        int currentNumber = Random.Range(0, card.Length);
        for (int i = 0; i < 3;)
        {
            if(star5card==4)
            {
                cardIntList.Add(0);
                cardIntList.Add(1);
                cardIntList.Add(2);


                break;
            }

           
            if (cardIntList.Contains(currentNumber))
            {
                
                currentNumber = Random.Range(0, card.Length);
            }
            else if (card[currentNumber].skillLevel>5)
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
        if (card[cardIntList[0]].skillLevel < 5)
        {
            itemName[0].text = card[cardIntList[0]].itemName;
            itemDescription[0].text = card[cardIntList[0]].description;
            FillStar(card[cardIntList[0]].skillLevel);
        }
        else
        {
            itemName[0].text = card[cardIntList[0]].itemName;
            itemDescription[0].text = Card5(card[cardIntList[0]].itemName) ;
            FillStar(card[cardIntList[0]].skillLevel);

            Card5(card[cardIntList[0]].itemName);
        }
        if (card[cardIntList[1]].skillLevel < 5)
        {
            itemName[1].text = card[cardIntList[1]].itemName;
            itemDescription[1].text = card[cardIntList[1]].description;
            FillStar2(card[cardIntList[1]].skillLevel);
        }
        else
        {
            itemName[1].text = card[cardIntList[1]].itemName;
            itemDescription[1].text = Card5(card[cardIntList[1]].itemName); ;
            FillStar2(card[cardIntList[1]].skillLevel);
        }

        if (card[cardIntList[2]].skillLevel < 5)
        {
            itemName[2].text = card[cardIntList[2]].itemName;
            itemDescription[2].text = card[cardIntList[2]].description;
            FillStar3(card[cardIntList[2]].skillLevel);
        }
        else
        {
            itemName[2].text = card[cardIntList[2]].itemName;
            itemDescription[2].text = Card5(card[cardIntList[2]].itemName); ;
            FillStar3(card[cardIntList[2]].skillLevel);
        }
    }

    void FillStar(int x)
    {
        if (x < 5)
        {
            for (int i = 0; i < x; i++)
            {
                CardStarUi[i].SetActive(true);
            }
        }
        else
        {
            RedUi[0].SetActive(true);
        }
    }
    void FillStar2(int x)
    {
        if (x < 5)
        {
            for (int i = 0; i < x; i++)
            {
                CardStarUi2[i].SetActive(true);
            }
        }
        else
        {
            RedUi[1].SetActive(true);
        }
    }
    void FillStar3(int x)
    {
        if (x < 5)
        {
            for (int i = 0; i < x; i++)
            {
                CardStarUi3[i].SetActive(true);
            }
        }
        else
        {
            RedUi[2].SetActive(true);
        }
    }

    string Card5(string s)
    {
        switch (s)
        {
            case "데미지 증가":
                return "데미지가 10인 최대 데미지가 됩니다.";
            case "체력 회복":
                return "최대 체력이 10% 증가합니다. ";
            case "이동 속도 증가":
                return "최대 이동 속도가 됩니다.";
            case "공격 속도 증가":
                return "최대 공격 속도가 됩니다.";
            case "추가 공격":
                return "발사되는 총알의 수가 4개로 줄어들지만 운석을 한 번 관통합니다.";
            case "경험치 흭득량 증가":
                return "흠..";
        }
        return "asd";
    }

    public void Clickbtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        Debug.Log(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
        

        switch (clickObject.GetComponentInChildren<Text>().text)
        {
            case "데미지 증가":
                if (card[0].skillLevel < 6)
                {
                    if (card[0].skillLevel==5)
                    {
                        star5card++;
                        gameManager.MaxDamageUp();
                    }
                    else
                    {
                        gameManager.DamageUp();

                    }
                    card[0].skillLevel++;
                    

                   
                }
                else
                {
                    card[0].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;
            case "체력 회복":
                if (card[1].skillLevel < 6)
                {
                    if (card[1].skillLevel == 5)
                    {
                        star5card++;
                        gameManager.MaxHpUp();
                        //이런식으로 다 강화됬을때 따로//
                    }
                    else
                    {
                        gameManager.HpUp();
                    }
                    card[1].skillLevel++;
                    
                }
                else
                {
                    card[1].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;
            case "이동 속도 증가":
                if (card[2].skillLevel < 6)
                {
                    if (card[2].skillLevel == 5)
                    {
                        star5card++;
                    }
                    card[2].skillLevel++;
                    gameManager.SpeedUp();
                }
                else
                {
                    card[2].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;
            case "공격 속도 증가":
                if (card[3].skillLevel < 6)
                {
                    if (card[3].skillLevel == 5)
                    {
                        star5card++;
                        gameManager.MaxShootSpeed();
                    }
                    else
                    {
                        gameManager.ShootSpeedUp();
                    }
                    card[3].skillLevel++;
                    
                   
                }
                else
                {
                    card[3].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;
            case "추가 공격":
                if (card[4].skillLevel < 6)
                {
                    if (card[4].skillLevel == 5)
                    {
                        star5card++;
                    }
                    card[4].skillLevel++;
                    gameManager.AddShoot();
                }
                else
                {
                    card[4].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;
            case "경험치 흭득량 증가":
                if (card[5].skillLevel <6)
                {
                    if (card[5].skillLevel == 5)
                    {
                        star5card++;
                    }
                    card[5].skillLevel++;
                    gameManager.ExpUp();
                }
                else
                {
                    card[5].skillLevel++;
                    levelUpUi.SetActive(false);
                    Time.timeScale = 1;
                }

                break;




        }




    }



}
