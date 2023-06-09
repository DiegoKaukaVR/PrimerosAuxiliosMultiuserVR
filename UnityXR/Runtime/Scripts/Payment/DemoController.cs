﻿// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

#if !UNITY_EDITOR
#if UNITY_ANDROID
#define ANDROID_DEVICE
#elif UNITY_IPHONE
#define IOS_DEVICE
#elif UNITY_STANDALONE_WIN
#define WIN_DEVICE
#endif
#endif

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.PXR
{
    public class DemoController : MonoBehaviour
    {
        Callback callback;
        GameObject msg;
        string currentOrderID;
        public GameObject loading;
        public GameObject BG;
        public GameObject InputPanel;
        public delegate void showLoadingEventHandler();
        public static showLoadingEventHandler showLoading;

        void Awake()
        {
            Debug.Log(loading.name);
            Debug.Log(BG.name);
            showLoading += StopLoading;
            InputManager.doEnter += DoPayByCode;
            currentOrderID = "";
        }
        void Start()
        {
            msg = GameObject.Find("MassageInfo");
            InitDelegate();
            callback = new Callback();

            InputPanel.SetActive(false);

        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Joystick1Button1) || UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (InputPanel.activeInHierarchy)
                {
                    InputPanel.SetActive(false);
                }
                else
                {
                    Application.Quit();
                }
            }
        }

        void InitDelegate()
        {
            ArrayList btnsName = new ArrayList();

            btnsName.Add("Login");
            btnsName.Add("GetUserAPI");
            btnsName.Add("PayOne");
            btnsName.Add("PayCode");
            btnsName.Add("QueryOrder");

            foreach (string btnName in btnsName)
            {
                GameObject btnObj = GameObject.Find(btnName);
                Button btn = btnObj.GetComponent<Button>();
                btn.onClick.AddListener(delegate () { OnClick(btnObj); });
            }
        }

        void OnClick(GameObject btnObj)
        {
            switch (btnObj.name)
            {
                case "Login":
                    StartLoading();
                    LoginSDK.Login();
                    break;

                case "PayOne":
                    CommonDic.getInstance().setParameters("subject", "game");
                    CommonDic.getInstance().setParameters("body", "gamePay");
                    CommonDic.getInstance().setParameters("order_id", getRamdomTestOrderID());
                    CommonDic.getInstance().setParameters("total", "1");
                    CommonDic.getInstance().setParameters("goods_tag", "game");
                    CommonDic.getInstance().setParameters("notify_url", "www.picovr.com");
                    CommonDic.getInstance().setParameters("pay_code", "");

                    StartLoading();
                    PicoPaymentSDK.Pay(CommonDic.getInstance().PayOrderString());

                    break;
                case "PayCode":
                    InputPanel.SetActive(true);
                    break;

                case "QueryOrder":
                    StartLoading();
                    PicoPaymentSDK.QueryOrder(currentOrderID);
                    break;

                case "GetUserAPI":
                    StartLoading();
                    LoginSDK.GetUserAPI();
                    break;

            }
        }

        public string getRamdomTestOrderID()
        {
            currentOrderID = (Random.value * 65535).ToString();
            return currentOrderID;
        }

        private void StartLoading()
        {
            loading.SetActive(true);
            BG.SetActive(true);
        }

        public void StopLoading()
        {
            if (loading && BG)
            {
                loading.SetActive(false);
                BG.SetActive(false);
            }
            else
            {
                Debug.LogError("PXRLog User defined, non demo.");
            }

        }

        public void DoPayByCode()
        {
            CommonDic.getInstance().setParameters("subject", "game");
            CommonDic.getInstance().setParameters("body", "gamePay");
            CommonDic.getInstance().setParameters("order_id", getRamdomTestOrderID());
            CommonDic.getInstance().setParameters("total", "0");
            CommonDic.getInstance().setParameters("goods_tag", "game");
            CommonDic.getInstance().setParameters("notify_url", "www.picovr.com");
            CommonDic.getInstance().setParameters("pay_code", GameObject.Find("CodeText").GetComponent<Text>().text);
            Debug.Log("PXRLog DoPayByCode" + GameObject.Find("CodeText").GetComponent<Text>().text);
            StartLoading();
            GameObject.Find("CodeText").GetComponent<Text>().text = "";
            InputPanel.SetActive(false);
            PicoPaymentSDK.Pay(CommonDic.getInstance().PayOrderString());
        }

        bool VerifyLocalToken()
        {
            if (CommonDic.getInstance().access_token.Equals(""))
            {
                GameObject.Find("MassageInfo").GetComponent<Text>().text = "{code:exception,msg:Please log in first}";
                currentOrderID = "";
                StopLoading();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

