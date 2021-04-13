namespace ServicesForDotnetClientApps
{
  public class Location
  {
    public Location(int id, string name, string address)
    {
      this.Id = id;
      this.Name = name;
      this.Address = address;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
  }
}