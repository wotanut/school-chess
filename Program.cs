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
        public piece(pieceType t, pieceColour c)
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
    class Program
    {
        static void Main(string[] args)
        {
            piece x = new piece(pieceType.Queen, pieceColour.White);
            Console.WriteLine(x.name());
        }
    }
}
