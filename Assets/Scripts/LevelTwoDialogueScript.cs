using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoDialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText;
    Coroutine dialogueCoroutine;
    public BossHealthManager bossHealthManager;

    bool dialogue50Percent = false;
    bool dialogue25Percent = false;

    void Awake()
    {
        if(!dialogueText)
        {
            dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
            return;
        }
    }

    void Start()
    {
        if (bossHealthManager == null && GameObject.Find("BossHealthManager") != null)
        {
            bossHealthManager = GameObject.Find("Unknown-boss").GetComponent<BossHealthManager>();
        }

        dialogueText.gameObject.SetActive(true);
        dialogueCoroutine = StartCoroutine(InitialDialogue());
    }

    private void Update()
    {
        if(bossHealthManager.health <= 50 && !dialogue50Percent)
        {
            dialogueCoroutine = StartCoroutine(On50PercentDialogue());
            dialogue50Percent = true;
        }
        if (bossHealthManager.health <= 25 && !dialogue25Percent && dialogue50Percent)
        {
            dialogueCoroutine = StartCoroutine(On25PercentDialogue());
            dialogue25Percent = true;
        }
    }

    IEnumerator InitialDialogue()
    {
        dialogueText.text = "No.. You're not!!";
        dialogueText.color = Color.white;
        yield return new WaitForSeconds(4f);
        dialogueText.text = "";

        yield return null;
        dialogueCoroutine = null;
    }

    IEnumerator On50PercentDialogue()
    {
        dialogueText.text = "You think you can redeem yourself by beating me?";
        dialogueText.color = Color.red;
        yield return new WaitForSeconds(3f);
        dialogueText.text = "You'll suffer a never-ending painful death inside your own mind!";
        yield return new WaitForSeconds(3f);
        dialogueText.text = "So we're inside my own conciousness..?";
        dialogueText.color = Color.white;
        yield return new WaitForSeconds(3f);
        dialogueText.text = "";

        yield return null;
        dialogueCoroutine = null;
    }

    IEnumerator On25PercentDialogue()
    {
        dialogueText.text = "How can I end this nightmare..";
        dialogueText.color = Color.white;
        yield return new WaitForSeconds(3f);
        dialogueText.text = "Should I really just accept my fate..";
        yield return new WaitForSeconds(3f);
        dialogueText.text = "";

        yield return null;
        dialogueCoroutine = null;
    }

    public void OnAOR()
    {
        dialogueText.text = "You're pathetic.";
    }

    public void OffAOR()
    {
        dialogueText.text = "";
    }
}
