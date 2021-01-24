using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    
    public enum RarityClass
    {
        Low,Normal,High,VeryHigh
    }
    [System.Serializable]
    public class EconomyEntry
    {
        public string category;
        public float multiplier;
        public RarityClass rarityClass;
        public float dropChance;

           
    }
    public List<EconomyEntry> EconomyList = new List<EconomyEntry>();

    [Header("Rarity Modifiers")]
    [SerializeField]
    private int lowCount;
    [Range(0,5)]
    public int lowLimit = 2;
    [Range(0,1)]
    public float chanceLow;
    [Range(0,1)]
    public float lowPriceMultiplier;
    [SerializeField]
    private int highCount;
    [Range(0, 5)]
    public int highLimit = 2;
    [Range(0, 1)]
    public float chanceHigh;
    [Range(1, 2)]
    public float highPriceMultiplier;
    [SerializeField]
    private int veryHighCount;
    [Range(0, 5)]
    public int veryHighLimit = 1;
    [Range(0, 1)]
    public float chanceVeryHigh;
    [Range(1, 2)]
    public float veryHighPriceMultiplier;
    [Range(0, 1)]
    public float chanceNormal;








    private void Awake()
    {
        string[] temp = System.Enum.GetNames(typeof(Categories));
        foreach (string category in temp)
        {
            EconomyEntry tempEntry = new EconomyEntry();
            tempEntry.category = category;
            tempEntry.multiplier = 1;
            tempEntry.rarityClass = RarityClass.Normal;
            EconomyList.Add(tempEntry);
        }
        lowCount = 0;
        highCount = 0;
        veryHighCount = 0;
    }

    public void ResetEconomy()
    {
        lowCount = 0;
        highCount = 0;
        veryHighCount = 0;
        
        foreach(EconomyEntry category in EconomyList)
        {
            category.multiplier = 1;
            category.rarityClass = RarityClass.Normal;
        }
    }

    private void DistributeRarityClasses()
    {
        foreach (EconomyEntry category in EconomyList)
        {
            if (veryHighCount < veryHighLimit)
            {
                if (Random.Range(0f, 1f) <= chanceVeryHigh)
                {
                    category.rarityClass = RarityClass.VeryHigh;
                    veryHighCount++;
                    continue;
                }

            }
            if(highCount < highLimit)
            {
                if(Random.Range(0f, 1f) <= chanceHigh)
                {
                    category.rarityClass = RarityClass.High;
                    highCount++;
                    continue;
                }
            }
            if (lowCount < lowLimit)
            {
                if (Random.Range(0f, 1f) <= chanceLow)
                {
                    category.rarityClass = RarityClass.Low;
                    lowCount++;
                    continue;
                }
            }
            category.rarityClass = RarityClass.Normal;
        }
    }
    private void SetPercentages()
    {
        int lowAmmount = 0;
        int normalAmmount = 0;
        int highAmmount = 0;
        int veryHighAmmount = 0;
        foreach (EconomyEntry category in EconomyList)
        {
            if (category.rarityClass == RarityClass.Low)
            {
                lowAmmount++;
                continue;
            }
            if (category.rarityClass == RarityClass.Normal)
            {
                normalAmmount++;
                continue;

            }
            if (category.rarityClass == RarityClass.High)
            {
                highAmmount++;
                continue;

            }
            if (category.rarityClass == RarityClass.VeryHigh)
            {
                veryHighAmmount++;
                continue;

            }
        }
        foreach (EconomyEntry category in EconomyList)
        {
            if(category.rarityClass == RarityClass.Low)
            {
                category.multiplier = lowPriceMultiplier;
                category.dropChance = chanceLow;
               
            }
            if (category.rarityClass == RarityClass.Normal)
            {
                category.multiplier = 1;
                category.dropChance = chanceNormal;

            }
            if (category.rarityClass == RarityClass.High)
            {
                category.multiplier = highPriceMultiplier;
                category.dropChance = chanceHigh;

            }
            if (category.rarityClass == RarityClass.VeryHigh)
            {
                category.multiplier = veryHighPriceMultiplier;
                category.dropChance = chanceVeryHigh;


            }
        }
    }
    public void RandomizeEconomy()
    {
        ResetEconomy();
        DistributeRarityClasses();
        SetPercentages();

        


    }


}
