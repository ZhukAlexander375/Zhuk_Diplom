using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class WordDatabaseReader : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayText;

    private List<string> _wordList = new List<string>();

    void Awake()
    {
        LoadWordDatabase();
    }

    void LoadWordDatabase()
    {        
        string filePath = "Assets/StreamingAssets/wordDatabase.txt";

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            _wordList.AddRange(lines);
        }
        else
        {
            Debug.LogError("Word database file not found!");
        }
    }

    public void DisplayRandomWord()
    {
        if (_wordList.Count > 0)
        {
            int randomIndex = Random.Range(0, _wordList.Count);
            string randomWord = _wordList[randomIndex];
            _displayText.text = randomWord;
        }
        else
        {
            _displayText.text = "Word database is empty!";
        }
    }
}
