using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoDialogueScript : MonoBehaviour
{
    public TMP_Text DialogueText;
    private Coroutine _dialogueCoroutine;
    public BossHealthManager BossHealthManager;

    private bool _dialogueAtHalfHP = false;
    private bool _dialogueAtQuarterHP = false;

    void Awake()
    {
        if(!DialogueText)
        {
            DialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
            return;
        }
    }

    void Start()
    {
        if (BossHealthManager == null && GameObject.Find("BossHealthManager") != null)
        {
            BossHealthManager = GameObject.Find("Unknown-boss").GetComponent<BossHealthManager>();
        }

        DialogueText.gameObject.SetActive(true);
        _dialogueCoroutine = StartCoroutine(InitialDialogue());
    }

    private void Update()
    {
        if(BossHealthManager.Health <= 50 && !_dialogueAtHalfHP)
        {
            _dialogueCoroutine = StartCoroutine(On50PercentDialogue());
            _dialogueAtHalfHP = true;
        }
        if (BossHealthManager.Health <= 25 && !_dialogueAtQuarterHP && _dialogueAtHalfHP)
        {
            _dialogueCoroutine = StartCoroutine(On25PercentDialogue());
            _dialogueAtQuarterHP = true;
        }
    }

    IEnumerator InitialDialogue()
    {
        DialogueText.text = "No.. You're not!!";
        DialogueText.color = Color.white;
        yield return new WaitForSeconds(4f);
        DialogueText.text = "";

        yield return null;
        _dialogueCoroutine = null;
    }

    IEnumerator On50PercentDialogue()
    {
        DialogueText.text = "You think you can redeem yourself by beating me?";
        DialogueText.color = Color.red;
        yield return new WaitForSeconds(3f);
        DialogueText.text = "You'll suffer a never-ending painful death inside your own mind!";
        yield return new WaitForSeconds(3f);
        DialogueText.text = "So we're inside my own conciousness..?";
        DialogueText.color = Color.white;
        yield return new WaitForSeconds(3f);
        DialogueText.text = "";

        yield return null;
        _dialogueCoroutine = null;
    }

    IEnumerator On25PercentDialogue()
    {
        DialogueText.text = "How can I end this nightmare..";
        DialogueText.color = Color.white;
        yield return new WaitForSeconds(3f);
        DialogueText.text = "Should I really just accept my fate..";
        yield return new WaitForSeconds(3f);
        DialogueText.text = "";

        yield return null;
        _dialogueCoroutine = null;
    }

    public void OnAOR()
    {
        DialogueText.text = "You're pathetic.";
    }

    public void OffAOR()
    {
        DialogueText.text = "";
    }
}
