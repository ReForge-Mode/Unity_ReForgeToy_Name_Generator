using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Analyzer analyzer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log(GenerateRandomName());
        }
    }

    private string GenerateRandomName()
    {
        //Decide how long the name should be
        int nameLength = WeightedRandom(analyzer.lengthOccurenceTable.percentage);

        //Generate letter one by one
        string name = "";
        for (int i = 0; i < nameLength; i++)
        {
            int letterIndex = WeightedRandom(analyzer.letterOccurenceTable.percentage);
            name += analyzer.letterOccurenceTable.letter[letterIndex];
        }

        return name;
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
