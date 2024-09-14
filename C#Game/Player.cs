/**
 * Player class for the "FLEE!" game.
 * 
 * Creates the player, with methods to update, draw, reset, and jump.
 * 
 * @author Hazel Nguyen
 * @version 13 Sept 2024
 */
using System;
using System.Drawing;

/**
 * Player object class.
 */
public class Player
{
    float vel = 0;
	float gravity = 100;
	float x = 10;
	float y = 250;

	/**
	 * Player constructor.
	 */
	public Player() {

	}

	/**
	 * Returns the x coordinate of the player.
	 * @return float x
	 */
	public float getX()
	{
		return this.x;
	}


	/**
	 * Returns the y coordinate of the player.
	 * @return float y
	 */
	public float getY()
	{
		return this.y;
	}

	/**
	 * Returns the velocity of the player.
	 * @return float vel
	 */
	public float getVel()
	{
		return this.vel;
	}

	/**
	 * Returns the gravity.
	 * @return float gravity
	 */
	public float getGravity()
	{
		return this.gravity;
	}

	/**
	 * Sets the x coordinate of the player.
	 */
	public void setX(float x)
	{
		this.x = x;
	}

	/**
	 * Sets the y coordinate.
	 */
	public void setY(float y)
	{
		this.y = y;
	}

	/**
	 * Sets the velocity of the player.
	 */
	public void setVel(float vel)
	{
		this.vel = vel;
	}

	/**
	 * Draws the player.
	 * @param g Graphics object to draw
	 */
	public void DrawCircle(Graphics g) {
		Color c = Color.FromArgb(48, 182, 45); //could be figure
		Brush brush = new SolidBrush(c);
		g.FillEllipse(brush, x, y, 10, 10);
	}

	/**
	 * Updates the player to be constantly falling towards the bottom of the screen.
	 * @param dt float representing the delta time 
	 */
	public void Update(float dt) {
		this.y = this.y + this.vel * dt;
        this.vel = this.vel + this.gravity * dt; //to fall, continuously

	}

	/**
	 * Allows the player to jump.
	 */
	public void Jump() {
		this.vel = -50; //vel - 20; could cause wave effect?
	}

	/**
	 * Resets the player to it's original coordiantes and position.
	 */
	public void Reset()
	{
        this.gravity = 100;
        this.x = 10;
        this.y = 250;
		this.vel = 0;
    }
}
