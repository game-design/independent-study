using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;


public class loggerrequest : MonoBehaviour {

    //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://nodejs-dev.imtc.gatech.edu:8081/user");
    private string base_url = "http://nodejs-dev.imtc.gatech.edu:8081/";
    private string login = "dev";
    private string password = "dev";
    private string game_secret = "M7RKdi-yyHPWQm1gfDpzxXCB1XiuTcUvRKlIHd9HItg";
    private string game_id = "8809";
    private string game_name = "8809_project";
    public static int session_id = 0;
    public string player_name = "Dingfeng Shao";
    
    // Use this for initialization
    void Start () {
        Debug.Log("start");
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(base_url+ "gameplay/:8809");
        string send_item = "{\"session\":"+
            "{\"id\":\"test\",\"player\":\"test\",\"game\":\"cs8809_project\",\"version\":\"1.0\"},"+
            "\"play_events\":"+
            "[{\"time\":\"2015 - 02 - 17T22: 43:45 - 5:00\",\"event\":\"PowerUp.FireBall\",\"value\":\"1.0\",\"level\":\"1-1\",\"position\":\"(42,42)\"},"+
            "{\"time\":\"2015-02-17T22:45:45-5:00\",\"event\":\"PowerUp.Mushroom\",\"value\":\"2.0\",\"level\":\"1-1\",\"position\":\"(142,142)\"}]}";
        //string hash = CalculateMD5Hash(send_item + game_secret);
        //webRequest.Headers[HttpRequestHeader.Authorization] = "mac " + game_id + ":" + hash;
        webRequest.Credentials = new NetworkCredential(login, password);
        webRequest.ContentType = "application/json; charset=utf-8";
        webRequest.Method = "POST";
        
        
        using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
        {
            streamWriter.Write(send_item);
            streamWriter.Flush();
        }
        
        var httpResponse = (HttpWebResponse)webRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Debug.Log(result);
        }
        

        //hash = md5(request.body + GAME_SECRET).toHex()
//request.headers.authorization = "mac " + GAME_ID + ":" + hash

    }
	
	// Update is called once per frame
	void Update () {
        
    }




    private string CalculateMD5Hash(string input)

    {


        MD5 md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(input);

        byte[] hash = md5.ComputeHash(inputBytes);


        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)

        {

            sb.Append(hash[i].ToString("X2"));

        }

        return sb.ToString();

    }


}
