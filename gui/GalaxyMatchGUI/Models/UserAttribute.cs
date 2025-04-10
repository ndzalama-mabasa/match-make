using ReactiveUI;
using System;

public class UserAttribute : ReactiveObject
{
    private int _userAttributesId;
    private int _attributeId;
    private Guid _userId;

    public int UserAttributesId
    {
        get => _userAttributesId;
        set => this.RaiseAndSetIfChanged(ref _userAttributesId, value);
    }

    public int AttributeId
    {
        get => _attributeId;
        set => this.RaiseAndSetIfChanged(ref _attributeId, value);
    }

    public Guid UserId
    {
        get => _userId;
        set => this.RaiseAndSetIfChanged(ref _userId, value);
    }
}