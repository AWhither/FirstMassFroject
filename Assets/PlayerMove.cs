using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    protected Animator _anim;

    protected Transform _cam;

    public float playerSpeed;
    public float jumpForce;
    protected bool facingRight = true;

    protected bool touchedGround;

    public GameObject _menu;

    IEnumerator falledCd()
    {
        yield return new WaitForSeconds(0.08f);
    }
    private void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _collider = _rb.GetComponent<Collider2D>();
        _anim = _rb.GetComponent<Animator>();
        _menu = GameObject.FindGameObjectWithTag("Menu");
        _menu.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (!_menu.activeSelf)
        {
            _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, _rb.velocity.y);
        }
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && Physics2D.IsTouchingLayers(_collider, LayerMask.GetMask("Ground")) && touchedGround && !_menu.activeSelf)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            touchedGround = false;
        }
        if (!Physics2D.IsTouchingLayers(_collider, LayerMask.GetMask("Ground")))
        {
            touchedGround = false;
        }
        if(touchedGround == false && Physics2D.IsTouchingLayers(_collider, LayerMask.GetMask("Ground"))) 
        { 
            StartCoroutine(falledCd());
            touchedGround = true;
        }

        if (Physics2D.IsTouchingLayers(_collider, LayerMask.GetMask("Spike")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Physics2D.IsTouchingLayers(_collider, LayerMask.GetMask("Finish")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = _rb.transform.localScale;
        Scaler.x *= -1;
        _rb.transform.localScale = Scaler;
    }

    private void Update()
    {
        if (!facingRight && Input.GetAxis("Horizontal") > 0 && !_menu.activeSelf)
        {
            Flip();
        }
        else if (facingRight && Input.GetAxis("Horizontal") < 0 && !_menu.activeSelf)
        {
            Flip();
        }

        //menu
        if (!_menu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(true);
        }
        else if (_menu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(false);
        }
    }

    //Exit button in menu
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //restart button in menu
    public void RestartB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
