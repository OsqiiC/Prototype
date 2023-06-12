using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Image reloadingImage;
    [SerializeField] private HorizontalLayoutGroup bulletGroup;

    public UnityEvent onPressed => button.onClick;

    private List<GameObject> bullets = new List<GameObject>();

    public void CreateBullets(int count)
    {
        Clear();
        for (int i = 0; i < count; i++)
        {
            var bullet = Instantiate(bulletPrefab, bulletGroup.transform);
            bullet.gameObject.SetActive(true);
            bullets.Add(bullet);
        }
    }

    public void DestroyBullet()
    {
        if (bullets.Count < 1) return;
        var bullet = bullets[0];
        Destroy(bullet);
        bullets.Remove(bullet);
    }

    public void SetReloadFill(float value)
    {
        reloadingImage.fillAmount = value;
    }

    private void Clear()
    {
        foreach (var item in bullets)
        {
            Destroy(item);
        }

        bullets.Clear();
    }
}
