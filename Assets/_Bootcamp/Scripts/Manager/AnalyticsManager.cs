using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Threading.Tasks;

public class AnalyticsManager : MonoBehaviour
{
    public int fallCount = 0;          
    public float completionTime = 0f;    

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Analytics service initialized.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize Unity Services: {ex}");
        }
    }
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendCustomEvent();
        }
    }
    */

    public void SendCustomEvent()
    {
        var parameters = new Dictionary<string, object>
        {
            { "fall_count", fallCount },            
            { "completion_time", completionTime }    
        };

        AnalyticsService.Instance.CustomData("Custom_event", parameters);
        Debug.Log("Recording Event: Custom_event with fall_count and completion_time");
    }
}
