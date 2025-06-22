using UnityEngine;
using UnityEngine.UI;

// This makes the script run in the editor, so you can see the layout update live
[ExecuteInEditMode] 
public class FitToTextHeight : MonoBehaviour
{
    // Drag your TextMeshPro object here in the Inspector
    public RectTransform textToFit;

    private RectTransform myRectTransform;

    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (textToFit == null || myRectTransform == null)
        {
            return;
        }

        // Get the height the text wants to be
        float preferredHeight = LayoutUtility.GetPreferredHeight(textToFit);

        // If our current height doesn't match, update it
        if (myRectTransform.sizeDelta.y != preferredHeight)
        {
            myRectTransform.sizeDelta = new Vector2(myRectTransform.sizeDelta.x, preferredHeight);
        }
    }
} 