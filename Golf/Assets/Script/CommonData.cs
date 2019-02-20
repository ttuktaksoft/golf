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

    public static float[] LEVEL_COVER = {
        0.0f, 6.0f, 3.0f, 0.0f
    };

    public enum TRAINING_TYPE
    {
        TRAINING_POSE,
        TRAINING_TEMPO,
    }
   
    public enum TRAINING_POSE
    {
        TRAINING_ADDRESS = 0,
        TRAINING_BACKSWING = 6,
        TRAINING_IMPACT = 12,
    }
    public enum TRAINING_ANGLE
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
        TRAINING_TEMPO_BEGINER,
        TRAINING_TEMPO_AMA,
        TRAINING_TEMPO_PRO
    }

    public enum GRADE_TYPE
    {
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
    }

    public static int ANGLE_MAX_LEVEL = 4;

    public static int[] TRAINING_TIME = { 5, 10, 15, 20 };
    public static int TRAINING_READY_TIME = 3;
    public static float TEMPO_TRAINING_WAIT_TIME = 7f;

    public static string[] TEMP_ALARM_MSG =
    {
        "1월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
        "2월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
        "3월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
    };
}
