using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("�ӵ�����")]
    public float speed = 15f;//�ӵ��ٶ�
    public float basicDamage = 1;//�ӵ��˺�
    public float lifetime = 2f; // �Զ�����ʱ��
    private Vector3 dir;

    private Rigidbody2D rb;
    private Property playerProperty;
    private Health playerHealth;

    public void Init(Vector3 dir)
    {
        this.dir = dir;


    }


    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerProperty = player.GetComponent<Property>();
        playerHealth= player.GetComponent<Health>();

        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        // ��ǰ�ƶ�
        if (rb != null&&dir!=null)
        {
            rb.velocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ע�⣺���˵���ײ����Trigger������ʹ��OnTriggerEnter2D

        // ��������ʱ����˺�������
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Health>();
            if (enemy != null)
            {
                //������
                basicDamage = DealCrit(basicDamage);
                //������Ѫ
                if (DealGetBlood())
                {
                    var blood = 1;
              
                    playerHealth.InCreaseHealth(blood);

                }
                //��������
                enemy.DecreaseHealth(basicDamage);
            }
            Destroy(gameObject);
        }
        // ����ǽ�ڵ��ϰ���ʱ����
        else if (other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }


    private float DealCrit(float damage)
    {
        bool isCrit = UnityEngine.Random.Range(0f, 100f) <= playerProperty.CriticalRate;
        float newDamage;
        if (isCrit)
        {
            newDamage = damage * (1 + playerProperty.attackBonus * 0.01f) * playerProperty.CriticalValue * 0.01f;
        }
        else
        {
            newDamage = damage * (1 + playerProperty.attackBonus * 0.01f);
        }

        return newDamage;
    }

    private float DealDefense(float damage)
    {
        float newDamage = damage * (1 - playerProperty.Defense * 0.01f);

        return newDamage;
    }



    private bool DealEvasion()
    {
        bool isEvasion = UnityEngine.Random.Range(0f, 100f) <= playerProperty.EvasionRate;


        return isEvasion;

    }

    private bool DealGetBlood()
    {
        bool isGet=UnityEngine.Random.Range(0f, 100f) <= playerProperty.GetBloodRate;


        return isGet;
    }
}
