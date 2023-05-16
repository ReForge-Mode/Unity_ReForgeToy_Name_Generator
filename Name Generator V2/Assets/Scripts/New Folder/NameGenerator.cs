using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{
    // Reference to the NameAnalyzer script
    public NameAnalyzer nameAnalyzer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            string randomName = GenerateRandomName();
            Debug.Log("Random name: " + randomName);
        }
    }

    // Function to generate a random name based on the Markov Chain
    public string GenerateRandomName()
    {
        // Get the Markov Chain from the NameAnalyzer script
        Dictionary<char, Dictionary<char, int>> markovChain = nameAnalyzer.GetMarkovChain();

        // Choose a random letter to start the name
        char currentLetter = GetRandomLetter(markovChain.Keys);
        string name = currentLetter.ToString();

        // Keep choosing the next letter in the name until the dictionary for the current letter is empty
        while (markovChain[currentLetter].Count > 0)
        {
            currentLetter = GetWeightedRandomLetter(markovChain[currentLetter]);
            name += currentLetter;
        }

        return name;
    }

    // Function to get a random letter from a list of letters
    private char GetRandomLetter(ICollection<char> letters)
    {
        int index = Random.Range(0, letters.Count);
        int i = 0;
        foreach (char letter in letters)
        {
            if (i == index)
            {
                return letter;
            }
            i++;
        }
        return ' ';
    }

    // Function to get a weighted random letter from a dictionary of letters and their count
    private char GetWeightedRandomLetter(Dictionary<char, int> letters)
    {
        int totalCount = 0;
        foreach (int count in letters.Values)
        {
            totalCount += count;
        }
        int randomValue = Random.Range(0, totalCount);
        int currentCount = 0;
        foreach (char letter in letters.Keys)
        {
            currentCount += letters[letter];
            if (randomValue < currentCount)
            {
                return letter;
            }
        }
        // Return the first letter in the dictionary if no other letter is chosen
        return letters.Keys.GetEnumerator().Current;
    }
}