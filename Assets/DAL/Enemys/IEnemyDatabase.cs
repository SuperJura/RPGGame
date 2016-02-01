using System;
using System.Collections.Generic;

public interface IEnemyDatabase
{
    EnemyInformation GetEnemyInformation(string staticID);
}
