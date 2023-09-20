# 2D Car Game in C#

<img src="./assets/gameplay.PNG" alt="Game Screenshot" width="400" height="300">

## Description

"Welcome to the 2D Car Game in C#! This game was developed as part of my studies to practice the fundamentals of C#. It represents my first and larger project, and as such, it may not adhere to best coding practices or maintain clean code. However, there is room for significant improvement."

The goal of the game is to pass the finish line with your car in the shortest time possible while avoiding obstacles (cones) coming from the top. You have three lives, and each time you collide with a cone, you lose one life. When you run out of lives, it's game over.

This game offers three different modes:

1. **Length Mode**: You can type in the length of the street, and the game will generate a street of that length with cones.

2. **Cone Mode**: Type in the number of cones that will descend from the top. The challenge is to navigate through a set number of cones to reach the finish line.

3. **Endless Mode**: Drive endlessly until you run out of lives. Cones keep coming down, and your goal is to survive as long as possible.

## Gameplay

- Control your car using the following keys:
  - **Right**: `.`
  - **Left**: `,`
  - **Accelerate**: `A`
  - **Nitro Boost**: `S` (Available every 5 seconds)
- Use acceleration strategically to move faster but be ready to slow down to avoid cones.
- Nitro boost helps you move up the screen quickly, potentially allowing you to pass the finish line faster. However, it also increases the difficulty as you'll need faster reactions to dodge the cones.

- When you successfully pass the finish line, your total time is displayed, and you have the option to play again.

## Screenshots

<div style="display: flex; justify-content: space-between;">

  <div style="flex: 1; padding: 5px;">
    <img src="./assets/winner.PNG" alt="Game Screenshot 1" width="400" >
  </div>

  <div style="flex: 1; padding: 5px;">
    <img src="./assets/game-over.PNG" alt="Game Screenshot 2" width="400" >
  </div>

</div>
