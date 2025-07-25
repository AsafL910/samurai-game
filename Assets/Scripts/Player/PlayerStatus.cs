using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    private float hp;
    private float totalhp;
    private float resolve;
    private float totalResolve;
    private bool canSuperSlash;
    private bool canHeal;
    private bool canDoubleJump;
    private float posX;
    private float posY;
    private float posZ;
    private int ShurikenCount;

    private bool sawTutorial;

    private int sceneIndex;

    public PlayerStatus()
    {

    }

    public void Start()
    {
        LoadPlayer();
    }

    public void NewStart()
    {
        InitPlayerStatus(300f, 300f, 0f, 100f, false, true, false, new Vector3(-37, 10, 0), 0, 2);
    }


    public void InitPlayerStatus(float hp, float totalHp, float resolve, float totalResolve, bool canSuperSlash, bool canHeal, bool canDoubleJump, Vector2 positionTransform, int shurikenCount, int sceneIndex)
    {
        this.hp = hp;
        this.totalhp = totalHp;
        this.resolve = resolve;
        this.totalResolve = totalResolve;
        this.canSuperSlash = canSuperSlash;
        this.canHeal = canHeal;
        this.canDoubleJump = canDoubleJump;
        this.posX = positionTransform.x;
        this.posY = positionTransform.y;
        this.posZ = 0f;
        this.ShurikenCount = shurikenCount;
        this.sceneIndex = sceneIndex;
        sawTutorial = false;
    }
    public void InitPlayerStatus(PlayerStatus other)
    {
        this.hp = other.hp;
        this.totalhp = other.totalhp;
        this.resolve = other.resolve;
        this.totalResolve = other.totalResolve;
        this.canSuperSlash = other.canSuperSlash;
        this.canHeal = other.canHeal;
        this.canDoubleJump = other.canDoubleJump;
        this.posX = other.posX;
        this.posY = other.posY;
        this.posZ = other.posZ;
        this.ShurikenCount = other.ShurikenCount;
        this.sceneIndex = other.sceneIndex;
    }

    public void TakeDamage(float damage)
    {
        this.SetHP(this.GetHP() - damage);
    }
    public bool isResolveFull()
    {
        return this.GetResolve() == this.GetTotalResolve();
    }
    public float GetHP()
    {
        return this.hp;
    }

    public void FillHP()
    {
        this.SetHP(this.GetTotalHP());
    }

    public float GetTotalHP()
    {
        return this.totalhp;
    }

    public float GetResolve()
    {
        return this.resolve;
    }

    public float GetTotalResolve()
    {
        return this.totalResolve;
    }

    public bool CanSuperSlash()
    {
        return this.canSuperSlash;
    }

    public bool CanHeal()
    {
        return this.canHeal;
    }

    public bool CanDoubleJump()
    {
        return this.canDoubleJump;
    }

    public Vector3 GetTransform()
    {
        return new Vector3(this.posX, this.posY, this.posZ);
    }

    public int GetSceneIndex()
    {
        return this.sceneIndex;
    }

    public bool HasSeenTutorial()
    {
        return sawTutorial;
    }

    public void SetSawTutorial(bool value)
    {
        this.sawTutorial = value;
    }
    public void SetSceneIndex(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
    }
    public void SetTransform(Vector3 transform)
    {
        this.posX = transform.x;
        this.posY = transform.y;
        this.posZ = transform.z;
    }

    public void SetHP(float value)
    {
        this.hp = value;
    }

    public void FillHp()
    {
        this.hp = this.GetTotalHP();
    }
    public void SetResolve(float value)
    {
        this.resolve = value;
    }

    public void SetCanHeal(bool value)
    {
        this.canHeal = value;
    }
    public void SetCanSuperSlash(bool value)
    {
        this.canSuperSlash = value;
    }
    public void SetCanDoubleJump(bool value)
    {
        this.canDoubleJump = value;
    }

    public void SavePlayer()
    {
        SaveSystem.Save(this);
    }

    public void LoadPlayer()
    {
        var status = SaveSystem.Load();
        if (status == null)
        {
            Debug.Log("Player Status created successfully.");
            NewStart();
        }
        else
        {
            InitPlayerStatus(status);
            Debug.Log("Player Status loaded successfully.");
        }

    }

    public bool HasShuriken()
    {
        return ShurikenCount > 0;
    }

    public void SetShurikenCount(int shurikenCount)
    {
        this.ShurikenCount = shurikenCount;
    }

    public int GetShurikenCount()
    {
        return ShurikenCount;
    }
}
