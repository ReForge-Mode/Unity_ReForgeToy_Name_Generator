using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyzer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Library library;

    [Header("Table Values")]
    public LengthOccurenceTable lengthOccurenceTable;
    public LetterOccurenceTable letterOccurenceTable;
    public int[,] nextLetterLookupTable;
    public float[,] nextLetterLookupTablePercent;

    private void Awake()
    {
        nextLetterLookupTable = new int[26, 26];
        nextLetterLookupTablePercent = new float[26, 26];
        LetterOccurenceTableInitializer();
    }

    private void Update()
    {
        //For Debug Purposes
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Print2DArray();
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Print2DArrayPercent();
        }
    }
    private void Clear()
    {
        lengthOccurenceTable.nameCount = 0;
        if(lengthOccurenceTable.count.Count > 0) lengthOccurenceTable.count.Clear();
        if(lengthOccurenceTable.percentage.Count > 0) lengthOccurenceTable.percentage.Clear();

        letterOccurenceTable.letterCount = 0;
        for (int i = 0; i < letterOccurenceTable.count.Count; i++)
        {
            letterOccurenceTable.count[i] = 0;
            letterOccurenceTable.percentage[i] = 0;
        }
    }

    public void RunAnalyzer()
    {
        Clear();

        //In case the Library is empty, don't do anything
        if(library.nameList.Count == 0)
        {
            library.console.text = "Error. Please load text file to the Library first";
            return;
        }

        ////Analyze each words
        //foreach (var item in library.nameList)
        //{
        //    LengthOccurenceAnalyzer(item);

        //    //Analyze each letter in a word
        //    foreach (char c in item)
        //    {
        //        LetterOccurenceAnalyzer(c);
        //    }
        //}

        //Analyze each words
        for (int i = 0; i < library.nameList.Count; i++)
        {
            LengthOccurenceAnalyzer(library.nameList[i]);

            //Analyze each letter in a word
            char previousLetter = 'a';            //Part of Next Letter Lookup Table
            for (int j = 0; j < library.nameList[i].Length; j++)
            {
                LetterOccurenceAnalyzer(library.nameList[i][j]);

                if (j > 0)
                {
                    //Input into Next Letter Lookup Table
                    int col, row;
                    col = ((int)previousLetter) - 97;            //First, convert the previousLetter to int
                    row = ((int)library.nameList[i][j]) - 97;    //Then, convert the current letter to int

                    nextLetterLookupTable[row, col]++;                                          //Add the counter in that letter

                }
                previousLetter = library.nameList[i][j];
                //Assign the current letter as the last letter for next iteration
            }
        }

        //Count all percentages
        LengthOccurencePercentageCounter();
        LetterOccurencePercentageCounter();
        NextLetterLookupTableCounter();

        library.console.text = "Analyzer successful. Please generate names next.";
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
        for (int i = 97; i <= 122; i++)
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

    #region Next Letter Lookup Table
    private void Print2DArray()
    {
        string temp = "";

        for (int i = 0; i < 27; i++)
        {
            //Print Column Header at 0
            if (i == 0)
            {
                temp += "   ";
                for (int k = 0; k < 26; k++)
                {
                    temp += ((char)(65 + k)) + " ";
                }
            }
            else
            {
                for (int j = 0; j < 27; j++)
                {
                    //Print Row Header at 0
                    if (j == 0)
                    {
                        temp += ((char)(65 + i - 1)) + " ";
                    }
                    else
                    {
                        //Actual Numbers
                        temp += nextLetterLookupTable[i - 1, j - 1] + " ";
                    }
                }
            }
            temp += "\n";
        }
        Debug.Log(temp);
    }

    private void Print2DArrayPercent()
    {
        string temp = "";

        for (int i = 0; i < 27; i++)
        {
            //Print Column Header at 0
            if (i == 0)
            {
                temp += "   ";
                for (int k = 0; k < 26; k++)
                {
                    temp += ((char)(65 + k)) + " ";
                }
            }
            else
            {
                for (int j = 0; j < 27; j++)
                {
                    //Print Row Header at 0
                    if (j == 0)
                    {
                        temp += ((char)(65 + i - 1)) + " ";
                    }
                    else
                    {
                        //Actual Numbers
                        temp += nextLetterLookupTablePercent[i - 1, j - 1] + " ";
                    }
                }
            }
            temp += "\n";
        }
        Debug.Log(temp);
    }

    private void NextLetterLookupTableCounter()
    {
        for (int i = 0; i < 26; i++)
        {
            //Count the total
            int totalOccurence = 0;
            for (int j = 0; j < 26; j++)
            {
                totalOccurence += nextLetterLookupTable[j,i];
            }

            //Now count the percentage in each column
            //That is, if it's greater than 0, divide by zero problem.
            if (totalOccurence != 0)
            {
                for (int j = 0; j < 26; j++)
                {
                    nextLetterLookupTablePercent[j,i] = (float)nextLetterLookupTable[j, i]/(float)totalOccurence;
                }
            }
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