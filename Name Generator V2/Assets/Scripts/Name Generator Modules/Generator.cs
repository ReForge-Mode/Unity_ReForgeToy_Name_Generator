using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Analyzer analyzer;

    public string GenerateRandomName()
    {
        //Decide how long the name should be
        int nameLength = WeightedRandom(analyzer.lengthOccurenceTable.percentage);

        //Find the first letter
        List<float> firstLetterList = GetListFromArray(0);
        int prevLetterIndex = WeightedRandom(firstLetterList);

        //Look up the table and generate the next letter based on the table
        string name = "";
        name += analyzer.letterOccurenceTable.letter[prevLetterIndex];

        for (int i = 1; i < nameLength; i++)
        {
            //Convert the current column to list
            List<float> list = GetListFromArray(prevLetterIndex);
            int letterIndex = WeightedRandom(list);

            //Try to make it so that the next letter isn't the same letter
            for (int k = 0; k < 10; k++)
            {
                letterIndex = WeightedRandom(list);
                if (prevLetterIndex != letterIndex)
                {
                    break;
                }
            }

            name += analyzer.letterOccurenceTable.letter[letterIndex];
            prevLetterIndex = letterIndex;
        }

        return name;
    }

    private List<float> GetListFromArray(int column)
    {
        List<float> list = new List<float>();
        for (int i = 0; i < 26; i++)
        {
            float currentItem = analyzer.nextLetterLookupTablePercent[column, i];
            if (currentItem > 0)
            {
                list.Add(currentItem);
            }
        }
        return list;
    }

    /// <summary>
    /// This function takes list of item and percentage weight
    /// and output the index of the chosen string
    /// </summary>
    /// <param name="weightList"></param>
    /// <returns></returns>
    private int WeightedRandom(List<float> weightList)
    {
        //Calculate sum of total weight
        float totalWeight = 0;
        for (int i = 0; i < weightList.Count; i++)
        {
            //Convert decimal to integer with two decimal point
            totalWeight += weightList[i];
        }

        //Picking random number
        float random = Random.Range(0f, totalWeight);

        //Go through the items one at a time,
        //Subtracting their weight from your random number,
        //Until we get the item where the random number is less than that item's weight
        for (int i = 0; i < weightList.Count; i++)
        {
            if (random < weightList[i])
                return i;
            random -= weightList[i];
        }
        return -1;
    }
}
