using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.IO;


namespace Tetris
{
    public partial class Form1 : Form
    {
        Game_Class game;
        Point pt_obj = new Point();
        ScreenDimensions scren_dimensions = new ScreenDimensions();
        Music music_obj = new Music();
        Tiles current_block = new Tiles();
        Tiles next_block = new Tiles();
        int game_speed = 1;
        int level = 1;
        int score = 0;
        int total_lines = 0;
        bool check_for_rows = false;
        bool check_for_start_game = false;
        bool check_for_game_over = false;
        bool check_for_game_completion = false;
        bool check_for_rotation = false;
        Bitmap saveImage;
        SoundPlayer sound = new SoundPlayer("C:\\TetrisMusic.wav");
       
        public Form1()
        {
            InitializeComponent();
        }

        private void picField_Paint(object sender, PaintEventArgs e)
        {
            if (check_for_start_game == true)
            {
                if (check_for_rows == true)
                {
                    saveImage = new Bitmap(game.Block.Draw(e.Graphics, pt_obj, scren_dimensions, true), new Size(picField.Width, picField.Height));
                    e.Graphics.DrawImage(saveImage, new PointF(0, 0));
                    
                    pt_obj.y = -1;
                    check_for_rows = false;
                    return;
                }

                e.Graphics.DrawImage(saveImage, 0, 0, saveImage.Width, saveImage.Height);
                game.Block.Draw(e.Graphics, pt_obj, scren_dimensions, false);
            }

            if (check_for_game_completion == true || check_for_game_over == true)
            {
                e.Graphics.DrawImage(saveImage, 0, 0, saveImage.Width, saveImage.Height);
                game.Block.Draw(e.Graphics, pt_obj, scren_dimensions, false);
                if (check_for_game_completion == true)
                {
                    Size s = e.Graphics.MeasureString("Congratulations!!!", new Font("Arial Black", 16)).ToSize();
                    e.Graphics.DrawString("Congratulations!!!", new Font("Arial Black", 16), new SolidBrush(Color.White), new PointF((picField.Width - s.Width) / 2, (picField.Height - s.Height) / 4));
                }
                else
                {
                    Size s = e.Graphics.MeasureString("Game Over!!!", new Font("Arial Black", 20)).ToSize();
                    e.Graphics.DrawString("Game Over!!!", new Font("Arial Black", 20),
                                                          new SolidBrush(Color.White),
                                                          new PointF((picField.Width - s.Width) / 2, (picField.Height - s.Height) / 4));
                }
                GameStart(false);
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // initalize the width and height of the stage.
            game = new Game_Class(new ScreenDimensions(0, 0, picField.Width, picField.Height));
            game.ProcessEvent += new Tetris.TetrisHandler(Tetris_Process);
            game.Block.Block_Width = 24;
            game.Block.Block_Height = 24;
            saveImage = new Bitmap(picField.Width, picField.Height);
       
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            if (check_for_start_game == true)
            {  // show the next block.
                game.Block.blockPrivew(e.Graphics, new ScreenDimensions(0, 0, picPreview.Width, picPreview.Height), next_block);
            }
        }

        private void PlayBlock(Tetris.Tiles sbBlock, Boolean isNew)
        {
            if (isNew)
            {    // create a new block
                sbBlock = game.Block.blocks_generation(game.Difficulty);
            }
            else
            {
                game.blocksMovements(pt_obj, scren_dimensions);
            }
            game.Block.Assign(sbBlock);
            game.Block.Build();
            game.Block.set_block_position(ref scren_dimensions);
            // draw the block in center-x, top-y
            pt_obj.x = (picField.Width / game.Block.Block_Width - scren_dimensions.width) / 2;
            pt_obj.y = 0;

            ShowNextBlock(isNew);
            picField.Invalidate();

            if (game.check_for_collision(pt_obj, scren_dimensions))
            {
                check_for_game_over = true;
                if (mnuGameSettingsSound.Checked)
                {
                   using (SoundPlayer player = new SoundPlayer("C:\\GameOver.wav"))
                    {
                        player.PlaySync();
                    }
                }
            }
        }

        private void ShowNextBlock(Boolean isNew)
        {
            if (isNew)
                //    I really don't why I need to add this code everytime the game starts. This will produce 
                // two different blocks for current and next block. Without this, 
                // the current and next block will always be the same everytime the game start...
                System.Threading.Thread.Sleep(200);

            next_block = game.Block.blocks_generation(game.Difficulty);
            picPreview.Invalidate();
        }

        private void tmrGame_Tick(object sender, System.EventArgs e)
        {
            if (picField.Height > (game.Block.Block_Width * (pt_obj.y + scren_dimensions.height)))
                if (game.check_for_collision(new Point(pt_obj.x, pt_obj.y + 1), scren_dimensions))
                {
                    copyImage();
                    // The block has collided, set the next block.
                    PlayBlock(next_block, false);
                }
                else
                {
                    picField.Invalidate();
                    pt_obj.y++;
                }
            else
            {
                copyImage();
                // where at the bottom, set the next block.
                PlayBlock(next_block, false);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!check_for_start_game) return;

            // check if key input is valid (up, down, left, right, space, escape)
            if (!(e.KeyCode.Equals(Keys.Up) || e.KeyCode.Equals(Keys.Down) ||
                  e.KeyCode.Equals(Keys.Left) || e.KeyCode.Equals(Keys.Right) ||
                  e.KeyCode.Equals(Keys.Space) || e.KeyCode.Equals(Keys.Escape)))
                return;

            Point newPos = pt_obj; // get block current position
            Boolean isValidMove = false;

            switch (e.KeyCode)
            {
                case Keys.Right: // Right
                    // could go right?
                    if ((newPos.x + scren_dimensions.width) * game.Block.Block_Width < picField.Width)
                        newPos.x++;

                    if (newPos.x.Equals(pt_obj.x))
                        return;
                    break;
                case Keys.Left: // Left
                    // could go left?
                    if (newPos.x > 0)
                        newPos.x--;

                    if (newPos.x.Equals(pt_obj.x))
                        return;
                    break;
                case Keys.Down: // Down
                    // could go down?
                    if ((newPos.y + scren_dimensions.height) * game.Block.Block_Height < picField.Height)
                        newPos.y++;

                    if (newPos.y.Equals(pt_obj.y))
                        return;
                    break;
                case Keys.Up:    // Up       (rotate)
                case Keys.Space: // Spacebar (rotate)
                    ScreenDimensions newBlockAdj = new ScreenDimensions();

                    // Save old angle.
                    Tetris.block_rotation saveAngle = game.Block.Rotation;

                    // try clockwise
                    newBlockAdj = game.Block.Block_Rotation(game.Block.rotationType(0));

                    if ((newPos.x + newBlockAdj.width) * game.Block.Block_Width > picField.Width)
                        newPos.x = picField.Width / game.Block.Block_Width - newBlockAdj.width;
                    if ((newPos.y + newBlockAdj.height) * game.Block.Block_Height > picField.Height)
                        return;

                    if (game.check_for_collision(new Point(newPos.x, newPos.y), newBlockAdj))
                    {
                        // try counter-clockwise
                        newBlockAdj = game.Block.Block_Rotation(game.Block.rotationType(1));

                        if ((newPos.x + newBlockAdj.width) * game.Block.Block_Width > picField.Width)
                            newPos.x = picField.Width / game.Block.Block_Width - newBlockAdj.width;
                        if ((newPos.y + newBlockAdj.height) * game.Block.Block_Height > picField.Height)
                            return;

                        if (game.check_for_collision(new Point(newPos.x, newPos.y), newBlockAdj))
                            check_for_rotation = false;
                        else
                            check_for_rotation = true;
                    }
                    else
                        check_for_rotation = true;

                    if (check_for_rotation)
                    {
                        if (scren_dimensions.bottom.Equals(newBlockAdj.bottom) &&
                            scren_dimensions.top.Equals(newBlockAdj.top) &&
                            scren_dimensions.width.Equals(newBlockAdj.width) &&
                            scren_dimensions.height.Equals(newBlockAdj.height))
                            // nothing has changed, just leave it.
                            return;

                        // can rotate, apply the new settings.
                        scren_dimensions = newBlockAdj;
                        pt_obj = newPos;
                        isValidMove = true;
                    }
                    else
                    {
                        // can't rotate, restore old angle.
                        game.Block.Block_Rotation(saveAngle);
                        return;
                    }
                    break;
            }

            if (!(e.KeyCode.Equals(Keys.Space) || e.KeyCode.Equals(Keys.Up)))
                if (!game.check_for_collision(new Point(newPos.x, newPos.y), scren_dimensions))
                {
                    pt_obj = newPos;
                    isValidMove = true;
                }

            if (isValidMove)
                picField.Invalidate();
        }
        private void Tetris_Process(object o, Tetris.EventArgs e)
        {
            if (e.RowsCompleted > 0)
            {
                check_for_rows = true;
                if (e.RowsCompleted > 1)
                {
                    score += e.RowsCompleted * 300;
                }
                else
                {
                    score += e.RowsCompleted * 100;
                }
                total_lines += e.RowsCompleted;

                // Increase the game speed according to the number of lines completed.
                if ((total_lines >= 5) && (total_lines <= 10))
                    game_speed = 2;
                else if ((total_lines >= 11) && (total_lines <= 15))
                    game_speed = 3;
                else if ((total_lines >= 16) && (total_lines <= 20))
                    game_speed = 4;
                else if ((total_lines >= 21) && (total_lines <= 25))
                    game_speed = 5;
                else if ((total_lines >= 26) && (total_lines <= 30))
                    game_speed = 6;
                else if ((total_lines >= 31) && (total_lines <= 35))
                    game_speed = 7;
                else if ((total_lines >= 36) && (total_lines <= 40))
                    game_speed = 8;
                else if ((total_lines >= 41) && (total_lines <= 90))
                    game_speed = 9;

                // level is equal to speed.
                level = game_speed;
                // calculate new speed
                tmrGame.Interval = 1000 - (game_speed * 100);

                picField.Invalidate();
                ShowStatus();
            }
        }

        private void ShowStatus()
        {
            lblScore.Text = "Score : " + score.ToString();
            lblLines.Text = "Lines : " +total_lines.ToString();
            lblSpeed.Text = "Speed : " + game_speed.ToString();
            lblLevel.Text = "Level : " + level.ToString();
         
            if (total_lines >= 100)
            {
                check_for_game_completion = true;
                if (mnuGameSettingsSound.Checked)
                {
                   // music_obj.StartGame(global::Tetris.SoundResource.S104);
                }
            }
        }

        private void copyImage()
        {
            picField.DrawToBitmap(saveImage, new Rectangle(0, 0, picField.Width, picField.Height));
        }

        private void mnuGameSettingsEasy_Click(object sender, System.EventArgs e)
        {
            mnuGameSettingsEasy.Checked = true;
            mnuGameSettingsHard.Checked = false;
        }

        private void mnuGameSettingsHard_Click(object sender, System.EventArgs e)
        {
            mnuGameSettingsEasy.Checked = false;
            mnuGameSettingsHard.Checked = true;
        }

        private void mnuGameSettingsSound_Click(object sender, System.EventArgs e)
        {
            mnuGameSettingsSound.Checked = !mnuGameSettingsSound.Checked;
            sound.Stop();
            if (mnuGameSettingsSound.Checked)
            {
                sound.PlayLooping();
            }
        }

        public void GameStart(Boolean isStart)
        {
            if (isStart)
            {
                if (mnuGameSettingsEasy.Checked)
                    game.Difficulty = game_level.Easy;
                else
                    game.Difficulty = game_level.Hard;

                // reset game
                game.reposition_of_blocks();
                // copy the background image and place it to saveImage.
                copyImage();
                // reset menus
                mnuGameSettingsEasy.Enabled = false;
                mnuGameSettingsHard.Enabled = false;
                // reset status
                game_speed = 1;
                level = 1;
                score = 0;
                total_lines = 0;
                // show status
                ShowStatus();
                // reset variables
                check_for_game_completion = false;
                check_for_game_over = false;
                check_for_start_game = true;
                // set current block
                PlayBlock(current_block, true);
                // initialize the timer.
                tmrGame.Interval = 1000;
                // start the game.
                tmrGame.Enabled = true;

                mnuGamePlay.Text = "&Stop";
            }
            else
            {
                // reset menus
                mnuGameSettingsEasy.Enabled = true;
                mnuGameSettingsHard.Enabled = true;
                // reset variables
                check_for_game_completion = false;
                check_for_game_over = false;
                check_for_start_game = false;
                // stop the game.
                check_for_start_game = false;
                tmrGame.Enabled = false;

                mnuGamePlay.Text = "&Start";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }

        private void mnuGame_Click(object sender, System.EventArgs e)
        {

        }

        private void mnuGamePlay_Click(object sender, System.EventArgs e)
        {
            if (mnuGamePlay.Text.ToUpper().Equals("&START"))
            {
                sound.PlayLooping();
                GameStart(true);
            }
            else
            {
                GameStart(false);
            }
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
