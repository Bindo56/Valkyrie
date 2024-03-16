using System;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce info")]
    [SerializeField] int amountOfBounce;
    [SerializeField] float bounceGravity;
    [SerializeField] float bounceSpeed;

    [Header("Pierce")]
    [SerializeField]int pierceAmount;
    [SerializeField] float pierceGravity;

    [Header("Spin")]
    [SerializeField] float maxTravelDistance = 7;
    [SerializeField] float spinDuration = 2;
    [SerializeField] float spinGravity = 1;
    [SerializeField] float hitCooldown = .35f;

    [Header("Skill Info ")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] Vector2 lunchDir;
    [SerializeField] float swordGravity;
    [SerializeField] float freezeTimeDuration;
    [SerializeField] float returnSpeed;

    private Vector2 finalDir;

    

    [Header("Aim Dots")]
    [SerializeField] int numberOfDots;
    [SerializeField] float spaceBetweenDots;
    [SerializeField] GameObject dotPrefabs;
    [SerializeField] Transform dotParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenereateDots();
        //  player = GetComponent<Player>();
        // player = PlayerManager.instance.player;
        SetupGravity();
    }

    private void SetupGravity()
    {
        if(swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
        }

        else if (swordType == SwordType.Pierce)
        {
            swordGravity = pierceGravity;
        }

        else if (swordType == SwordType.Spin)
        {
            swordGravity = spinGravity;
        }


    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * lunchDir.x, AimDirection().normalized.y * lunchDir.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }


    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if (swordType == SwordType.Bounce)
        {

            newSwordScript.SetupBounce(true, amountOfBounce,bounceSpeed);
        }

        else if (swordType == SwordType.Pierce)
            newSwordScript.SetupPierce(pierceAmount);

        else if (swordType == SwordType.Spin)
            newSwordScript.SetUpSpin(true, maxTravelDistance, spinDuration,hitCooldown);


        newSwordScript.SetupSword(finalDir, swordGravity, player , freezeTimeDuration,returnSpeed);

        player.AssignNewSword(newSword);


        DotsActive(false);





        /* Debug.Log(lunchDir); 
         Debug.Log(swordGravity);*/
    }
    #region Aim
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
        // Debug.Log(direction);

    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenereateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefabs, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);

        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * lunchDir.x,
            AimDirection().normalized.y * lunchDir.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }
    #endregion
}
