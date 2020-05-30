using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class BeatEffects : MonoBehaviour {
    //INSTANCE VARIABLES
    public bool vaporwaveMode, glitchEffects;
    public GameObject aestheticSun, textParent;
    public PostProcessingProfile postProcess;
    public Material gridMaterial, backgroundMaterial;
    public int spectrumIndex;

    [Space]
    public float gridSpeedMultiplier;
    public float gridSpeedDampen;

    [Space]
    public float bloomMultiplier;
    public float bloomDampen;

    [Space]
    public float chromaticMultiplier;
    public float chromaticDampen;

    [Space]
    public float sunMultiplier;
    public float sunDampening;

    [Space]
    [ColorUsageAttribute(true, true)]
    public Color vaporwaveColor;
    [ColorUsageAttribute(true, true)]
    public Color synthwaveColor;

    [Space]
    public float fovMultiplier;

    //run every frame
    private void Update() {
        //offset grid material texture with speed relative to audio
        gridMaterial.mainTextureOffset -= new Vector2(0, AudioManager.spectrum[spectrumIndex] * gridSpeedMultiplier);

        //sync camera bloom intensity to audio
        BloomModel.Settings bloomSettings = postProcess.bloom.settings;
        bloomSettings.bloom.intensity = Mathf.Lerp(bloomSettings.bloom.intensity, AudioManager.spectrum[spectrumIndex] * bloomMultiplier, bloomDampen * Time.deltaTime);
        postProcess.bloom.settings = bloomSettings;

        //sync chromatic aberration intensity to audio
        ChromaticAberrationModel.Settings chromaticSettings = postProcess.chromaticAberration.settings;
        chromaticSettings.intensity = Mathf.Lerp(chromaticSettings.intensity, AudioManager.spectrum[spectrumIndex] * chromaticMultiplier, chromaticDampen * Time.deltaTime);
        postProcess.chromaticAberration.settings = chromaticSettings;

        //sync camera fov to audio
        Camera.main.fieldOfView = 60 - AudioManager.spectrum[spectrumIndex] * fovMultiplier;

        //sync sun size to audio
        aestheticSun.transform.localScale = Vector3.Lerp(
            aestheticSun.transform.localScale, 
            new Vector3(AudioManager.spectrum[spectrumIndex] * sunMultiplier + 1.6f, 1, AudioManager.spectrum[spectrumIndex] * sunMultiplier + 1.6f),
            sunDampening * Time.deltaTime);

        //extra functions
        if (Input.GetKeyDown(KeyCode.F2)) {
            vaporwaveMode = !vaporwaveMode;

            if (vaporwaveMode)
                backgroundMaterial.SetColor("_EmissionColor", vaporwaveColor);
            else
                backgroundMaterial.SetColor("_EmissionColor", synthwaveColor);

            GameObject.Find("VaporwaveText").GetComponent<Text>().enabled = vaporwaveMode;
            GameObject.Find("SynthwaveText").GetComponent<Text>().enabled = !vaporwaveMode;
        } else if (Input.GetKeyDown(KeyCode.F3)) {
            glitchEffects = !glitchEffects;

            GameObject.Find("EffectsCamera").GetComponent<CameraFilterPack_FX_Glitch1>().enabled = glitchEffects;
            GameObject.Find("EffectsCamera").GetComponent<CameraFilterPack_NewGlitch3>().enabled = glitchEffects;
            GameObject.Find("PlayImage").GetComponent<SpriteRenderer>().enabled = glitchEffects;
        } else if (Input.GetKeyDown(KeyCode.F1))
            textParent.SetActive(!textParent.activeSelf);
        else if (Input.GetKeyDown(KeyCode.F4))
            Camera.main.GetComponent<PostProcessingBehaviour>().enabled = !Camera.main.GetComponent<PostProcessingBehaviour>().enabled;
    }
}
