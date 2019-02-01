using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    // TURN, BEND, SIDE 순
    // ADDRESS, BACKSWING, IMPACT 순
    public static int[] REF_MAN = {
        7,  17, 35, 45,  11, 18,
      -86, -74,  2, 15, -45, -39,
       26,  34, 29, 42,  24,  33
    };

    public static int[] REF_WOMAN = {
        7,  10, 36, 47,  9, 12,
      -96, -83, -2, 13, -46, -40,
       25,  39, 27, 45,  21,  31
    };

    public enum TRAINING_MODE
    {
        TRAINING_ADDRESS = 0,
        TRAINING_BACKSWING = 6,
        TRAINING_IMPACT = 12,
        TRAINING_TEMPO = 3
    }
    public enum TRAINING_ANGLE_MODE
    {
        TRAINING_ANGLE_SIDE,
        TRAINING_ANGLE_TURN,
        TRAINING_ANGLE_BEND,
    }

    public enum GENDER
    {
        GENDER_MAN = 0,
        GENDER_WOMAN  = 1
    }

    public enum SOUND_TYPE
    {
        BUTTON,
        TRAINING_START,
        TRAINING_SUCCESS,
    }

}
