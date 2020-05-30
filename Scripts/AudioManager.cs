using UnityEngine;

public class AudioManager : MonoBehaviour {
    //INSTANCE VARIABLES
    public static float[] spectrum = new float[1024];

    //sync spectrum data to audio data
    private void Update() {
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
}