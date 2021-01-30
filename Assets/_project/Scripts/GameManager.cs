using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int FishAmount { get; set; } = 0;

    public float CamClampAngleYMax { get; set; } = 130;
    public float CamClampAngleYMin { get; set; } = -130;

    public void ResetClamp()
    {
        CamClampAngleYMax = 130;
        CamClampAngleYMin = -130;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
