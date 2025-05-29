namespace lb4;

public class GroupedResource(
    int leftResource,
    int rightResource
)
{

    public int LeftResource
    {
        get
        {
            return leftResource;
        }
    }

    public int RightResource
    {
        get
        {
            return rightResource;
        }
    }

    public bool IsLastInQueue(int queueLength)
    {
        return leftResource == queueLength;
    }

    public override string ToString()
    {
        return $"left({leftResource}) right({rightResource})";
    }
}