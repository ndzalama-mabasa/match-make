using System.Collections.Generic;
using ReactiveUI;

public class Planet : ReactiveObject
{
    private int _id;
    private string _planetName;
    private IReadOnlyList<Profile> _profiles;

    public Planet()
    {
        _profiles = new List<Profile>().AsReadOnly();
    }
    
    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string PlanetName
    {
        get => _planetName;
        set => this.RaiseAndSetIfChanged(ref _planetName, value);
    }

    public IReadOnlyList<Profile> Profiles
    {
        get => _profiles;
        set => this.RaiseAndSetIfChanged(ref _profiles, value);
    }
}