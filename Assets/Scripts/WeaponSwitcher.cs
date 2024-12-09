using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private int selectWeapon = 0;
    public Animation animation;
    public AnimationClip draw;
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectWeapon = 03;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectWeapon = 04;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectWeapon = 05;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectWeapon = 06;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectWeapon = 07;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectWeapon = 08;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectWeapon >= transform.childCount - 1)
            {
                selectWeapon = 0;
            }
            else
            {
                selectWeapon += 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectWeapon <= 0)
            {
                selectWeapon = transform.childCount - 1;
            }
            else
            {
                selectWeapon -= 1;
            }
        }

        if (previousSelectedWeapon != selectWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {

        if (selectWeapon >= transform.childCount)
        {
            selectWeapon = transform.childCount - 1;

        }
        animation.Stop();
        animation.Play(draw.name);

        int i = 0;
        foreach (Transform _weapon in transform)
        {
            if (i == selectWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}