using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public GameObject ContinueButton;
    public GameObject EndButton;
    public TextMeshProUGUI Text;
    public Animator anim;
    public string FilePath;
    public string FileName;
    public float WaitTime = 0;
    float WaitSav = 0;
    public int Index = 0;
    public NPCData data;
    public bool HasStarted;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "";
        data = new NPCData();
        string destination = Application.streamingAssetsPath + FilePath + FileName + ".json";
        data = JsonUtility.FromJson<NPCData>(File.ReadAllText(destination));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("OpenBox");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("CloseBox");
        }
    }

    public void StartInteraction()
    {
        StartCoroutine(TypeSentence());
    }

    public IEnumerator TypeSentence()
    {
        if (HasStarted == false)
        {
            Index = 0;
            HasStarted = true;
            WaitSav = WaitTime;
        }
        if (Index < data.Text.Length)
        {
            Text.text = data.Name + ": ";
            ContinueButton.SetActive(false);
            EndButton.SetActive(false);
            foreach (char c in data.Text[Index])
            {
                Text.text += c;
                if (c == ',' || c == '.' || c == '!' || c == '?')
                {
                    WaitTime *= 4f;
                }
                else
                {
                    WaitTime = WaitSav;
                }
                yield return new WaitForSeconds(WaitTime);
            }
            Index++;
            if (Index < data.Text.Length)
            {
                ContinueButton.SetActive(true);
            }
            else
            {
                ContinueButton.SetActive(false);
                EndButton.SetActive(true);
                HasStarted = false;
            }
        }
    }
}

[Serializable]
public class NPCData
{
    public string Name;
    public string[] Text;
}
