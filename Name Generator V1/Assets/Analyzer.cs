using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyzer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Library library;

    [Header("Table Values")]
    public LengthOccurenceTable lengthOccurenceTable;
    public LetterOccurenceTable letterOccurenceTable;


    private void Awake()
    {
        //nextLetterLookupTable = new int[25,25];
        LetterOccurenceTableInitializer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            RunAnalyzer();
        }
    }

    private void RunAnalyzer()
    {
        //Analyze each words
        foreach (var item in library.nameList)
        {
            LengthOccurenceAnalyzer(item);

            //Analyze each letter in a word
            foreach (char c in item)
            {
                LetterOccurenceAnalyzer(c);
            }
        }

        //Count all percentages
        LengthOccurencePercentageCounter();
        LetterOccurencePercentageCounter();
    }

    #region Length Occurence Table
    /// <summary>
    /// Tally up the length of every names in the list
    /// </summary>
    /// <param name="item"></param>
    private void LengthOccurenceAnalyzer(string item)
    {
        lengthOccurenceTable.nameCount++;

        //Get the length of the item on the list
        int length = item.Length;

        //If the index for that length already exists
        if (lengthOccurenceTable.count.Count > length)
        {
            lengthOccurenceTable.count[length] += 1;
        }
        else
        {
            //Otherwise, add new values in the list until we reached that length
            int currentLoopValue = lengthOccurenceTable.count.Count;
            do
            {
                lengthOccurenceTable.count.Add(0);
                lengthOccurenceTable.percentage.Add(0);
                currentLoopValue++;
            }
            while (currentLoopValue < length);

            lengthOccurenceTable.count.Add(1);
            lengthOccurenceTable.percentage.Add(0);
        }
    }

    /// <summary>
    /// Count the percentage of every length occurence in the list
    /// </summary>
    private void LengthOccurencePercentageCounter()
    {
        for (int i = 0; i < lengthOccurenceTable.count.Count; i++)
        {
            lengthOccurenceTable.percentage[i] = (float)lengthOccurenceTable.count[i] / lengthOccurenceTable.nameCount;
        }
    }
    #endregion

    #region Letter Occurence Table
    /// <summary>
    /// This function initialize all letter value in the table
    /// </summary>
    private void LetterOccurenceTableInitializer()
    {
        //Initialize all letters in the table
        for (int i = 65; i <= 90; i++)
        {
            letterOccurenceTable.letter.Add(System.Convert.ToChar(i));
            letterOccurenceTable.count.Add(0);
            letterOccurenceTable.percentage.Add(0);
        }
    }

    /// <summary>
    /// This function counts all letter occurences in the whole list
    /// </summary>
    /// <param name="item"></param>
    private void LetterOccurenceAnalyzer(char item)
    {
        //Uppercase ascii code: 65-90
        //Lowercase ascii code: 97-122

        int asciiCode = System.Convert.ToInt32(item);

        //For lowercase:
        if (asciiCode <= 90 && asciiCode >= 65)
        {
            letterOccurenceTable.count[asciiCode - 65]++;
        }
        else if (asciiCode <= 122 && asciiCode >= 97)
        {
            letterOccurenceTable.count[asciiCode - 97]++;
        }
        else return;

        letterOccurenceTable.letterCount++;
    }

    /// <summary>
    /// This function tally up all letter occurences as percentages
    /// </summary>
    private void LetterOccurencePercentageCounter()
    {
        for (int i = 0; i < letterOccurenceTable.count.Count; i++)
        {
            letterOccurenceTable.percentage[i] = (float)letterOccurenceTable.count[i] / letterOccurenceTable.letterCount;
        }
    }
    #endregion
}

[System.Serializable]
public class LengthOccurenceTable
{
    public int nameCount = 0;

    //The index of these lists is the length
    public List<int> count;           //How many times this length has appeared in the list
    public List<float> percentage;
}

[System.Serializable]
public class LetterOccurenceTable
{
    public int letterCount = 0;

    public List<char> letter;           //How many times this length has appeared in the list
    public List<int> count;
    public List<float> percentage;
}

[System.Serializable]
public class NextLetterLookupTable
{
    public int[,] nextLetterLookupCount;
    public float[,] percetageTable;
}