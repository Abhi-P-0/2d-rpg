using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour {

    private PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerScript = GetComponentInParent<PlayerScript>();

    }

    private void AnimationTrigger() {
        playerScript.AttackOver();
    }
}
