# Toon Blast Clone 🎮

A clone of the popular mobile game **Toon Blast**, built using **Unity** and **C#**. This puzzle game challenges players to match 2 or more blocks of the same color to clear them from the board. Each level has specific objectives, and players must complete them to progress to the next level.

---

## 📖 About the Project

This project was created as a **case study** and is **not intended for commercial use**. It replicates core gameplay mechanics of Toon Blast while implementing clean architectural practices and efficient development patterns.

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
- Custom **UniTask** library for async operations

---

## 🧩 Architecture

This project follows the **Model-View-Presenter (MVP)** architectural pattern for clean separation of concerns and maintainability:

1. **StartScene**: The entry point of the project where the `ProjectContext` is activated.
2. **GamePresenter**: Initialized as non-lazy in the `ProjectContext` to set up the core game logic.
3. The rest of the game components and systems follow after `GamePresenter`.

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
1. The game starts in the **MainScene**, where the player sees a **LevelButton** displaying the current level.
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
   git clone https://github.com/yourusername/toon-blast-clone.git
