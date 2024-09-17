namespace WAMS.Backend.Model
{
    public class Timetable
    {
        public Course[,] Days { get; set; } = new Course[5, 10];
    }
}
