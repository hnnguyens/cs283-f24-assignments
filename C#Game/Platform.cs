/**
 * Platform class that creates and updates the obstacle objects.
 * 
 * Creates, updates, moves, and resets the platforms. 
 * 
 * @author Hazel Nguyen
 * @version 13 Sept 2024
 */

using System;
using System.Drawing;

public class Platform
{
	float x = 480;
	float y;
	float vel = -100; //for the speed of the platform

	/**
	 * Creates a new platform.
	 */
	public Platform(float x, float y)
	{
		this.x = x;
		this.y = y;
    }

	/**
	 * Returns the X coordinate of the platform.
	 * @return float x
	 */
	public float getX()
	{
		return this.x;
	}

	/**
	 * Gets the Y coordinate of the platform.
	 * @return float y
	 */
	public float getY()
	{
		return this.y;
	}

	/**
	 * Gets the velocity of the platform.
	 * @return float vel
	 */
	public float getVel()
	{
		return vel;
	}

	/**
	 * Draws the platform.
	 * @param g Graphics object
	 */
	public void Draw(Graphics g) {
		Color r = ColorTranslator.FromHtml("#8F3700"); //brown 
		Brush brush = new SolidBrush(r);
		g.FillRectangle(brush, x, y, 50, 10); 
	}

	/**
	 * Updates the position of the platform.
	 */
	public void Update(float dt) {
		this.x = this.x + vel * dt; //moves the platforms to the left of screen

	}

	/**
	 * Resets the platform to it's original position. 
	 */
	public void Reset()
	{
		this.x = 480;
		this.vel = -100;
	}
}