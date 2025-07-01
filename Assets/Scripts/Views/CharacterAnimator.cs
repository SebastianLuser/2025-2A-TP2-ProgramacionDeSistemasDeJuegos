using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string speedParameter = "Speed";
    [SerializeField] private string isJumpingParameter = "IsJumping";
    [SerializeField] private string isFallingParameter = "IsFalling";
    
    private AnimationOverrideController _overrideController;

    private void Reset()
    {
        character = GetComponentInParent<Character>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (!character)
            character = GetComponentInParent<Character>();
        if (!animator)
            animator = GetComponentInParent<Animator>();
        if (!spriteRenderer)
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
            
        _overrideController = GetComponent<AnimationOverrideController>();
        if (!_overrideController)
            _overrideController = gameObject.AddComponent<AnimationOverrideController>();
    }

    private void OnEnable()
    {
        CharacterAnimatorRegistry.Register(this);
        
        if (!character || !animator || !spriteRenderer)
        {
            Debug.LogError($"{name} <color=grey>({GetType().Name})</color>: At least one reference is null!");
            enabled = false;
        }
    }
    
    private void OnDisable()
    {
        CharacterAnimatorRegistry.Unregister(this);
    }

    private void Update()
    {
        if (_overrideController.IsOverriding)
            return;
        
        var speed = character.Velocity;
        animator.SetFloat(speedParameter, Mathf.Abs(speed.x));
        animator.SetBool(isJumpingParameter, character.Velocity.y > 0);
        animator.SetBool(isFallingParameter, character.Velocity.y < 0);
        spriteRenderer.flipX = speed.x < 0;
    }
    
    public void ForceAnimation(AnimationCommandConfig animConfig, float duration = 1f)
    {
        _overrideController.ForceAnimation(animConfig, duration);
    }
}