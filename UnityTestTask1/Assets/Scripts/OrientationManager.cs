using UnityEngine;

public class OrientationManager : MonoBehaviour
{
    // Set the desired orientations in the Unity Inspector
    public bool allowPortrait = true;
    public bool allowLandscape = true;
    
    private void Start()
    {
        // Set the initial orientation
        SetOrientation();
    }

    private void SetOrientation()
    {
        if (allowPortrait && allowLandscape)
        {
            // Allow both portrait and landscape modes
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
        else if (allowPortrait)
        {
            // Allow only portrait mode
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
        else if (allowLandscape)
        {
            // Allow only landscape mode
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
        else
        {
            // Disable rotation altogether
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
