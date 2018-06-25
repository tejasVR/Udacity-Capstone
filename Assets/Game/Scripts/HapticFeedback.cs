using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour {

    public static void HapticAmount(int hapticAmount, bool dominantController, bool nonDominantController)
    {
        if (dominantController)
            PlayerScript._deviceDominant.TriggerHapticPulse((ushort)hapticAmount);

        if (nonDominantController)
            PlayerScript._deviceNonDominant.TriggerHapticPulse((ushort)hapticAmount);
    }


}
