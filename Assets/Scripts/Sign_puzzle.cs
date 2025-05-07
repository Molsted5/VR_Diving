using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[System.Serializable]
public class Sign
{
    public string Label;
    public Sprite Image;
}

public class Sign_puzzle : MonoBehaviour
{
    public List<Sign> _AllSigns = new List<Sign>();
    private List<Sign> _SelectedSigns = new List<Sign>();
    private List<Button> _ActiveButtons = new List<Button>();

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    public TMP_Text codeTextBox;
    public TMP_Text descriptionText;
    private string _Code;
    private string _Input;
    private int btncount;
    private bool checkingcode;
    public List<string> _btncheck = new List<string>();
    public Boolean pin_activated = false;
    public Boolean buttonsset = false;

    public GameObject[] cables;
    public Color cableColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (btncount == 4 && !checkingcode)
        {
            checkingcode = true;
            StartCoroutine(CheckCode());
        };
    }

    void RandomizeButtons()
    {
        //Reset everything
        btncount = 0;
        _btncheck.Clear();
        _Input = "";
        _SelectedSigns.Clear();

        //Copies the full list of signs and shuffles them
        List<Sign> _shuffledSigns = new List<Sign>(_AllSigns);
        ShuffleList(_shuffledSigns);

        //Get the first 4 items in _shufflesSigns and store them (GetRange is exclusive)
        _SelectedSigns = _shuffledSigns.GetRange(0, 4);

        //Assign the items in _SelectedSigns to the 4 buttons
        _ActiveButtons = new List<Button> { Button1, Button2, Button3, Button4 };
        for (int i = 0;  i < _ActiveButtons.Count; i++)
        {
            var currentbtn = _ActiveButtons[i];
            var currentsign = _SelectedSigns[i];

            currentbtn.onClick.RemoveAllListeners();
            currentbtn.image.sprite = currentsign.Image;
            currentbtn.onClick.AddListener(() => btninput(currentsign.Label, currentbtn));
        }

        //Create code from _SelectedSigns and randomise using the randomiser
        List<String> _newCode = new List<String>();
        //Select all labels in _SelectedSigns and add to _newCode
        foreach(Sign sign in _SelectedSigns)
        {
            _newCode.Add(sign.Label);
        }
        //Randomize _newCode and add string to _Code
        ShuffleList(_newCode);
        _Code = string.Join("", _newCode);

        //Set visible text to the same as the code but with spaces
        codeTextBox.text = "Code: " + string.Join(", ", _newCode);

        //Set the booleans
        buttonsset = true;
        checkingcode = false;
    }

    //Generic Fisher-Yates shuffler for lists (counts backwards and swaps items around)
    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    //Coroutine that matches _Input with _Code and changes buttoncolor and pin_activated based on result
    IEnumerator CheckCode()
    {
        if (_Input == _Code)
        {
            Debug.Log("Match");
            Button1.image.color = Color.green;
            Button2.image.color = Color.green;
            Button3.image.color = Color.green;
            Button4.image.color = Color.green;
            pin_activated = true;
        }
        else
        {
            Debug.Log("No match");
            Button1.image.color = Color.red;
            Button2.image.color = Color.red;
            Button3.image.color = Color.red;
            Button4.image.color = Color.red;
        }

        yield return new WaitForSeconds(1.5f);
        
        if(pin_activated == true)
        {
            Button1.gameObject.SetActive(false);
            Button2.gameObject.SetActive(false);
            Button3.gameObject.SetActive(false);
            Button4.gameObject.SetActive(false);
            descriptionText.text = "Fabricator has been activated";
            codeTextBox.text = "";
            foreach( GameObject cable in cables ) {
                cable.GetComponent<Renderer>().material.color = cableColor;
            }
        }
        else
        {
            Button1.image.color = Color.white;
            Button2.image.color = Color.white;
            Button3.image.color = Color.white;
            Button4.image.color = Color.white;
            checkingcode = false;
            btncount = 0;
            _Input = "";
            _btncheck.Clear();
            RandomizeButtons();
        }
    }

    //Function called when a button is clicked. Makes sure every button can only be pressed once and adds that buttons label to _Input;
    void btninput(string sign, Button mybutton)
    {
        
        if (_btncheck.Contains(sign))
        {
            Debug.Log("Already pressed");
        }
        else
        {
            _btncheck.Add(sign);
            _Input = _Input + sign;
            btncount++;
            mybutton.image.color = Color.blue;
            Debug.Log(_Input);
            Debug.Log(_Code);
        }   
    }
}

