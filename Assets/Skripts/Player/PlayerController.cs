using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Все для анимации
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking, dash, sword, archery;
    public AnimationReferenceAsset[] anims;
    public Entity entity;
    public PlayerAttack playerAttack;
    public PlayerShoot playerShoot;
    float timer = 0;
    public int maxRoots;
    public int numRoots;

    internal AnimationController animController;

    public Level playerLevel;

    void Start()
    {
        animController = GetComponent<AnimationController>();
        entity = GetComponent<Entity>();
        playerAttack = GetComponent<PlayerAttack>();
        playerShoot = GetComponent<PlayerShoot>();
        numRoots = maxRoots - 3;
        entity.currentHP = entity.maxHP * 0.75f;
        playerShoot.numArrows = playerShoot.maxArrows - 10;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && timer > 0.5f)
            Heal();
        if (Input.GetKey(KeyCode.F5) && timer > 1f)
            Save();
        if (Input.GetKey(KeyCode.F6) && timer > 1f)
            Load();
        if (Input.GetKey(KeyCode.Tab) && timer > 1f)
            ChangeAnimMode();

        timer += Time.deltaTime;
    }

    // Изменяет скин игрока.
    void ChangeAnimMode()
    {
        if (animController.skeletonAnimation.skeleton.Skin.ToString() == "swordsman")
            animController.skeletonAnimation.Skeleton.SetSkin("archer");
        else
            animController.skeletonAnimation.Skeleton.SetSkin("swordsman");

        timer = 0;
    }

    void Heal()
    {
        if (entity.currentHP == entity.maxHP)
            return;
        if (numRoots == 0)
            return;
        entity.currentHP += entity.maxHP * 0.20f;
        if (entity.currentHP > entity.maxHP)
            entity.currentHP = entity.maxHP;
        numRoots -= 1;
        timer = 0;
    }

    public void Save()
    {
        SaveLoadSystem.Save(this);
    }

    public void Load()
    {
        PlayerData data = SaveLoadSystem.Load();
        entity.maxHP = data.maxHP;
        entity.currentHP = data.currentHP;
        maxRoots = data.maxRoots;
        numRoots = data.numRoots;
        playerAttack.damage = data.damage;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
