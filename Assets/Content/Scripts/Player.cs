using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character, IPersistedData
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Transform bulletEmmiter;
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private ShootButton shootButton;
    [SerializeField] private RangedAttack bulletProjectile;
    [SerializeField] private ItemCollector itemCollector;
    [SerializeField] private Inventory inventory;
    [SerializeField] private float reloadingDuration = 1;

    private int maxBulletsCount = 5;
    private int bulletsCount = 5;
    private bool reloading = false;
    private Vector2 movementDirection;

    public void Initalize()
    {
        itemCollector.onItemObtained.AddListener(AddItem);
        inventory.onItemRemoved.AddListener(RemoveItem);
        shootButton.CreateBullets(maxBulletsCount);
        shootButton.onPressed.AddListener(Shoot);
        GameData.Instance.persistedDatas.Add(this);
    }

    private void Update()
    {
        SetMovementDirection();
        characterMovement.Move(movementDirection);
        RotateCharacter();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void SetMovementDirection()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var Vertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, Vertical).normalized;

        if (movementDirection.magnitude != 0) return;

        movementDirection = joystick.Direction;
    }

    private void RotateCharacter()
    {
        if (movementDirection.x == 0) return;

        transform.rotation = Quaternion.Euler(movementDirection.x < 0 ? Vector3.zero : Vector3.up * 180);
    }

    private void Shoot()
    {
        if (reloading) return;

        bulletProjectile.Create(this, bulletContainer, bulletEmmiter.position - transform.position, bulletEmmiter.position);
        bulletsCount--;
        shootButton.DestroyBullet();
        if (bulletsCount > 0) return;

        StartCoroutine(Reload());
    }

    private void AddItem(Item item)
    {
        inventory.AddItem(item);
        entity.items.Add(item.itemData);
    }

    private IEnumerator Reload()
    {
        reloading = true;

        for (float i = 0; i < reloadingDuration; i += Time.deltaTime)
        {
            shootButton.SetReloadFill(i / reloadingDuration);
            yield return null;
        }

        bulletsCount = maxBulletsCount;
        shootButton.SetReloadFill(0);
        shootButton.CreateBullets(maxBulletsCount);

        reloading = false;
    }

    private void RemoveItem(int itemID)
    {
        var item = entity.items.Find(item => item.itemID == itemID);
        entity.items.Remove(item);
    }

    protected override void Death()
    {
        base.Death();
        GameData.Instance.persistedDatas.Remove(this);
        entity.healthComponent.currentHealth = entity.healthComponent.maxHealth;
        entity.rotation = new Entity.Vector3Json(transform.rotation.eulerAngles);
        entity.position = new Entity.Vector3Json(transform.position);
        GameData.Instance.SetPlayerData(entity);
    }

    void IPersistedData.Save()
    {
        entity.rotation = new Entity.Vector3Json(transform.rotation.eulerAngles);
        entity.position = new Entity.Vector3Json(transform.position);
        GameData.Instance.SetPlayerData(entity);
    }

    void IPersistedData.Load()
    {
        if (GameData.Instance.playerEntity == null)
        {
            return;
        }

        entity = GameData.Instance.playerEntity;

        transform.position = entity.position.Vector3;
        transform.rotation = Quaternion.Euler(entity.rotation.Vector3);
        TakeDamage(0);
        // inventory.Clear();
        foreach (var item in entity.items)
        {
            inventory.AddItem(item);
        }
    }
}
