using System;
using System.IO;
using System.Collections.Generic;

namespace chess
{

// some enums
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

        // location class so we can tell where the piece is on the board
        public int x {get;}
        public int y {get;}
        public location(int x, int y)
        {
            // error handling
            if ( x < 1 || x > 8 || y < 1 || y > 8)
            {
                throw new ArgumentException("Parameter out of range, both the X and Y value must be between 1 and 8 inclusive (X=" + x + "  Y=" + y + ")");
            }

            this.x = x;
            this.y = y;
        }

        // converts it to a human readable format (A3, B8, E4 etc)
        override public string ToString ()
        {
            return Convert.ToString(Convert.ToChar(this.x + 64)) + Convert.ToString(this.y);
        }

        // get a new tile relative to the old one (N E S W NE SE SW NW)
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
        // piece class
        public pieceType type {get;}
        public pieceColour colour {get;}

        public location location {get; set;}
        public piece(pieceColour c, pieceType t)
        {
            this.type = t;
            this.colour = c;
        }

         // converts a character to a piece
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

        // returns a piece type

        override public string ToString ()
        {
            bool unicode = false; // change this to true if you want to view the board in unicode characters

            // black colours
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

            // white colours
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
            // error handling
            throw new ArgumentException("Unkown piece");
        }
    }

    class board
    {
        // board class

        // create the 2d array
        piece[,] tiles;
        public board ()
        {
            // load in the 2d array
            tiles = new piece[8,8];
        }

        // a defualt board so you don't have to create a new one each time
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

        // saves the board to a file
        public void save(string file)
        {
            File.WriteAllText(file,ToString());
        }

        // loads a board from a file
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

        // adds a piece to the board in a specified location
        public void addToBoard(location l, piece p)
        {
            tiles[l.x -1 ,l.y -1] = p;
            p.location = l;
        }

        // get informatino about a piece at that location
        public piece getPieceAtLocation(location l)
        {
            return tiles[l.x -1 ,l.y -1 ];
        }

        // find all the pieces of any type or colour
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

        // get the next piece in any direction
        public piece getNextPiece(location l, direction d)
        {
            piece found = null;

            while (found == null && (l = l.getNextLocation(d)) != null)
            {
                found = getPieceAtLocation(l);
            }

            return found;
        }

        // prints out the table
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
            // loads in the chess board if an argument is given (dotnet run board.txt)
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

            // find the black king and checks if it is in check
            location black_king = chess_board.findPieces(pieceType.King,pieceColour.Black)[0].location;
            piece king = chess_board.getPieceAtLocation(black_king);

            piece p = checks(king,black_king,chess_board);
            
            if (p == null)
            {
                Console.WriteLine("The black King is not in check");
            }
            else
            {
                Console.WriteLine("The black king is in check");
                Console.WriteLine(p.location);
                Console.WriteLine(p);
            }

            // find the white king and checks if it is in check
            location white_king = chess_board.findPieces(pieceType.King,pieceColour.White)[0].location;
            king = chess_board.getPieceAtLocation(black_king);

            p = checks(king,white_king,chess_board);
            
            if (p == null)
            {
                Console.WriteLine("The black King is not in check");
            }
            else
            {
                Console.WriteLine("The black king is in check");
                Console.WriteLine(p.location);
                Console.WriteLine(p);
            }
   
   
        }
        static piece checks(piece checKing,location king, board chess_board)
        {
            // runs the functions to check all around it
            return
            in_check_straight_line(checKing, king,chess_board.getNextPiece(king,direction.North)) ??
            in_check_diagonal(checKing, king,chess_board.getNextPiece(king,direction.NorthEast)) ??
            in_check_straight_line(checKing, king,chess_board.getNextPiece(king,direction.East)) ??
            in_check_diagonal(checKing, king,chess_board.getNextPiece(king,direction.SouthEast)) ??
            in_check_straight_line(checKing, king,chess_board.getNextPiece(king,direction.South)) ??
            in_check_diagonal(checKing, king,chess_board.getNextPiece(king,direction.SouthWest)) ??
            in_check_straight_line(checKing, king,chess_board.getNextPiece(king,direction.West)) ??
            in_check_diagonal(checKing, king,chess_board.getNextPiece(king,direction.NorthWest));
        }
        static piece in_check_straight_line(piece checKing, location king, piece p)
        {
            // checks to see if any piece N S E or W can take it
            if (p == null || p.colour == checKing.colour)
            {
                return null;
            }
            if (p.type == pieceType.Queen || p.type == pieceType.Rook)
            {
                return p;
            }
            return null;
        }
        static piece in_check_diagonal(piece checKing, location king, piece p)
        {
            // checks to see if any piece NE SE SW or NW can take it
            if (p == null|| p.colour == checKing.colour)
            {
                return null;
            }
            if (p.type == pieceType.Queen || p.type == pieceType.Bishop)
            {
                return p;
            }
            return null;
        }
        static bool check_by_pawn(piece checKing, location king, piece p,board chess_board)
        {
            location location_1 = king.getNextLocation(direction.SouthWest);
            location location_2 = king.getNextLocation(direction.SouthEast);
            if (chess_board.getPieceAtLocation(location_1).type == pieceType.Pawn || chess_board.getPieceAtLocation(location_2).type == pieceType.Pawn)
            {
                if (chess_board.getPieceAtLocation(location_1).colour != checKing.colour || chess_board.getPieceAtLocation(location_2).colour != checKing.colour)
                {
                    return true;
                }
            }
            return false;
        }
        static bool check_by_knight(piece checKing, location king, piece p,board chess_board)
        {
            return false;
        }
    }
}
