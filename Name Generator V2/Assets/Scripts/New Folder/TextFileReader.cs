using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TextFileReader : MonoBehaviour
{
    // Declare a list to store the lines of text from the file
    public List<string> lines;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            OpenTextFile();
        }
    }

    // Function to open the text file using EditorUtility
    private void OpenTextFile()
    {
        string filePath = EditorUtility.OpenFilePanel("Select text file", "", "txt");
        if (filePath.Length != 0)
        {
            StreamReader reader = new StreamReader(filePath);
            lines = new List<string>();
            ReadTextFile(reader);
        }
    }

    // Function to read the text file line by line and add each line to the list
    private void ReadTextFile(StreamReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }
    }

    // Function to access the list of lines from the text file
    public List<string> GetLines()
    {
        return lines;
    }
}