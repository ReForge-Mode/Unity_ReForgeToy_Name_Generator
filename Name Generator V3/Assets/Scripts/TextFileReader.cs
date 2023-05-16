using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;

public class TextFileReader : MonoBehaviour
{
    // List to store the output of each line
    public List<string> outputLines = new List<string>(); void Start()
    {
        // Open the text file
        OpenTextFile();
    }

    // Function to open the text file
    void OpenTextFile()
    {
        // Use EditorUtility to open the file browser and select a text file
        string filePath = EditorUtility.OpenFilePanel("Select Text File", "", "txt");

        // Read the text file and store the lines in an array
        string[] lines = File.ReadAllLines(filePath);

        // Call the ProcessLines function and pass in the lines array
        ProcessLines(lines);
    }

    // Function to process the lines from the text file
    void ProcessLines(string[] lines)
    {
        // Iterate through each line in the array
        foreach (string line in lines)
        {
            // Remove any non-letter characters from the line
            string processedLine = Regex.Replace(line, @"[^a-zA-Z]", "");

            // Add the processed line to the output list
            outputLines.Add(processedLine);
        }
    }

}