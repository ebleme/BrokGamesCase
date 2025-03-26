// maebleme2

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ebleme.Interactables
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField]
        private Canvas bilboard;

        bool isFocus = false; // Is this interactable currently being focused?
        bool hasInteracted = false; // Have we already interacted with the object?


        public UnityEvent OnInteracted;

        private InputHandler iputHandler;
        
        private void Start()
        {
            Defocuse();
            
            // Todo: Get this via Zenject
            iputHandler = FindFirstObjectByType<InputHandler>();
        }

        public virtual void Interact()
        {
            Defocuse();
            hasInteracted = true;
            OnInteracted?.Invoke();
            Debug.Log("Interacted");
        }

        public void AddListener(UnityAction action)
        {
            OnInteracted.AddListener(action);
        }
        
        public void RemoveListener(UnityAction action)
        {
            OnInteracted.RemoveListener(action);
        }
        
        public void RemoveAllListeners()
        {
            OnInteracted.RemoveAllListeners();
        }

        public void Focuse()
        {
            if (hasInteracted)
            {
                return;
            }
            
            isFocus = true;
            hasInteracted = false;
            bilboard.gameObject.SetActive(true);
        }

        public void Defocuse()
        {
            isFocus = false;
            hasInteracted = false;
            bilboard.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isFocus && iputHandler.InteractPressed && !hasInteracted)
            {
                Interact();
            }
        }
    }
}