// this script only lerps the transform's Z scale to desired scale

using UnityEngine;

public class BoxScaler : MonoBehaviour {
    //INSTANCE VARIABLES
    public float desiredScale = 1.0f;

    //scale gameobject every frame to desiredScale
    private void Update() {
        transform.localScale = new Vector3(
            transform.localScale.x,
            desiredScale,
            transform.localScale.z);
    }
}