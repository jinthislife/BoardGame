﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using System.Xml.Linq;

namespace BoardGame
{
    //public class Move: ICommand
    //{
    //    public int row;
    //    public int col;
    //    public Player player;
    //    public MoveTracker moveTracker;
    //    public Board board;

    //    public Move(int row, int col, Player player, MoveTracker moveTracker, Board board)
    //    {
    //        this.row = row;
    //        this.col = col;
    //        this.player = player;
    //        this.moveTracker = moveTracker;
    //        this.board = board;
    //    }

    //    public void Execute()
    //    {
    //        board.placeMove(this);
    //        moveTracker.InsertMove(this);
    //    }

    //    public void UnExecute()
    //    {
    //        board.withdrawMove(this);
    //    }
    //}

    public class Move
    {
        public int row { get; set; }
        public int col { get; set; }
        public Player player { get; set; }

        
        public Move(int row, int col, Player player)
        {
            this.row = row;
            this.col = col;
            this.player = player;
        }

        public override string ToString()
        {
            return $"{row},{col},{((player is HumanPlayer) ? 1 : 0)},{player.name},{player.piece.GetType()},{player.piece.ToString()}";
        }
    }
}