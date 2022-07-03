using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition
{
    public int x;
    public int z;

    public GridPosition(int x,int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public override int GetHashCode()
    {
        int hashCode = 1553271884;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(GridPosition a,GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        GridPosition x = new GridPosition();
        x.x = a.x + b.x;
        x.z = a.z + b.z;
        return x;
    }
}
