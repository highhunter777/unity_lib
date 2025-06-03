using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子弹设置")]
    public float speed = 15f;//子弹速度
    public float basicDamage = 1;//子弹伤害
    public float lifetime = 2f; // 自动销毁时间
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
        // 向前移动
        if (rb != null&&dir!=null)
        {
            rb.velocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 注意：敌人的碰撞器是Trigger，所以使用OnTriggerEnter2D

        // 碰到敌人时造成伤害并销毁
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Health>();
            if (enemy != null)
            {
                //处理暴击
                basicDamage = DealCrit(basicDamage);
                //处理吸血
                if (DealGetBlood())
                {
                    var blood = 1;
              
                    playerHealth.InCreaseHealth(blood);

                }
                //敌人受伤
                enemy.DecreaseHealth(basicDamage);
            }
            Destroy(gameObject);
        }
        // 碰到墙壁等障碍物时销毁
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
