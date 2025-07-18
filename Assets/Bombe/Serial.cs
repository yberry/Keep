using TMPro;
using UnityEngine;

public class Serial : Instance<Serial>
{
    private const string vowels = "AEIOUY";
    private const int nbCharSerial = 6;

    private string serial;

    public bool NumPair { get; private set; } = false;
    public bool HasVowel { get; private set; } = false;

    public void Generate()
    {
        char[] tab = new char[nbCharSerial];
        for (int i = 0; i < nbCharSerial; i++)
        {
            char ch;
            if (i < nbCharSerial - 1)
            {
                int index = Random.Range(0, 26);
                ch = (char)('A' + index);
                HasVowel |= vowels.Contains(ch);
            }
            else
            {
                int index = Random.Range(0, 10);
                ch = (char)('0' + index);
                NumPair = (index & 1) == 0;
            }
            tab[i] = ch;
        }
        serial = new string(tab);
        GetComponent<TextMeshPro>().text = serial;
    }
}
