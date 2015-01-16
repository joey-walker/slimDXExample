using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;
using System.Security.Principal;
using SlimDX.Design;

namespace DirectX9TestProject
{
	public class deviceCreation
	{
	
		static Device device9;
		static Sprite sprite;
		static Texture texture;
		static float x = 20;
		static float y = 20;
		public static void initializeGraphics(RenderForm form){
		
			PresentParameters presentParamaters = new PresentParameters ();
			presentParamaters.Windowed = true;
			presentParamaters.BackBufferWidth = form.ClientRectangle.Width;
			presentParamaters.BackBufferHeight = form.ClientRectangle.Height;;


				//Acquire the int that the form is bound to.
			presentParamaters.DeviceWindowHandle = form.Handle;

		


			device9 = new Device (new Direct3D(),0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, presentParamaters);

	

			sprite = new Sprite (device9);
			texture = Texture.FromFile (device9, "C:\\COMP566\\slimDXExample\\DirectX9TestProject\\sprites\\test2.png");
		}


		public static void Main ()
		{

			using (RenderForm form = new RenderForm ("Dreadnought Kamzhor")) {
			
				form.Width = 1024;
				form.Height = 728;
				form.FormBorderStyle = FormBorderStyle.Fixed3D;

				initializeGraphics (form);
			
				MessagePump.Run (form, GameLoop);

				Cleanup ();
			}
		}
		 
		private static void GameLoop(){

			GameLogic ();
			RenderFrames ();
			x = x + 0.07f;
			y = y + 0.04f;
		}


		private static void GameLogic(){


		}

		private static void RenderFrames(){
		
			device9.Clear(ClearFlags.Target, Color.AliceBlue, 1.0f, 0);
			device9.BeginScene();


			SlimDX.Color4 color = new SlimDX.Color4(Color.White);
			sprite.Begin(SpriteFlags.AlphaBlend);
		
			sprite.Transform = Matrix.Translation (x, y, 0);



			sprite.Draw (texture, color);
			sprite.End();

			// End the scene.
			device9.EndScene();

			// Present the backbuffer contents to the screen.
			device9.Present();

		}

		private static void Cleanup()
		{
			if (device9 != null)
				device9.Dispose();
			sprite.Dispose ();
		}


	}
}

