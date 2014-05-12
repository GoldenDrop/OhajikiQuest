using UnityEngine;
using System.Collections;

public static class Phase {

    // 現在のフェイズ
    public enum PresentPhase
    {
        Wait,
        Player,
        Enemy,
        Title,
        GameOver,
        Result
    }
}
