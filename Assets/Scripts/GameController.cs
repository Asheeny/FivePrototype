using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //[SerializeField]
    //private List<GameObject> stages = null;
    //[SerializeField]
    //private List<GameObject> obstaclePrefabs = null;

    private int curStage = 0;
    [SerializeField]
    private GameObject stagePrefab = null;

    private GameObject stage = null;

    [SerializeField]
    private bool canFail = true;

    private bool started = false;
    [SerializeField]
    private PlayerController player = null;
    private Vector3 startingPos;
    [SerializeField]
    private ParticleSystem deathEffect = null;

    [SerializeField]
    private Text cDown = null;
    private const float maxTime = 5;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    [SerializeField]
    private GameObject lvlComplete = null;
    private bool complete = false;

    void Start()
    {
        startingPos = new Vector3(-7.6f, -1.822276f, 0.0f);
        stage = stagePrefab;
        LoadStage();
    }

    void Update()
    {
        if (complete)
            return;

        if (started)
        {
            Countdown();
        }
        else
        {
            cDown.text = ToRoman(timer.ToString("F0"));
        }

        if (player.DetectMovement() && player.isActiveAndEnabled)
        {
            started = true;
        }
    }

    private void Countdown()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            cDown.text = ToRoman(timer.ToString("F0"));
        }
        else if (timer <= 0.0f && !doOnce && canFail)
        {
            canCount = false;
            doOnce = true;
            StageFailed();
        }
    }

    private string ToRoman(string seconds)
    {
        switch (seconds)
        {
            case "1":
                return "I";
            case "2":
                return "II";
            case "3":
                return "III";
            case "4":
                return "IV";
            case "5":
                return "V";
            default:
                return "";
        }
    }

    private void StageFailed()
    {
        player.gameObject.SetActive(false);
        curStage = 0;       
        StartCoroutine(StageIncrement(2f, true));
    }

    private void LoadStage()
    {
        if (curStage < 10)
        {
            curStage++;
        }

        foreach(StageComponent sC in stage.GetComponentsInChildren<StageComponent>(true))
        {
            if(sC.GetActiveFromStage() <= curStage)
            {
                sC.gameObject.SetActive(true);
            }
        }

        if (!complete)
        {
            timer = maxTime;
            started = false;
        }

        if (!player.gameObject.activeSelf)
        {
            player.gameObject.SetActive(true);
            player.SetStun(false);
            player.transform.position = startingPos;
        }
    }

    public IEnumerator StageIncrement(float delay, bool failed)
    {
        player.gameObject.SetActive(false);
        if (!failed)
        {
            FindObjectOfType<AudioController>().Play("Portal_Enter");
            yield return new WaitForSeconds(delay);
            if (curStage == 10)
            {
                LevelComplete();
            }
        }
        else
        {
            Destroy(Instantiate(deathEffect, player.transform.position, Quaternion.identity), delay);
            FindObjectOfType<AudioController>().Play("Player_Death");
            yield return new WaitForSeconds(delay);
        }

        canCount = true;
        doOnce = false;
        player.enabled = false;
        LoadStage();
        yield return new WaitForSeconds(0.5f);
        player.enabled = true;
    }

    private void LevelComplete()
    {
        complete = true;
        cDown.GetComponent<Animator>().SetBool("lvlComp", true);
        Instantiate(lvlComplete, new Vector3(0, 2.5f, 0), Quaternion.identity);
    }
}
