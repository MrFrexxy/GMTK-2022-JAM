using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dice", menuName = "Dice")]
public class Dice : ScriptableObject
{
    [Serializable]
    public class DiceFace
    {
        enum FaceType : int
        {
            Attack = 0,
            SelfHarm = 1,
            Heal = 2,
            EnemyHeal = 3,
            Special = 4
        }
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private FaceType type;
        [SerializeField]
        private int value;
        [SerializeField]
        private GameObject specialAction;
        private DiceAction specAction;
        public void ActivateFace(GameObject target, GameObject player)
        {
            if((int)type == 0)
            {
                target.GetComponent<StatusManager>().AddHealth(-value);
            }
            if((int)type == 1)
            {
                player.GetComponent<StatusManager>().AddHealth(-value);
            }
            if((int)type == 2)
            {
                player.GetComponent<StatusManager>().AddHealth(value);
            }
            if((int)type == 3)
            {
                target.GetComponent<StatusManager>().AddHealth(value);
            }
            if((int)type == 4)
            {
                specAction = specialAction.GetComponent(typeof(DiceAction)) as DiceAction;
                specAction.DoAction(target, player, value);
            }
        }
        public void DoAction<T>(T param, GameObject target, GameObject player, int value) where T : DiceAction
        {
            param.DoAction(target, player, value);
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
