using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    /// <summary>
    /// 相机震动方向
    /// </summary>
    public static Vector3 shakeDir = Vector3.one.normalized;
    /// <summary>
    /// 相机震动时间
    /// </summary>
    public static float shakeTime = 1.0f;

    private static float currentTime = 0.0f;
    private static float totalTime = 0.0f;

    public static void shakeScreen()
    {
        totalTime = shakeTime;
        currentTime = shakeTime;

        shakeDir = Vector3.one.normalized;
    }

    public static void shakeScreen(float time,float power,float frequency)
    {
        currentTime = time;
        totalTime = time;
        shakeDir = new Vector3(power, power, power);
 
    }
    public static void shakeScreen(float time, Vector3 power, float frequency)
    {
        currentTime = time;
        totalTime = time;
        shakeDir = power;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            shakeScreen();
        }
    }


    public void Stop()
    {
        currentTime = 0.0f;
        totalTime = 0.0f;
    }

    public void UpdateShake()
    {
        if (currentTime > 0.0f && totalTime > 0.0f)
        {
      
                float percent = currentTime / totalTime;

                Vector3 shakePos = Vector3.zero;
                shakePos.x = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.x) * percent, Mathf.Abs(shakeDir.x) * percent);
                shakePos.y = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.y) * percent, Mathf.Abs(shakeDir.y) * percent);
                shakePos.z = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.z) * percent, Mathf.Abs(shakeDir.z) * percent);

                Camera.main.transform.position += shakePos;

                currentTime -= Time.deltaTime;

        }
        else
        {
            currentTime = 0.0f;
            totalTime = 0.0f;
          
        }
    }

    void LateUpdate()
    {
        UpdateShake();
    }

 

}