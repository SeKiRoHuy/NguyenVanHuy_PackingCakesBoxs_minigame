using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : BaseGrid
{
    protected override void Start()
    {
        rows = 3;
        columns = 3;
        cellSpacing = 1.55f;
        moveSpeed = 3f;

        base.Start();
    }

    protected override void PlaceObstacles()
    {
        PlaceObstacle(1, 1);
        
        // Thêm các vật cản khác cho Level 1 nếu cần
    }
}
