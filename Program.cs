using System;

namespace chess
{

    public enum direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,

    }
    public enum pieceType
    {
        King,
        Queen,
        Bishop,
        Knight,
        Rook,
        Pawn
    }
    public enum pieceColour
    {
        Black,
        White,
    }

    class location
    {
        public int x {get;}
        public int y {get;}
        override public string ToString ()
        {
            return Convert.ToString(Convert.ToChar(this.x + 64)) + Convert.ToString(this.y);
        }
        public location(int x, int y)
        {
            if ( x < 1 || x > 8 || y < 1 || y > 8)
            {
                throw new ArgumentException("Parameter out of range, both the X and Y value must be between 1 and 8 inclusive (X=" + x + "  Y=" + y + ")");
            }

            this.x = x;
            this.y = y;
        }
    }
    class piece
    {
        public pieceType type {get;}
        public pieceColour colour {get;}
        public piece(pieceColour c, pieceType t)
        {
            this.type = t;
            this.colour = c;
        }

        public string name()
        {
            if (colour == pieceColour.Black)
            {
                switch (type)
                {
                    case pieceType.King:
                        return "♚";
                    case pieceType.Queen:
                        return "♛";
                    case pieceType.Bishop:
                        return "♝";
                    case pieceType.Knight:
                        return "♞";
                    case pieceType.Rook:
                        return "♜";
                    case pieceType.Pawn:
                        return "♟︎";
                }
            }
            else
            {
                switch (type)
                {
                    case pieceType.King:
                        return "♔";
                    case pieceType.Queen:
                        return "♕";
                    case pieceType.Bishop:
                        return "♗";
                    case pieceType.Knight:
                        return "♘";
                    case pieceType.Rook:
                        return "♖";
                    case pieceType.Pawn:
                        return "♙";
                }
            }
            return "?";
        }
    }

    class board
    {
        piece[,] tiles;
        public board ()
        {
            tiles = new piece[8,8];
            // tiles[7,7] = new piece(pieceColour.White, pieceType.Knight);
        }

        public piece getNextPiece(direction d)
        {
            int dx = 0;
            int dy = 0;
            switch (d)
            {
                case direction.North:
                    dy = 1;
                    break;
                case direction.NorthEast:
                    dy = 1;
                    dx = 1;
                    break;
                case direction.East:
                    dx = 1;
                    break;
                case direction.SouthEast:
                    dx = 1;
                    dy = -1;
                    break;
                case direction.South:
                    dx = -1;
                    break;
                case direction.SouthWest:
                    dx = -1;
                    dy = -1;
                    break;
                case direction.West:
                    dy = -1;
                    break;
                case direction.NorthWest:
                    dx = -1;
                    dy = 1;
                    break;
            }
            return null;
        }

        override public string ToString()
        {
            string output = "";

            for (int row=0; row < 8; row++)
            {
                for (int column=0; column < 8; column++)
                {
                    piece p = tiles[row,column];
                    if (p == null)
                    {
                        output += " ";
                    }
                    else
                    {
                        output += p.name();
                    }
                }
                output += "\n";
            }

            return output;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            piece x = new piece(pieceColour.White, pieceType.Queen);
            board chess_board = new board();
            

            Console.WriteLine(chess_board);
            Console.WriteLine(new location(1,1));
        }
    }
}
