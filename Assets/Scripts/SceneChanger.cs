using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneChanger : MonoBehaviour
{
    
    public Animator anim;

    void Start()
    {
        anim.SetBool("fade", false);
        Debug.Log(DateTime.Now.ToString(@"MM\/dd\/yyyy"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => GetComponent<SpriteRenderer>().color.a == 1);
        SceneManager.LoadScene(1);
    }
}
