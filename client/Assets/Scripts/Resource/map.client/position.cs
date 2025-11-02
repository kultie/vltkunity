
using System.Drawing;

namespace game.resource.map
{
    public class Position
    {
        public class Sequential
        {
            public struct Grid
            {
                public int gridTop;
                public int gridLeft;

                public Grid(int top, int left)
                {
                    this.gridTop = top;
                    this.gridLeft = left;
                }

                public Position.Grid ToPosition()
                {
                    return new Position.Grid(this.gridTop, this.gridLeft);
                }
            }

            public struct Node
            {
                public int nodeTop;
                public int nodeLeft;

                public Node(int top, int left)
                {
                    this.nodeTop = top;
                    this.nodeLeft = left;
                }

                public Position.Node ToPosition()
                {
                    return new Position.Node(this.nodeTop, this.nodeLeft);
                }
            }

            public struct Origin
            {
                public int top;
                public int left;

                public Origin(int top, int left)
                {
                    this.top = top;
                    this.left = left;
                }

                public Position ToPosition()
                {
                    return new Position(this.top, this.left);
                }
            }
        }

        // 1, 2, 3, 4, 5, 6, ...
        public class Cell
        {
            public int cellTop = 0;
            public int cellLeft = 0;

            public Cell() { }

            public Cell(int _top, int _left)
            {
                this.cellTop = _top;
                this.cellLeft = _left;
            }

            public Position.Node GetNode()
            {
                return new()
                {
                    nodeTop = this.cellTop * map.Static.nodeMapDimension,
                    nodeLeft = this.cellLeft * map.Static.nodeMapDimension
                };
            }
        }

        // 64, 128, 192, 256, ...
        public class Grid
        {
            public int gridTop = 0;
            public int gridLeft = 0;

            public Grid() { }

            public Grid(int _top, int _left)
            {
                this.gridTop = _top;
                this.gridLeft = _left;
            }

            public Position.Node GetNode()
            {
                return new()
                {
                    nodeTop = (this.gridTop - (this.gridTop % map.Static.nodeMapDimension)),
                    nodeLeft = (this.gridLeft - (this.gridLeft % map.Static.nodeMapDimension))
                };
            }
        }

        // 512, 1024, 1536, 2048, ...
        public class Node
        {
            public int nodeTop = 0;
            public int nodeLeft = 0;

            public Node() { }

            public Node(int _top, int _left)
            {
                this.nodeTop = _top;
                this.nodeLeft = _left;
            }

            public Position.Sequential.Node ToSequential()
            {
                return new(this.nodeTop, this.nodeLeft);
            }

            public Position.Cell GetCell()
            {
                return new()
                {
                    cellTop = this.nodeTop / map.Static.nodeMapDimension,
                    cellLeft = this.nodeLeft / map.Static.nodeMapDimension
                };
            }
        }

        //////////////////////////////////////////////////////////////////

        public int top = 0;
        public int left = 0;

        public Position() { }

        public Position(UnityEngine.Vector3 position)
        {
            this.top = (int)(position.y * -100);
            this.left = (int)(position.x * 100);
        }

        public Position(int _top, int _left)
        {
            this.top = _top;
            this.left = _left;
        }

        public Position(map.Position _position)
        {
            this.top = _position.top;
            this.left = _position.left;
        }

        public Position(uint top1, uint left1)
        {
        }

        // 1, 2, 3, 4, 5, 6, ...
        public Position.Cell GetCell()
        {
            return new()
            {
                cellTop = this.top / map.Static.nodeMapDimension,
                cellLeft = this.left / map.Static.nodeMapDimension
            };
        }

        // 64, 128, 192, 256, ...
        public Position.Grid GetGrid()
        {
            return new()
            {
                gridTop = (this.top - (this.top % map.Static.gridMapDimension)),
                gridLeft = (this.left - (this.left % map.Static.gridMapDimension))
            };
        }

        // 512, 1024, 1536, 2048, ...
        public Position.Node GetNode()
        {
            return new()
            {
                nodeTop = (this.top - (this.top % map.Static.nodeMapDimension)),
                nodeLeft = (this.left - (this.left % map.Static.nodeMapDimension))
            };
        }

        public UnityEngine.Vector3 GetCameraPosition(int z = -10)
        {
            return new UnityEngine.Vector3()
            {
                x = this.left / 100f,
                y = this.top / -100f,
                z = z
            };
        }

        public double CalculateDistance(Position otherPoint)
        {
            int dx = top - otherPoint.top;
            int dy = left - otherPoint.left;
            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        //////////////////////////////////////////////////////////////////

        public static Position Zero
        {
            get
            {
                return new Position(0, 0);
            }
        }
    }
}
