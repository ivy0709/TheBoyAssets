using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClick : MonoBehaviour {
    public void OnPress(bool isPressed)
    {
        if (isPressed == false)
        {
            StartMenuController._instance.OnCharacterClicked(transform.parent.gameObject);
        }
    }
}
