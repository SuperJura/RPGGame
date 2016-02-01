using System;
using System.Collections.Generic;

public interface IItemDataBase
{

    List<BaseHead> allHeads {get; set; }
    List<BaseShoulders> allShoulders { get; set; }
    List<BaseChest> allChest { get; set; }
    List<BaseLegs> allLegs { get; set; }
    List<BaseFeet> allFeet { get; set; }
    List<BaseCardHolder> allCardHolder { get; set; }
    List<BaseWeapon> allWeapon { get; set; }
    List<BaseOffhand> allOffhand { get; set; }
    List<Card> allCards { get; set; }

    Equipment GetRandomHead();

    Equipment GetRandomShoulders();

    Equipment GetRandomChest();

    Equipment GetRandomLegs();

    Equipment GetRandomFeet();

    Equipment GetRandomCardHolder();

    Equipment GetRandomWeapon();

    Equipment GetRandomOffhand();

    Equipment GetEquipment(string staticID);

    Card GetCard(string staticID);

    Equipment GetRandomEquipment();

    Equipment GetCopyEquipment(string staticID);
}
