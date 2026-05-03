using System;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views
{
    public class DragManipulator : PointerManipulator
    {
        public event Action<SlotElement, SlotElement> OnDropPerformed;
        
        private readonly VisualElement _root;
        
        private Vector2 _startMousePosition;
        private VisualElement _ghost;

        public DragManipulator(VisualElement target, VisualElement root)
        {
            this.target = target;
            _root = root;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
            target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            target.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            _startMousePosition = evt.position;

            _ghost = CreateGhost();

            _root.Add(_ghost);

            target.CapturePointer(evt.pointerId);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            if (_ghost != null && target.HasPointerCapture(evt.pointerId))
            {
                Vector2 delta = (Vector2)evt.position - _startMousePosition;

                _ghost.style.translate = new Translate(delta.x, delta.y, 0);
            }
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (_ghost != null)
            {
                _root.Remove(_ghost);
                
                _ghost = null;
            }

            target.ReleasePointer(evt.pointerId);
            
            VisualElement mouseOver = target.panel.Pick(evt.position);

            if (target is SlotElement sourceSlot && mouseOver is SlotElement targetSlot)
            {
                OnDropPerformed?.Invoke(sourceSlot, targetSlot);
            }
        }

        private VisualElement CreateGhost()
        {
            VisualElement ghost = new VisualElement();
            ghost.AddToClassList("slot");
            ghost.style.backgroundImage = target.style.backgroundImage;
            ghost.style.left = target.worldBound.x;
            ghost.style.top = target.worldBound.y;
            ghost.style.position = Position.Absolute;
            ghost.pickingMode = PickingMode.Ignore;
            return ghost;
        }
    }
}