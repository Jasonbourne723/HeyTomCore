namespace HeyTom.Manage.Model
{
    public class Menu
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public int ParentId { get; set; }

        public string Icon { get; set; }
    }
}
