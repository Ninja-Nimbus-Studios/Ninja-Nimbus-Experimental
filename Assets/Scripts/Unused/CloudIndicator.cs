// UNUSED
// create red cloud indicator in the middle of the spawned clouds


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudIndicator : MonoBehaviour
{
    SpriteRenderer cloudIndicator;

    // Start is called before the first frame update
    void Start()
    {
        cloudIndicator = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor()
    {
        cloudIndicator.color = new Color(0,255,0);
    }
}
