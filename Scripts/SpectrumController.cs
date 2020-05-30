using UnityEngine;

public class SpectrumController : MonoBehaviour {
    //INSTANCE VARIABLES
    public int spectrumIndex;
    public int scale = 32;

    private BoxScaler _scaleComp;

    //run when program starts
    private void Start() {
        _scaleComp = GetComponent<BoxScaler>();
    }

    //run every physics update and scale to audio
    private void FixedUpdate() {
        _scaleComp.desiredScale = 1 + AudioManager.spectrum[spectrumIndex] * scale;
    }
}