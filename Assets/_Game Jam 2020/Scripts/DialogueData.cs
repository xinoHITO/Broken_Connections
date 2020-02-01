using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/Dialogue Data", order = 1)]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string text;
        public string[] anwers;
        public int correctAnswerIndex;
    }
    [System.Serializable]
    public class Dialogue
    {
        public string[] lines;
        public Question question;
    }
    public Dialogue[] dialogues;
}