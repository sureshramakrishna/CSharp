using System;
public class My_Family
{
    public void member()
    {
        Console.WriteLine("Total number of family members: 3");
    }
}
public class My_Member : My_Family
{
    public void member()
    {
        Console.WriteLine("Total number of family members: 4");
    }
}
class Program
{
    static public void Main()
    {
        My_Member obj = new My_Member();
        obj.member();
    }
}