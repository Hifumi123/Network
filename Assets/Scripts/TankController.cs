using UnityEngine;
using UnityEngine.Networking;

public class TankController : NetworkBehaviour
{
    public float moveSpeed = 2;

    public float turnSpeed = 30;

    public GameObject bullet;

    public Transform bulletStart;

    private Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();

        if (isLocalPlayer)
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().target = transform;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Move();

        Shoot();
    }

    public override void OnStartLocalPlayer()
    {
        Color color = new Color(132f / 255, 112f / 255, 1);

        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
        transform.GetChild(3).GetComponent<MeshRenderer>().material.color = color;
    }

    [Command]
    private void CmdMove(float h, float v)
    {
        if (v == 0)
            m_Animator.SetBool("Move", false);
        else
            m_Animator.SetBool("Move", true);

        transform.Translate(v * moveSpeed * Time.deltaTime, 0, 0);
        transform.Rotate(0, h * turnSpeed * Time.deltaTime, 0);
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        CmdMove(h, v);
    }

    [ClientRpc]
    private void RpcShoot()
    {
        m_Animator.SetTrigger("Shoot");
    }

    [Command]
    private void CmdShoot()
    {
        RpcShoot();

        GameObject temp = Instantiate(bullet, bulletStart.position, transform.rotation);

        NetworkServer.Spawn(temp);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CmdShoot();
    }
}
