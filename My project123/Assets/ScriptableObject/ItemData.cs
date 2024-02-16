using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data",order =int.MaxValue)]
public class ItemData : ScriptableObject
{

    public enum Card
    {
        Damage_Up,
        Hp_Up,
        Speed_Up,
        ShootSpeed_Up,
        AddShoot,
        Exp_Up,
    }

    public Card card;

    public string itemName;
    public string description;
    public int skillLevel;
    public Sprite cardBackground;

    
}
