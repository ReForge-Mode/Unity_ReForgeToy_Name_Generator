using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Newtonsoft.Json.Linq;

public class TextAnalysis : MonoBehaviour
{
    // Reference to the TextFileReader script
    public TextFileReader textFileReader;// 2D array to store the occurences of each letter combination
    public int[,] letterOccurences = new int[26, 26];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnalyzeList();
        }
    }

    private void AnalyzeList()
    {
        // Iterate through each line in the output list from the TextFileReader script
        for (int i = 0; i < textFileReader.outputLines.Count; i++)
        {
            // Get the current line
            string line = textFileReader.outputLines[i];

            // Iterate through each character in the line
            for (int j = 0; j < line.Length - 1; j++)
            {
                // Make sure the current character and the next character are both letters
                if (char.IsLetter(line[j]) && char.IsLetter(line[j + 1]))
                {
                    // Get the ASCII value of the current character and the next character
                    int currentASCII = (int)char.ToUpper(line[j]) - 65;
                    int nextASCII = (int)char.ToUpper(line[j + 1]) - 65;

                    // Increment the occurences of the current character and the next character in the 2D array
                    letterOccurences[currentASCII, nextASCII]++;
                }
            }
        }
    }

    void OnGUI()
    {
        // Calculate the size of each cell in the grid
        int cellSize = 20;
        int gridWidth = 26 * cellSize;
        int gridHeight = 26 * cellSize;

        // Draw the grid background
        GUI.Box(new Rect(10, 10, gridWidth, gridHeight), "");

        // Draw the row and column headers
        for (int i = 0; i < 26; i++)
        {
            // Draw the column header
            GUI.Label(new Rect(30 + i * cellSize, 10, cellSize, cellSize), ((char)('A' + i)).ToString());

            // Draw the row header
            GUI.Label(new Rect(10, 30 + i * cellSize, cellSize, cellSize), ((char)('A' + i)).ToString());
        }

        // Draw the cell values
        for (int row = 0; row < 26; row++)
        {
            for (int col = 0; col < 26; col++)
            {
                GUI.Label(new Rect(30 + col * cellSize, 30 + row * cellSize, cellSize, cellSize), letterOccurences[row, col].ToString());
            }
        }
    }
}