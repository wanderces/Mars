using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.NavigateTerrain
{
    /// <summary>
    /// Class Robot
    /// </summary>
    public sealed class Robot
    {

        #region Variables & Constants

        private const char FACING_NORTH = 'N';
        private const char FACING_EAST = 'E';
        private const char FACING_WEST = 'W';
        private const char FACING_SOUTH = 'S';

        private const char TURN_LEFT = 'L';
        private const char TURN_RIGHT = 'R';

        private const char MOVE_FRONT = 'F';

        private int[,] _CurrentPosition = new int[,] { {1,1}};

        private char _CurrentFacing = FACING_NORTH;

        #endregion 

        #region Properties

        /// <summary>
        /// Matrix Input property.
        /// </summary>
        public String MatrixInput { get; set; }

        /// <summary>
        /// Commands Input property.
        /// </summary>
        public String CommandsInput { get; set; }

        /// <summary>
        /// Current Position property: Column x Line
        /// </summary>
        public int[,] CurrentPosition
        {
            get
            {
                return this._CurrentPosition;
            }
        }

        /// <summary>
        /// Current Facing property
        /// </summary>
        public char CurrentFacing
        {
            get
            {
                return this._CurrentFacing;
            }
        }

        /// <summary>
        /// Matrix columns property
        /// </summary>
        private int MatrixColumns {
            get
            {
                if (MatrixInput.ToLower().Contains("x"))
                {
                    return int.Parse(MatrixInput.ToLower().Split('x')[0]);
                }
                else
                {
                    //without validation
                    return 0;
                }
            }
        }

        /// <summary>
        /// Matrix Lines property
        /// </summary>
        private int MatrixLines
        {
            get
            {
                if (MatrixInput.ToLower().Contains("x"))
                {
                    return int.Parse(MatrixInput.ToLower().Split('x')[1]);
                }
                else
                {
                    //without validation
                    return 0;
                }
            }
        }

        /// <summary>
        /// Current column index property
        /// </summary>
        private int CurrentColumnIndex
        {
            get
            {
                return _CurrentPosition[0,0];
            }
        }

        /// <summary>
        /// Current line index property
        /// </summary>
        private int CurrentLineIndex
        {
            get
            {
                return _CurrentPosition[0, 1];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the values.
        /// </summary>
        private void Initialize()
        {
            this._CurrentFacing = FACING_NORTH;
            this._CurrentPosition = new int[,] { { 1, 1 } };
        }

        /// <summary>
        /// Update current facing.
        /// </summary>
        /// <param name="direction">Direction value.</param>
        private void UpdateCurrentFacing(char direction)
        {
            switch (direction)
            {
                case TURN_LEFT:
                    if (_CurrentFacing == FACING_NORTH)
                        _CurrentFacing = FACING_WEST;
                    else if (_CurrentFacing == FACING_WEST)
                        _CurrentFacing = FACING_SOUTH;
                    else if (_CurrentFacing == FACING_SOUTH)
                        _CurrentFacing = FACING_EAST;
                    else if (_CurrentFacing == FACING_EAST)
                        _CurrentFacing = FACING_NORTH;
                    break;
                case TURN_RIGHT:
                    if (_CurrentFacing == FACING_NORTH)
                        _CurrentFacing = FACING_EAST;
                    else if (_CurrentFacing == FACING_EAST)
                        _CurrentFacing = FACING_SOUTH;
                    else if (_CurrentFacing == FACING_SOUTH)
                        _CurrentFacing = FACING_WEST;
                    else if (_CurrentFacing == FACING_WEST)
                        _CurrentFacing = FACING_NORTH;
                    break;
            }
        }

        /// <summary>
        /// Valid the next movement.
        /// </summary>
        /// <returns>A boolean.</returns>
        private bool IsValid()
        {
            bool IsValid = false;

            switch (_CurrentFacing)
            {
                case FACING_NORTH:
                    if (this.CurrentLineIndex + 1 <= this.MatrixLines)
                        IsValid = true;
                    break;

                case FACING_SOUTH:
                    if (this.CurrentLineIndex - 1 > 0)
                        IsValid = true;
                    break;

                case FACING_EAST:
                    if (this.CurrentColumnIndex + 1 <= this.MatrixColumns)
                        IsValid = true;
                    break;

                case FACING_WEST:
                    if (this.CurrentColumnIndex - 1 > 0)
                        IsValid = true;
                    break;
            }

            return IsValid;
        }

        /// <summary>
        /// Move
        /// </summary>
        private void Move()
        {
            switch (_CurrentFacing)
            {
                case FACING_NORTH:
                    if (this.IsValid())
                        this._CurrentPosition = new int[,] { { this.CurrentColumnIndex, this.CurrentLineIndex + 1 } };
                    break;

                case FACING_SOUTH:
                    if (this.IsValid())
                        this._CurrentPosition = new int[,] { { this.CurrentColumnIndex, this.CurrentLineIndex - 1 } };
                    break;

                case FACING_EAST:
                    if (this.IsValid())
                        this._CurrentPosition = new int[,] { { this.CurrentColumnIndex + 1, this.CurrentLineIndex } };
                    break;

                case FACING_WEST:
                    if (this.IsValid())
                        this._CurrentPosition = new int[,] { { this.CurrentColumnIndex - 1, this.CurrentLineIndex } };
                    break;
            }
        }

        /// <summary>
        /// Execute the command: F:Front, L:Left, R:Right
        /// </summary>
        /// <param name="command">Command value.</param>
        private void ExecuteCommand(char command)
        {
            switch (command)
            {
                case TURN_LEFT:
                case TURN_RIGHT:
                    this.UpdateCurrentFacing(command);
                    break;
                case MOVE_FRONT:
                    Move();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Get  Middle Number
        /// </summary>
        /// <returns>The middle number.</returns>
        private int GetMiddleNumber()
        {
            // if number is odd, return middle number
            if (MatrixColumns % 2 == 1)
                return (MatrixColumns / 2) + 1;
            else
                return 0;
        }

        /// <summary>
        /// Get Cardinal Direction.
        /// </summary>
        /// <returns></returns>
        private String GetCardinalDirection()
        {
            String currentSide = String.Empty;

            if (this.MatrixColumns % 2 == 0)
            {
                if (this.CurrentColumnIndex <= this.MatrixColumns / 2)
                    currentSide = "West";
                else
                    currentSide = "East";
            }
            else
            {
                if (this.CurrentColumnIndex < this.GetMiddleNumber())
                    currentSide = "West";
                else if (this.CurrentColumnIndex == this.GetMiddleNumber())
                    currentSide = "Middle";
                else if (this.CurrentColumnIndex > this.GetMiddleNumber())
                    currentSide= "East";
            }

            return currentSide;
        }

        /// <summary>
        /// Get Result method.
        /// </summary>
        /// <returns>A string value with the result.</returns>
        public String GetResult()
        {
            try
            {
                //Initialize the values.
                Initialize();

                String result = String.Empty;

                char[] commandsArray = this.CommandsInput.ToCharArray();

                //Loop the command values.
                foreach (char command in commandsArray)
                {
                    ExecuteCommand(Char.ToUpper(command));
                }

                result = String.Format("{0}, {1}, {2}", this._CurrentPosition[0, 0], this._CurrentPosition[0, 1], GetCardinalDirection());

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Sorry Error.", ex);
            }
        }

        #endregion
        
    }
}
