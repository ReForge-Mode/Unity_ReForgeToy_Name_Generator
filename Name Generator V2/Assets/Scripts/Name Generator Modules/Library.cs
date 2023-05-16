using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
public class Library : MonoBehaviour
{
    public List<string> nameList;

    [Header("References")]
    [SerializeField] private FileManager fileManager;
    public TextMeshProUGUI console;

    public void GetNameList()
    {
        nameList = fileManager.GetNameList();
        console.text = "Text File loaded. Please run Analyzer next";
    }
}
