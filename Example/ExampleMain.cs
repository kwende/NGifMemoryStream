using System;
using System.Drawing;
using System.Drawing.Imaging;
using Gif.Components;
using System.IO;

namespace Example
{
	class ExampleMain
	{
		[STAThread]
		static void Main(string[] args)
		{
			/* create Gif */
            string[] imageFilePaths = Directory.GetFiles(@"C:\Users\someuser\Desktop\images"); 
			AnimatedGifEncoder e = new AnimatedGifEncoder();
            MemoryStream mem = new MemoryStream();
            e.Start(mem);
			e.SetDelay(500);
			//-1:no repeat,0:always repeat
			e.SetRepeat(0);
			for (int i = 0, count = imageFilePaths.Length; i < count; i++ ) 
			{
				e.AddFrame( Image.FromFile( imageFilePaths[i] ) );
			}
			e.Finish();

            File.WriteAllBytes("c:/users/someuser/desktop/test.gif", mem.GetBuffer()); 

			/* extract Gif */
			GifDecoder gifDecoder = new GifDecoder();
            gifDecoder.Read("c:/users/someuser/desktop/test.gif");
			for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
			{
				Image frame = gifDecoder.GetFrame( i );  // frame i
                frame.Save("c:/users/someuser/desktop/" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png);
			}
		}
	}
}
