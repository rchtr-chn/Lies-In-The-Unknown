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
  - <a href="https://jordytandiano.carrd.co">Jordy Tandiano</a> (Game designer and music artist)

<h2>💡 My Contributions</h2>

  as the sole programmer of this project, I am tasked to make all of the mechanics that make the game function as intended, such as the player move and shoot mechanic, enemy AI/state machine, and scene manager.
<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `DeckManagerScript.cs` | Manages starting deck and saves any modification done to deck by player |
  | `HandManagerScript.cs` | Receives cards from `DeckManagerScript.cs` to be drawn on hand and returned to when needed|
  | `GameManagerScript.cs` | Organizes and centralized other minor managers and manages the turn-based system |
  | `ShopManagerScript.cs` | Manages the shop's cards to be displayed and sold to the player |
  | `Card.cs` | Blueprint for SOs that will carry a card's value and the potential card effect |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── Rat-Gambler                      # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Audio                        # Stores all BGM and audio clips used in this project
      ├── Fonts                        # Stores all fonts used in this project
      ├── Resources                    # Parent folder to organize blueprints (Scriptable Objects) and prefabs
        ├── CardData                   # Parent folder of all scriptable object types that are used in this project
          ...
        ├── Prefabs                    # Parent folder that stores prefabs that are instantiated during the project's runtime
          ...
      ├── Scenes                       # Stores all Unity Scenes used in this project
      ├── Scripts                      # Parent folder of all types of scripts that are used in this project
        ├── BackgroundManagers         # Stores scripts related to managers that function the game in the background
        ├── CardBehavior               # Stores scripts related to a card prefab
        ├── CardEffects                # Stores scripts consisting the logic behind every power cards
        ├── Cardshop                   # Stores scripts related to the card shop
        ├── CardSystem                 # Stores scripts related to card deck creation and usability during gameplay
        ├── Cookie                     # Stores scripts related to wagering cookies mechanic and cookie value modification
      ├── Sprites                      # Parent folder of all sprites that are used in this project
      ...
    ...
  ...
  ```
