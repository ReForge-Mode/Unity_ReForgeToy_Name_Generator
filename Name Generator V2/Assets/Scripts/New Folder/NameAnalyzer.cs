using UnityEngine;
using System.Collections.Generic;

public class NameAnalyzer : MonoBehaviour
{
    public TextFileReader textFileReader;

    // Declare a dictionary to store the Markov Chain
    private Dictionary<char, Dictionary<char, int>> markovChain;// Function to build the Markov Chain from the list of names

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            BuildMarkovChain(textFileReader.lines);
        }
    }

    public void BuildMarkovChain(List<string> names)
    {
        markovChain = new Dictionary<char, Dictionary<char, int>>();

        // Iterate through each name in the list
        foreach (string name in names)
        {
            // Add each letter in the name to the Markov Chain
            for (int i = 0; i < name.Length - 1; i++)
            {
                char currentLetter = name[i];
                char nextLetter = name[i + 1];

                // If the current letter is not already in the Markov Chain, add it
                if (!markovChain.ContainsKey(currentLetter))
                {
                    markovChain.Add(currentLetter, new Dictionary<char, int>());
                }

                // If the next letter is not already in the dictionary for the current letter, add it
                if (!markovChain[currentLetter].ContainsKey(nextLetter))
                {
                    markovChain[currentLetter].Add(nextLetter, 0);
                }

                // Increment the count for the next letter in the dictionary for the current letter
                markovChain[currentLetter][nextLetter]++;
            }
        }
    }

    // Function to get the Markov Chain
    public Dictionary<char, Dictionary<char, int>> GetMarkovChain()
    {
        return markovChain;
    }

    //private void OnGUI()
    //{
    //    if (markovChain != null)
    //    {
    //        // Draw the table header
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("", GUILayout.Width(20));
    //        for (char c = 'A'; c <= 'Z'; c++)
    //        {
    //            GUILayout.Label(c.ToString(), GUILayout.Width(20));
    //        }
    //        GUILayout.EndHorizontal();

    //        // Iterate through each letter in the Markov Chain
    //        for (char c = 'A'; c <= 'Z'; c++)
    //        {
    //            GUILayout.BeginHorizontal();
    //            GUILayout.Label(c.ToString(), GUILayout.Width(20));

    //            // Iterate through each next letter in the dictionary for the current letter
    //            for (char nextLetter = 'A'; nextLetter <= 'Z'; nextLetter++)
    //            {
    //                int count = 0;
    //                if (markovChain[c].ContainsKey(nextLetter))
    //                {
    //                    count = markovChain[c][nextLetter];
    //                }
    //                GUILayout.Label(count.ToString(), GUILayout.Width(20));
    //            }
    //            GUILayout.EndHorizontal();
    //        }
    //    }
    //}
}