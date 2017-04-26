using UnityEngine;
using System.Collections.Generic;

public class SequencesFils : Module {

    private static readonly Dictionary<Color, List<string>> tab = new Dictionary<Color, List<string>>()
    {
        { Color.red, new List<string>() { "C", "B", "A", "AC", "B", "AC", "ABC", "AB", "B" } },
        { Color.blue, new List<string>() { "B", "AC", "B", "A", "B", "BC", "C", "AC", "A" } },
        { Color.black, new List<string>() { "ABC", "AC", "B", "AC", "B", "BC", "AB", "C", "C" } }
    };
}
