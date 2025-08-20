using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;   
using UnityEngine.UI;

public class searchFilter : MonoBehaviour
{
    [Header("Search Input Field")]
    public TMP_InputField searchBar;   

    [Header("Buttons to Filter")]
    public Button[] buttons;   

    public void OnSearchValueChanged()
    {
        string searchText = searchBar.text.ToLower();

        foreach (Button btn in buttons)
        {
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    btn.gameObject.SetActive(true);
                }
                else
                {
                    bool match = btnText.text.ToLower().Contains(searchText);
                    btn.gameObject.SetActive(match);
                }
            }
        }
    }
}
