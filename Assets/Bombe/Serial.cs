using TMPro;
using UnityEngine;

public class Serial : Instance<Serial>
{
    private const string VOWELS = "AEIOUY";
    private const int SERIAL_LENGTH = 6;

    private string serial;

    public bool LastEven { get; private set; } = false;
    public bool HasVowel { get; private set; } = false;

    public void Generate()
    {
        char[] tab = new char[SERIAL_LENGTH];
        for (int i = 0; i < SERIAL_LENGTH; i++)
        {
            char ch;
            if (i < SERIAL_LENGTH - 1)
            {
                int index = Random.Range(0, 26);
                ch = (char)('A' + index);
                HasVowel |= VOWELS.Contains(ch);
            }
            else
            {
                int index = Random.Range(0, 10);
                ch = (char)('0' + index);
                LastEven = (index & 1) == 0;
            }
            tab[i] = ch;
        }
        serial = new string(tab);
        GetComponent<TextMeshPro>().text = serial;

        float ty, tz;
        float rx, ry;

        // Up / Down
        if (Bombe.HeadsOrTails)
        {
            ty = 1.01f;
            rx = 90f;
        }
        else
        {
            ty = -1.01f;
            rx = -90f;
        }

        // Front / Back
        if (Bombe.HeadsOrTails)
        {
            tz = -0.8f;
            ry = 0f;
        }
        else
        {
            tz = 0.8f;
            ry = 180f;
        }

        transform.localPosition = new Vector3(Random.Range(-1, 2), ty, tz);
        transform.localEulerAngles = new Vector3(rx, ry, 0f);
    }
}
