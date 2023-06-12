using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Menu menu;
    [SerializeField] private GameData gameData;
    [SerializeField] private ItemPrefabStorage prefabStorage;

    private void Awake()
    {
        gameData.Initalize();
        prefabStorage.Initalize();
        inventory.Initalize();
        menu.Initalize();
        player.Initalize();
        objectSpawner.Initalize();

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
