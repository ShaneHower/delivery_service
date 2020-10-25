using UnityEngine;

public class Player : Creature
{   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.characterController = GetComponent<CharacterController>();
        base.walkSpeed = 2.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        playerInput();
        base.Update();
    }

    void playerInput()
    {
        // player movement inputs
        base.horizontal = Input.GetAxisRaw("Horizontal");
        base.vertical = Input.GetAxisRaw("Vertical");
    }
}
