namespace EduLingual.Domain.Dtos
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public string MediaLink { get; set; }
        public string SelfLink { get; set; }
        public string PublicLink { get; set; }
        public ulong Size { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string ContentType { get; set; }
    }
}
