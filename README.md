Hey! First of all, thanks for taking the time to read this, in this document I will add some descriptions on the decisions taken on the assesment.

Before that, you may notice that there are no commit/branching, this is because as I encountered major personal issues I could not work properly on the project and I wanted to finish ASAP to no waste the time of the team reviewing this assesment.

I'm aware that the usage of the version control system is an evaluation criteria, so if the assesment is appealing enough and you would like to continue I can add more documentation on how I would have handled the version control for the assesment.

DECISIONS TAKEN ON THE ASSESMENT: 

I tried to make a modular approach to the architecture (Bootstrapper, PlayerManager, PieceManager) and event-driven approach with an EventManager, this will help with the separation of concerns

All pieces/important game info is handled via scriptable obejcts, making it a scalable solution if we want to tweak data easily or add new data

Most of the classes (Cell, Board, Piece) have been treated following the single responsibility principle, having clear and focused responsibilities. In a bigger project probably the Event Manager should have been split into more concise managers

Classes like Piece are extendable through inheritance and polymorphism, taking in made the open-closed principle

The game UI was built with layouts/content size fitters to ensure a correct display through different device resolutions (iphone/ipad/android)

For the game feel, created a wrapper around the dotween asset so we can easily use it through different scenarios

-----

Please confirm receipt of this case study with your estimated time of delivery 👍

# Tatedrez-game

Create a Tatedrez game using good coding practices, taking into account the maintainability, scalability and readability of the code.
You should use the best practices available to you to ensure the best and easiest reusability of the code.

Explain, in a separate text document, your implementation choices for the different systems or modules implemented.
The juiciness and attractiveness of the gameplay and UI will also be evaluated. [Evaluation criteria section link](#what-we-will-be-evaluated)

---

# GAME DESCRIPTION AND RULES:
Here's a step-by-step description of how a game of Tatedrez would unfold:  

* **Pieces:**
    The game has only 3 pieces. Knight, Bishop and Rook:
    * Knight (Horse): The knight moves in an L-shape: two squares in one direction (either horizontally or vertically), followed by one square perpendicular to the previous direction. Knights can jump over other pieces on the board, making their movement unique. Knights can move to any square on the board that follows this L-shaped pattern, regardless of the color of the squares.
    * Rook: The rook moves in straight lines either horizontally or vertically. It can move any number of squares in the chosen direction, as long as there are no pieces blocking its path.
    * Bishop: The bishop moves diagonally on the board. It can move any number of squares diagonally in a single move, as long as there are no pieces obstructing its path.

* **Board Setup:**
    An empty board is placed, consisting of a 3x3 grid, similar to a Tic Tac Toe game.

  <img width="320" alt="image" src="illustrations/board.png">

* **Piece Placement:**
    Choose a random player to start.  
    Player 1 places one of their pieces in an empty square on the board.  
    Player 2 places one of their pieces in another empty square on the board.  
    They continue alternating until both players have placed their three pieces on the board.

  <img width="321" alt="image" src="illustrations/board-with-pieces.png">
  

* **Checking for TicTacToe:**
    After all players have placed their three pieces on the board, it's checked whether anyone has managed to create a line of three pieces in a row, column, or diagonal – a TicTacToe.

* **Dynamic Mode:**
    If neither player has achieved a TicTacToe with the placed pieces, the game enters the dynamic mode of Tateddrez.
    If X player can't move, the other player move twice.
    In this mode, players take turns to move one of their pieces following chess rules.
    **Capturing opponent's pieces is not allowed.**

* **Seeking TicTacToe:**
    In dynamic mode, players strategically move their pieces to form a TicTacToe.  
    They continue moving their pieces in turns until one of them achieves a TicTacToe with their three pieces.

  <img width="321" alt="image" src="illustrations/board-with-pieces-1.png">


* **Game Conclusion:**
    The game of Tateddrez concludes when one of the players manages to achieve a TicTacToe with their three pieces, either during the initial placement phase or during dynamic mode.  
    The player who achieves the TicTacToe is declared the winner.

  <img width="317" alt="image" src="illustrations/board-with-pieces-2.png">


---
# Tech requirements
* Use Unity 2022.3.21
* Please use only free assets. No paid assets or plugins should be used.
* Any external module/plugin/library/resource should be in the project. Please don't use github URLs to intstall UPM packages.  
---
# Delivery
* Fork this repository or clone this repository and create a new one with your github account. Share the new repository with the users "shanickcuello", "juanblasco" and "kk-homa" or make it public.
* Build Android .apk and upload it to the repository.  
---
# What we will be evaluated?  
* Additional documentation    
* Project folder structure  
* General code architecture  
* Scalability  
* Single responsibility principle
* Open-closed principle 
* KISS principle  
* Clean code principles  
* Use of interfaces and/or abstract classes  
* Use of the design patterns    
* Game & Feel  
* Visuals  
* Use of scriptable objects  
* Use of the version control system  
* Presence of Unit Tests  
---
### Disclaimer
The company reserves the right not to provide feedback on the outcome of your case study.  

Good luck!
