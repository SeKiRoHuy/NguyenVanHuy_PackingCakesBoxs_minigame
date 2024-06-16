using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : BaseGrid
{
    protected override void Start()
    {
        rows = 4;
        columns = 3;
        cellSpacing = 1.55f;
        moveSpeed = 3f;

        base.Start();
    }

    protected override void PlaceObstacles()
    {
        PlaceObstacle(2, 0);
        PlaceObstacle(1, 2);
        PlaceObstacle(3, 1);


        // Thêm các vật cản khác cho Level 1 nếu cần
    }
}