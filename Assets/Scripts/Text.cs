using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Text", menuName = "Text/Create New Text")]
public class TextSO : ScriptableObject
{
    public Text text;
}
[System.Serializable]
public class Text
{
    public string[] m_Text;
}

