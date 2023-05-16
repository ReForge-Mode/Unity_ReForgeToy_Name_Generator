using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayNames : MonoBehaviour
{
    [SerializeField] private Generator generator;
    [SerializeField] private Analyzer analyzer;         //This analyzer is just to find the longest names
    [SerializeField] private GameObject tmp;
    [SerializeField] private List<TextMeshProUGUI> tmpList;

    public void Awake()
    {
        InializeTMPList();
    }

    private void InializeTMPList()
    {
        for (int y = -75; y >= -255; y -= 45)
        {
            for (int x = -400; x <= 400; x += 200)
            {
                GameObject temp = Instantiate(tmp, transform);
                temp.transform.localPosition = new Vector3(x, y, 0);
                tmpList.Add(temp.GetComponent<TextMeshProUGUI>());
            }
        }
    }

    public void GenerateDisplayNames()
    {
        //If the Analyzer is empty, don't run this function
        if (analyzer.lengthOccurenceTable.percentage.Count == 0)
        {
            analyzer.library.console.text = "Error. Please run Analyzer first";
            return;
        }

        //To make the display perfectly square
        for (int i = 0; i < tmpList.Count; i++)
        {
            //Capitalize the first letter
            string temp = generator.GenerateRandomName();
            char[] a = temp.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            tmpList[i].text = new string(a);
        }

        analyzer.library.console.text = "Names generated. You can click any of the button again.";
    }
}
