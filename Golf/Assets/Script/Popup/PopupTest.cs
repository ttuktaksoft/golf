using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTest : Popup
{
    public InputField ADDRESS_1_MIN;
    public InputField ADDRESS_1_MAX;
    public InputField ADDRESS_2_MIN;
    public InputField ADDRESS_2_MAX;
    public InputField ADDRESS_3_MIN;
    public InputField ADDRESS_3_MAX;

    public InputField BACKSWING_1_MIN;
    public InputField BACKSWING_1_MAX;
    public InputField BACKSWING_2_MIN;
    public InputField BACKSWING_2_MAX;
    public InputField BACKSWING_3_MIN;
    public InputField BACKSWING_3_MAX;

    public InputField IMPACT_1_MIN;
    public InputField IMPACT_1_MAX;
    public InputField IMPACT_2_MIN;
    public InputField IMPACT_2_MAX;
    public InputField IMPACT_3_MIN;
    public InputField IMPACT_3_MAX;

    public Button OK;
    public Button Cancel;

    private int[] REF_DATA;
    private bool Man;
    public Text Title;

    private void Awake()
    {
        OK.onClick.AddListener(OnClickOK);
        Cancel.onClick.AddListener(OnClickCancel);
    }

    public void Start()
    {
        PopupMgr.Instance.RegisterPopup(this);
    }

    public PopupTest()
        : base(PopupMgr.POPUP_TYPE.TEST)
    {

    }

    public class PopupData : PopupBaseData
    {
        public bool Man;

        public PopupData(bool man)
        {
            Man = man;
        }
    }

    public override void SetData(PopupBaseData data)
    {
        var popupData = data as PopupData;
        if (popupData == null)
            return;

        Man = popupData.Man;
        if (popupData.Man)
        {
            Title.text = "남자";
            REF_DATA = CommonData.REF_MAN;
        }
        else
        {
            Title.text = "여자";
            REF_DATA = CommonData.REF_WOMAN;
        }
        RefreshData();
    }

    public void RefreshData()
    {
        ADDRESS_1_MIN.text = string.Format("{0}", REF_DATA[2]);
        ADDRESS_1_MAX.text = string.Format("{0}", REF_DATA[3]);
        ADDRESS_2_MIN.text = string.Format("{0}", REF_DATA[0]);
        ADDRESS_2_MAX.text = string.Format("{0}", REF_DATA[1]);
        ADDRESS_3_MIN.text = string.Format("{0}", REF_DATA[4]);
        ADDRESS_3_MAX.text = string.Format("{0}", REF_DATA[5]);

        BACKSWING_1_MIN.text = string.Format("{0}", REF_DATA[8]);
        BACKSWING_1_MAX.text = string.Format("{0}", REF_DATA[9]);
        BACKSWING_2_MIN.text = string.Format("{0}", REF_DATA[6]);
        BACKSWING_2_MAX.text = string.Format("{0}", REF_DATA[7]);
        BACKSWING_3_MIN.text = string.Format("{0}", REF_DATA[10]);
        BACKSWING_3_MAX.text = string.Format("{0}", REF_DATA[11]);

        IMPACT_1_MIN.text = string.Format("{0}", REF_DATA[14]);
        IMPACT_1_MAX.text = string.Format("{0}", REF_DATA[15]);
        IMPACT_2_MIN.text = string.Format("{0}", REF_DATA[12]);
        IMPACT_2_MAX.text = string.Format("{0}", REF_DATA[13]);
        IMPACT_3_MIN.text = string.Format("{0}", REF_DATA[16]);
        IMPACT_3_MAX.text = string.Format("{0}", REF_DATA[17]);
    }

    public void OnClickOK()
    {
        REF_DATA[2] = int.Parse(ADDRESS_1_MIN.text.ToString());
        REF_DATA[3] = int.Parse(ADDRESS_1_MAX.text.ToString());
        REF_DATA[0] = int.Parse(ADDRESS_2_MIN.text.ToString());
        REF_DATA[1] = int.Parse(ADDRESS_2_MAX.text.ToString());
        REF_DATA[4] = int.Parse(ADDRESS_3_MIN.text.ToString());
        REF_DATA[5] = int.Parse(ADDRESS_3_MAX.text.ToString());

        REF_DATA[8] = int.Parse(BACKSWING_1_MIN.text.ToString());
        REF_DATA[9] = int.Parse(BACKSWING_1_MAX.text.ToString());
        REF_DATA[6] = int.Parse(BACKSWING_2_MIN.text.ToString());
        REF_DATA[7] = int.Parse(BACKSWING_2_MAX.text.ToString());
        REF_DATA[10] = int.Parse(BACKSWING_3_MIN.text.ToString());
        REF_DATA[11] = int.Parse(BACKSWING_3_MAX.text.ToString());

        REF_DATA[14] = int.Parse(IMPACT_1_MIN.text.ToString());
        REF_DATA[15] = int.Parse(IMPACT_1_MAX.text.ToString());
        REF_DATA[12] = int.Parse(IMPACT_2_MIN.text.ToString());
        REF_DATA[13] = int.Parse(IMPACT_2_MAX.text.ToString());
        REF_DATA[16] = int.Parse(IMPACT_3_MIN.text.ToString());
        REF_DATA[17] = int.Parse(IMPACT_3_MAX.text.ToString());

        if (Man)
            CommonData.REF_MAN = REF_DATA;
        else
            CommonData.REF_WOMAN = REF_DATA;



        PopupMgr.Instance.DismissPopup();
    }

    public void OnClickCancel()
    {
        PopupMgr.Instance.DismissPopup();
    }
}
