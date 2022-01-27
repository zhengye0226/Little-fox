using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardContorller : MonoBehaviour
{
    public static KeyBoardContorller keyboardController;

    [Header("Up Button")]
    [SerializeField] private Text upTextCode = null;

    [Header("Down Button")]
    [SerializeField] private Text downTextCode = null;

    [Header("Left Button")]
    [SerializeField] private Text leftTextCode = null;

    [Header("Right Button")]
    [SerializeField] private Text rightTextCode = null;

    [Header("Attack Button")]
    [SerializeField] private Text attackTextCode = null;

    Event keyEvent;
    KeyCode newKey;
    bool waitingForKey;

    public KeyCode up { set; get; }
    public KeyCode down { set; get; }
    public KeyCode left { set; get; }
    public KeyCode right { set; get; }
    public KeyCode attack { set; get; }


    private void Awake()
    {

        if (keyboardController == null)
        {
            DontDestroyOnLoad(gameObject);
            keyboardController = this;
        }
        else if (keyboardController != this)
        {
            Destroy(gameObject);
        }

        up = (KeyCode)PlayerPrefs.GetInt("UpKey");
        down = (KeyCode)PlayerPrefs.GetInt("DownKey");
        left = (KeyCode)PlayerPrefs.GetInt("LeftKey");
        right = (KeyCode)PlayerPrefs.GetInt("RightKey");
        attack = (KeyCode)PlayerPrefs.GetInt("AttackKey");

        upTextCode.text = ((KeyCode)PlayerPrefs.GetInt("UpKey")).ToString();
        downTextCode.text = ((KeyCode)PlayerPrefs.GetInt("DownKey")).ToString();
        leftTextCode.text = ((KeyCode)PlayerPrefs.GetInt("LeftKey")).ToString();
        rightTextCode.text = ((KeyCode)PlayerPrefs.GetInt("RightKey")).ToString();
        attackTextCode.text = ((KeyCode)PlayerPrefs.GetInt("AttackKey")).ToString();

        waitingForKey = false;
    }
    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey){
            Debug.Log("I AM WAITING");
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "up":
                up = newKey;
                upTextCode.text = newKey.ToString();
                PlayerPrefs.SetInt("UpKey", (int)newKey);
                break;
            case "down":
                down = newKey;
                downTextCode.text = newKey.ToString();
                PlayerPrefs.SetInt("DownKey", (int)newKey);
                break;
            case "left":
                left = newKey;
                leftTextCode.text = newKey.ToString();
                PlayerPrefs.SetInt("LeftKey", (int)newKey);
                break;
            case "right":
                right = newKey;
                rightTextCode.text = newKey.ToString();
                PlayerPrefs.SetInt("RightKey", (int)newKey);
                break;
            case "attack":
                attack = newKey;
                attackTextCode.text = newKey.ToString();
                PlayerPrefs.SetInt("AttackKey", (int)newKey);
                break;
        }

        yield return null;
    }
}
