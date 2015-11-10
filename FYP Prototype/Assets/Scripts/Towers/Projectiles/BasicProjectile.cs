using UnityEngine;
using System.Collections;

public class BasicProjectile : TowerProjectile {

    // Animations
    private bool playAnimation = true;
    private float animationTimer = 0.38f;

    void Update()
    {
        if (target != null)
        {
            if (playAnimation)
            {
                if (animationTimer <= 0)
                {
                    playAnimation = false;
                }

                Vector3 targetLoc = new Vector3(target.transform.position.x,
                                                this.transform.position.y,
                                                target.transform.position.z);

                transform.LookAt(targetLoc);
                transform.position += (speed / 2) * transform.forward * Time.deltaTime;

                animationTimer -= Time.deltaTime;
            }
            else
            {
                gameObject.transform.LookAt(target.gameObject.transform);
                transform.position += speed * transform.forward * Time.deltaTime;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
