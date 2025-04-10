using ReactiveUI;
using System;

public class Profile : ReactiveObject
{
    private int _id;
    private Guid _userId;
    private string _displayName;
    private string _bio;
    private string _avatarUrl;
    private int _speciesId;
    private int _planetId;
    private int? _genderId;
    private float? _heightInGalacticInches;
    private int? _galacticDateOfBirth;
    private Species _species;
    private Planet _planet;
    private Gender _gender;
    private User _user;

    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public Guid UserId
    {
        get => _userId;
        set => this.RaiseAndSetIfChanged(ref _userId, value);
    }

    public string DisplayName
    {
        get => _displayName;
        set => this.RaiseAndSetIfChanged(ref _displayName, value);
    }

    public string Bio
    {
        get => _bio;
        set => this.RaiseAndSetIfChanged(ref _bio, value);
    }

    public string AvatarUrl
    {
        get => _avatarUrl;
        set => this.RaiseAndSetIfChanged(ref _avatarUrl, value);
    }

    public int SpeciesId
    {
        get => _speciesId;
        set => this.RaiseAndSetIfChanged(ref _speciesId, value);
    }

    public int PlanetId
    {
        get => _planetId;
        set => this.RaiseAndSetIfChanged(ref _planetId, value);
    }

    public int? GenderId
    {
        get => _genderId;
        set => this.RaiseAndSetIfChanged(ref _genderId, value);
    }

    public float? HeightInGalacticInches
    {
        get => _heightInGalacticInches;
        set => this.RaiseAndSetIfChanged(ref _heightInGalacticInches, value);
    }

    public int? GalacticDateOfBirth
    {
        get => _galacticDateOfBirth;
        set => this.RaiseAndSetIfChanged(ref _galacticDateOfBirth, value);
    }

    public Species Species
    {
        get => _species;
        set => this.RaiseAndSetIfChanged(ref _species, value);
    }
    
    public Planet Planet
    {
        get => _planet;
        set => this.RaiseAndSetIfChanged(ref _planet, value);
    }
    
    public Gender Gender
    {
        get => _gender;
        set => this.RaiseAndSetIfChanged(ref _gender, value);
    }
    
    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
}