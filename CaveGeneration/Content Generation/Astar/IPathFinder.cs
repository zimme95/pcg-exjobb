using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;
using CaveGeneration.Content_Generation.Astar;
using static CaveGeneration.Content_Generation.Astar.PathFinderFast;

namespace CaveGeneration.Content_Generation.Astar
{
    [Author("Franco, Gustavo")]
    interface IPathFinder
    {

        #region Properties
        bool Stopped
        {
            get;
        }

        HeuristicFormula Formula
        {
            get;
            set;
        }

        bool Diagonals
        {
            get;
            set;
        }

        bool HeavyDiagonals
        {
            get;
            set;
        }

        int HeuristicEstimate
        {
            get;
            set;
        }

        bool PunishChangeDirection
        {
            get;
            set;
        }

        bool TieBreaker
        {
            get;
            set;
        }

        int SearchLimit
        {
            get;
            set;
        }

        double CompletedTime
        {
            get;
            set;
        }

        bool DebugProgress
        {
            get;
            set;
        }

        bool DebugFoundPath
        {
            get;
            set;
        }
        #endregion

        #region Methods
        void FindPathStop();
        List<PathFinderNodeFast> FindPath(Vector2 start, Vector2 end, int characterWidth, int characterHeight, short maxCharacterJumpHeight);
        #endregion

    }
}
