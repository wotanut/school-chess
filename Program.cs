using System;

namespace chess
{

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

        public string display()
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

            Console.WriteLine(chess_board.display());
        }
    }
}
