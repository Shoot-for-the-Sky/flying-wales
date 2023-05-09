using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroLeverCheckListManager : MonoBehaviour
{

    [SerializeField] Text currentCheckText;
    [SerializeField] Text nextCheckText;
    public TextAsset jsonFilecheckList;
    private CheckList checkListJson;
    private int checkIndex = 0;
    private int checkListLenth;

    void Start()
    {
        checkListJson = JsonUtility.FromJson<CheckList>(jsonFilecheckList.text);
        checkListLenth = checkListJson.checkList.Length;
        SetCheckText(currentCheckText, 0);
        SetCheckText(nextCheckText, 1);
    }

    private void SetCheckText(Text check, int index)
    {
        check.text = checkListJson.checkList[index].text;
    }

    private void NextCheck()
    {
        checkIndex++;

        // current check
        if (checkIndex < checkListLenth)
        {
            SetCheckText(currentCheckText, checkIndex);
        }
        else
        {
            Debug.Log("Done Check List");
        }

        // next check
        if (checkIndex + 1 < checkListLenth)
        {
            SetCheckText(nextCheckText, checkIndex + 1);
        } else
        {
            nextCheckText.text = "";
        }
    }
}
