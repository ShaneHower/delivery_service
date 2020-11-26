using UnityEngine;

public class Player : Creature
{
    public Transform playerCamera;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.creatureController = GetComponent<CharacterController>();
        base.creatureAnimator = GetComponent<Animator>();
        base.walkSpeed = 6.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        playerInput();
        setMoveParams();
        base.Update();
    }

    void playerInput()
    {
        // player movement inputs
        base.horizontal = Input.GetAxisRaw("Horizontal");
        base.vertical = Input.GetAxisRaw("Vertical");
    }

    void setMoveParams()
    {
        base.rotateRef = playerCamera.eulerAngles.y;
    }
}
