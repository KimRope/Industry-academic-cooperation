using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpView : MonoBehaviour
{
    public Transform[] helpImages;
    public Button leftButton;
    public Button rightButton;
    public Transform menuUI;

    int helpPage = 0;
    // Start is called before the first frame update  
    void Start()
    {
        for (int i = 0; i < helpImages.Length; i++)
        {
            helpImages[i].gameObject.SetActive(false);
        }
        helpImages[helpPage].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (helpPage == 0)
            leftButton.interactable = false;
        else if (helpPage == helpImages.Length - 1)
            rightButton.interactable = false;
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
    }

    public void L_BtnClick()
    {
        helpImages[helpPage].gameObject.SetActive(false);
        helpPage--;
        helpImages[helpPage].gameObject.SetActive(true);
    }
    public void R_BtnClick()
    {
        helpImages[helpPage].gameObject.SetActive(false);
        helpPage++;
        helpImages[helpPage].gameObject.SetActive(true);
    }

    public void BtnHelpOpne()
    {
        gameObject.SetActive(true);
    }

    public void BtnHelpClose()
    {
        gameObject.SetActive(false);
        menuUI.gameObject.SetActive(true);
        helpImages[helpPage].gameObject.SetActive(false);
        helpPage = 0;
        helpImages[helpPage].gameObject.SetActive(true);
       
    }
}
