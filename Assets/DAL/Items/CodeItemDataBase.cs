using System;
using System.Collections.Generic;

public class CodeItemDataBase : IItemDataBase
{
    public List<BaseHead> allHeads { get; set; }
    public List<BaseShoulders> allShoulders { get; set; }
    public List<BaseChest> allChest { get; set; }
    public List<BaseLegs> allLegs { get; set; }
    public List<BaseFeet> allFeet { get; set; }
    public List<BaseCardHolder> allCardHolder { get; set; }
    public List<BaseWeapon> allWeapon { get; set; }
    public List<BaseOffhand> allOffhand { get; set; }
    public List<Card> allCards { get; set; }

    private Random r;

    public CodeItemDataBase()
    {
        allHeads = new List<BaseHead>();
        allShoulders = new List<BaseShoulders>();
        allChest = new List<BaseChest>();
        allLegs = new List<BaseLegs>();
        allFeet = new List<BaseFeet>();
        allCardHolder = new List<BaseCardHolder>();
        allWeapon = new List<BaseWeapon>();
        allOffhand = new List<BaseOffhand>();
        allCards = new List<Card>();

        r = new Random();
        FillDatabase();
    }

    private void FillDatabase()
    {
        allHeads.Add(new BaseHead() { Name = "BF cap", Health = 50, StaticIDEquipment = "H_1", PhysDMGMultiplication = 0.1f, Quality = Enumerations.EquipmentQuality.Common });
        allHeads.Add(new BaseHead() { Name = "Lion head", Health = 10, StaticIDEquipment = "H_2", PhysDMGMultiplication = 0.25f, Quality = Enumerations.EquipmentQuality.Common });
        allHeads.Add(new BaseHead() { Name = "Magus capus", Health = 5, Mana = 50, StaticIDEquipment = "H_3", MagicDMGMultiplication = 0.25f, Quality = Enumerations.EquipmentQuality.Common });
        allHeads.Add(new BaseHead() { Name = "Spiked Ore", Health = 200, Mana = 50, StaticIDEquipment = "H_4", PhysDMGMultiplication = 0.75f, Quality = Enumerations.EquipmentQuality.Legendary });
        allHeads.Add(new BaseHead() { Name = "Arcane Halo", Health = 100, Mana = 300, StaticIDEquipment = "H_5", MagicDMGMultiplication = 0.75f, Quality = Enumerations.EquipmentQuality.Legendary });

        allShoulders.Add(new BaseShoulders() { Name = "Bear paw", Health = 15, StaticIDEquipment = "S_1", JumpForce = 10, Quality = Enumerations.EquipmentQuality.Common });
        allShoulders.Add(new BaseShoulders() { Name = "Half orbs", Mana = 20, StaticIDEquipment = "S_2", JumpForce = 15, Quality = Enumerations.EquipmentQuality.Common });

        allChest.Add(new BaseChest() { Name = "Chest guard", Health = 50, StaticIDEquipment = "C_1", Quality = Enumerations.EquipmentQuality.Common });
        allChest.Add(new BaseChest() { Name = "Demon skin", Mana = 50, StaticIDEquipment = "C_2", MagicDMGMultiplication = 0.2f, Quality = Enumerations.EquipmentQuality.Rare });

        allLegs.Add(new BaseLegs() { Name = "Leather pantaloons", Health = 10, StaticIDEquipment = "L_1", Quality = Enumerations.EquipmentQuality.Common });
        allLegs.Add(new BaseLegs() { Name = "Blood wears", Mana=5, StaticIDEquipment = "L_2", Quality = Enumerations.EquipmentQuality.Common });

        allFeet.Add(new BaseFeet() { Name = "Blood socks", MagicDMGMultiplication = 0.1f, StaticIDEquipment = "F_1", Quality = Enumerations.EquipmentQuality.Common });
        allFeet.Add(new BaseFeet() { Name = "Leather boots", Health=10, StaticIDEquipment = "F_2", Quality = Enumerations.EquipmentQuality.Common });


        allCards.Add(new Card() { Name = "Blue bird", Health = 2, Attack = 1, StaticIDCard = "R_1", DefaultCooldown = 2, Quality = Enumerations.EquipmentQuality.Rare });
        allCards.Add(new Card() { Name = "Red panda", Health = 4, Attack = 2, StaticIDCard = "R_2", DefaultCooldown = 4, Quality = Enumerations.EquipmentQuality.Legendary });
        allCards.Add(new Card() { Name = "CUbe", Health = 2, Attack = 2, StaticIDCard = "R_3", DefaultCooldown = 3, Quality = Enumerations.EquipmentQuality.Common });
    }

    public Equipment GetRandomHead()
    {
        return allHeads[r.Next(0, allHeads.Count)];
    }

    public Equipment GetRandomShoulders()
    {
        return allShoulders[r.Next(0, allShoulders.Count)];
    }

    public Equipment GetRandomChest()
    {
        return allChest[r.Next(0, allChest.Count)];
    }

    public Equipment GetRandomLegs()
    {
        return allLegs[r.Next(0, allLegs.Count)];
    }

    public Equipment GetRandomFeet()
    {
        return allFeet[r.Next(0, allFeet.Count)];
    }

    public Equipment GetRandomCardHolder()
    {
        return allCardHolder[r.Next(0, allCardHolder.Count)];
    }

    public Equipment GetRandomWeapon()
    {
        return allWeapon[r.Next(0, allWeapon.Count)];
    }

    public Equipment GetRandomOffhand()
    {
        return allOffhand[r.Next(0, allOffhand.Count)];
    }

    private BaseHead GetHead(string staticID)
    {
        foreach (BaseHead bh in allHeads)
        {
            if (bh.StaticIDEquipment == staticID)
            {
                return bh;
            }
        }
        return null;
    }

    private BaseShoulders GetShoulders(string staticID)
    {
        foreach (BaseShoulders bs in allShoulders)
        {
            if (bs.StaticIDEquipment == staticID)
            {
                return bs;
            }
        }
        return null;
    }

    private BaseChest GetChest(string staticID)
    {
        foreach (BaseChest bc in allChest)
        {
            if (bc.StaticIDEquipment == staticID)
            {
                return bc;
            }
        }
        return null;
    }

    private BaseLegs GetLegs(string staticID)
    {
        foreach (BaseLegs bl in allLegs)
        {
            if (bl.StaticIDEquipment == staticID)
            {
                return bl;
            }
        }
        return null;
    }

    private BaseFeet GetFeet(string staticID)
    {
        foreach (BaseFeet bf in allFeet)
        {
            if (bf.StaticIDEquipment == staticID)
            {
                return bf;
            }
        }
        return null;
    }

    private BaseCardHolder GetCardHolder(string staticID)
    {
        foreach (BaseCardHolder bch in allCardHolder)
        {
            if (bch.StaticIDEquipment == staticID)
            {
                return bch;
            }
        }
        return null;
    }

    private BaseWeapon GetWeapon(string staticID)
    {
        foreach (BaseWeapon bw in allWeapon)
        {
            if (bw.StaticIDEquipment == staticID)
            {
                return bw;
            }
        }
        return null;
    }

    private BaseOffhand GetOffhand(string staticID)
    {
        foreach (BaseOffhand bo in allOffhand)
        {
            if (bo.StaticIDEquipment == staticID)
            {
                return bo;
            }
        }
        return null;
    }

    public Card GetCard(string staticID)
    {
        foreach (Card c in allCards)
        {
            if (c.StaticIDCard == staticID)
            {
                return c;
            }
        }

        return null;
    }

    public Equipment GetRandomEquipment()
    {
        Random r = new Random();
        int number = r.Next(1, 4);  //za sada 4 dok nisam stavio ostatak itema

        switch (number)
        {
            case 1:
                return Equipment.GetCopy(GetRandomHead());
            case 2:
                return Equipment.GetCopy(GetRandomShoulders());
            case 3:
                return Equipment.GetCopy(GetRandomChest());
            case 4:
                return Equipment.GetCopy(GetRandomLegs());
            case 5:
                return Equipment.GetCopy(GetRandomFeet());
            case 6:
                return Equipment.GetCopy(GetRandomCardHolder());
            case 7:
                return Equipment.GetCopy(GetRandomWeapon());
            case 8:
                return Equipment.GetCopy(GetRandomOffhand());
            default:
                return Equipment.GetCopy(GetRandomHead());
        }
    }

    public Equipment GetCopyEquipment(string staticID)
    {
        return Equipment.GetCopy(GetEquipment(staticID));
    }

    public Equipment GetEquipment(string staticID)
    {
        switch (staticID.Split('_')[0])
        {
            case "H":
                return GetHead(staticID);
            case "S":
                return GetShoulders(staticID);
            case "C":
                return GetChest(staticID);
            case "L":
                return GetLegs(staticID);
            case "F":
                return GetFeet(staticID);
            case "A":
                return GetCardHolder(staticID);
            case "W":
                return GetWeapon(staticID);
            case "O":
                return GetOffhand(staticID);
            default:
                return null;
        }
    }

}
