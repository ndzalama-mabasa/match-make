using System.Collections.Generic;
using ReactiveUI;

public class Gender : ReactiveObject
{
    private int _id;
    private string _gender;

    private IReadOnlyList<Profile> _profiles;

    public Gender()
    {
        _profiles = new List<Profile>().AsReadOnly();
    }

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string GenderName
    {
        get => _gender;
        set => this.RaiseAndSetIfChanged(ref _gender, value);
    }

    public IReadOnlyList<Profile> Profiles
    {
        get => _profiles;
        set => this.RaiseAndSetIfChanged(ref _profiles, value);
    }
}