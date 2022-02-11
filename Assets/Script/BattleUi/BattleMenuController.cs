using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMenuController : MonoBehaviour
{
    public GameObject menu;
    private void Awake() {
        NewGameXml();
        if(PlayerPrefs.GetInt("LoadHallToBattle") == 1)
        {
            Invoke("WaitloadXml", Time.deltaTime);
        }
    }
    public void NewGame()
    {
        UnPause();
        if (Props._prop != null)
        {
            foreach (var item in Props._prop.propslist)
            {
                Destroy(item);
            }
        }
            foreach (var item in Enemy._control.EnemyGameObject)
            {
                Destroy(item);
            }
        if (GameManger._instance != null)
        {
            for (int i = 0; i < (int)MissionType.NONE; i++)
            {
                GameManger._instance.missionStart[i] = false;
                GameManger._instance.missionCompelete[i] = false;
                GameManger._instance.missionCondition[i] = 1;
                GameManger._instance.missionProgress[i] = 0;
            }
        }
        LoadByXml(Application.dataPath + "NewGameXml.text");
    }
    public void SaveButton()
    {
        SaveByXml();
    }
    public void SaveNewGame()
    {
        NewGameXml();
    }
    public void LoadButton()
    {
        UnPause();
        foreach (var item in Props._prop.propslist)
        {
            Destroy(item);
        }
        foreach (var item in Enemy._control.EnemyGameObject)
        {
            Destroy(item);
        }
        Invoke("WaitloadXml", Time.deltaTime);
        //StartCoroutine("WaitloadXml");
    }

    public void WaitloadXml()
    {
        LoadByXml(Application.dataPath + "DataXml.text");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
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

    private GameSave CreatSaveManager()
    {
        GameSave save = new GameSave();

        save.currentHealth = GameManger._instance.currentHealth;
        save.maxHealth = GameManger._instance.maxHealth;

        save.playerPositionX = GameManger._instance.position.x;
        save.playerPositionY = GameManger._instance.position.y;
        if (GameManger._instance.missionStart != null)
        {
            foreach (var item in GameManger._instance.missionStart.Keys)
            {
                save.missionStart[item] = GameManger._instance.missionStart[item];
                save.missionCompelete[item] = GameManger._instance.missionCompelete[item];
                save.missionCondition[item] = GameManger._instance.missionCondition[item];
                save.missionProgress[item] = GameManger._instance.missionProgress[item];
            }

        }

        for (int i = 0; i < (int)ENEMY.NONE; i++)
        {
            if (Enemy._control.EnmeyPosition[(ENEMY)i].Count != 0)
            {
                save.enemyPosition.Add((ENEMY)i, Enemy._control.EnmeyPosition[(ENEMY)i]);

                save.isDead.Add((ENEMY)i, Enemy._control.EnemyIsDead[(ENEMY)i]);

            }

        }

        for (int i = 0; i < (int)GAMEPROPS.NONE; i++)
        {
            if (Props._prop != null && Props._prop.PropsNumber[(GAMEPROPS)i] != 0)
            {
                save.PropsNumber[(GAMEPROPS)i] = Props._prop.PropsNumber[(GAMEPROPS)i];
                if (Props._prop.PropsNumber[(GAMEPROPS)i] != 0)
                {
                    save.PropsPosition[(GAMEPROPS)i] = Props._prop.PropsPosition[(GAMEPROPS)i];
                }
            }
        }


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

        XmlElement playerMission = xmlDocument.CreateElement("Mission");
        player.AppendChild(playerMission);

        XmlElement missionId, missionCondition, missionSatrt, missionCompelete, missionProgress;

        foreach (var item in save.missionCondition.Keys)
        {
            missionCondition = xmlDocument.CreateElement("MissionCondition");
            missionId = xmlDocument.CreateElement("MissionId");
            missionSatrt = xmlDocument.CreateElement("MissionStart");
            missionCompelete = xmlDocument.CreateElement("MissionCompelete");
            missionProgress = xmlDocument.CreateElement("KillNumber");

            missionId.InnerText = item.ToString();
            missionCondition.InnerText = save.missionCondition[item].ToString();
            missionSatrt.InnerText = save.missionStart[item].ToString();
            missionCompelete.InnerText = save.missionCompelete[item].ToString();
            missionProgress.InnerText = save.missionProgress[item].ToString();

            playerMission.AppendChild(missionId);
            playerMission.AppendChild(missionSatrt);
            playerMission.AppendChild(missionCompelete);
            playerMission.AppendChild(missionProgress);
            playerMission.AppendChild(missionCondition);
        }
        XmlElement enemy = xmlDocument.CreateElement("Enemy");
        root.AppendChild(enemy);
        for (int i = 0; i < (int)ENEMY.NONE; i++)
        {
            if (Enemy._control.EnmeyPosition[(ENEMY)i].Count != 0)
            {
                for (int j = 0; j < Enemy._control.EnmeyPosition[(ENEMY)i].Count; j++)
                {
                    XmlElement enemyType = xmlDocument.CreateElement((ENEMY)i + "");
                    enemy.AppendChild(enemyType);
                    XmlElement enemyId, enemyPositionX, enemyPositionY, isDead;
                    enemyId = xmlDocument.CreateElement((ENEMY)i + "Id");
                    enemyPositionX = xmlDocument.CreateElement((ENEMY)i + "PositionX");
                    enemyPositionY = xmlDocument.CreateElement((ENEMY)i + "PositionY");
                    isDead = xmlDocument.CreateElement((ENEMY)i + "isDead");

                    enemyId.InnerText = j.ToString();
                    enemyPositionX.InnerText = save.enemyPosition[(ENEMY)i][j].x.ToString();
                    enemyPositionY.InnerText = save.enemyPosition[(ENEMY)i][j].y.ToString();
                    isDead.InnerText = save.isDead[(ENEMY)i][j].ToString();

                    enemyType.AppendChild(enemyId);
                    enemyType.AppendChild(enemyPositionX);
                    enemyType.AppendChild(enemyPositionY);
                    enemyType.AppendChild(isDead);
                }
                //from stack top to bottom 5-0
                // foreach (var item in Enemy._control.EnmeyPosition[(ENEMY)i].Keys)
                // {
                //     XmlElement enemyType = xmlDocument.CreateElement((ENEMY)i + "");
                //     enemy.AppendChild(enemyType);
                //     XmlElement enemyId, enemyPositionX, enemyPositionY, isDead;
                //     enemyId = xmlDocument.CreateElement((ENEMY)i + "Id");
                //     enemyPositionX = xmlDocument.CreateElement((ENEMY)i + "PositionX");
                //     enemyPositionY = xmlDocument.CreateElement((ENEMY)i + "PositionY");
                //     isDead = xmlDocument.CreateElement((ENEMY)i + "isDead");

                //     enemyId.InnerText = item.ToString();
                //     enemyPositionX.InnerText = save.enemyPosition[(ENEMY)i][item].x.ToString();
                //     enemyPositionY.InnerText = save.enemyPosition[(ENEMY)i][item].y.ToString();
                //     isDead.InnerText = save.isDead[(ENEMY)i][item].ToString();

                //     enemyType.AppendChild(enemyId);
                //     enemyType.AppendChild(enemyPositionX);
                //     enemyType.AppendChild(enemyPositionY);
                //     enemyType.AppendChild(isDead);
                // }
            }
        }
        XmlElement props = xmlDocument.CreateElement("Prop");
        if (props.HasChildNodes)
        {
            props.RemoveAll();
        }
        root.AppendChild(props);

        for (int i = 0; i < (int)GAMEPROPS.NONE; i++)
        {
            XmlElement propType, propId, propPositionX, propPositionY;
            if (Props._prop != null && save.PropsNumber.ContainsKey((GAMEPROPS)i))
            {
                for (int j = 0; j < save.PropsNumber[(GAMEPROPS)i]; j++)
                {
                    propType = xmlDocument.CreateElement(((GAMEPROPS)i).ToString());
                    propId = xmlDocument.CreateElement("PropTypeId");
                    propPositionX = xmlDocument.CreateElement((GAMEPROPS)i + "PositionX");
                    propPositionY = xmlDocument.CreateElement((GAMEPROPS)i + "PositionY");

                    propId.InnerText = i + "";
                    propPositionX.InnerText = save.PropsPosition[(GAMEPROPS)i][j].x.ToString();
                    propPositionY.InnerText = save.PropsPosition[(GAMEPROPS)i][j].y.ToString();

                    props.AppendChild(propType);
                    propType.AppendChild(propId);
                    propType.AppendChild(propPositionX);
                    propType.AppendChild(propPositionY);
                }
            }

        }
        #endregion

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "DataXml.text");
        if (File.Exists(Application.dataPath + "DataXml.text"))
        {
            Debug.Log(Application.dataPath + "DataXml.text");
        }
    }

    private void LoadByXml(string Path)
    {
        if (File.Exists(Path))
        {
            GameSave save = new GameSave();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path);
            #region Load SaveXml
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

            PlayerController.player.gameObject.transform.position = GameManger._instance.position;

            for (int i = 0; i < (int)ENEMY.NONE; i++)
            {
                XmlNodeList enemyGetType = xmlDocument.GetElementsByTagName((ENEMY)i + "");
                if (enemyGetType != null)
                {
                    Dictionary<int, Vector2> list = new Dictionary<int, Vector2>();
                    save.enemyPosition.Add((ENEMY)i, list);

                    Dictionary<int, bool> EnemyisDead = new Dictionary<int, bool>();
                    save.isDead.Add((ENEMY)i, EnemyisDead);

                    if (enemyGetType.Count != 0)
                    {
                        for (int j = 0; j < enemyGetType.Count; j++)
                        {
                            XmlNodeList enemyName = xmlDocument.GetElementsByTagName((ENEMY)i + "Id");
                            int Enemyid = int.Parse(enemyName[j].InnerText);

                            XmlNodeList enemyPositionX = xmlDocument.GetElementsByTagName((ENEMY)i + "PositionX");
                            float positionX = float.Parse(enemyPositionX[j].InnerText);

                            XmlNodeList enemyPositionY = xmlDocument.GetElementsByTagName((ENEMY)i + "PositionY");
                            float positionY = float.Parse(enemyPositionY[j].InnerText);
                            if (!save.enemyPosition[(ENEMY)i].ContainsKey(Enemyid))
                            {
                                save.enemyPosition[(ENEMY)i].Add(Enemyid, new Vector2(positionX, positionY));
                            }
                            else
                            {

                                save.enemyPosition[(ENEMY)i][Enemyid] = new Vector2(positionX, positionY);
                            }

                            XmlNodeList enemyIsDead = xmlDocument.GetElementsByTagName((ENEMY)i + "isDead");
                            bool isDead = bool.Parse(enemyIsDead[j].InnerText);
                            if (save.isDead[(ENEMY)i].ContainsKey(Enemyid))
                            {
                                save.isDead[(ENEMY)i][Enemyid] = isDead;
                            }
                            else
                            {
                                save.isDead[(ENEMY)i].Add(Enemyid, isDead);
                            }

                            if (Enemy._control.EnmeyPosition[(ENEMY)i].ContainsKey(j))
                            {
                                Enemy._control.EnmeyPosition[(ENEMY)i][Enemyid] = save.enemyPosition[(ENEMY)i][Enemyid];
                                Enemy._control.EnemyIsDead[(ENEMY)i][Enemyid] = save.isDead[(ENEMY)i][Enemyid];
                            }
                            else
                            {
                                Enemy._control.EnmeyPosition[(ENEMY)i].Add(Enemyid, save.enemyPosition[(ENEMY)i][Enemyid]);
                                Enemy._control.EnemyIsDead[(ENEMY)i].Add(Enemyid, save.isDead[(ENEMY)i][Enemyid]);
                            }

                            Instantiate(Enemy._control.gameObjects[i], Enemy._control.EnmeyPosition[(ENEMY)i][Enemyid], Quaternion.identity);

                        }
                    }
                }

            }

            XmlNodeList mission = xmlDocument.GetElementsByTagName("Mission");
            if (mission != null)
            {
                for (int i = 0; i < mission.Count; i++)
                {
                    XmlNodeList missionId = xmlDocument.GetElementsByTagName("MissionId");
                    int mission_Id = int.Parse(missionId[i].InnerText);
                    XmlNodeList missionStart = xmlDocument.GetElementsByTagName("MissionStart");
                    bool mission_Start = bool.Parse(missionStart[i].InnerText);
                    XmlNodeList missionCompelete = xmlDocument.GetElementsByTagName("MissionCompelete");
                    bool mission_Competele = bool.Parse(missionCompelete[i].InnerText);
                    XmlNodeList missionKillNumber = xmlDocument.GetElementsByTagName("KillNumber");
                    int mission_killNumber = int.Parse(missionKillNumber[i].InnerText);
                    XmlNodeList missionCondition = xmlDocument.GetElementsByTagName("MissionCondition");
                    int mission_Condition = int.Parse(missionCondition[i].InnerText);

                    if (save.missionCondition.ContainsKey(mission_Id))
                    {
                        save.missionStart[mission_Id] = mission_Start;
                        save.missionCompelete[mission_Id] = mission_Competele;
                        save.missionProgress[mission_Id] = mission_killNumber;
                    }
                    else
                    {
                        save.missionStart.Add(mission_Id, mission_Start);
                        save.missionCompelete.Add(mission_Id, mission_Competele);
                        save.missionProgress.Add(mission_Id, mission_killNumber);
                        save.missionCondition.Add(mission_Id, mission_Condition);
                    }
                    GameManger._instance.missionStart[mission_Id] = save.missionStart[mission_Id];
                    GameManger._instance.missionCompelete[mission_Id] = save.missionCompelete[mission_Id];
                    GameManger._instance.missionProgress[mission_Id] = save.missionProgress[mission_Id];
                    GameManger._instance.missionCondition[mission_Id] = save.missionCondition[mission_Id];
                }
            }

            for (int i = 0; i < (int)GAMEPROPS.NONE; i++)
            {
                XmlNodeList propType = xmlDocument.GetElementsByTagName(((GAMEPROPS)i).ToString());
                if (propType != null)
                {
                    for (int j = 0; j < propType.Count; j++)
                    {
                        XmlNodeList propId = xmlDocument.GetElementsByTagName("PropTypeId");
                        int prop_Id = int.Parse(propId[j].InnerText);
                        XmlNodeList propPositionX = xmlDocument.GetElementsByTagName((GAMEPROPS)i + "PositionX");
                        float prop_PositionX = float.Parse(propPositionX[j].InnerText);
                        XmlNodeList propPositionY = xmlDocument.GetElementsByTagName((GAMEPROPS)i + "PositionY");
                        float prop_PositionY = float.Parse(propPositionY[j].InnerText);

                        Instantiate(Props._prop.gameObjects[i], new Vector2(prop_PositionX, prop_PositionY), Quaternion.identity);
                    }
                }
            }
            #endregion
        }
        else
        {
            Debug.Log("No save Files");
        }
    }
    private GameSave NewGameSaveCreate()
    {
        GameSave save = new GameSave();

        save.currentHealth = 5;
        save.maxHealth = 5;

        save.playerPositionX = 0;
        save.playerPositionY = 0;

        return save;
    }

    private void NewGameXml()
    {
        GameSave save = NewGameSaveCreate();
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

        xmlDocument.Save(Application.dataPath + "NewGameXml.text");
    }
}
