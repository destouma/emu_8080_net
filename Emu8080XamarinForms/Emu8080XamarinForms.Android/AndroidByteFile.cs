using System;
using Android.Content.Res;
using System.IO;
using Java.IO;
using Android.Content;

namespace Emu8080XamarinForms.Droid
{
	public class AndroidByteFile : IByteFile
	{
        private Context context;

		public AndroidByteFile(Context context)
		{
            this.context = context;
		}


        public byte[] getBuffer()
        {
            byte[] content;
            const int maxReadSize = 256 * 1024;
            AssetManager assets = context.Assets;
            using (BinaryReader br = new BinaryReader(assets.Open("invaders.bin")))
            {
                content = br.ReadBytes(maxReadSize); 
            }

            return content;
        }

    }
}

