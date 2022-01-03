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
        public location(int x, int y)
        {
            if ( x < 1 || x > 8 || y < 1 || y > 8)
            {
                throw new ArgumentException("Parameter out of range, both the X and Y value must be between 1 and 8 inclusive (X=" + x + "  Y=" + y + ")");
            }

            this.x = x;
            this.y = y;
        }

        override public string ToString ()
        {
            return Convert.ToString(Convert.ToChar(this.x + 64)) + Convert.ToString(this.y);
        }
        public location getNextLocation(direction d)
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
            int newx = this.x + dx;
            int newy = this.y + dy;
            try
            {
                return new location(newx,newy);
            }
            catch
            {
                return null;
            }
        }
    }
    class piece
    {
        public pieceType type {get;}
        public pieceColour colour {get;}

        public location location {get;}
        public piece(pieceColour c, pieceType t)
        {
            this.type = t;
            this.colour = c;
        }

        override public string ToString ()
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
            throw new ArgumentException("Unkown piece");
        }
    }

    class board
    {
        piece[,] tiles;
        public board ()
        {
            tiles = new piece[8,8];
        }

        public static board DefaultBoard()
        {
            board b = new board();

            // generate two rows of pawns for each player
            for (int i=0; i<8; i++)
            {
                b.tiles[1,i] = new piece(pieceColour.White, pieceType.Pawn);
            }
            for (int i=0; i<8; i++)
            {
                b.tiles[6,i] = new piece(pieceColour.Black, pieceType.Pawn);
            }

            // generates the rest of the white team

            b.tiles[0,0] = new piece(pieceColour.White, pieceType.Rook);
            b.tiles[0,1] = new piece(pieceColour.White, pieceType.Knight);
            b.tiles[0,2] = new piece(pieceColour.White, pieceType.Bishop);
            b.tiles[0,3] = new piece(pieceColour.White, pieceType.Queen);
            b.tiles[0,4] = new piece(pieceColour.White, pieceType.King);
            b.tiles[0,5] = new piece(pieceColour.White, pieceType.Bishop);
            b.tiles[0,6] = new piece(pieceColour.White, pieceType.Knight);
            b.tiles[0,7] = new piece(pieceColour.White, pieceType.Rook);

            // generates the rest of the black team

            b.tiles[7,0] = new piece(pieceColour.Black, pieceType.Rook);
            b.tiles[7,1] = new piece(pieceColour.Black, pieceType.King);
            b.tiles[7,2] = new piece(pieceColour.Black, pieceType.Bishop);
            b.tiles[7,3] = new piece(pieceColour.Black, pieceType.Queen);
            b.tiles[7,4] = new piece(pieceColour.Black, pieceType.King);
            b.tiles[7,5] = new piece(pieceColour.Black, pieceType.Bishop);
            b.tiles[7,6] = new piece(pieceColour.Black, pieceType.King);
            b.tiles[7,7] = new piece(pieceColour.Black, pieceType.Rook);


            return b;
        }
        public piece getPieceAtLocation(location l)
        {
            return tiles[l.x -1 ,l.y -1 ];
        }

        public piece getNextPiece(location l, direction d)
        {
            piece found = null;

            while (found == null && (l = l.getNextLocation(d)) != null)
            {
                found = getPieceAtLocation(l);
            }

            return found;
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
                        output += p.ToString();
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
            board chess_board = board.DefaultBoard();

            Console.WriteLine(chess_board.getPieceAtLocation(new location(1,1)));

            Console.WriteLine(chess_board);
            Console.WriteLine(new location(1,1).getNextLocation(direction.North));
            
        }
    }
}
