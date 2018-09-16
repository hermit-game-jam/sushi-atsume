using System;
using System.Linq;
using Random = UnityEngine.Random;
using Masters;

public static class GachaSystem
{
    public static int Lottery(this SushiMasterRepository master)
    {
        var totalWeight = master.Values.Sum(x => x.GachaWeight);
        var point = Random.Range(0, totalWeight);

        foreach (var sushi in master.Values)
        {
            totalWeight -= sushi.GachaWeight;

            if (totalWeight <= point)
            {
                return sushi.Code;
            }
        }
        
        throw new Exception();
    }
}
