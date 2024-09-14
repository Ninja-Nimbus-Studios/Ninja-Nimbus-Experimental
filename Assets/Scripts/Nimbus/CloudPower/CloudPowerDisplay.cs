using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CloudPowerDisplay : MonoBehaviour
{
    [SerializeField] Image fillImage;

    public void UpdateCloudPowerDisplay(float cloudPower, float maxCloudPower)
    {
        fillImage.fillAmount = cloudPower / maxCloudPower;
    }
}
