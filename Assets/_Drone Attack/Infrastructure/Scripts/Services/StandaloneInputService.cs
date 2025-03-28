﻿using UnityEngine;

namespace _Drone_Attack.Infrastructure.Scripts.Services
{
    public class StandaloneInputService : InputService
    {
        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        
        public override Vector2 MoveAxis => UnityMoveAxis();
        public override Vector2 RotateAxis => UnityRotateAxis();
        public override bool Interact => Input.GetButton(INTERACT_BUTTON);
        public override bool SwitchWeapon => Input.GetButtonDown(SWITCH_WEAPON);
        public override bool UseAbility => Input.GetButtonDown(USE_ABILITY);

        private static Vector2 UnityMoveAxis() => new(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
        private static Vector2 UnityRotateAxis() => new Vector2(Input.GetAxis(MOUSE_X), Input.GetAxis(MOUSE_Y));

    }
}