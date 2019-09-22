﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace simulation
{
    static class Layout
    {
        static public List<Block> Blocks=new List<Block>();
        static public Switch LeftSwitch;
        static public Switch RightSwitch;

        static public void Initialize()
        {
            for (int i = 1; i < 8; i++) Blocks.Add(new Block(i));
            int x, y;
            // ***************** Block#1 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 2;
            Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            x += Field.SquareSize;
            for (int i = 0; i < 4; i++)
            {
                Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomLeft));
            Blocks[0].CWSignal = new Signal(360, 340, SignalOrientation.CW);
            Blocks[0].CCWSignal = new Signal(840, 310, SignalOrientation.CCW);
            // ***************** Block#2 *****************
            x = Field.SquareSize * 3; y = Field.SquareSize * 3;
            for (int i = 0; i < 4; i++)
            {
                Blocks[1].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[1].CWSignal = new Signal(285, 410, SignalOrientation.CW);
            Blocks[1].CCWSignal = new Signal(915, 380, SignalOrientation.CCW);
            // ***************** Block#3 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 3;
            LeftSwitch = new Switch(x, y, SwitchOrientation.CCW);
            Blocks[2].Tracks.Add(LeftSwitch);
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopRight));
            Blocks[2].CCWSignal = new Signal(230, 380, SignalOrientation.CCW);
            // ***************** Block#4 *****************
            x = 0; y = Field.SquareSize * 2;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            // ***************** Block#5 *****************
            x = Field.SquareSize * 1; y = 0;
            for (int i = 0; i < 8; i++)
            {
                Blocks[4].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            // ***************** Block#6 *****************
            x = Field.SquareSize * 9; y = 0;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomLeft));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            // ***************** Block#7 *****************
            x = Field.SquareSize * 9; y = Field.SquareSize * 3;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopLeft));
            x -= Field.SquareSize;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            RightSwitch = new Switch(x, y, SwitchOrientation.CW);
            Blocks[6].Tracks.Add(RightSwitch);
            x -= Field.SquareSize;
            Blocks[6].CWSignal = new Signal(970, 410, SignalOrientation.CW);
        }

        static public void Draw()
        {
            //MainWindow.AppCanvas.Children.Clear();
            Blocks.ForEach(b => b.Draw());
        }

        static private void AddLabel(int x, int y, string content)
        {
            Label L = new Label();
            L.Content = content;
            L.FontSize = 14;
            Canvas.SetTop(L, y);
            Canvas.SetLeft(L, x);
            Canvas.SetZIndex(L, 1);
            MainWindow.AppCanvas.Children.Add(L);
        }
        

    }
}
