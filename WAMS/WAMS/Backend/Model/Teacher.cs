namespace WAMS.Backend.Model
{
    public class Teacher : User
    {
        public List<Class>? Classes { get; set; }
    }
}
