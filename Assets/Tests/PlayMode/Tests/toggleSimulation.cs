using System.Collections;
using System.Collections.Generic; // List dependency
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class toggleSimulation : MonoBehaviour
{
    private GameObject canvas;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("OneScreen"); 
        yield return null; 

        canvas = GameObject.Find("Canvas");
        Assert.IsNotNull(canvas, "Canvas not found in scene!");

        if (GameObject.FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator Toggles_CanBeClicked_OnAndOff_Simulation()
    {
        var stationsContainer = canvas.transform.Find("UI/individualTrainScreen/specificTrains/IR1622/scrollView/Viewport/stationsContainer");
        Assert.IsNotNull(stationsContainer, "stationsContainer not found!");

		 List<string> qaReport = new List<string>();

        foreach (Transform child in stationsContainer)
        {
            var toggle = child.GetComponent<Toggle>();
            Assert.IsNotNull(toggle, $"{child.name} has no Toggle component!");

            ExecuteEvents.Execute(toggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            yield return null;
			// yield return new WaitForSeconds(0.5f);
            // Debug.Log($"{child.name} Was toggled on");
			qaReport.Add($"{child.name} Was toggled on");
            Assert.IsTrue(toggle.isOn, $"{child.name} failed to turn ON!");


            ExecuteEvents.Execute(toggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            yield return null; 
			// yield return new WaitForSeconds(0.5f);
            // Debug.Log($"{child.name} Was toggled off");
			qaReport.Add($"{child.name} Was toggled off");
            Assert.IsFalse(toggle.isOn, $"{child.name} failed to turn OFF!");
        }
		
		Debug.Log("-- QA Toggle Report --\n" + string.Join("\n", qaReport));
    }
}