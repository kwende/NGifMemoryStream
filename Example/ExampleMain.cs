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
			//you should replace filepath
			//String [] imageFilePaths = new String[]{"c:\\01.png","c:\\02.png","c:\\03.png"}; 
            string[] imageFilePaths = Directory.GetFiles(@"C:\Users\brush\Desktop\images"); 
			String outputFilePath = "c:\\test.gif";
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

            File.WriteAllBytes("c:/users/brush/desktop/test.gif", mem.GetBuffer()); 

			/* extract Gif */
			GifDecoder gifDecoder = new GifDecoder();
            gifDecoder.Read("c:/users/brush/desktop/test.gif");
			for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
			{
				Image frame = gifDecoder.GetFrame( i );  // frame i
				frame.Save( "c:/users/brush/desktop/" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png );
			}
		}
	}
}
