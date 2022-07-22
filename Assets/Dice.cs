using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dice", menuName = "Dice")]
public class Dice : ScriptableObject
{
    [CustomEditor(typeof(DiceFace))]
    [Serializable]
    public class DiceFace
    {
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private int value;
        [SerializeField]
        private DiceAction[] actions;
        public void ActivateFace(GameObject target, GameObject player)
        {
            foreach(DiceAction action in actions)
            {
                action.DoAction(target, player, value);
            }
            
        }
        public Sprite GetSprite()
            {
                return sprite;
            }
    }
    public string diceName;
    [TextArea(3,5)]
    public string description;
    public Sprite previewSprite;
    public DiceFace[] faces;
    [Range(0, 9)]
    public int rarity;
}

[CustomEditor(typeof(Dice)), CanEditMultipleObjects]
public class DiceEditor : Editor
{

}