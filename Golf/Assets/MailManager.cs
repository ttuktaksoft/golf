using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System.Net;
using System;

public class MailManager : MonoBehaviour
{

    public static MailManager _instance = null;
    public static MailManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MailManager>() as MailManager;
            }
            return _instance;
        }
    }


    private MailAddress sendAddress = null;
    private MailAddress toAddress = null;
    private string sendPassWord = "";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Init("ttuktaksoft@gmail.com");
        SetToAddress("ttuktaksoft@gmail.com");
        SendEmail("제목", "내용");
    }

    public void Init(string sendMail)
    {
        sendAddress = new MailAddress(sendMail);
    }
    public void SetToAddress(string toMail)
    {
        toAddress = new MailAddress(toMail);
    }

    public string SendEmail(string subject, string body)
    {
        SmtpClient smtp = null;
        MailMessage message = null;

        try
        {
            smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(sendAddress.Address, sendPassWord),
                Timeout = 20000
            };

            message = new MailMessage(sendAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            smtp.Send(message);

            return "send mail success";
        }
        catch(Exception e)
        {
            return "send mail fail";
        }
        finally
        {
            if (smtp != null) smtp.Dispose();
            if (message != null) message.Dispose();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
