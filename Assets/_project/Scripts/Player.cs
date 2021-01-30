using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FishCatching fishCatching;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject fishRod;
    [SerializeField] private GameObject pointerObject;
    [SerializeField] private GameObject bait;
    [SerializeField] private Image powerBar;
    
    private Rigidbody rb;

    [SerializeField] float chargeSpeed = 70;
    private float chargeLimit = 100;
    private float chargeAmount = 0;

    public bool throwSuccesful = false;

    private Vector3 pointerPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        powerBar.fillAmount = 0;
    }
    
    void Update()
    {
        SetPointerPos();
        if (Input.GetMouseButton(0) && !throwSuccesful)
            ChargeRod();
        if (Input.GetMouseButtonUp(0) && !throwSuccesful)
            ThrowBait();
        if(throwSuccesful)
            fishCatching.CommenceCatching();
    }

    private void ChargeRod()
    {
        if ( chargeAmount < chargeLimit)
            chargeAmount += Time.deltaTime * chargeSpeed;
    
        powerBar.fillAmount = chargeAmount / chargeLimit;
    }

    private void ThrowBait()
    {
        float tempCharge = chargeAmount;
        chargeAmount = 0;
        if (!pointerObject.activeSelf || Physics.CheckSphere(transform.position, 8, 1<<9) ) return;

        if (tempCharge > 40 && tempCharge < 60)
        {
            bait.transform.position = pointerPos;
            Debug.Log("Good throw!");
        }
        else if (tempCharge > 20 && tempCharge < 80)
        {
            bait.transform.position = new Vector3(Random.Range(pointerPos.x-4, pointerPos.x+4), pointerPos.y, Random.Range(pointerPos.z-4, pointerPos.z+4));
            Debug.Log("Bad throw!");
        }
        else
        {
            Debug.Log("That didn't go anywhere");
            return;
        }

        var rotation = transform.rotation;
        gameManager.CamClampAngleYMin = rotation.eulerAngles.y - 30;
        gameManager.CamClampAngleYMax = rotation.eulerAngles.y + 30;

        throwSuccesful = true;
    }

    private void SetPointerPos()
    {
        int layerMask1 = 1 << 8;
        int layerMask2 = 1 << 2;
        int layerMask3 = 1 << 9;
        int finalMask = layerMask1 | layerMask2 | layerMask3;
        
        finalMask = ~finalMask;
        
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 40, finalMask))
        {
            if (!pointerObject.activeSelf)
                pointerObject.SetActive(true);
            pointerObject.transform.position = hit.point + (hit.normal / 5);
            pointerPos = hit.point;
        }
        else
        {
            if (pointerObject.activeSelf) 
                pointerObject.SetActive(false);
        }
    }
}
