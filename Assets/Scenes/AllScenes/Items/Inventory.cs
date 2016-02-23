using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    List<Equipment> inventory;
    List<Card> cardCollection;

    public List<Equipment> PlayerInventory
    {
        get { return inventory; }
        set { inventory = value; }
    }
    
    public List<Card> CardCollection
    {
        get { return cardCollection; }
        set { cardCollection = value; }
    }
    
    private BaseHead equipedHead;
    public BaseHead EquipedHead
    {
        get { return equipedHead; }
        set 
        { 
            onEquipmentChanged(equipedHead, value);
            equipedHead = value;
        }
    }

    private BaseShoulders equipedShoulders;
    public BaseShoulders EquipedShoulders
    {
        get { return equipedShoulders; }
        set
        {
            onEquipmentChanged(equipedShoulders, value);
            equipedShoulders = value;
        }
    }

    private BaseChest equipedChest;
    public BaseChest EquipedChest
    {
        get { return equipedChest; }
        set
        {
            onEquipmentChanged(equipedChest, value);
            equipedChest = value;
        }
    }

    private BaseLegs equipedLegs;
    public BaseLegs EquipedLegs
    {
        get { return equipedLegs; }
        set
        {
            onEquipmentChanged(equipedLegs, value);
            equipedLegs = value;
        }
    }

    private BaseFeet equipedFeet;
    public BaseFeet EquipedFeet
    {
        get { return equipedFeet; }
        set
        {
            onEquipmentChanged(equipedFeet, value);
            equipedFeet = value;
        }
    }

    private BaseCardHolder equipedCardHolder;
    public BaseCardHolder EquipedCardHolder
    {
        get { return equipedCardHolder; }
        set
        {
            onEquipmentChanged(equipedCardHolder, value);
            equipedCardHolder = value;
        }
    }

    private BaseWeapon equipedWeapon;
    public BaseWeapon EquipedWeapon
    {
        get { return equipedWeapon; }
        set
        {
            onEquipmentChanged(equipedWeapon, value);
            equipedWeapon = value;
        }
    }

    private BaseOffhand equipedOffHand;
    public BaseOffhand EquipedOffHand
    {
        get { return equipedOffHand; }
        set
        {
            onEquipmentChanged(equipedOffHand, value);
            equipedOffHand = value;
        }
    }
    
    
    public delegate void EquipmentChangedHandler(Equipment oldEquip, Equipment newEquip);
    [field: NonSerialized]
    private EquipmentChangedHandler onEquipmentChanged;
    public event EquipmentChangedHandler OnEquipmentChanged
    {
        add
        {
            if (onEquipmentChanged == null)
            {
                onEquipmentChanged += value;
            }
        }
        remove
        {
            onEquipmentChanged -= value;
        }
    }

    public delegate void InventoryChangedHandler();
    [field: NonSerialized]
    public event InventoryChangedHandler OnInventoryChanged;

    public Inventory()
    {
        inventory = new List<Equipment>();
        cardCollection = new List<Card>();
    }

    public void ChangeEquip(bool equip ,Equipment e)    //if false, just unequip e.slot
    {
        switch (e.Slot)
        {
            case Enumerations.EquipmentSlot.Head:
                if (equip == true)
                {
                    EquipedHead = (BaseHead)e;
                }
                else
                {
                    EquipedHead = null;
                }
                break;
            case Enumerations.EquipmentSlot.Shoulders:
                if (equip == true)
                {
                    EquipedShoulders = (BaseShoulders)e;
                }
                else
                {
                    EquipedShoulders = null;
                }
                break;
            case Enumerations.EquipmentSlot.Chest:
                if (equip == true)
                {
                    EquipedChest = (BaseChest)e;
                }
                else
                {
                    EquipedChest = null;
                }
                break;
            case Enumerations.EquipmentSlot.Legs:
                if (equip == true)
                {
                    EquipedLegs = (BaseLegs)e;
                }
                else
                {
                    EquipedLegs = null;
                }
                break;
            case Enumerations.EquipmentSlot.Feet:
                if (equip == true)
                {
                    EquipedFeet = (BaseFeet)e;
                }
                else
                {
                    EquipedFeet = null;
                }
                break;
            case Enumerations.EquipmentSlot.CardHolder:
                if (equip == true)
                {
                    EquipedCardHolder = (BaseCardHolder)e;
                }
                else
                {
                    EquipedCardHolder = null;
                }
                break;
            case Enumerations.EquipmentSlot.Weapon:
                if (equip == true)
                {
                    EquipedWeapon = (BaseWeapon)e;
                }
                else
                {
                    EquipedWeapon = null;
                }
                break;
            case Enumerations.EquipmentSlot.Offhand:
                if (equip == true)
                {
                    EquipedOffHand = (BaseOffhand)e;
                }
                else
                {
                    EquipedOffHand = null;
                }
                break;
            default:
                break;
        }
    }

    public void AddEquipment(Equipment eq)
    {
        PlayerInventory.Add(eq);
        OnInventoryChanged();
    }

    public bool HasEquiped(string staticID)
    {
        try
        {
            switch (staticID.Split('_')[0])
            {
                case "H":
                    if (equipedHead.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "S":
                    if (equipedShoulders.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "C":
                    if (equipedChest.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "L":
                    if (equipedLegs.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "F":
                    if (equipedFeet.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "A":
                    if (equipedCardHolder.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "W":
                    if (equipedWeapon.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                case "O":
                    if (equipedOffHand.StaticIDEquipment == staticID)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " Najvjerojatnije nista nije imao prije");
        }
        return false;
    }
}