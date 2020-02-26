using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Program
{
    
    public class Game1 : Game
    {
        // Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState kbState;
        KeyboardState previouskbState;
        Player player=null;
        Boss boss;
        Door door;

        BackgroundElement bg;
        BackgroundElement floor;
        BackgroundElement bg2;
        BackgroundElement floor2;
        BackgroundElement bossBG;
        BackgroundElement bossFloorBG;

    GameState gameState;
        GameState previousGameState;

        Texture2D playerTexture;
        Texture2D enemyTexture;
        Texture2D healTexture;                  //Heal card
        Texture2D sacrificeTexture;             //Sacrifice card
        Texture2D meteorTexture;                //Meteor card
        Texture2D backgroundTexture;
        Texture2D bossTexture;
        Texture2D bossBGTexture;
        Texture2D bossFloorTexture;
        Texture2D floorTexture;
        Texture2D swordTexture;
        Texture2D fireballTexture;
        Texture2D mainMenuBG;
        Texture2D titleLogo;
        Texture2D playButton;
        Texture2D deckButton;
        Texture2D doorTexture;
        Texture2D healthBar;
        Texture2D UI;
        Texture2D manaAndHealthBar;
        Texture2D manaBar;
        Texture2D bossProjectile;
        Texture2D gameInstructions;
        Texture2D playerWalk1;
        Texture2D playerWalk2;
        Texture2D vampWalk1;
        Texture2D vampWalk2;

        Rectangle rectangle;
        Rectangle card1Position;                //card 1
        Rectangle card2Position;                //card 2
        Rectangle card3Position;                //card 3
        Rectangle backgroundPosition;
        Rectangle floorPosition;
        Rectangle backgroundPosition2;
        Rectangle floorPosition2;
        Rectangle mainMenuPosition;
        Rectangle titlePosition;
        Rectangle playButtonPosition;
        Rectangle deckButtonPosition;
        Rectangle bossProjectilePosition;

        Rectangle healthBarPosition;
        Rectangle UIPosition;
        Rectangle manaAndHealthBarPosition;
        Rectangle manaBarPosition;

        List<Enemy> enemies;
        List<Cards> cards;

        SpriteFont font;
        Random rng;                            //random object as a parameter for all objects that need rng
        int width;
        int height;
        int[] cardIndex;
        
        int frame;              
        double timeCounter;     
        double fps;             
        double timePerFrame;
        const int WalkFrameCount = 3;


        //GameStates for the various states of our game
        public enum GameState
        {
            MainMenu,
            DeckBuilder,
            Pause,
            GameLevel1,
            BossLevel,
            EndGame,
            GameOver,
        }




        //Game1 Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //INITIALIZE
        protected override void Initialize()
        {

            //Creating a new random object
            rng = new Random();

            //Creating the list of enemies
            enemies = new List<Enemy>();

            //creating the list of cards
            cards = new List<Cards>();

            //Setting the window width and height to local variables
            graphics.PreferredBackBufferHeight = 550;
            graphics.ApplyChanges();
            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;

            //Creating the rectangle for the main player
            rectangle = new Rectangle(0, 0, 44, 100);

            //Setting the game state to start at the main menu
            gameState = GameState.MainMenu;

            //Establishing card positions
            card1Position = new Rectangle(0, height - 115, 100, 100);
            card2Position = new Rectangle(150, height - 115, 100, 100);
            card3Position = new Rectangle(300, height - 115, 100, 100);

            //Setting asset positions
            backgroundPosition = new Rectangle(0, 0, width, 165);
            floorPosition = new Rectangle(0, 165, width, 185);
            backgroundPosition2 = new Rectangle(width, 0, width, 165);
            floorPosition2 = new Rectangle(width, 165, width, 185);
            mainMenuPosition = new Rectangle(0, 0, width, height);
            titlePosition = new Rectangle(15, 0, 400, 250);
            deckButtonPosition = new Rectangle(420, 375, 370, 70);
            playButtonPosition = new Rectangle(420, 300, 370, 70);
            healthBarPosition = new Rectangle(155, 391, 100, 20);
            manaBarPosition = new Rectangle(155, 447, 200, 20);
            UIPosition = new Rectangle(0, 350, 800, 200);
            manaAndHealthBarPosition = new Rectangle(56, 385, 273, 90);
            bossProjectilePosition = new Rectangle(100, 100, 50, 50);

            base.Initialize();
        }




        //LOAD CONTENT
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Setting game textures
            playerTexture = Content.Load<Texture2D>("idleFrame0");
            enemyTexture = Content.Load<Texture2D>("vampIdleFrame0");
            healTexture = Content.Load<Texture2D>("heal");
            sacrificeTexture = Content.Load<Texture2D>("sacrifice");
            meteorTexture = Content.Load<Texture2D>("meteor");
            backgroundTexture = Content.Load<Texture2D>("backgroundPlaceholder");
            bossBGTexture = Content.Load<Texture2D>("bossLevel");
            bossFloorTexture = Content.Load<Texture2D>("bossLevelFloor");
            floorTexture = Content.Load<Texture2D>("floorPlaceholder");
            swordTexture = Content.Load<Texture2D>("swordPlaceholder");
            fireballTexture = Content.Load<Texture2D>("fireball");
            mainMenuBG = Content.Load<Texture2D>("BG");
            titleLogo = Content.Load<Texture2D>("title");
            deckButton = Content.Load<Texture2D>("deck");
            playButton = Content.Load<Texture2D>("play");
            doorTexture = Content.Load<Texture2D>("castle");
            healthBar = Content.Load<Texture2D>("healthbar");
            manaBar = Content.Load<Texture2D>("manabar");
            manaAndHealthBar = Content.Load<Texture2D>("mana_and_health_bar");
            UI = Content.Load<Texture2D>("UI");
            bossProjectile = Content.Load<Texture2D>("redOrb");
            gameInstructions = Content.Load<Texture2D>("gameInstructions");
            playerWalk1 = Content.Load<Texture2D>("walkFrame1");
            playerWalk2 = Content.Load<Texture2D>("walkFrame2");
            vampWalk1 = Content.Load<Texture2D>("vampWalkFrame1");
            vampWalk2 = Content.Load<Texture2D>("vampWalkFrame2");
            bossTexture = Content.Load<Texture2D>("enemyPlaceHolder");


            font = Content.Load<SpriteFont>("font");
        
            //Creating the main player
            player = new Player(
                rectangle, 
                playerTexture, 
                playerWalk1, 
                playerWalk2,
                swordTexture,
                5, 1000, 50, 
                width, height);

            //Creating game backgrounds
            bg = new BackgroundElement(backgroundPosition, backgroundTexture);
            floor = new BackgroundElement(floorPosition, floorTexture);
            bg2 = new BackgroundElement(backgroundPosition2, backgroundTexture);
            floor2 = new BackgroundElement(floorPosition2, floorTexture);
            bossBG = new BackgroundElement(backgroundPosition, bossBGTexture);
            bossFloorBG = new BackgroundElement(floorPosition2, bossFloorTexture);

            //initialize for animation
            fps = 10.0;
            timePerFrame = 1.0 / fps;

            //Using cutom methods
            levelCreation("level1");
            DeckCreation();
            
            cardIndex = new int[3];
            
            ShuffleCards();

            //WIP CODE
            //temporarily making fireball
            //Rectangle tmpRectangle = GetCardObjectRectangle("Fireball");
            //cards.Add(new Cards(tmpRectangle, fireballTexture, "Fireball", 1));
            //temporary Meteor
            //tmpRectangle = GetCardObjectRectangle("Meteor");
            //cards.Add(new Cards(tmpRectangle, fireballTexture, "Meteor", 1));

            //Adding a new enemy to the enemy list
            //enemies.Add(new Enemy1(new Rectangle(50, 50, 20, 100), texture2D, 10, 100, width));
        }




        //UNLOAD CONTENT
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }




        //UPDATE
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Getting keyboard state to determine game state            
            kbState = Keyboard.GetState();

            //Switch statement for game states
            switch (gameState)
            {
                // *--------------MAIN MENU--------------*
                // Can move between the main menu and the deck builder
                case GameState.MainMenu:

                    if (SingleKeyPress(Keys.Enter) == true)
                    {
                        Reset();
                        gameState = GameState.GameLevel1;
                    }

                    if (SingleKeyPress(Keys.B) == true)
                        gameState = GameState.DeckBuilder;

                    break;

                // *--------------DECK BUILDER--------------*
                //Can move between the deck builder and the main menu
                case GameState.DeckBuilder:

                    if (SingleKeyPress(Keys.M) == true)
                        gameState = GameState.MainMenu;

                    break;

                // *--------------PAUSE MENU--------------*
                //Can move from pause to the last game state or go to the main menu
                case GameState.Pause:

                    if (SingleKeyPress(Keys.P) == true)
                        gameState = previousGameState;

                    if (SingleKeyPress(Keys.M) == true)
                        gameState = GameState.MainMenu;

                    break;

                // *--------------LEVEL 1--------------*
                //Can move from level 1 to pause, level 2, or game over
                case GameState.GameLevel1:

                    if (SingleKeyPress(Keys.P) == true)
                    {
                        previousGameState = gameState;
                        gameState = GameState.Pause;
                    }

                    //Calling the enemies update method
                    for (int i = 0; i < enemies.Count; i++)
                        enemies[i].Update(gameTime, player, enemies);

                    //Calling update for the cards
                    for (int i = 0; i < cards.Count; i++)
                        cards[i].Update(gameTime, player, enemies);

                    //Calling the cardActivation helper method for card actions
                    cardActivation(gameTime, player, enemies);

                    //Calling the players update method
                    player.Update(gameTime, player, enemies);
                    GameMovement();

                    //Reseting player position if off playable screen
                    if (door.RectangleX > 550)
                    {
                        if (player.RectangleX <= 145)
                            player.RectangleX = 150;

                        if (player.RectangleX >= 500)
                            player.RectangleX = 495;
                    }

                    else
                    {
                        if (player.RectangleX <= 145)
                            player.RectangleX = 150;

                        if (player.RectangleX >= 630)
                            player.RectangleX = 630;
                    }

                    //Checks if player health is less than 0 and goes to GameOver if true
                    if (player.Health <= 0)
                        gameState = GameState.GameOver;

                    //Updating the players health bar as they lose health
                    healthBarPosition.Width = player.Health / 7;

                    //Updating the players mana bar as they lose mana
                    manaBarPosition.Width = player.Mana * 30;

                    //checks to see if player reaches the door and presses Space
                    if (player.Rectangle.Intersects(door.Rectangle) && SingleKeyPress(Keys.Space))
                    {
                        LevelTransition();
                        gameState = GameState.BossLevel;
                    }
                    break;

                // *--------------BOSS LEVEL--------------*

                case GameState.BossLevel:
                    // Implementing rest of player movement
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        player.RectangleX -= 5;
                        player.State = CharacterState.WalkLeft;
                    }

                    else if (kbState.IsKeyDown(Keys.Right))
                    {
                        player.RectangleX += 5;
                        player.State = CharacterState.WalkRight;
                    }

                    if (SingleKeyPress(Keys.P) == true)             //State change
                    {
                        previousGameState = gameState;
                        gameState = GameState.Pause;
                    }

                    if (player.Health <= 0)                         //State change
                        gameState = GameState.GameOver;

                    if (SingleKeyPress(Keys.P) == true)
                    {
                        previousGameState = gameState;
                        gameState = GameState.Pause;
                    }

                    //Calling the enemies update method
                    for (int i = 0; i < enemies.Count; i++)
                        enemies[i].Update(gameTime, player, enemies);

                    // Removes boss from list if it dies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (!enemies[i].Alive)
                        {
                            enemies.RemoveAt(i);
                            i--;
                        }
                    }

                    //Calling update for the cards
                    for (int i = 0; i < cards.Count; i++)
                        cards[i].Update(gameTime, player, enemies);

                    //Calling the cardActivation helper method for card actions
                    cardActivation(gameTime, player, enemies);

                    //Calling the players update method
                    player.Update(gameTime, player, enemies);

                    //calling the boss update method
                    boss.Update(gameTime, player, enemies);

                    //Checks if player health is less than 0 and goes to GameOver if true
                    if (player.Health <= 0)
                        gameState = GameState.GameOver;

                    //Updating the players health bar as they lose health
                    healthBarPosition.Width = player.Health / 7;

                    //Updating the players mana bar as they lose mana
                    manaBarPosition.Width = player.Mana * 30;

                    // Transition to end game if boss dies
                    if (enemies.Count <= 0)
                        gameState = GameState.EndGame;

                    break;
            
                // *--------------END GAME--------------*
                case GameState.EndGame:

                    if (SingleKeyPress(Keys.M) == true)             //State change
                        gameState = GameState.MainMenu;

                    break;

                // *--------------GAME OVER--------------*
                //Resetting all the properties for the player and enemy (WIP)
                //Can move from game over to main menu
                case GameState.GameOver:

                    player.Health = 1000;
                    player.Mana = 5;
                    player.RectangleX = 0;
                    player.RectangleY = 0;

                    enemies[0].RectangleX = 100;
                    enemies[0].RectangleY = 100;

                    if (SingleKeyPress(Keys.M) == true)
                        gameState = GameState.MainMenu;

                    break;
            }

            //Records previous keyboard state for next frame
            previouskbState = kbState;

            base.Update(gameTime); 
        }




        //DRAW METHOD
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();


            //Switch statement for each game states
            switch (gameState)
            {
                // *--------------MAIN MENU--------------*
                //Drawing basic information at the main menu
                case GameState.MainMenu:
                    spriteBatch.Draw(
                        mainMenuBG,
                        mainMenuPosition,
                        Color.White);

                    spriteBatch.Draw(
                        titleLogo,
                        titlePosition,
                        Color.White);

                    spriteBatch.Draw(
                        playButton,
                        playButtonPosition,
                        Color.White);

                    spriteBatch.Draw(
                        deckButton,
                        deckButtonPosition,
                        Color.White);

                    spriteBatch.Draw(
                        gameInstructions,
                        new Rectangle(50, 280, 325, 200),
                        Color.White);

                    break;

                // *--------------DECK BUILDER--------------*
                //Drawing basic information for the deck builder
                case GameState.DeckBuilder:

                    spriteBatch.DrawString(font, "This is just a test. The deck builder doesn't do anything yet!", new Vector2((width / 2) - 170, (height / 2) - 40), Color.White);
                    spriteBatch.DrawString(font, "Press M to get back to the main menu.", new Vector2((width / 2) - 110, (height / 2) - 20), Color.White);

                    break;

                // *--------------PAUSE MENU--------------*
                //Drawing the basic pause menu information
                case GameState.Pause:

                    spriteBatch.DrawString(font, "Pause", new Vector2((width / 2) - 40, (height / 2) - 40), Color.White);
                    spriteBatch.DrawString(font, "Press M to get back to the main menu.", new Vector2((width / 2) - 110, (height / 2) - 20), Color.White);

                    break;

                // *--------------LEVEL 1--------------*
                //Drawing the players and enemies for level 1
                case GameState.GameLevel1:

                    player.UpdateAnimation(gameTime);
                    
                    //Drawing UI components
                    spriteBatch.Draw(UI, UIPosition, Color.White);
                    spriteBatch.Draw(healthBar, healthBarPosition, Color.White);
                    spriteBatch.Draw(manaBar, manaBarPosition, Color.White);
                    spriteBatch.Draw(manaAndHealthBar, manaAndHealthBarPosition, Color.White);

                    // Drawing cards
                    int tmpCardPositionX = 370;
                    int tmpCardPositionY = 389;
                    int cardWidth = 65;
                    int cardHeight = 92;
                    int cardDisplacement = 98;

                    for (int i = 0; i < cardIndex.Length; i++)
                    {
                        switch (cards[cardIndex[i]].Name)
                        {
                            case "Fireball":
                                if(cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(meteorTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Meteor":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(meteorTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Heal":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(healTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Sacrifice":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(sacrificeTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            // case "Shock":
                        }
                    }
                    
                    // Drawstring for player position
                    spriteBatch.DrawString(font, player.RectangleX + "," + player.RectangleY, new Vector2(), Color.White);
                    
                    // Drawing Background
                    bg.Draw(spriteBatch);
                    bg2.Draw(spriteBatch);

                    // Drawing Floor
                    floor.Draw(spriteBatch);
                    floor2.Draw(spriteBatch);

                    //Draw door
                    door.Draw(spriteBatch);

                    // Drawing enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].Alive)
                        {
                            enemies[i].UpdateAnimation(gameTime);
                            enemies[i].Draw(spriteBatch);
                        }
                    }

                    //Drawing the player if it is alive
                    if (player.Alive)
                        player.Draw(spriteBatch);

                    //Drawing card abilities
                    for(int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].Active)
                            cards[i].Draw(spriteBatch);
                    }
                    
                    break;

                // *--------------LEVEL 2--------------*
                case GameState.BossLevel:

                    player.UpdateAnimation(gameTime);

                    //Drawing UI components
                    spriteBatch.Draw(UI, UIPosition, Color.White);
                    spriteBatch.Draw(healthBar, healthBarPosition, Color.White);
                    spriteBatch.Draw(manaBar, manaBarPosition, Color.White);
                    spriteBatch.Draw(manaAndHealthBar, manaAndHealthBarPosition, Color.White);

                    // Drawing cards
                    tmpCardPositionX = 370;
                    tmpCardPositionY = 389;
                    cardWidth = 65;
                    cardHeight = 92;
                    cardDisplacement = 98;

                    for (int i = 0; i < cardIndex.Length; i++)
                    {
                        switch (cards[cardIndex[i]].Name)
                        {
                            case "Fireball":
                                if(cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(meteorTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Meteor":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(meteorTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Heal":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(healTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            case "Sacrifice":
                                if (cards[cardIndex[i]].OnUseTriggers)
                                    spriteBatch.Draw(sacrificeTexture, new Rectangle(tmpCardPositionX + i * cardDisplacement, tmpCardPositionY, cardWidth, cardHeight), Color.White);
                                break;

                            // case "Shock":
                        }
                    }
                    
                    // Drawstring for player position
                    spriteBatch.DrawString(font, player.RectangleX + "," + player.RectangleY, new Vector2(), Color.White);

                    // Drawing Background
                    bossBG.Draw(spriteBatch);

                    // Drawing Floor
                    bossFloorBG.Draw(spriteBatch);

                    // Drawing enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].Alive)
                        {
                            enemies[i].UpdateAnimation(gameTime);
                            enemies[i].Draw(spriteBatch);
                        }

                    }

                    //Drawing the player if it is alive
                    if (player.Alive)
                        player.Draw(spriteBatch);

                    //Drawing the boss
                    if (boss.Alive)
                        boss.Draw(spriteBatch);

                    //Drawing card abilities
                    for(int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].Active)
                            cards[i].Draw(spriteBatch);
                    }
                    break;

                // *--------------END GAME--------------*
                case GameState.EndGame:

                    spriteBatch.DrawString(font, "Congratulations, you have completed the first level!", new Vector2((width / 2) - 160, (height / 2) - 40), Color.White);
                    spriteBatch.DrawString(font, "There are no more levels at the moment, but thanks for playing!", new Vector2((width / 2) - 200, (height / 2) - 20), Color.White);
                    spriteBatch.DrawString(font, "Press M to go back to the main menu", new Vector2((width / 2) - 120, (height / 2)), Color.White);

                    break;

                // *--------------GAME OVER--------------*
                //Drawing the basic information for the game over screen
                case GameState.GameOver:

                    spriteBatch.DrawString(font, "The game is now over!", new Vector2((width / 2) - 70, (height / 2) - 40), Color.White);
                    spriteBatch.DrawString(font, "Press M to go back to the main menu", new Vector2((width / 2) - 120, (height / 2) - 20), Color.White);

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }




        //GAME MOVEMENT
        public void GameMovement()
        {
            kbState = Keyboard.GetState();
            
            int speed = 5;

            bg.ResetLocation();
            bg2.ResetLocation();
            floor.ResetLocation();
            floor2.ResetLocation();

            //scrolling of all assets besides the player
            if (player.RectangleX == 150 && kbState.IsKeyDown(Keys.Left))
            {
                player.State = CharacterState.WalkLeft;
                bg.Scrolling(speed);
                floor.Scrolling(speed);
                bg2.Scrolling(speed);
                floor2.Scrolling(speed);
                door.Scrolling(speed);

                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].Scrolling(speed);

                for (int i = 0; i < cards.Count; i++)
                    cards[i].Scrolling(speed);
            }

            //changes scrolling if player reaches the door
            else if (player.RectangleX == 495 && kbState.IsKeyDown(Keys.Right) && door.RectangleX > 550)
            {
                player.State = CharacterState.WalkRight;
                bg.Scrolling(-speed);
                floor.Scrolling(-speed);
                bg2.Scrolling(-speed);
                floor2.Scrolling(-speed);
                door.Scrolling(-speed);

                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].Scrolling(-speed);

                for (int i = 0; i < cards.Count; i++)
                    cards[i].Scrolling(-speed);
            }

            //player movement when in middle of screen
            else if (kbState.IsKeyDown(Keys.Left))
            {
                player.RectangleX -= speed;
                player.State = CharacterState.WalkLeft;
            }

            else if (kbState.IsKeyDown(Keys.Right))
            {
                player.RectangleX += speed;
                player.State = CharacterState.WalkRight;
            }
        }




        //Creating a single key press method that checks for a key being pressed only once
        public bool SingleKeyPress(Keys key)
        {
            kbState = Keyboard.GetState();

            if (kbState.IsKeyUp(key) && previouskbState.IsKeyDown(key))
                return true;

            else
                return false;
        }



        //Create Deck through FILE IO
        private void DeckCreation()
        {
            StreamReader read = null;
            string fileName = "..\\..\\..\\..\\..\\Decks//deck1"; //takes data from deck1 file

            try
            {
                read = new StreamReader(fileName);
                string line = null;
                while((line = read.ReadLine()) != null)
                {
                    //gets the total number of cards in the deck
                    int cardsInDeck = int.Parse(line);

                    for(int i = 0; i < cardsInDeck; i++)
                    {
                        line = read.ReadLine();
                        //gets the name of the card and which iteration of that card it is, Example: Fireball1 or Fireball2
                        string[] data = line.Split(',');
                        string cardName = data[0];
                        int cardNumber = int.Parse(data[1]);

                        Rectangle tempRectangle = GetCardObjectRectangle(cardName);
                        cards.Add(new Cards(tempRectangle, fireballTexture, cardName, cardNumber));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (read != null)
            {
                read.Close();
            }
        }




        //helper method to get the rectangle sizes of each card's in game object
        private Rectangle GetCardObjectRectangle(string cardName)
        {
            //if the cardName is Fireball return this rectangle specific to fireball
            if(cardName == "Fireball")
                return new Rectangle(player.RectangleX, player.RectangleY, 100, 100);

            if (cardName == "Meteor")
                return new Rectangle(player.RectangleX, player.RectangleY, 200, 200);

            //placeholder not really useful
            return Rectangle.Empty;
        }




        //method that holds all the controls for activating cards
        private void cardActivation(GameTime gameTime, GameObject check, List<Enemy> checkEnemy)
        {
            if (SingleKeyPress(Keys.Q)) // Left Card
            {
                if (!cards[cardIndex[0]].Active && cards[cardIndex[0]].Usable && (player.Mana >= cards[0].ManaCost || cards[cardIndex[0]].Name == "Sacrifice"))
                {
                    cards[cardIndex[0]].StartValues(player);
                    cards[cardIndex[0]].Active = true;
                    cards[cardIndex[0]].Action(gameTime, player, enemies);
                }
            }

            if (SingleKeyPress(Keys.W)) // Middle card
            {
                if (!cards[cardIndex[1]].Active && cards[cardIndex[1]].Usable && (player.Mana >= cards[1].ManaCost || cards[cardIndex[1]].Name == "Sacrifice"))
                {
                    cards[cardIndex[1]].StartValues(player);
                    cards[cardIndex[1]].Active = true;
                    cards[cardIndex[1]].Action(gameTime, player, enemies);
                }
            }

            if (SingleKeyPress(Keys.E)) // Right Card
            {
                if (!cards[cardIndex[2]].Active && cards[cardIndex[2]].Usable && (player.Mana >= cards[2].ManaCost || cards[cardIndex[2]].Name == "Sacrifice"))
                {
                    cards[cardIndex[2]].StartValues(player);
                    cards[cardIndex[2]].Active = true;
                    cards[cardIndex[2]].Action(gameTime, player, enemies);
                }
            }

            if (SingleKeyPress(Keys.R)) // key for shuffling
                ShuffleCards();
        }




        //creating gamelevel from text file (work in progress)
        private void levelCreation(string lvlName)
        {
            //loads the level from text file
            int objectSize = 100;
            int spawnAreaStartY = 80;

            StreamReader read = null;
            string fileName = "..\\..\\..\\..\\..\\Levels//" + lvlName; //uses the Level1 textfile

            try
            {
                read = new StreamReader(fileName);

                string line = null;

                while ((line = read.ReadLine()) != null)
                {
                    //put dimensions
                    string[] data = line.Split(',');
                    int levelWidth = int.Parse(data[0]);
                    int levelHeight = int.Parse(data[1]);
                    
                    //take each character and place objects in place of it in the game
                    for(int i = 0; i < levelHeight; i++)
                    {
                        line = read.ReadLine();
                        for(int j = 0; j < levelWidth; j++)
                        {
                            //checks if there is an enemy on in the current position on the grid
                            if(line.Substring(j,1) == "X")
                            {
                                enemies.Add(new Enemy1(new Rectangle(j * objectSize + objectSize, i * 20 + spawnAreaStartY, 44, 100), enemyTexture, vampWalk1, vampWalk2, 100, 100, width));
                                //enemies.Add(new Enemy1(new Rectangle(50, 50, 20, 100), texture2D, 10, 100, width));
                            }
                            //position of the player
                            else if(line.Substring(j,1) == "@")
                            {
                                player.RectangleX = j * objectSize;
                                player.RectangleY = i * objectSize;
                            }
                            //places the door when we have a door sprite (will happen later on)
                            else if(line.Substring(j,1) == "D")
                            {
                                door = new Door(new Rectangle(j * objectSize + objectSize, i * 20 + spawnAreaStartY, 200, 200), doorTexture);
                            }
                            //makes boss enemy if level text file has the character B
                            else if (line.Substring(j,1) == "B")
                            {
                                boss = new Boss(new Rectangle(j * objectSize + objectSize, i * 20 + spawnAreaStartY, 200, 200), bossTexture, bossTexture, bossTexture, player, 20000, 500, bossProjectile, bossProjectilePosition);
                                enemies.Add(boss);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (read != null)
            {
                read.Close();
            }
        }

        public void ShuffleCards()
        {
            int tmpIndex = 14006902;

            cardIndex[0] = rng.Next(0, cards.Count);
            tmpIndex = rng.Next(0, cards.Count);

            while (tmpIndex == cardIndex[0])
            {
                tmpIndex = rng.Next(0, cards.Count);
            }

            cardIndex[1] = tmpIndex;

            while (tmpIndex == cardIndex[0] || tmpIndex == cardIndex[1])
            {
                tmpIndex = rng.Next(0, cards.Count);
            }

            cardIndex[2] = tmpIndex;

            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].Usable = true;
                cards[i].OnUseTriggers = true;
            }
        }

        public void LevelTransition()
        {
            enemies.Clear();
            levelCreation("bossLevel");
            floor.RectangleX = 0;
            bg.RectangleX = 0;
        }

        public void Reset()
        {
            floor.RectangleX = 0;
            floor2.RectangleX = width;
            bg.RectangleX = 0;
            bg2.RectangleX = width;
            enemies.Clear();
            cards.Clear();
            player.Alive = true;
            player.Mana = 5;
            player.Health = 1000;
            levelCreation("level1");
            DeckCreation();
        }
    }
}
