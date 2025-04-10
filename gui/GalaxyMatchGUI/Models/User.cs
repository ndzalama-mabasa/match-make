using ReactiveUI;
using System;
using System.Collections.Generic;

public class User : ReactiveObject
{
    private Guid _id;
    private string _oauthId;
    private bool _inactive;
    private Profile _profile;
    private IReadOnlyList<UserAttribute> _attributes;
    private IReadOnlyList<UserInterest> _interests;

    public User()
    {
        _attributes = new List<UserAttribute>().AsReadOnly();
        _interests = new List<UserInterest>().AsReadOnly();
    }

    public Guid Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string OAuthId
    {
        get => _oauthId;
        set => this.RaiseAndSetIfChanged(ref _oauthId, value);
    }

    public bool Inactive
    {
        get => _inactive;
        set => this.RaiseAndSetIfChanged(ref _inactive, value);
    }

    public Profile Profile
    {
        get => _profile;
        set => this.RaiseAndSetIfChanged(ref _profile, value);
    }
    
    public IReadOnlyList<UserAttribute> Attributes
    {
        get => _attributes;
        set => this.RaiseAndSetIfChanged(ref _attributes, value);
    }
    
    public IReadOnlyList<UserInterest> Interests
    {
        get => _interests;
        set => this.RaiseAndSetIfChanged(ref _interests, value);
    }
}