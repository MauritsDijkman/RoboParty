using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    private void Update()
    {
        float newScale = 1 / transform.parent.parent.localScale.y;

        //Debug.Log($"Local scale: {transform.localScale} || Parent scale: {transform.parent.parent.localScale} || New scale: {newScale}");

        transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);
    }
}
