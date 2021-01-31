using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private FishCatching fishCatching;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject fishRod;
    [SerializeField] private GameObject pointerObject;
    [SerializeField] private Material pointerMatBad;
    [SerializeField] private Material pointerMatGood;
    [SerializeField] public GameObject bait;
    [SerializeField] private Image powerBar;
    
    [SerializeField] float chargeSpeed = 70 ;

    [SerializeField] private GameObject ropeOrigin;
    [SerializeField] private GameObject ropeDest;

    public bool baitLineActive = false;
    
    private float chargeLimit = 100;
    private float chargeAmount = 0;

    public bool throwSuccesful = false;

    private Vector3 pointerPos;

    void Start()
    {
        powerBar.fillAmount = 0;
    }
    
    void Update()
    {
        if (gameManager.enableFishing)
        {
            SetPointerPos();
            if (Input.GetMouseButton(0) && !throwSuccesful)
                ChargeRod();
            if (Input.GetMouseButtonUp(0) && !throwSuccesful)
                ThrowBait();
            if (throwSuccesful)
                fishCatching.CommenceCatching();
        }

        if (baitLineActive)
            SetBaitLine();
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
        bait.gameObject.SetActive(true);
        if (tempCharge > 60 && tempCharge < 75)
        {
            bait.transform.position = pointerPos + Vector3.up/4;
            baitLineActive = true;
            uiManager.SetPowerText("Good!",Color.green, true);
            StartCoroutine(disablePowerText());
            Debug.Log("Good throw!");
        }
        else if (tempCharge > 40 && tempCharge < 80)
        {
            baitLineActive = true;
            bait.transform.position = new Vector3(Random.Range(pointerPos.x-4, pointerPos.x+4), pointerPos.y + 1/4, Random.Range(pointerPos.z-4, pointerPos.z+4));
            uiManager.SetPowerText("Bad!", Color.yellow, true);
            StartCoroutine(disablePowerText());
            Debug.Log("Bad throw!");
        }
        else
        {
            bait.gameObject.SetActive(false);
            uiManager.SetPowerText("Fail!",Color.red, true);
            StartCoroutine(disablePowerText());
            Debug.Log("That didn't go anywhere");
            return;
        }

        var rotation = transform.rotation;
        gameManager.CamClampAngleYMin = rotation.eulerAngles.y - 30;
        gameManager.CamClampAngleYMax = rotation.eulerAngles.y + 30;

        throwSuccesful = true;
    }

    private void SetBaitLine()
    {
        LineRenderer lr = bait.GetComponent<LineRenderer>();
        lr.SetPosition(0, ropeOrigin.transform.position);
        lr.SetPosition(1, ropeDest.transform.position);
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
            pointerObject.transform.position = hit.point + (hit.normal / 2);
            pointerPos = hit.point;
        }
        else
        {
            if (pointerObject.activeSelf) 
                pointerObject.SetActive(false);
        }
        if (Physics.CheckSphere(transform.position, 8, 1<<9))
            pointerObject.GetComponent<MeshRenderer>().material = pointerMatBad;
        else
            pointerObject.GetComponent<MeshRenderer>().material = pointerMatGood;
    }

    IEnumerator disablePowerText()
    {
        yield return new WaitForSeconds(2);
        uiManager.SetPowerText("", Color.black, false);
    }
}
