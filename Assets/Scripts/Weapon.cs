using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public Camera camera;
    private float nextFire;

    [Header("VFX")]
    public GameObject hitVFX;
    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;
    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;
    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;
    [Header("Recoil Setting")]
    // [Range(0, 1)]
    // public float recoilPercent = 8.3f;
    [Range(0, 2)]
    public float recoverPercent = 8.7f;
    [Space]
    public float recoilUp = 1f;
    public float recoilBack = 0f;

    private Vector3 originPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    private bool recovering;
    void Start()
    {
        textMag();
        originPosition = transform.localPosition;

        recoilLength = 0;
        recoverLength = 1 / fireRate * recoverPercent;
    }
    void textMag()
    {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }

    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false)
        {
            nextFire = 1 / fireRate;
            ammo--;
            textMag();
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R) && mag > 0)
        {
            Reload();
        }

        if (recoiling)
        {
            Recoil();
        }
        if (recovering)
        {
            Recovering();
        }
    }

    void Reload()
    {
        animation.Play(reload.name);
        if (mag > 0)
        {
            mag--;
            ammo = magAmmo;
        }

        textMag();
    }
    RaycastHit hit;

    void Fire()
    {
        recoiling = true;
        recovering = false;
        PhotonNetwork.LocalPlayer.AddScore(1);
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                PhotonNetwork.LocalPlayer.AddScore(damage);
                if (damage >= hit.transform.gameObject.GetComponent<Health>().health)
                {

                    RoomManager.Instance.kills++;
                    RoomManager.Instance.SetHashes();
                    PhotonNetwork.LocalPlayer.AddScore(100);
                }

                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, hit.point);
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originPosition.x, originPosition.y * recoilUp, originPosition.z * recoilBack);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);
        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    }

    void Recovering()
    {
        Vector3 finalPosition = originPosition;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);
        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }

}
