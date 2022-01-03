using System;
using System.IO;
using System.Collections.Generic;

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
                    dy = -1;
                    break;
                case direction.SouthWest:
                    dx = -1;
                    dy = -1;
                    break;
                case direction.West:
                    dx = -1;
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

        public location location {get; set;}
        public piece(pieceColour c, pieceType t)
        {
            this.type = t;
            this.colour = c;
        }

        public static piece charToPiece(char c)
        {
            switch (c)
            {
                // whites
                case 'K':
                case '♚':
                    return new piece(pieceColour.Black,pieceType.King);
                case 'Q':
                case '♛':
                    return new piece(pieceColour.Black,pieceType.Queen);
                case 'B':
                case '♝':
                    return new piece(pieceColour.Black,pieceType.Bishop);
                case 'N':
                case '♞':
                    return new piece(pieceColour.Black,pieceType.Knight);
                case 'R':
                case '♜':
                    return new piece(pieceColour.Black,pieceType.Rook);
                case 'P':
                case '♟':
                    return new piece(pieceColour.Black,pieceType.Pawn);

                // blacks

                case 'k':
                case '♔':
                    return new piece(pieceColour.White,pieceType.King);
                case 'q':
                case '♕':
                    return new piece(pieceColour.White,pieceType.Queen);
                case 'b':
                case '♗':
                    return new piece(pieceColour.White,pieceType.Bishop);
                case 'n':
                case '♘':
                    return new piece(pieceColour.White,pieceType.Knight);
                case 'r':
                case '♖':
                    return new piece(pieceColour.White,pieceType.Rook);
                case 'p':
                case '♙':
                    return new piece(pieceColour.White,pieceType.Pawn);
                
            }
            return null;
        }

        override public string ToString ()
        {
            bool unicode = false;
            if (colour == pieceColour.Black)
            {
                switch (type)
                {
                    case pieceType.King:
                        return unicode ? "♚": "K";
                    case pieceType.Queen:
                        return unicode ? "♛": "Q";
                    case pieceType.Bishop:
                        return unicode ? "♝": "B";
                    case pieceType.Knight:
                        return unicode ? "♞": "N";
                    case pieceType.Rook:
                        return unicode ? "♜": "R";
                    case pieceType.Pawn:
                        return unicode ? "♟": "P";
                }
            }
            else
            {
                switch (type)
                {
                    case pieceType.King:
                        return unicode ? "♔" : "k";
                    case pieceType.Queen:
                        return unicode ? "♕": "q";
                    case pieceType.Bishop:
                        return unicode ? "♗": "b";
                    case pieceType.Knight:
                        return unicode ? "♘": "n";
                    case pieceType.Rook:
                        return unicode ? "♖": "r";
                    case pieceType.Pawn:
                        return unicode ? "♙": "p";
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
            for (int i=1; i<=8; i++)
            {
                b.addToBoard(new location(i,2), new piece(pieceColour.White, pieceType.Pawn));
            }
            for (int i=1; i<=8; i++)
            {
                b.addToBoard(new location(i,7), new piece(pieceColour.Black, pieceType.Pawn));
            }

            // generates the rest of the white team


            b.addToBoard(new location(1,1), new piece(pieceColour.White, pieceType.Rook));
            b.addToBoard(new location(2,1), new piece(pieceColour.White, pieceType.Knight));
            b.addToBoard(new location(3,1), new piece(pieceColour.White, pieceType.Bishop));
            b.addToBoard(new location(4,1), new piece(pieceColour.White, pieceType.Queen));
            b.addToBoard(new location(5,1), new piece(pieceColour.White, pieceType.King));
            b.addToBoard(new location(6,1), new piece(pieceColour.White, pieceType.Bishop));
            b.addToBoard(new location(7,1), new piece(pieceColour.White, pieceType.Knight));
            b.addToBoard(new location(8,1), new piece(pieceColour.White, pieceType.Rook));

            // generates the rest of the black team

            b.addToBoard(new location(1,8), new piece(pieceColour.Black, pieceType.Rook));
            b.addToBoard(new location(2,8), new piece(pieceColour.Black, pieceType.Knight));
            b.addToBoard(new location(3,8), new piece(pieceColour.Black, pieceType.Bishop));
            b.addToBoard(new location(4,8), new piece(pieceColour.Black, pieceType.Queen));
            b.addToBoard(new location(5,8), new piece(pieceColour.Black, pieceType.King));
            b.addToBoard(new location(6,8), new piece(pieceColour.Black, pieceType.Bishop));
            b.addToBoard(new location(7,8), new piece(pieceColour.Black, pieceType.Knight));
            b.addToBoard(new location(8,8), new piece(pieceColour.Black, pieceType.Rook));


            return b;
        }

        public void save(string file)
        {
            File.WriteAllText(file,ToString());
        }

        public static board load(string file)
        {
            board b = new board();
            string[] lines = File.ReadAllLines(file);
            for (int i=0; i<8; i++)
            {
                char[] characters = lines[i].ToCharArray();
                int row = 8 - i;
                for (int j=0; j<8; j++)
                {
                    char c = characters[j];
                    piece p = piece.charToPiece(c);
                    if (p !=null)
                    {
                        b.addToBoard(new location(j+1,row), p);
                    }
                }
            }
            return b;
        }

        public void addToBoard(location l, piece p)
        {
            tiles[l.x -1 ,l.y -1] = p;
            p.location = l;
        }
        public piece getPieceAtLocation(location l)
        {
            return tiles[l.x -1 ,l.y -1 ];
        }

        public List<piece> findPieces(pieceType type, pieceColour colour)
        {
            List<piece> pieces = new List<piece> ();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    piece p = tiles[i,j];
                    if (p != null && p.type == type && p.colour ==  colour)
                    {
                        pieces.Add(p);
                    }
                }
            }

            return pieces;
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

            for (int row=7; row >= 0; row--)
            {
                for (int column=0; column < 8; column++)
                {
                    piece p = tiles[column,row];
                    if (p == null)
                    {
                        output += "·"; // for better visual effects change this to a · to see the rest of the board better
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
            board chess_board;
            if (args.Length == 1)
            {
                Console.WriteLine("Reading from file " + args[0]);
                chess_board = board.load(args[0]);
            }
            else
            {
                chess_board = board.DefaultBoard();
            }

            // writes out the board
            Console.WriteLine(chess_board);

            location black_king = chess_board.findPieces(pieceType.King,pieceColour.Black)[0].location;

            piece p = checks(black_king,chess_board);
            
            if (p == null)
            {
                Console.WriteLine("King is not in check");
            }
            else
            {
                Console.WriteLine("The king is in check");
                Console.WriteLine(p.location);
                Console.WriteLine(p);
            }
   
        }
        static piece checks(location black_king, board chess_board)
        {
            return
            in_check_straight_line(black_king,chess_board.getNextPiece(black_king,direction.North)) ??
            in_check_diagonal(black_king,chess_board.getNextPiece(black_king,direction.NorthEast)) ??
            in_check_straight_line(black_king,chess_board.getNextPiece(black_king,direction.East)) ??
            in_check_diagonal(black_king,chess_board.getNextPiece(black_king,direction.SouthEast)) ??
            in_check_straight_line(black_king,chess_board.getNextPiece(black_king,direction.South)) ??
            in_check_diagonal(black_king,chess_board.getNextPiece(black_king,direction.SouthWest)) ??
            in_check_straight_line(black_king,chess_board.getNextPiece(black_king,direction.West)) ??
            in_check_diagonal(black_king,chess_board.getNextPiece(black_king,direction.NorthWest));
        }
        static piece in_check_straight_line(location black_king, piece p)
        {
            if (p == null || p.colour == pieceColour.Black)
            {
                return null;
            }
            if (p.type == pieceType.Queen || p.type == pieceType.Rook)
            {
                return p;
            }
            return null;
        }
        static piece in_check_diagonal(location black_king, piece p)
        {
            if (p == null|| p.colour == pieceColour.Black)
            {
                return null;
            }
            if (p.type == pieceType.Queen || p.type == pieceType.Bishop)
            {
                return p;
            }
            return null;
        }
    }
}
