﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public static int GetExpRequiredByLevel(int level)
    {
        if (level < 1)
            return -1;
        int count = level - 1;
        return count * 100 + count * (count - 1) * 5;
    }
}
