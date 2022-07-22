using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "", menuName = "Dice Face Effects/~~~")]
public abstract class DiceAction : ScriptableObject
{
    // Start is called before the first frame update
    public virtual void DoAction(GameObject target, GameObject player, int value)
    {
        
    }
}
