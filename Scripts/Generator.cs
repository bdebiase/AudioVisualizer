using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    //INSTANCE VARIABLES
    public int amount;
    public float spacing;
    public GameObject prefab;

    private List<GameObject> _audioBars = new List<GameObject>();

    //create row of audio sensitive bars
    private void Start() {
        for (var i = 0; i < amount; i++) {
            var newAudioBar = Instantiate(prefab, transform);
            newAudioBar.transform.position = new Vector3(i * spacing - ((amount - 1) * spacing) / 2f, transform.position.y, transform.position.z);
            newAudioBar.GetComponent<SpectrumController>().spectrumIndex = Random.Range(0, 12);
            _audioBars.Add(newAudioBar);
        }
    }
}