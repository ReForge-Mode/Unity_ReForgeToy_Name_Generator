using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FileManager : MonoBehaviour
{
    [SerializeField] private List<string> nameList;


    /// <summary>
    /// This is the function called to start reading names from text files
    /// </summary>
    /// <returns></returns>
    public List<string> GetNameList()
    {
        //In case the list is not empty, clear it all out
        nameList.Clear();


        string path = OpenFileExplorer();
        if(path != null)
        {
            ReadTextFile(path);
            return nameList;
        }

        return null;
    }

    /// <summary>
    /// This is the function to read the file line by line
    /// </summary>
    /// <param name="file_path"></param>
    private void ReadTextFile(string file_path)
    {
        StreamReader inputStream = new StreamReader(file_path);

        while (!inputStream.EndOfStream)
        {
            string inputLine = inputStream.ReadLine();
            
            // Do Something with the input. 
            HandleInputPerLine(inputLine);
        }

        inputStream.Close();
    }

    /// <summary>
    /// This is the function that will open the windows dialogue to find the text file
    /// </summary>
    /// <returns></returns>
    private string OpenFileExplorer()
    {
        return EditorUtility.OpenFilePanel("Find Text File (.txt)", "", "txt");
    }

    /// <summary>
    /// This is the function to handle what to do per line in the text file
    /// </summary>
    /// <param name="inputLine"></param>
    private void HandleInputPerLine(string inputLine)
    {
        //In this case, just add it to the List
        nameList.Add(inputLine);
    }

}
