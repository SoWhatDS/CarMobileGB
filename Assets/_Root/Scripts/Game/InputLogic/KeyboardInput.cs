using UnityEngine;
using JoostenProductions;

namespace Game.InputLogic
{
    internal class KeyboardInput : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 1;

        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);

        private void Move()
        {
            float axisOffset = Input.GetAxis("Horizontal");
            float moveValue = Speed * _inputMultiplier * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }      
    }
}
