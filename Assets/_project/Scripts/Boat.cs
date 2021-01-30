using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private bool floatMovement = true;
    [SerializeField] private float floatMagnitude = 0.1f;
    private float yOrig;

    private int boatWeight;
    private int maxWeight = 100;

    private void Start()
    {
        uiManager.EnableBoatSank(false);
        yOrig = transform.position.y;
    }

    void Update()
    {
        if (floatMovement)
            transform.position = new Vector3(transform.position.x, yOrig + floatMagnitude * Mathf.Sin(2 * Time.time), transform.position.z) ;
    }

    public void ChangeWeight(int amount)
    {
        boatWeight += amount;
        uiManager.UpdateBoatWeight(boatWeight);
        if (boatWeight > maxWeight)
            SinkBoat();
    }

    public void SinkBoat()
    {
        floatMovement = false;
        gameManager.enableFishing = false;
        uiManager.EnableBoatSank(true);
    }
}
