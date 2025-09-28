<table width="100%">
  <tr>
    <!-- Top large gif -->
    <td colspan="2" align="center">
      <img src="https://github.com/rchtr-chn/Lies-In-The-Unknown/raw/main/gif-1.gif" width="100%"/>
    </td>
  </tr>
  <tr>
    <!-- Bottom two gifs -->
    <td align="center" width="50%">
      <img src="https://github.com/rchtr-chn/Lies-In-The-Unknown/raw/main/gif-2.gif" width="100%"/>
    </td>
    <td align="center" width="50%">
      <img src="https://github.com/rchtr-chn/Lies-In-The-Unknown/raw/main/gif-3.gif" width="100%"/>
    </td>
  </tr>
</table>

<h2>👹 Lies In The Unknown</h2>
  <img width=350px align="left" src=https://img.itch.zone/aW1hZ2UvMzc4NzQ2Ny8yMjU2OTM1My5wbmc=/347x500/kOxbxh.png>

Sitting alone in his room, eyes lost in depression — directionless, abandoned, without purpose. Should his life come to an end?

For a moment, his mind feels dizzy, when he open his eyes, it got him puzzled about where he is.

Then, a mysterious figure appears — one that reminds him of…

THE TRAUMA…

Will he uncover what’s been lies...

Or… (There's 2 ending)


<h2>⬇️ Game Pages</h2>
  itch.io: https://rchtr-chn.itch.io/lies-in-the-unknown
  
<h2>🎮 Controls</h2>

  | Input | Function |
  | -------------------- | --------------------- |
  | A Key | Move Left |
  | D Key | Move Right |
  | Q Key | Accept (interact) |
  | E Key | Reject (interact) |
  | Space key | Jump |
  | Left Mouse Button | Shoot/Attack |
  
<h2>📋 Project Information</h2>

  ![Unity Version 2022.3.19f1](https://img.shields.io/badge/Unity_Version-2022.3.19f1-FFFFFF.svg?style=flat-square&logo=unity) <br/>
  Game Build: ![Windows](https://img.shields.io/badge/Windows-004fe1.svg?style=flat-square&logo=windows) <br/>
  All art/visual assets are made by our <a href="https://kelvinkel.carrd.co">game artist</a>. <br/>
  All soundtracks/background musics are made by our <a href="https://jordytandiano.carrd.co">game designer</a>. <br/>
  All SFX can be found in [![Pixabay](https://img.shields.io/badge/Pixabay-191B26.svg?style=flat-square&logo=Pixabay)](https://pixabay.com) <br/> <br/>
  
  <b>Team:</b>
  - <a href="https://github.com/rchtr-chn">Richter Cheniago</a> (Game programmer)
  - <a href="https://kelvinkel.carrd.co">Kelvin</a> (Game artist)
  - <a href="https://jordytandiano.my.canva.site">Jordy Tandiano</a> (Game designer and music artist)

<h2>💡 My Contributions</h2>

  As the sole programmer of this project, I am tasked to make all of the mechanics that make the game function as intended, such as the player move and shoot mechanic, enemy AI/state machine, and scene manager.
<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `CameraMoveScript.cs ` | Manages camera movement according to player and level interaction |
  | `PlayerAimingScript.cs` | Managers playter's ability to aim via mouse cursor|
  | `ExplosionExpandHitboxScript.cs` | Manages enemy projectiles to expand it's hitbox |
  | `BossHealthManager.cs` | Manages both bosses' healthpoint |
  | `LevelManagerScript.cs` | Manages level initialization and events depending on level progression |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── Lies-In-The-Unknown              # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Misc                         # Stores all types of miscellanous materials/components used in this project (ex: audio mixers, input action map, physics 2D material, etc)
      ├── Resources                    # Parent folder to organize scriptable objects, visual assets, sound assets, and prefabs
        ├── ArtAssets                  # Parent folder of all visual assets that are used in this project
        ├── ArtAssets                  # Parent folder of all sound assets that are used in this project
        ├── Prefabs                    # Parent folder that stores prefabs that are instantiated during the project's runtime
        ├── Videos                     # Contains all video cutscenes that are used in this project
      ├── Scenes                       # Stores all Unity Scenes used in this project
      ├── Scripts                      # Parent folder of all types of scripts that are used in this project
        ├── Player                     # Stores scripts related to input functionality for the player
        ├── Enemy                      # Stores scripts related to the functionality and state machine of enemy
        ├── Managers                   # Stores scripts related to managers that function beneath the surface of this project
        ├── Settings                   # Stores scripts related to the settings menu's functionality
      ...
    ...
  ...
  ```
