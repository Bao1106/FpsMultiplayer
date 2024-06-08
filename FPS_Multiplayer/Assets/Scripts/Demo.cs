using GOAP.Sensors;
using Managers;
using Services.DependencyInjection;
using UnityEngine;

public class Demo : MonoBehaviour
{
    //[Inject] private IPlayerSensor playerSensor;

    private void Start()
    {
        //playerSensor.IsUserInRange.AddListener(OnUpdateSensor);
        //Move to Launcher
    }

    private void OnUpdateSensor(bool test)
    {
       //Debug.LogError($"Check sensor: {test}");
    }
}
