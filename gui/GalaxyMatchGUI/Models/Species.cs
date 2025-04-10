using System.Collections.Generic;
using ReactiveUI;

public class Species : ReactiveObject
{
    private int _id;
    private string _speciesName;

    private IReadOnlyList<Profile> _profiles;

    public Species()
    {
        _profiles = new List<Profile>().AsReadOnly();
    }

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string SpeciesName
    {
        get => _speciesName;
        set => this.RaiseAndSetIfChanged(ref _speciesName, value);
    }

    public IReadOnlyList<Profile> Profiles
    {
        get => _profiles;
        set => this.RaiseAndSetIfChanged(ref _profiles, value);
    }
}