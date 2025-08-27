using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleTests
{
    private GameObject canvas;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
       SceneManager.LoadScene("OneScreen"); 
        yield return null;

        canvas = GameObject.Find("Canvas");
        Assert.IsNotNull(canvas, "Canvas not found in scene!");
    }

    [UnityTest]
    public IEnumerator Toggles_CanBeClicked_OnAndOff()
    {
        
        var stationsContainer = canvas.transform.Find("UI/individualTrainScreen/specificTrains/IR1622/scrollView/Viewport/stationsContainer");
        Assert.IsNotNull(stationsContainer, "stationsContainer not found!");

       
        foreach (Transform child in stationsContainer)
        {
            var toggle = child.GetComponent<Toggle>();
            Assert.IsNotNull(toggle, $"{child.name} has no Toggle component!");

            toggle.isOn = true;
            yield return null; // wait one frame
			Debug.Log($"{child.name} Was toggled on");
            Assert.IsTrue(toggle.isOn, $"{child.name} failed to turn ON!");

            toggle.isOn = false;
            yield return null; // wait one frame
			Debug.Log($"{child.name} Was toggled off");
            Assert.IsFalse(toggle.isOn, $"{child.name} failed to turn OFF!");
        }
    }
}
