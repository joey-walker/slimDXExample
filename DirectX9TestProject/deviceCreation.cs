using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;

namespace DirectX9TestProject
{
	public class deviceCreation
	{
	
		//Our graphics device
		static Device device9;

		//We use this to load sprites.
		static Sprite sprite;

		//Our texture file
		static Texture texture;

		//Absolute location start for sprite
		static float x = 20;
		static float y = 20;



		//Intialize graphics
		public static void initializeGraphics (RenderForm form)
		{
		
			//Device presentation paramaters
			PresentParameters presentParamaters = new PresentParameters ();
			//Windowed
			presentParamaters.Windowed = true;
			//Form's width
			presentParamaters.BackBufferWidth = form.ClientRectangle.Width;
			//Forms' height
			presentParamaters.BackBufferHeight = form.ClientRectangle.Height;


			//Acquire the sys int that the form is bound to.
			presentParamaters.DeviceWindowHandle = form.Handle;


			//Our device for graphics
			device9 = new Device (new Direct3D (), 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, presentParamaters);

	

			//sprite based off device
			sprite = new Sprite (device9);

			//Our texture
			texture = Texture.FromFile (device9, "..\\..\\sprites\\test2.png");
		}


		public static void Main ()
		{
			//using allows cleanup of form afterwards
			using (RenderForm form = new RenderForm ("Dreadnought Kamzhor")) {
			
				//Window resolution is 1024 x 728
				form.Width = 1024;
				form.Height = 728;
				//No resizing
				form.FormBorderStyle = FormBorderStyle.Fixed3D;

				//Create our device, textures and sprites
				initializeGraphics (form);
			
				//Application loop
				MessagePump.Run (form, GameLoop);

				//Dispose no longer in use objects.
				Cleanup ();
			}
		}

		private static void GameLoop ()
		{
			//Logic then render then loop
			GameLogic ();
			RenderFrames ();

			//Example change to offset to move picture accross
			x = x + 0.07f;
			y = y + 0.04f;
		}


		private static void GameLogic ()
		{
			//This is where would place game logic for a game
		}

		private static void RenderFrames ()
		{
		
			//Clear the whole screen
			device9.Clear (ClearFlags.Target, Color.AliceBlue, 1.0f, 0);

			//Render whole frame
			device9.BeginScene ();

			//not sure why we need this yet...
			SlimDX.Color4 color = new SlimDX.Color4 (Color.White);

			//being sprite render.
			sprite.Begin (SpriteFlags.AlphaBlend);
		
			//Translate the sprite with a 3d matrix with no z change.
			sprite.Transform = Matrix.Translation (x, y, 0);
		
			//Render sprite.
			sprite.Draw (texture, color);

			//end render
			sprite.End ();

			// End the scene.
			device9.EndScene ();

			// Present the backbuffer contents to the screen.
			device9.Present ();

		}

		//Dispose unused objects
		private static void Cleanup ()
		{
			if (device9 != null)
				device9.Dispose ();

			sprite.Dispose ();
			texture.Dispose ();
		}


	}
}

