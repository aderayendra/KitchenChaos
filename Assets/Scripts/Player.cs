using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float walkSpeed = 7.0f;
    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] private float playerRadius = 0.6f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform spawnPoint;

    private KitchenObject _kitchenObject;

    private bool _isWalking;
    private Vector3 _lastDirection;
    private BaseCounter _selectedKitchenCounter;

    public event EventHandler<OnSelectedKitchenCounterChangedEventArgs> OnSelectedKitchenCounterChanged;

    public class OnSelectedKitchenCounterChangedEventArgs : EventArgs
    {
        public BaseCounter KitchenCounter;
    }

    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Debug.LogError("There can't be more than one player");
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteract += GameInputOnOnInteract;
        gameInput.OnInteractAlternate += GameInputOnOnInteractAlternate;
    }

    private void GameInputOnOnInteractAlternate(object sender, EventArgs e)
    {
        if (_selectedKitchenCounter == null) return;
        _selectedKitchenCounter.InteractAlternate(this);
    }

    private void GameInputOnOnInteract(object sender, EventArgs e)
    {
        if (_selectedKitchenCounter == null) return;
        _selectedKitchenCounter.Interact(this);
    }

    private void Update()
    {
        Transform playerTransform = transform;
        Vector3 direction = gameInput.GetMovementVector3Normalized();
        HandleMovement(playerTransform, direction);
        HandleInteractions(playerTransform, direction);
    }

    private void HandleInteractions(Transform playerTransform, Vector3 direction)
    {
        if (direction != Vector3.zero) _lastDirection = direction;
        bool isHit = Physics.Raycast(playerTransform.position, _lastDirection, out RaycastHit hitInfo,
            interactDistance, layerMask);

        if (!isHit)
        {
            SetSelectedKitchenCounter(null);
            return;
        }

        bool isKitchenCounter = hitInfo.transform.TryGetComponent(out BaseCounter kitchenCounter);
        if (isKitchenCounter)
        {
            if (kitchenCounter == _selectedKitchenCounter) return;
            SetSelectedKitchenCounter(kitchenCounter);
        }
        else
        {
            SetSelectedKitchenCounter(null);
        }
    }

    private void HandleMovement(Transform playerTransform, Vector3 direction)
    {
        _isWalking = direction != Vector3.zero;

        if (!_isWalking)
        {
            return;
        }

        float distance = walkSpeed * Time.deltaTime;

        playerTransform.forward = Vector3.Slerp(playerTransform.forward, direction, Time.deltaTime * rotationSpeed);

        bool canMove = CanMove(playerTransform.position, direction, distance);

        if (!canMove)
        {
            Vector3 directionX = new Vector3(direction.x, 0, 0).normalized;
            canMove = CanMove(playerTransform.position, directionX, distance);
            if (canMove)
            {
                direction = directionX;
            }
            else
            {
                Vector3 directionZ = new Vector3(0, 0, direction.z).normalized;
                canMove = CanMove(playerTransform.position, directionX, distance);
                if (canMove)
                {
                    direction = directionZ;
                }
            }
        }

        if (canMove)
        {
            playerTransform.position += direction * distance;
        }
    }

    private bool CanMove(Vector3 position, Vector3 direction, float distance)
    {
        return !Physics.CapsuleCast(position, position + (Vector3.up * playerHeight), playerRadius, direction,
            distance);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void SetSelectedKitchenCounter(BaseCounter kitchenCounter)
    {
        _selectedKitchenCounter = kitchenCounter;
        OnSelectedKitchenCounterChanged?.Invoke(this, new OnSelectedKitchenCounterChangedEventArgs
        {
            KitchenCounter = kitchenCounter
        });
    }

    public Transform SpawnPoint => spawnPoint;

    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set => _kitchenObject = value;
    }

    public void ClearKitchenObject() => _kitchenObject = null;
    public bool HasKitchenObject() => _kitchenObject != null;
}