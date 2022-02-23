using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System;

public class DisplayScore : MonoBehaviour
{

    public Text attentionText;
    public Text recallText;
    public Text visuoText;

    public Text code;

    [DllImport("__Internal")]
    private static extern void InsertData(string tableName, string code, 
        string age, string race, string gender, string attentionData, string recallData, string visualData, float attentionScore, float recallScore, float visualScore, string timestamp);


    public void StringCallback(string info)
    {
        Debug.Log(info);
    }


    void Start()
    {
        List<int>[] attentionScoreTotal = ShapeSpawner.returnScore();
        int[] recallScoreTotal = RecallGame.returnScore();
        float visuoScore = GenerateShapes.returnScore();

        float attentionScore = 0;
        foreach(int num in attentionScoreTotal[0])
        {
            attentionScore += num;
        }

        System.Random generator = new System.Random();
        string codeStr = generator.Next(10000000, 100000000).ToString("D8");

        float recallScore = recallScoreTotal[0];

        if(recallScore > 30)
        {
            recallScore = 30;
        }

        float recallScorePercent = recallScore * 100 / 30;

        attentionText.text = "Attention: Your Final Score was " + attentionScore + "/100!";
        recallText.text = "Recall: Your Final Score was " + recallScore + "/30!";
        visuoText.text = "Visuospatial: Your Final Score was " + visuoScore + "/15";


        List<string> attentionData = DataStorage._attentionData;
        List<string> recallData = DataStorage._recallData;
        List<string> visualData = DataStorage._visualData;
        string age = DataStorage._age;
        string race = DataStorage._race;
        string gender = DataStorage._gender;

        string attention = "";
        string recall = "";
        string visuo = "";

        code.text += " " + codeStr;

        foreach (string a in attentionData)
        {
            attention += (a);
        }
        foreach (string r in recallData)
        {
            recall += (r);
        }
        foreach (string r in visualData)
        {
            visuo += (r);
        }

        string timestamp = DateTime.Now.ToString(@"MM\/dd\/yyyy");

        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            
            InsertData("neuro-spect-data", codeStr, age, race, gender, attention, recall, visuo, attentionScore, recallScore, visuoScore, timestamp);
        }

        /*string path = GetBaseAPIURL() + "/assessments/neurospect";

        string jsonStr = "{\"recallScore\": " + recallScorePercent.ToString() + ", \"attentionScore\": " + attentionScore.ToString() + "}";

        UnityWebRequest www = UnityWebRequest.Put(path, jsonStr);
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Authorization", "Bearer " + GetToken());
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        www.SendWebRequest();

        if (www.responseCode == 200 || www.responseCode == 201)
        {
            code.gameObject.SetActive(false);
            Debug.Log("Success: " + www.responseCode);
            NextScreen();
        } else if(www.responseCode == -1)
        {
            code.gameObject.SetActive(true);
            code.text = "We are unable to post your results to the server. Please try again later.";
            Debug.Log("Unsuccessful (Not submitted): " + www.responseCode);
        }
        else
        {
            Debug.Log("Unsuccessful: " + www.responseCode);
            code.gameObject.SetActive(true);
            code.text = "We are unable to post your results to the server. Please try again later.";
        }


        if(www.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Unsuccessful: " + www.responseCode);
            code.gameObject.SetActive(true);
            code.text = "We are unable to post your results to the server. Please try again later.";
        } else
        {
            code.gameObject.SetActive(false);
            Debug.Log("Success: " + www.responseCode);
            NextScreen();
        }*/
    }
}
