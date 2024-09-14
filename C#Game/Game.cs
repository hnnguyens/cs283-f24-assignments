/**
 * The game loop for game "FLEE!".
 * 
 * Initializes the game, with methods allowing the user to interact with the game.
 * 
 * @author Hazel Nguyen
 * @version 13 Sept 2024
 */
using System;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Windows.Forms;

/**
 * The game loop class.
 */
public class Game
{
    Platform[] platforms = new Platform[1000];
    Player player = new Player();
    bool gameOver = false; 
    bool signature = false; //text box
    bool start = false; //to start the game

    /**
     * Called once before the game starts.
     */
    public void Setup() 
    {
        Random rnd = new Random();
        for (int i = 0; i < 1000; i++)
        {
            float y = rnd.Next(0, 450); //places platform in a randomized location
            platforms[i] = new Platform(30 * (i+1), y); //creates platforms
        }
    }

    /**
     * Called once per frame with the elapsed time in seconds.
     * @param dt float representing delta time
     */
    public void Update(float dt)
    {
        if (start == true) 
        {
            foreach (Platform platform in platforms)
            {
                platform.Update(dt); //spawns platforms 

                //if colliding with platform
                if (player.getX() >= platform.getX() && player.getX() <= (platform.getX() + 50) &&
                    player.getY() >= platform.getY() && player.getY() <= (platform.getY() + 10))
                {
                    player.setX(player.getX() + platform.getVel() * dt);
                    player.setVel(0);
                    platform.Reset(); 
                    gameOver = true;
                }

                if (player.getY() >= Window.height) //die when off screen
                {
                    gameOver = true;
                }
            }

            player.Update(dt);
        }
    }

    /**
     * Called when the window is refreshed.
     * @param g Graphics object
     */
    public void Draw(Graphics g)
    {
        foreach (Platform platform in platforms)
        {
            platform.Draw(g);
        }
        player.DrawCircle(g);


        if (gameOver == true) //code taken from example on assignment
        { 
            Font font = new Font("Comic Sans", 30);
            SolidBrush fontBrush = new SolidBrush(Color.Black);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;

            g.DrawString("        GAME OVER!\nPress enter to play again", 
                font, fontBrush,
                (float) (Window.width * 0.16),
                (float) (Window.height * 0.5),
                format);
                start = false;
        }

        //signature box top left
        if (signature == true)
        {
            Pen pen = new Pen(Color.Black, 3);
            g.DrawRectangle(pen, 0, 0, 150, 50);

            Font font = new Font("Comic Sans", 12);
            SolidBrush fontBrush = new SolidBrush(Color.Black);

            g.DrawString("FLEE!\nHazel Nguyen '26", font, fontBrush, 0, 5);
        }
    }

    /**
     * Allows the game to be replayed.
     */
    public void Reset()
    {
        platforms = new Platform[1000];
        player = new Player();
        gameOver = false;
        signature = false; 
        start = false;
        Setup(); //restarts the game
    }

    /**
     * For window. Click produces coordinates. 
     * @param mouse
     */
    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            System.Console.WriteLine(mouse.Location.X + ", " + mouse.Location.Y); 
        }
    }

    /**
     * Allows the user to interact with the game. Calls associated methods.
     * @param key
     */
    public void KeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up) //for jump
        {
            start = true;
            player.Jump();
        }

        if (key.KeyCode == Keys.Add) { //signature box
            signature = true;
        }

        if (key.KeyCode == Keys.Enter) //restart the game!
        {
            player.Reset();
            Reset();
        }
    }
}
