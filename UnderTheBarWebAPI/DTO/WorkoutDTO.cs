namespace UnderTheBarWebAPI.DTO
{
    public class WorkoutDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
    }
}
