using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupScript : MonoBehaviour {
    public PlayerMovement pm;
    public CanvasHandler ch;
    List<PowerUpSpeed> bonus;
    List<PowerUpBoost> boost, boostBackup;
    void Start () {
        bonus = new List<PowerUpSpeed>();
        boost = new List<PowerUpBoost>();
        boostBackup = new List<PowerUpBoost>();
    }
    public bool BackupBoost(PowerUpBoost p)
    {
        if(boost.Count < 4)
        {
            boost.Add(p);
            return true;
        }
        return false;
    }
    public void BackUpBonus(PowerUpSpeed p)
    {
        bonus.Add(p);
    }
    public int BoostCount()
    {
        return boost.Count;
    }
    public void UseNitro()
    {
        if (boost.Count > 0)
        {
            boostBackup.Add(boost[0]);
            boost.RemoveAt(0);
            StartCoroutine(pm.UseNitro());
            ch.UpdateBoost(boost.Count);
        }
        if (boost.Count == 0)
        {
            ch.ActivateBoost(false);
        }
    }
    //Reactivates the bonuses on the scene
    public void RestoreAll()
    {
        Debug.Log("Backing up..");
        foreach (PowerUpSpeed g in bonus)
        {
            g.GetComponent<BoxCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (PowerUpBoost g in boost)
        {
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (PowerUpBoost g in boostBackup)
        {
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        //Clear the backups lists
        bonus.Clear();
        boost.Clear();
        boostBackup.Clear();
    }
}
