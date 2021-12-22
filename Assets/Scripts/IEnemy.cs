using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void onTrigger();

    void onTriggerGlowStick(GameObject glowStick);
}