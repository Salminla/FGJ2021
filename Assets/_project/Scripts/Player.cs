using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject fishRod;
    [SerializeField] private GameObject pointerObject;
    [SerializeField] private GameObject bait;
    [SerializeField] private Image powerBar;

    private Rigidbody rb;

    [SerializeField] float chargeLimit = 200;
    public float chargeAmount = 0;
    
    // Rotation
    private float minZ = -70;
    private float maxZ = -25;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        ChargeRod();
        SetPointerPos();
        if (Input.GetMouseButtonDown(0))
            ThrowBait();
    }

    private void ChargeRod()
    {
        if (Input.GetMouseButton(0))
        {
            if ( chargeAmount < chargeLimit)
                chargeAmount += Time.fixedDeltaTime * 50;
        }
        else
            chargeAmount = 0;
        powerBar.fillAmount = chargeAmount / chargeLimit;
    }

    private void ThrowBait()
    {
        
    }

    private void SetPointerPos()
    {
        int layerMask1 = 1 << 8;
        int layerMask2 = 1 << 2;
        int finalMask = layerMask1 | layerMask2;
        
        finalMask = ~finalMask;
        
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 30, finalMask))
        {
            if (!pointerObject.activeSelf)
                pointerObject.SetActive(true);
            pointerObject.transform.position = hit.point + (hit.normal / 5);
        }
        else
        {
            if (pointerObject.activeSelf) 
                pointerObject.SetActive(false);
        }
    }
}
