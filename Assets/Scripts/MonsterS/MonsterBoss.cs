using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoss : Monster
{
    public float distanceToPlayer;
    public float distanceAttack;
    public float speed;
    public float powerOfStomp;
    private Vector3 direction;
    private SpriteRenderer sprite;
    private Animator animator;
    private CharSCR player;
    private GameObject blood;
    public GameObject ground;
    public GameObject key;
    private int lives = 10;
    private bool isBattleBegin = false;
    private float distBtw; 
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        blood = Resources.Load<GameObject>("BloodDamage");
    }
    //Idle-0
    //Run-1
    //Attack-2
    //ReceiveDamage-3
    //Die-4
    protected override void Start()
    {
        direction = transform.right;
        player = FindObjectOfType<CharSCR>();
    }
    protected override void Update()
    {
        distBtw = (gameObject.transform.position - player.transform.position).magnitude;
        //�������� �� �������������
        if (distBtw <= distanceToPlayer && distBtw > distanceAttack && State != 4)
            TothePlayer();
        //�������� �� �����
        if (distBtw >= distanceToPlayer && State != 4)
            if (isBattleBegin && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < 2) // �������� �� �����
                State = 3;
            else
                State = 0;
        //�������� ������� �����
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceAttack && State != 4)
        {
            isBattleBegin = true;
            State = 2;
        }
        if (player.Lives < 1 && State != 4) //�������� ������ ������
        {
            isBattleBegin = false;
            State = 0;
        }

    }


    public void TothePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (gameObject.transform.position.x > player.transform.position.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
        State = 1;
    }
    public void Attack()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceAttack)
        {
            player.ReceiveDamage();
            player.State = CharState.RDamage;
            Debug.Log("�������� ������� ���� �� ������");
        }
    }
    public void Stomp()//������� �����
    {
        ground.transform.position = new Vector3(0, -1 * powerOfStomp);
        player.ReceiveDamage();
        player.State = CharState.RDamage;
        Debug.Log("�������� ������� ���� �� ������");
    }
    public void Stomp1()//������ �����
    {
        ground.transform.position = new Vector3(0, 0);
    }

    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    public override void ReceiveDamage()
    {
        lives--;
        if (lives > 0)
        {
            Debug.Log("��� ������� ����");
            isBattleBegin = true;
        }
        else
        {
            Debug.Log("��� ����!");
            State = 4;
            speed = 0.0F;
            Debug.Log("State is   " + State);
            key.SetActive(true);
        }
        Debug.Log("� ���� ������   " + lives);
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
        Destroy(newBlood, 1.0F);
    }
}
