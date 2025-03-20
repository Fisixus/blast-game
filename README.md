# Blast Game 🎮

A clone of the popular mobile game **Toon Blast**, built using **Unity** and **C#**. This puzzle game challenges players to match 2 or more blocks of the same color to clear them from the board. Each level has specific objectives, and players must complete them to progress to the next level.

[![Watch the video](https://img.youtube.com/vi/usEHX9ypPmM?feature=share/0.jpg)](https://youtube.com/shorts/usEHX9ypPmM?feature=share)

---

## 📖 About the Project

This project is **not intended for commercial use**. It replicates core gameplay mechanics of Toon Blast while implementing clean architectural practices and efficient development patterns.

---

## 🛠️ Built With

- **Unity 2022.3.8**
- **C#**

### Third-Party Libraries:
- [DoTween](http://dotween.demigiant.com/) - Tweening engine for animations
- [UniTask](https://github.com/Cysharp/UniTask) - Async/await utilities for Unity
- [Yellowpaper.SerializedDictionary](https://github.com/yellowpaper-dev/serialized-dictionary) - Serializable dictionary for Unity

### Custom Implementations:
- Custom **DI (Dependency Injection)** library
- Custom **UTask** library
---
## 🧩 Architecture

This project follows the **Model-View-Presenter (MVP)** architectural pattern for clean separation of concerns and maintainability. The structure is designed to ensure clarity, scalability, and testability across all game systems.

### Key Components

1. **Models**:
   - Models are responsible for storing and managing **game data**.
   - They encapsulate the state and rules of the game without direct interaction with Unity components.
   - Examples include:
     - Grid data for levels
     - Player progress and saved state

2. **Views**:
   - Views handle **visual feedback** and **user interaction**.
   - They are responsible for rendering the game’s UI and scene elements but do not contain any logic.
   - Views react to data changes communicated by the Presenters.
   - Examples:
     - Visual representation of the game grid
     - UI elements such as the LevelButton and Fail popup

3. **Presenters**:
   - Presenters act as the **logic layer** between Models and Views.
   - They process user input, update models, and synchronize the views accordingly.
   - Presenters are **high-level managers** of the game’s flow, thanks to their reliance on **Handler classes**.

4. **Handlers**:
   - Handlers are the **abstraction layer** for Presenters.
   - They break down game logic into more manageable units, ensuring Presenters remain clean and focused on orchestration.
   - Handlers handle specific responsibilities, such as:
     - **Interfacing with Factories** to create and destroy game objects
     - Managing specific game mechanics (e.g., TNT explosions, grid updates)
   - Examples:
     - MatchHandler: Handles matching logic and combo interactions.
     - EffectHandler: Manages visual effects like explosions and particle systems.

5. **Factories**:
   - Factories are responsible for **creating and destroying game objects** efficiently.
   - They are accessed exclusively through Handlers to maintain abstraction.
   - Examples:
     - ItemFactory: Creates new grid items (e.g., cubes, obstacles, TNT).
     - EffectFactory: Creates visual effects like particle systems or animations.

---
### Game Flow

1. **StartScene**:
   - The entry point of the project where the `ProjectContext` is activated.
   - The `ProjectContext` initializes the dependency injection system and ensures core services are ready for use.

2. **GamePresenter**:
   - The **GamePresenter** is initialized as **non-lazy** in the `ProjectContext`.
   - It sets up the core game logic and manages the overall flow of the game.

3. **MainScene**:
   - The **GamePresenter** activates the **ScenePresenter**, which loads the **MainScene**.
   - In the **MainScene**, the player can select a level using the **LevelButton**. The button displays the current level or "Finished" if all levels are complete.

4. **LevelScene**:
   - After selecting a level, the **LevelScene** is loaded.
   - The `SceneContext` inside the **LevelScene** is activated, dynamically initializing the level-specific data and systems for that level.
---

This architecture ensures:
- **Separation of Concerns**: Each layer has a specific responsibility, minimizing dependencies and ensuring scalability.
- **Clean Logic**: Presenters remain focused on high-level orchestration, with Handlers managing specific tasks.
- **Efficient Resource Management**: Factories optimize object creation and destruction, ensuring smooth gameplay even in complex scenarios.


---

## 🛠️ Features

- **Dynamic Level Editor**:
  Access the **Level Editor** from **"Tools > Level Editor"** in the Unity Editor. This tool allows you to:
  - Dynamically update the grid.
  - Modify the move count.
  
- **Multiple Levels**:
  Players progress through levels by completing specific objectives in each one.

---

## 🎮 Gameplay Summary

The game is a **level-based puzzle game** where the player must clear all obstacles to win the level. Here’s the gameplay flow and mechanics:

### Flow
1. In the **MainScene**, where the player sees a **LevelButton** displaying the current level.
   - If all levels are completed, the **LevelButton** displays "Finished".
2. Clicking the **LevelButton** loads the **LevelScene** for the current level.
3. After the level:
   - If the player wins:
     - Celebration particles and animations are shown.
     - The game returns to the **MainScene**.
   - If the player loses:
     - A fail popup appears with options to:
       - Return to the **MainScene**.
       - Replay the level.
4. Progress is saved locally, ensuring the game resumes from the last level after restarting Unity.

---

### Mechanics
- **Grid**:
  - Rectangular grid with dimensions between 6x6 and 10x10.
  - Each cell contains one item.

- **Cubes**:
  - 4 types of cubes: Red, Green, Blue, Yellow.
  - Tapping on 2 or more adjacent (non-diagonal) cubes of the same color clears them.
  - Cleared cubes fall vertically, and new cubes spawn at the top.

- **Moves**:
  - A move is consumed for each valid tap.
  - Players must clear all obstacles within the given move count.

- **TNT**:
  - Created when 5 or more cubes are cleared in one tap.
  - Explodes in a 5x5 area, affecting cubes and obstacles.
  - Can combine with other TNTs for a larger 7x7 explosion.

- **Obstacles**:
  - **Box**: Cleared with one adjacent blast or TNT explosion. Does not fall.
  - **Stone**: Cleared only by TNT explosion. Does not fall.
  - **Vase**: Requires 2 hits. Falls vertically like cubes.

---

## 📂 Level Structure

Each level is defined by the following properties:
- `level_number`: Number of the level.
- `grid_width`: Width of the grid.
- `grid_height`: Height of the grid.
- `move_count`: Total moves allowed for the level.
- `grid`: List of items in the grid, starting from the bottom-left and progressing horizontally to the top-right.

### Item Definitions
- **r, g, b, y**: Red, Green, Blue, Yellow cubes.
- **rand**: Randomly generates one of the above cubes.
- **t**: TNT.
- **bo**: Box (Obstacle).
- **s**: Stone (Obstacle).
- **v**: Vase (Obstacle).

---

## 📂 How to Use

Clone the repository:
   ```bash
   git clone https://github.com/fisixus/toon-blast-clone.git
