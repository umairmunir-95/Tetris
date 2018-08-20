using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Resources;
    using System.IO;
    using System.Media;

    struct ScreenDimensions
    {
        public int bottom;
        public int top;
        public int width;
        public int height;
        public ScreenDimensions(int Bottom, int Top, int Width, int Height)
        {
            this.bottom = Bottom;
            this.top = Top;
            this.width = Width;
            this.height = Height;
        }
    }

    struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Music
    {
        private SoundPlayer sp = new SoundPlayer();
        public void StartGame(UnmanagedMemoryStream soundResId)
        {
            // retrieve .wav from resource file.
            sp = new SoundPlayer(soundResId);
            sp.Play();
        }
        public void EndGame()
        {
            sp.Stop();
        }
        ~Music()
        {
            sp.Stop();
            sp.Dispose();
        }
    }
}
