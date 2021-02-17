using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Grid
{
    /// <summary>
    /// Grid Values
    /// 0: Empty
    /// 1: Wall or obstacle
    /// 2: Breakable obstacle
    /// 3: Trap 
    /// 4: Ball
    /// 5: Hole
    /// 
    /// Movable names: "ball1", "ball2", "hole"
    /// </summary>


    private int height;
    private int width;
    private float cellSize;
    private Vector2Int hole = new Vector2Int(1, 7);
    public Vector2Int ball1 = new Vector2Int(1, 1);
    public Vector2Int ball2 = new Vector2Int(3, 3);
    public bool ball1_active = true;
    public bool ball2_active = true;

    public int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.gridArray = new int[width, height];

        addWalls();
    }

    private void addWalls()
    {
        for (int y = 0; y < this.getHeight(); y++)
        {
            for (int x = 0; x < this.getWidth(); x++)
            {
                if (x == 0 || x == (this.getWidth() - 1) || y == 0 || y == (this.getHeight() - 1))
                {
                    this.gridArray[x, y] = 1;
                }
            }
        }
    }

    public void addObstacles(List<int[]> obstacles)
    {
        foreach (int[] obstacle in obstacles)
        {
            this.gridArray[obstacle[0], obstacle[1]] = 1;
        }
    }

    public void addBreakableObstacles(List<int[]> breakableObstacles)
    {
        foreach (int[] obstacle in breakableObstacles)
        {
            this.gridArray[obstacle[0], obstacle[1]] = 2;
        }
    }

    public void addTraps(List<int[]> traps)
    {
        foreach (int[] trap in traps)
        {
            this.gridArray[trap[0], trap[1]] = 3;
        }
    }

    public void setEmptyLocation(Vector2Int location)
    {
        this.gridArray[location.x, location.y] = 0;
    }

    public void updateMovableLocation(string movableName, Vector2Int movableLocation)
    {
        if (movableName == "ball1")
        {
            this.gridArray[this.ball1.x, this.ball1.y] = 0;
            this.ball1 = movableLocation;
        }
        else if (movableName == "ball2")
        {
            this.gridArray[this.ball2.x, this.ball2.y] = 0;
            this.ball2 = movableLocation;
        }
        else if (movableName == "hole")
        {
            this.gridArray[this.hole.x, this.hole.y] = 0;
            this.hole = movableLocation;
        }
        if (this.ball1_active)
            this.gridArray[this.ball1.x, this.ball1.y] = 4;
        if (this.ball2_active)
            this.gridArray[this.ball2.x, this.ball2.y] = 4;
        this.gridArray[this.hole.x, this.hole.y] = 5;
    }

    private Vector2Int unityCoorToGridCoor(Vector3 sourcePosition)
    {
        // TODO modify this to adapt itself to different cellSize. Now, it only works for cellSize = 1
        int x_pos = ((int)sourcePosition.x);
        int y_pos = ((int)sourcePosition.z);
        return new Vector2Int(x_pos, y_pos);
    }

    public (int, int) CalculateTargetPosition(string direction, Vector3 sourcePosition)
    {
        int step;
        int obstacleType;
        Vector2Int position = unityCoorToGridCoor(sourcePosition);
        if (direction == "up")
            (step, obstacleType) = this.calculateForwardStep(position);
        else if (direction == "down")
            (step, obstacleType) = this.calculateBackwardStep(position);
        else if (direction == "right")
            (step, obstacleType) = this.calculateRightStep(position);
        else if (direction == "left")
            (step, obstacleType) = this.calculateLeftStep(position);
        else
        {
            step = 0;
            obstacleType = 0;
        }

        // TODO use obstacleType
        return (step, obstacleType);
    }
    public (int, int) calculateForwardStep(Vector2Int position)
    {
        bool ballOnTheWay = false;
        int obstacleType = 0;
        int counter = 0;
        for (int temp_pos = position.y + 1; temp_pos < this.getHeight(); temp_pos++)
        {
            if (this.gridArray[position.x, temp_pos] != 0)
            {
                if (this.gridArray[position.x, temp_pos] == 1)
                    break;
                else if (this.gridArray[position.x, temp_pos] == 2)
                {                    
                    obstacleType = 2;
                    break;
                }
                else if (this.gridArray[position.x, temp_pos] == 3)
                {
                    if (ballOnTheWay)
                        counter += 1;
                    obstacleType = 3;
                    break;
                }
                else if (this.gridArray[position.x, temp_pos] == 4)
                {
                    ballOnTheWay = true;
                }
                else if (this.gridArray[position.x, temp_pos] == 5)
                {
                    counter += 1;
                    obstacleType = 5;
                    break;
                }
            }
            else
            {
                counter += 1;
            }
        }

        return (counter, obstacleType);
    }

    public (int, int) calculateBackwardStep(Vector2Int position)
    {
        bool ballOnTheWay = false;
        int obstacleType = 0;
        int counter = 0;
        for (int temp_pos = position.y - 1; temp_pos >= 0; temp_pos--)
        {
            if (this.gridArray[position.x, temp_pos] != 0)
            {
                if (this.gridArray[position.x, temp_pos] == 1)
                    break;
                else if (this.gridArray[position.x, temp_pos] == 2)
               {
                    obstacleType = 2;
                    break;
                }
                else if (this.gridArray[position.x, temp_pos] == 3)
                {
                    if (ballOnTheWay)
                        counter += 1;
                    obstacleType = 3;
                    break;
                }
                else if (this.gridArray[position.x, temp_pos] == 4)
                {
                    ballOnTheWay = true;
                }
                else if (this.gridArray[position.x, temp_pos] == 5)
                {
                    counter += 1;
                    obstacleType = 5;
                    break;
                }
            }
            else
            {
                counter += 1;
            }
        }

        return (counter, obstacleType);
    }

    public (int, int) calculateRightStep(Vector2Int position)
    {
        bool ballOnTheWay = false;
        int obstacleType = 0;
        int counter = 0;
        for (int temp_pos = position.x + 1; temp_pos < this.getWidth(); temp_pos++)
        {
            if (this.gridArray[temp_pos, position.y] != 0)
            {
                if (this.gridArray[temp_pos, position.y] == 1)
                    break;
                else if (this.gridArray[temp_pos, position.y] == 2)
                {                    
                    obstacleType = 2;
                    break;
                }
                else if (this.gridArray[temp_pos, position.y] == 3)
                {
                    if (ballOnTheWay)
                        counter += 1;
                    obstacleType = 3;
                    break;
                }
                else if (this.gridArray[temp_pos, position.y] == 4)
                {
                    ballOnTheWay = true;
                }
                else if (this.gridArray[temp_pos, position.y] == 5)
                {
                    counter += 1;
                    obstacleType = 5;
                    break;
                }
            }
            else
            {
                counter += 1;
            }
        }

        return (counter, obstacleType);
    }

    public (int, int) calculateLeftStep(Vector2Int position)
    {
        bool ballOnTheWay = false;
        int obstacleType = 0;
        int counter = 0;
        for (int temp_pos = position.x - 1; temp_pos > 0; temp_pos--)
        {
            if (this.gridArray[temp_pos, position.y] != 0)
            {
                if (this.gridArray[temp_pos, position.y] == 1)
                    break;
                else if (this.gridArray[temp_pos, position.y] == 2)
                {                    
                    obstacleType = 2;
                    break;
                }
                else if (this.gridArray[temp_pos, position.y] == 3)
                {
                    if (ballOnTheWay)
                        counter += 1;
                    obstacleType = 3;
                    break;
                }
                else if (this.gridArray[temp_pos, position.y] == 4)
                {
                    ballOnTheWay = true;
                }
                else if (this.gridArray[temp_pos, position.y] == 5)
                {
                    counter += 1;
                    obstacleType = 5;
                    break;
                }
            }
            else
            {
                counter += 1;
            }
        }

        return (counter, obstacleType);
    }


    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }

    public float getCellSize()
    {
        return cellSize;
    }

    public int getCellID(int i, int j)
    {
        return this.gridArray[i, j];
    }
}
