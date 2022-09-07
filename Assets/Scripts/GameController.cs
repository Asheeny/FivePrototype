using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int curStage = 0;
    [SerializeField]
    private GameObject stage = null;

    [SerializeField]
    private bool canFail = true;

    private bool started = false;
    [SerializeField]
    private PlayerController player = null;
    [SerializeField]
    private ParticleSystem deathEffect = null;

    [SerializeField]
    private GameObject cDown = null;
    private const float maxTime = 5;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    [SerializeField]
    private GameObject lvlComplete = null;
    private bool complete = false;

    private CinemachineController cinemachineController = null;
    private List<StageComponent> stageComponents = new List<StageComponent>();

    private void Awake()
    {
        cinemachineController = FindObjectOfType<CinemachineController>();
    }

    void Start()
    {
        foreach (StageComponent sC in stage.GetComponentsInChildren<StageComponent>(true))
        {
            stageComponents.Add(sC);
        }

        StartCoroutine(StageIncrement(3f, false));
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
            //cDown.text = ToRoman(timer.ToString("F0"));
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
            //cDown.text = ToRoman(timer.ToString("F0"));
            //if(timer > 2)
            //    cDown.GetComponentInParent<Animator>().SetTrigger("SwitchTimer");
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
        
        foreach(StageComponent sC in stageComponents)
        {
            if(sC.GetActiveFromStage() <= curStage)
            {          
                sC.transform.gameObject.SetActive(true);
            }
        }

        if (!complete)
        {
            timer = maxTime;
            started = false;
        }
    }

    public IEnumerator StageIncrement(float delay, bool failed)
    {
        cinemachineController.SwitchPriority(true);
        player.gameObject.SetActive(false);

        if (!failed)
        {
            FindObjectOfType<AudioController>().Play("Portal_Enter");
            if (curStage == 10)
            {
                LevelComplete();
            }
        }
        else
        {
            Destroy(Instantiate(deathEffect, player.transform.position, Quaternion.identity), delay);
            FindObjectOfType<AudioController>().Play("Player_Death");
        }

        canCount = true;
        doOnce = false;

        LoadStage();
        yield return new WaitForSeconds(delay);
        if(!complete)
        {
            cinemachineController.SwitchPriority(false);
            player.ResetPlayer();
        }
    }

    private void LevelComplete()
    {
        complete = true;
        //cDown.GetComponent<Animator>().SetBool("lvlComp", true);
        Instantiate(lvlComplete, new Vector3(0, 2.5f, 0), Quaternion.identity);
    }
}
