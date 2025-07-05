using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    private float hp;
    private float totalhp;
    private float resolve;
    private float totalResolve;
    private bool canSuperSlash;
    private bool canHeal;
    private bool canDoubleJump;
    private Transform positionTransform;
    public int ShurikenCount;

    private void Start()
    {
        InitPlayerStatus(300f, 300f, 0f, 100f, false, true, true, gameObject.transform, 0);
        //InitPlayerStatus(loadPlayer());
    }

    public void InitPlayerStatus(float hp, float totalHp, float resolve, float totalResolve, bool canSuperSlash, bool canHeal, bool canDoubleJump, Transform positionTransform, int shurikenCount)
    {
        this.hp = hp;
        this.totalhp = totalHp;
        this.resolve = resolve;
        this.totalResolve = totalResolve;
        this.canSuperSlash = canSuperSlash;
        this.canHeal = canHeal;
        this.canDoubleJump = canDoubleJump;
        this.positionTransform = positionTransform;
        this.ShurikenCount = shurikenCount;
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
        this.positionTransform = other.positionTransform;
        this.ShurikenCount = other.ShurikenCount;
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

    public Transform GetTransform()
    {
        return this.positionTransform;
    }

    public void SetTransform(Transform transform)
    {
        this.positionTransform = transform;
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

    public void savePlayer()
    {
        SaveSystem.Save(this);
    }

    public PlayerStatus loadPlayer()
    {
        return SaveSystem.Load();
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
