using System.Collections.Generic;
using ReactiveUI;

public class Attribute : ReactiveObject
{
    private int _attributesId;
    private string _attributeName;
    private User _user;
    private Attribute _attribute;

    private IReadOnlyList<UserAttribute> _userAttributes;

    public Attribute()
    {
        _userAttributes = new List<UserAttribute>().AsReadOnly();
    }

    public int AttributesId
    {
        get => _attributesId;
        set => this.RaiseAndSetIfChanged(ref _attributesId, value);
    }

    public string AttributeName
    {
        get => _attributeName;
        set => this.RaiseAndSetIfChanged(ref _attributeName, value);
    }

    public IReadOnlyList<UserAttribute> UserAttributes
    {
        get => _userAttributes;
        set => this.RaiseAndSetIfChanged(ref _userAttributes, value);
    }

    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public Attribute GetAttribute()
    {
        return _attribute;
    }

    public void SetAttribute(Attribute value)
    {
        this.RaiseAndSetIfChanged(ref _attribute, value);
    }
}