using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class Library : MonoBehaviour
{
    public List<string> nameList;

    [Header("References")]
    [SerializeField] private FileManager fileManager;

    private void Start()
    {
        nameList = fileManager.GetNameList();
    }


}
