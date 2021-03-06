﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    // ROTATE, BEND, SIDE 순
    // ADDRESS, BACKSWING, IMPACT 순
    public static int[] REF_MAN = {
        -2,  6, 35, 45,  -1, 5,             // 어드레스
      -86, -74,  2, 15, -45, -39,           // 백스윙
       26,  34, 29, 42,  24,  33            // 임팩트
    };

    public static int[] REF_WOMAN = {
        -1,  6, 36, 47,  -2, 3,
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
        TRAINING_ENTER,
        TRAINING_START,
        TRAINING_RETURN,

        TRAINING_SUCCESS_5SEC,
        TRAINING_SUCCESS_10SEC,
        TRAINING_SUCCESS_15SEC,
        TRAINING_SUCCESS_20SEC,

        TRAINING_TEMPO_BEGINER,
        TRAINING_TEMPO_AMA,
        TRAINING_TEMPO_PRO,   

        TRAINING_ROTATION_LEFT,
        TRAINING_ROTATION_RIGHT,
        TRAINING_BEND_UP,
        TRAINING_BEND_DOWN,
        TRAINING_SIDE_RIGHT,
        TRAINING_SIDE_LEFT,

        TRAINING_COUNT,

        TRAINING_ROTATION_OK,
        TRAINING_BEND_OK,
        TRAINING_SIDE_OK,

        TRAINING_INTRO,
        TRAINING_COUNTDOWN
    }

    public static int[] GRADE_POINT =
    {
        10000,
        20000,
        30000,
        40000,
        50000,
        60000,
    };
    public enum PURCHASE_ITEM
    {
        PURCHASE_MFS = 0,
        PURCHASE_COCONUT,
        PURCHASE_COFFEE
    }

    public enum SEASON_REWARD_TYPE
    {
        GIFTCON,
        PURCHASE
    }

    public static int ANGLE_MAX_LEVEL = 4;

    public static string[] GRADE_STR = { "동메달", "은메달", "금메달", "버디", "이글", "알바트로스" };
    public static string[] GRADE_IMG_STR = { "grade_1", "grade_2", "grade_3", "grade_4", "grade_5", "grade_6" };

    public static int[] TRAINING_TIME = { 5, 10, 15, 20 };
    public static int TRAINING_READY_TIME = 2;
    public static float TEMPO_TRAINING_WAIT_TIME = 7f;
    public static float ANGLE_OFFSET = 30f;
    public static int MAX_PRACTICE_COUNT = 25;

    public static string[] TEMP_ALARM_MSG =
    {
        "1월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
        "2월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
        "3월 아카데미 특강안내\n\nPGA 그린적중률 1위를 기록한 제이슨 데이입니다.\n\n파워풀하고 아주 간결한 스윙 스타일로 유명합니다.\n제이슨 데이가 시합 직전에 연습하는 패턴에서\n몇 개의 공을 치는지 정리해보았습니다.\n60 웨지샷 20~30미터 거리 6개\n60도 러프에서의 연습 6개\n그린 주변의 벙커샷 5개\n샌드웨지 어프로치샷 4개\n9번 아이언 풀스윙 8개\n7번 아이언 풀스윙 5개\n5번 아이언 풀스윙 9개",
    };

    public struct _sPurchaseInfo
    {
        public CommonData.PURCHASE_ITEM PurchaseType;

        public string PurchaseDate;

        public string PurchaseName;
        public string PurchaseAddress;
        public string PurchaseNumber;

        public string PurchasePrice;
    }

    public static string KAKAO_REST_API_KEY = "96fdf25c2067947d06be784620aa70c2";
    public static string KAKAO_REDIRECT_URI = "https://golfhometraining.firebaseapp.com/kakaoLogin.html";
    public static string KAKAO_PROPERTIES_FB = "properties.firebasekey";
    public static string KAKAO_PROPERTIES_FB_D = "firebasekey";
}
