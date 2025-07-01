using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrideController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private Coroutine _forceAnimationCoroutine;
    private Dictionary<string, object> _originalParameters = new();
    private bool _isOverriding = false;

    public bool IsOverriding => _isOverriding;

    private void Awake()
    {
        if (!animator)
            animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        StopCurrentAnimation();
    }

    public void ForceAnimation(AnimationCommandConfig animConfig, float duration = 1f)
    {
        if (animator == null)
        {
            Debug.LogWarning($"{name}: No Animator found!");
            return;
        }

        StopCurrentAnimation();
        _forceAnimationCoroutine = StartCoroutine(ForceAnimationCoroutine(animConfig, duration));
    }

    public void StopCurrentAnimation()
    {
        if (_forceAnimationCoroutine != null)
        {
            StopCoroutine(_forceAnimationCoroutine);
            _forceAnimationCoroutine = null;
            
            if (_isOverriding)
            {
                RestoreOriginalParameters();
                _isOverriding = false;
            }
        }
    }

    private IEnumerator ForceAnimationCoroutine(AnimationCommandConfig animConfig, float duration)
    {
        _isOverriding = true;
        
        SaveOriginalParameters(animConfig.parameters);
        ApplyParameters(animConfig.parameters);
        animator.Play(animConfig.animatorStateName);
        
        yield return new WaitForSeconds(duration);
        
        RestoreOriginalParameters();
        _isOverriding = false;
        _forceAnimationCoroutine = null;
    }

    private void SaveOriginalParameters(AnimationCommandConfig.AnimatorParameter[] parameters)
    {
        _originalParameters.Clear();
        
        foreach (var param in parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    _originalParameters[param.name] = animator.GetBool(param.name);
                    break;
                case AnimatorControllerParameterType.Float:
                    _originalParameters[param.name] = animator.GetFloat(param.name);
                    break;
                case AnimatorControllerParameterType.Int:
                    _originalParameters[param.name] = animator.GetInteger(param.name);
                    break;
            }
        }
    }

    private void ApplyParameters(AnimationCommandConfig.AnimatorParameter[] parameters)
    {
        foreach (var param in parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(param.name, param.boolValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(param.name, param.floatValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(param.name);
                    break;
            }
        }
    }

    private void RestoreOriginalParameters()
    {
        foreach (var kvp in _originalParameters)
        {
            var paramName = kvp.Key;
            var originalValue = kvp.Value;
            
            switch (originalValue)
            {
                case bool boolValue:
                    animator.SetBool(paramName, boolValue);
                    break;
                case float floatValue:
                    animator.SetFloat(paramName, floatValue);
                    break;
                case int intValue:
                    animator.SetInteger(paramName, intValue);
                    break;
            }
        }
    }
}