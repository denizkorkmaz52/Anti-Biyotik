using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
public class ThirdPersonControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
   // [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] public GameObject crosshairCanvas;
    private Animator animator;
    float fireTimer = 0f;
    private Attack attack;
    
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private void Awake()
    {
        attack = GetComponent<Attack>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            //debugTransform.position = raycastHit.point;
            mouseWorldPos = raycastHit.point;
        }
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            crosshairCanvas.SetActive(true);
            Vector3 worldAimTarget = mouseWorldPos;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            thirdPersonController.SetRotateOnMove(false);
            animator.SetBool("Aiming", true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            crosshairCanvas.SetActive(false);
            animator.SetBool("Aiming", false);
        }
        if (Input.GetMouseButtonDown(0) && starterAssetsInputs.aim && fireTimer <= 0f)
        {
            fireTimer = fireCooldown;
            Vector3 aimDir = (mouseWorldPos - firePoint.position).normalized;
            attack.Fire(firePoint, aimDir);
        }

        fireTimer -= Time.deltaTime;

    }
}
