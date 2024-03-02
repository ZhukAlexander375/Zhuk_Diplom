using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class WordDatabaseReader : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayText;

    private List<string> _wordList = new();
    private List<string> _usedWords = new();

    void Awake()
    {
        LoadWordDatabase();
    }

    void LoadWordDatabase()
    {
        TextAsset wordDatabase = Resources.Load<TextAsset>("wordDatabase");        

        if (wordDatabase != null)
        {
            string[] lines = wordDatabase.text.Split('\n');
            _wordList.AddRange(lines);
        }
        else
        {
            Debug.LogError("Word database file not found!");
        }
    }

    public string GetRandomWord()
    {
        if (_wordList.Count > 0)
        {
            if (_usedWords.Count == _wordList.Count)
            {                
                _usedWords.Clear();
                return "переиспользуем слова";
            }

            string randomWord;

            do
            {
                int randomIndex = Random.Range(0, _wordList.Count);
                randomWord = _wordList[randomIndex];
            }

            while (_usedWords.Contains(randomWord));

            _usedWords.Add(randomWord);

            //Debug.Log("заюзаные слова: " + string.Join(", ", _usedWords));
            
            return randomWord;
        }

        else
        {
            return "Word database is empty!";
        }
    }
}
