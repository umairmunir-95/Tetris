using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tetris
{
    enum block_rotation
    {
        zero_deg = 0,
        ninety_deg = 1,
        one_eighty_deg = 2,
        two_seventy_deg = 3
    }

    enum image_type
    {
        image1 = 0,
        image2 = 1,
        image3 = 2,
        image4 = 3,
        image5 = 4,
        image6 = 5,
        image7 = 6,
        image8 = 7,
        image9 = 8,
        image10 = 9,
        image11 = 10,
        image12 = 11
    }

    enum game_level
    {
        Easy = 0,
        Hard = 1
    }

    struct Tiles
    {
        public block_rotation angle;
        public image_type type;

        public Tiles(block_rotation newAngle, image_type newType)
        {
            this.angle = newAngle;
            this.type = newType;
        }
    }

    struct Tiles_shapes
    {
        public Color color;
        public bool isBlock;

        public Tiles_shapes(Color newColor, Boolean newIsBlock)
        {
            this.color = newColor;
            this.isBlock = newIsBlock;
        }
    }

    public delegate void TetrisHandler(object o, EventArgs e);

    public class EventArgs
    {
        public int RowsCompleted;

        public EventArgs(int r)
        {
            RowsCompleted = r;
        }
    }

    class BaseClass
    {
        protected static int tile_size = 4; // size of the block(4v4)
        protected static bool[] tiles_array = new bool[tile_size << 2];
        protected static ScreenDimensions game = new ScreenDimensions();
        protected static Point tile_position = new Point();
        protected static Tiles t_class_obj = new Tiles();
        protected static int tile_width;
        protected static int tile_height;
        protected static Tiles_shapes[] arrField;
    }

    class BlockClass : BaseClass
    {
        public ImageList obj = new ImageList();
        public BlockClass()
        {
            String path = String.Concat(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"\Images\Blocks\");
            obj.ColorDepth = ColorDepth.Depth32Bit;
            obj.ImageSize = new Size(this.Block_Width, this.Block_Height);
            obj.Images.Add(Image.FromFile(String.Concat(path, "red.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "blue.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "green.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "cyan.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "yellow.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "orange.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "magenta.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "brown.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "darkblue.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "greenyellow.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "pink.jpg")));
            obj.Images.Add(Image.FromFile(String.Concat(path, "white.jpg")));
        }

        public int Block_Width
        {
            get
            {
                if (tile_width.Equals(0))
                {
                    return 24;
                }
                else
                {
                    return tile_width;
                }
            }
            set
            {
                tile_width = value;
            }
        }

        public int Block_Height
        {
            get
            {
                if (tile_height.Equals(0))
                {
                    return 24;
                }
                else
                {
                    return tile_height;
                }
            }
            set
            {
                tile_height = value;
            }
        }

        public block_rotation Rotation
        {
            get
            {
                return t_class_obj.angle;
            }
            set
            {
                t_class_obj.angle = value;
            }
        }

        public image_type Shape
        {
            get
            {
                return t_class_obj.type;
            }
            set
            {
                t_class_obj.type = value;
            }
        }

        public int Size
        {
            get
            {
                return tile_size;
            }
        }

        public Color color(image_type tile_shape)
        {
            // this function returns the color of the block.
            switch (tile_shape)
            {
                case image_type.image1:
                    return Color.Red;
                case image_type.image2:
                    return Color.Blue;
                case image_type.image3:
                    return Color.Green;
                case image_type.image4:
                    return Color.Cyan;
                case image_type.image5:
                    return Color.Yellow;
                case image_type.image6:
                    return Color.Orange;
                case image_type.image7:
                    return Color.Magenta;
                case image_type.image8:
                    return Color.Brown;
                case image_type.image9:
                    return Color.DarkBlue;
                case image_type.image10:
                    return Color.GreenYellow;
                case image_type.image11:
                    return Color.Pink;
                default:
                    return Color.White;
            }
        }

        public Tiles blocks_generation(game_level level)
        {
            Random r = new Random();

            if (level.Equals(game_level.Easy))
            {
                return new Tiles((block_rotation)r.Next(0, Enum.GetNames(typeof(block_rotation)).Length), (image_type)r.Next(0, 7));
            }
            else
            {
                return new Tiles((block_rotation)r.Next(0, Enum.GetNames(typeof(block_rotation)).Length), (image_type)r.Next(0, Enum.GetNames(typeof(image_type)).Length));
            }
        }

        public ScreenDimensions Block_Rotation(block_rotation rot)
        {
            ScreenDimensions wr = new ScreenDimensions();
            Rotation = rot;
            Build();
            set_block_position(ref wr);
            return wr;
        }

        public void Build()
        {
            // Receives specific info for tiles
            tiles_array = Blocks_Creation(new Tiles(Rotation, Shape));
        }

        public bool[] Blocks_Creation(Tiles Tiles)
        {
            // 0123
            // 4567
            // 8901
            // 2345

            // data for 4v4 block shapes
            bool[] array_for_blocks_creation = new bool[tile_size << 2];

            switch (Tiles.type)
            {
                case image_type.image1:
                    if (Tiles.angle.Equals(block_rotation.zero_deg) || Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[2] = true; // ..#. 0123
                        array_for_blocks_creation[6] = true; // ..#. 4567
                        array_for_blocks_creation[10] = true; // ..#. 8901
                        array_for_blocks_creation[14] = true; // ..#. 2345
                    }
                    else
                    {
                        array_for_blocks_creation[12] = true; // .... 0123
                        array_for_blocks_creation[13] = true; // .... 4567
                        array_for_blocks_creation[14] = true; // .... 8901
                        array_for_blocks_creation[15] = true; // #### 2345
                    }

                    break;
                case image_type.image2:
                    array_for_blocks_creation[0] = true; // ##.. 0123
                    array_for_blocks_creation[1] = true; // ##.. 4567
                    array_for_blocks_creation[4] = true; // .... 8901
                    array_for_blocks_creation[5] = true; // .... 2345
                    break;
                case image_type.image3:
                    if (Tiles.angle.Equals(block_rotation.zero_deg) || Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[5] = true; // .... 0123
                        array_for_blocks_creation[6] = true; // .##. 4567
                        array_for_blocks_creation[8] = true; // ##.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[5] = true; // .##. 4567
                        array_for_blocks_creation[6] = true; // ..#. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    break;
                case image_type.image4:
                    if (Tiles.angle.Equals(block_rotation.zero_deg) || Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[4] = true; // .... 0123
                        array_for_blocks_creation[5] = true; // ##.. 4567
                        array_for_blocks_creation[9] = true; // .##. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[2] = true; // ..#. 0123
                        array_for_blocks_creation[5] = true; // .##. 4567
                        array_for_blocks_creation[6] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    break;
                case image_type.image5:
                    if (Tiles.angle.Equals(block_rotation.zero_deg))
                    {
                        array_for_blocks_creation[4] = true; // .... 0123
                        array_for_blocks_creation[5] = true; // ###. 4567
                        array_for_blocks_creation[6] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.ninety_deg))
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[4] = true; // ##.. 4567
                        array_for_blocks_creation[5] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[5] = true; // .... 0123
                        array_for_blocks_creation[8] = true; // .#.. 4567
                        array_for_blocks_creation[9] = true; // ###. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[5] = true; // .##. 4567
                        array_for_blocks_creation[6] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    break;
                case image_type.image6:
                    if (Tiles.angle.Equals(block_rotation.zero_deg))
                    {
                        array_for_blocks_creation[4] = true; // .... 0123
                        array_for_blocks_creation[5] = true; // ###. 4567
                        array_for_blocks_creation[6] = true; // #... 8901
                        array_for_blocks_creation[8] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.ninety_deg))
                    {
                        array_for_blocks_creation[0] = true; // ##.. 0123
                        array_for_blocks_creation[1] = true; // .#.. 4567
                        array_for_blocks_creation[5] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[6] = true; // .... 0123
                        array_for_blocks_creation[8] = true; // ..#. 4567
                        array_for_blocks_creation[9] = true; // ###. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[5] = true; // .#.. 4567
                        array_for_blocks_creation[9] = true; // .##. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    break;
                case image_type.image7:
                    if (Tiles.angle.Equals(block_rotation.zero_deg))
                    {
                        array_for_blocks_creation[4] = true; // .... 0123
                        array_for_blocks_creation[5] = true; // ###. 4567
                        array_for_blocks_creation[6] = true; // ..#. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.ninety_deg))
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[5] = true; // .#.. 4567
                        array_for_blocks_creation[8] = true; // ##.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[4] = true; // .... 0123
                        array_for_blocks_creation[8] = true; // #... 4567
                        array_for_blocks_creation[9] = true; // ###. 8901
                        array_for_blocks_creation[10] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .##. 0123
                        array_for_blocks_creation[2] = true; // .#.. 4567
                        array_for_blocks_creation[5] = true; // .#.. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    break;
                case image_type.image8:
                    array_for_blocks_creation[0] = true; // #... 0123
                    // .... 4567
                    // .... 8901
                    // .... 2345
                    break;
                case image_type.image9:
                    if (Tiles.angle.Equals(block_rotation.zero_deg) || Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[1] = true; // .##. 0123
                        array_for_blocks_creation[2] = true; // .#.. 4567
                        array_for_blocks_creation[5] = true; // ##.. 8901
                        array_for_blocks_creation[8] = true; // .... 2345
                        array_for_blocks_creation[9] = true; // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[0] = true;  // #... 0123
                        array_for_blocks_creation[4] = true;  // ###. 4567
                        array_for_blocks_creation[5] = true;  // ..#. 8901
                        array_for_blocks_creation[6] = true;  // .... 2345
                        array_for_blocks_creation[10] = true;
                    }
                    break;
                case image_type.image10:
                    if (Tiles.angle.Equals(block_rotation.zero_deg) || Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[0] = true; // ##.. 0123
                        array_for_blocks_creation[1] = true; // .#.. 4567
                        array_for_blocks_creation[5] = true; // .##. 8901
                        array_for_blocks_creation[9] = true; // .... 2345
                        array_for_blocks_creation[10] = true;
                    }
                    else
                    {
                        array_for_blocks_creation[3] = true; // ...# 0123
                        array_for_blocks_creation[5] = true; // .### 4567
                        array_for_blocks_creation[6] = true; // .#.. 8901
                        array_for_blocks_creation[7] = true; // .... 2345
                        array_for_blocks_creation[9] = true;
                    }
                    break;
                case image_type.image11:
                    if (Tiles.angle.Equals(block_rotation.zero_deg))
                    {
                        array_for_blocks_creation[0] = true; // ###. 0123
                        array_for_blocks_creation[1] = true; // .#.. 4567
                        array_for_blocks_creation[2] = true; // .#.. 8901
                        array_for_blocks_creation[5] = true; // .... 2345
                        array_for_blocks_creation[9] = true;
                    }
                    else if (Tiles.angle.Equals(block_rotation.ninety_deg))
                    {
                        array_for_blocks_creation[2] = true; // ..#. 0123
                        array_for_blocks_creation[4] = true; // ###. 4567
                        array_for_blocks_creation[5] = true; // ..#. 8901
                        array_for_blocks_creation[6] = true; // .... 2345
                        array_for_blocks_creation[10] = true;
                    }
                    else if (Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[1] = true;  // .#.. 0123
                        array_for_blocks_creation[5] = true;  // .#.. 4567
                        array_for_blocks_creation[8] = true;  // ###. 8901
                        array_for_blocks_creation[9] = true;  // .... 2345
                        array_for_blocks_creation[10] = true;
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[5] = true; // .### 4567
                        array_for_blocks_creation[6] = true; // .#.. 8901
                        array_for_blocks_creation[7] = true; // .... 2345
                        array_for_blocks_creation[9] = true;
                    }
                    break;
                case image_type.image12:
                    if (Tiles.angle.Equals(block_rotation.zero_deg))
                    {
                        array_for_blocks_creation[2] = true; // ..#. 0123
                        array_for_blocks_creation[5] = true; // .##. 4567
                        array_for_blocks_creation[6] = true; // .... 8901
                        // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.ninety_deg))
                    {
                        array_for_blocks_creation[1] = true; // .#.. 0123
                        array_for_blocks_creation[4] = true; // .##. 4567
                        array_for_blocks_creation[5] = true; // .... 8901
                        // .... 2345
                    }
                    else if (Tiles.angle.Equals(block_rotation.one_eighty_deg))
                    {
                        array_for_blocks_creation[1] = true;  // .##. 0123
                        array_for_blocks_creation[2] = true;  // ..#. 4567
                        array_for_blocks_creation[6] = true;  // .... 8901
                        // .... 2345
                    }
                    else
                    {
                        array_for_blocks_creation[1] = true; // .##. 0123
                        array_for_blocks_creation[2] = true; // .#.. 4567
                        array_for_blocks_creation[5] = true; // .... 8901
                        // .... 2345
                    }
                    break;
            }

            return array_for_blocks_creation;
        }

        public void set_block_position(ref ScreenDimensions wr)
        {
            set_block_position(ref wr, tiles_array);
        }

        public void set_block_position(ref ScreenDimensions wr, bool[] array_for_blocks_creation)
        {
            wr = new ScreenDimensions();
            bool check_for_block_position;
            int rows, columns;
            check_for_block_position = true;
            for (columns = 0; columns < tile_size; columns++)
            {
                for (rows = 0; rows < tile_size; rows++)
                    if (array_for_blocks_creation[columns + rows * tile_size])
                    {
                        check_for_block_position = false;
                        break;
                    }
                if (check_for_block_position)
                {
                    wr.bottom++;
                }
                else
                {
                    break;
                }
            }
            check_for_block_position = true;
            for (rows = 0; rows < tile_size; rows++)
            {
                for (columns = 0; columns < tile_size; columns++)
                    if (array_for_blocks_creation[columns + rows * tile_size])
                    {
                        check_for_block_position = false;
                        break;
                    }

                if (check_for_block_position)
                {
                    wr.top++;
                }
                else
                {
                    break;
                }
            }
            check_for_block_position = true;
            for (columns = tile_size - 1; columns >= 0; columns--)
            {
                for (rows = 0; rows < tile_size; rows++)
                    if (array_for_blocks_creation[columns + rows * tile_size])
                    {
                        check_for_block_position = false;
                        break;
                    }

                if (check_for_block_position)
                {
                    wr.width++;
                }
                else
                {
                    break;
                }
            }
            wr.width = tile_size - (wr.bottom + wr.width); //exact width of block
            check_for_block_position = true;
            for (rows = tile_size - 1; rows >= 0; rows--)
            {
                for (columns = 0; columns < tile_size; columns++)
                    if (array_for_blocks_creation[columns + rows * tile_size])
                    {
                        check_for_block_position = false;
                        break;
                    }

                if (check_for_block_position)
                {
                    wr.height++;
                }
                else
                {
                    break;
                }
            }
            wr.height = tile_size - (wr.top + wr.height); //exact height of block
        }

        public Image Draw(Graphics graphics, Point point, ScreenDimensions wnd, bool flag = false)
        {
            if (flag == true)
            {
                return Game_Class.generate_pic_field(graphics, wnd);
            }
            else
            {
                if ((wnd.width > 0) && (wnd.height > 0))
                {
                    Image temp = new Bitmap(wnd.width * this.Block_Width, wnd.height * this.Block_Height);
                    Graphics g = Graphics.FromImage(temp);

                    for (int rows = wnd.top; rows < wnd.top + wnd.height; rows++)
                        for (int columns = wnd.bottom; columns < wnd.bottom + wnd.width; columns++)
                            if (tiles_array[columns + rows * tile_size])
                            {
                                obj.Draw(g, new System.Drawing.Point(this.Block_Width * (columns - wnd.bottom), this.Block_Height * (rows - wnd.top)), imageType(Shape));
                            }
                    graphics.DrawImage(temp, new PointF(this.Block_Width * (point.x + wnd.bottom - wnd.bottom), this.Block_Height * (point.y + wnd.top - wnd.top)));
                    g.Dispose();
                    temp.Dispose();
                }
                return null;
            }
        }

        public int imageType(image_type tile)
        {
            switch (tile)
            {
                case image_type.image1:
                    return 0;
                case image_type.image2:
                    return 1;
                case image_type.image3:
                    return 2;
                case image_type.image4:
                    return 3;
                case image_type.image5:
                    return 4;
                case image_type.image6:
                    return 5;
                case image_type.image7:
                    return 6;
                case image_type.image8:
                    return 7;
                case image_type.image9:
                    return 8;
                case image_type.image10:
                    return 9;
                case image_type.image11:
                    return 10;
                default:
                    return 11;
            }
        }

        public int imageColor(Color clr)
        {
            if (clr.Equals(Color.Red))
                return 0;
            else if (clr.Equals(Color.Blue))
                return 1;
            else if (clr.Equals(Color.Green))
                return 2;
            else if (clr.Equals(Color.Cyan))
                return 3;
            else if (clr.Equals(Color.Yellow))
                return 4;
            else if (clr.Equals(Color.Orange))
                return 5;
            else if (clr.Equals(Color.Magenta))
                return 6;
            else if (clr.Equals(Color.Brown))
                return 7;
            else if (clr.Equals(Color.DarkBlue))
                return 8;
            else if (clr.Equals(Color.GreenYellow))
                return 9;
            else if (clr.Equals(Color.Pink))
                return 10;
            else
                return 11;
        }

        public void blockPrivew(Graphics graphics, ScreenDimensions wnd, Tiles t)
        {
            ScreenDimensions wnd_rect = new ScreenDimensions();
            bool[] array_for_blocks_creation = Blocks_Creation(t);
            Point point = new Point();
            set_block_position(ref wnd_rect, array_for_blocks_creation);
            point.x = (wnd.width - wnd_rect.width * this.Block_Width) / 2;
            point.y = (wnd.height - wnd_rect.height * this.Block_Height) / 2;
            for (int rows = wnd_rect.top; rows < wnd_rect.top + wnd_rect.height; rows++)
                for (int columns = wnd_rect.bottom; columns < wnd_rect.bottom + wnd_rect.width; columns++)
                    if (array_for_blocks_creation[columns + rows * tile_size])
                        obj.Draw(graphics, new System.Drawing.Point((point.x + this.Block_Width * (columns - wnd_rect.bottom)), (point.y + this.Block_Height * (rows - wnd_rect.top))), imageType(t.type));
        }

        public block_rotation rotationType(int rot_type)
        {
            if (rot_type.Equals(0))
                // clockwise
                switch (Rotation)
                {
                    case block_rotation.zero_deg:
                        return block_rotation.ninety_deg;
                    case block_rotation.ninety_deg:
                        return block_rotation.one_eighty_deg;
                    case block_rotation.one_eighty_deg:
                        return block_rotation.two_seventy_deg;
                    default:
                        return block_rotation.zero_deg;
                }
            else
                // counter-clockwise
                switch (Rotation)
                {
                    case block_rotation.zero_deg:
                        return block_rotation.two_seventy_deg;
                    case block_rotation.two_seventy_deg:
                        return block_rotation.one_eighty_deg;
                    case block_rotation.one_eighty_deg:
                        return block_rotation.ninety_deg;
                    default:
                        return block_rotation.zero_deg;
                }
        }

        public void Assign(Tiles t)
        {
            Rotation = t.angle;
            Shape = t.type;
        }
    }

    class Game_Class : BaseClass
    {
        static game_level diff_level;
        static int pic_field_width;
        static int pic_field_height;
        public event TetrisHandler ProcessEvent;
        public BlockClass Block = new BlockClass();
        public game_level Difficulty
        {
            get
            {
                return diff_level;
            }
            set
            {
                diff_level = value;
            }
        }

        public Game_Class(ScreenDimensions wr)
        {
            game = new ScreenDimensions(0, 0, wr.width / Block.Block_Width, wr.height / Block.Block_Height);
            pic_field_width = wr.width;
            pic_field_height = wr.height;
            create_pic_field();
        }

        public void reposition_of_blocks()
        {
            for (int i = 0; i < arrField.Length; i++)
            {
                arrField[i] = new Tiles_shapes(Color.Transparent, false);
            }
        }

        public void create_pic_field()
        {
            arrField = new Tiles_shapes[game.width * game.height];
        }

        public static Image generate_pic_field(Graphics e, ScreenDimensions wr)
        {
            Image temp = new Bitmap(pic_field_width, pic_field_height);
            Graphics g = Graphics.FromImage(temp);
            int w = game.width;
            int h = game.height;
            BlockClass b = new BlockClass();
            for (int rows = 0; rows < h; rows++)
                for (int columns = 0; columns < w; columns++)
                    if (((Tiles_shapes)arrField[columns + rows * w]).isBlock)
                    {
                        b.obj.Draw(g, new System.Drawing.Point(columns * b.Block_Width, rows * b.Block_Height), b.imageColor(((Tiles_shapes)arrField[columns + rows * w]).color));
                    }
            g.Dispose();

            return temp;
        }

        public bool check_for_collision(Point point, ScreenDimensions wr)
        {
            int width = game.width;
            int index_of_tile;
            int index_of_pic_field;

            for (int i = 0; i < wr.height; i++)
                for (int j = 0; j < wr.width; j++)
                {
                    index_of_tile = (wr.bottom + j) + ((wr.top + i) * tile_size);
                    index_of_pic_field = ((point.x + point.y * width) + j) + i * width;
                    if ((index_of_pic_field >= 0) && (index_of_pic_field < arrField.Length))
                    {
                        if (tiles_array[index_of_tile] && ((Tiles_shapes)arrField[index_of_pic_field]).isBlock)
                        {
                            return true;
                        }
                    }
                    else
                        return true;
                }

            return false;
        }

        public void blocksMovements(Point point, ScreenDimensions wr)
        {
            int index_of_tile;
            int index_of_pic_field;

            for (int i = 0; i < wr.height; i++)
                for (int j = 0; j < wr.width; j++)
                {
                    index_of_tile = (wr.bottom + j) + (wr.top + i) * Block.Size;
                    index_of_pic_field = (point.x - game.bottom + j) + (point.y - game.top + i) * game.width;
                    if (tiles_array[index_of_tile])
                    {
                        arrField[index_of_pic_field] = new Tiles_shapes(Block.color(Block.Shape), true);
                    }
                }

            rowsCompletion();
        }

        public void rowsCompletion()
        {
            int height = game.height;
            int width = game.width;
            int count = 0;
            bool isComplete = true;
            int total_rows = height - 1;
            Tiles_shapes[] array_for_blocks_creation = new Tiles_shapes[game.width * game.height];
            for (int i = height - 1; i >= 0; i--)
            {
                for (int columns = width - 1; (columns >= 0) && isComplete; columns--)
                    if (!((Tiles_shapes)arrField[columns + i * width]).isBlock)
                    {
                        isComplete = false;
                    }
                if (!isComplete)
                {
                    // copy the rows
                    for (int j = width - 1; j >= 0; j--)
                        array_for_blocks_creation[j + total_rows * width] = arrField[j + i * width];

                    total_rows--;
                    isComplete = true;
                }
                else
                {   // exclude rows that are completed.
                    count++;
                }
            }

            // get all the rows that are not completed.
            arrField = array_for_blocks_creation;
            EventArgs e = new EventArgs(count);
            isDone((object)this, e);
        }

        private void isDone(object o, EventArgs e)
        {
            if (ProcessEvent != null)
            {
                ProcessEvent(o, e);
            }
        }
    }
}
