using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObj
{
    public float attentionData;
    public float recallData;

    public ScoreObj(float attention, float recall)
    {
        attentionData = attention;
        recallData = recall;
    }
}
