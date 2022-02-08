using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class BattleMenuController : MonoBehaviour
{
    public GameObject menu;

    private void Start() {
        UnPause();
    }
    public void SaveButton(){
        SaveByXml();
    }
    public void HallIntoBattle()
    {
        LoadByXml();
    }
    public void LoadButton(){
        UnPause();
        LoadByXml();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private GameSave CreatSaveManager()
    {
        GameSave save = new GameSave();

        save.currentHealth = GameManger._instance.currentHealth;
        save.maxHealth = GameManger._instance.maxHealth;

        save.playerPositionX = GameManger._instance.position.x;
        save.playerPositionY = GameManger._instance.position.y;

        return save;
    }

    private void SaveByXml()
    {
        GameSave save = CreatSaveManager();
        XmlDocument xmlDocument = new XmlDocument();

        #region CreatXml elements
        XmlElement root = xmlDocument.CreateElement("Save"); // MRRKER <Save> elements </Save>
        root.SetAttribute("FileName", "File_01");

        XmlElement player = xmlDocument.CreateElement("Character");
        root.AppendChild(player);

        XmlElement playerHealth = xmlDocument.CreateElement("Health");
        player.AppendChild(playerHealth);

        XmlElement playerCurrentHealth = xmlDocument.CreateElement("CurrentHealth");
        playerCurrentHealth.InnerText = save.currentHealth.ToString();
        playerHealth.AppendChild(playerCurrentHealth);

        XmlElement playerMaxHealth = xmlDocument.CreateElement("MaxHealth");
        playerMaxHealth.InnerText = save.maxHealth.ToString();
        playerHealth.AppendChild(playerMaxHealth);

        XmlElement playerPosition = xmlDocument.CreateElement("Postition");
        player.AppendChild(playerPosition);

        XmlElement playerPositionX = xmlDocument.CreateElement("PositionX");
        playerPositionX.InnerText = save.playerPositionX.ToString();
        playerPosition.AppendChild(playerPositionX);

        XmlElement playerPositionY = xmlDocument.CreateElement("PositionY");
        playerPositionY.InnerText = save.playerPositionY.ToString();
        playerPosition.AppendChild(playerPositionY);

        #endregion

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "DataXml.text");
        if(File.Exists(Application.dataPath + "DataXml.text"))
        {
            Debug.Log(Application.dataPath + "DataXml.text");
        }
    }

    private void LoadByXml()
    {
        if(File.Exists(Application.dataPath + "DataXml.text"))
        {
            GameSave save = new GameSave();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "DataXml.text");

            // Get the Save File Data from the File
            XmlNodeList playerCurrentHealth = xmlDocument.GetElementsByTagName("CurrentHealth");
            int currentHealth = int.Parse(playerCurrentHealth[0].InnerText);
            save.currentHealth = currentHealth;

            XmlNodeList playerMaxHealth = xmlDocument.GetElementsByTagName("MaxHealth");
            int maxHealth = int.Parse(playerMaxHealth[0].InnerText);
            save.maxHealth = maxHealth;
            
            XmlNodeList playerPositionX = xmlDocument.GetElementsByTagName("PositionX");
            float PositionX = float.Parse(playerPositionX[0].InnerText);
            save.playerPositionX = PositionX;

            XmlNodeList playerPositionY = xmlDocument.GetElementsByTagName("PositionY");
            float PositionY = float.Parse(playerPositionY[0].InnerText);
            save.playerPositionY = PositionY;

            GameManger._instance.currentHealth = save.currentHealth;
            GameManger._instance.maxHealth = save.maxHealth;
            GameManger._instance.position = new Vector2(PositionX, PositionY);
            if(PlayerController.player != null)
            {
                PlayerController.player.transform.position = GameManger._instance.position;
            }
        }
        else
        {
            Debug.Log("No save Files");
        }
    }

    private void Pause() {
        GameManger._instance.isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    private void UnPause()
    {
        GameManger._instance.isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Continue()
    {
        UnPause();
    }
}
