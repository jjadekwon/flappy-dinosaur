using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurController : MonoBehaviour
{
    private float tiltSmooth = 2f;
    public float flySpeed = 500f;   // 클릭했을 때 올라가는 속도

    private Rigidbody2D rigidbody;

    private Vector2 startPosition = new Vector2(0, -0.2f);
    private Quaternion downRotation;    // 아래로 떨어질 때 회전값
    private Quaternion forwardRotation; // 위로 올라갈 때 회전값

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        downRotation = Quaternion.Euler(0, 0, -15);
        forwardRotation = Quaternion.Euler(0, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 종료
        if (GameManager.instance.gameState == GameState.End) return;

        if (GameManager.instance.gameState != GameState.Start)
        {
            rigidbody.simulated = false;
            return;
        }
        else
        {
            // 게임 시작 
            rigidbody.simulated = true;
        }

        // 위로 올라가고 있으면 프로펠러 돌아가는 애니메이션 재생
        if (rigidbody.velocity.y > 0)
        {
            animator.Play("Fly");
            transform.rotation = forwardRotation;
        }
        else
        {
            animator.Play("Fall");
            transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
        }

        // 화면 클릭 시 위로 가속
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(Vector2.up * flySpeed);
        }
    }

    // 기둥이랑 충돌했을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            Debug.Log("Dead");

            rigidbody.velocity = Vector2.zero;
            GameManager.instance.EndGame();
        }
    }
}
